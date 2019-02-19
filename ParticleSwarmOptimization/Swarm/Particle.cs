using System;
using ParticleSwarmOptimization.FitnessFunctions.Interface;
using ParticleSwarmOptimization.Swarm.Utilities;

namespace ParticleSwarmOptimization.Swarm
{
    public class Particle
    {
        private readonly IFitnessFunction fitnessFunction;
        
        private static readonly Random RandomPositionGenerator = new Random();
        public static Coords GlobalBestPosition { get; private set; }
        
        public static double GlobalBestFitness { get; set; }
        
        public Coords PersonalBestPosition;
        public Coords CurrentVelocity;
        public Coords CurrentPosition;

        private double personalBestFitness;

        public Particle(Config config)
        {
            PersonalBestPosition = new Coords(config.Dimensions);
            CurrentVelocity = new Coords(config.Dimensions);
            CurrentPosition = new Coords(config.Dimensions);
            GlobalBestPosition = new Coords(config.Dimensions);
            fitnessFunction = config.GetFitnessFunction();
            
            GlobalBestPosition.InitPosition(fitnessFunction.GetBounds());
            PersonalBestPosition.InitBestPosition(Config.InitialValues);
            CurrentVelocity.InitVelocity();
            CurrentPosition.InitPosition(fitnessFunction.GetBounds());
        }

        public void Update()
        {
            CurrentPosition = CurrentPosition.Add(VelocityCalculator.GetNextVelocity(this));
            var fitness = fitnessFunction.EvaluateFitness(this);

            if (IsPersonalBest(fitness))
            {
                PersonalBestPosition = new Coords(CurrentPosition);
                personalBestFitness = fitness;
            }

            if (IsGlobalBest(fitness))
            {
                GlobalBestPosition = new Coords(CurrentPosition);
                GlobalBestFitness = fitness;
            }
        }

        private static bool IsGlobalBest(double fitness)
        {
            return fitness < GlobalBestFitness;
        }

        private bool IsPersonalBest(double fitness)
        {
            return fitness < personalBestFitness;
        }
    }
}