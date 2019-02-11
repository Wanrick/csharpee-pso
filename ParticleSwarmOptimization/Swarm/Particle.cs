using System;
using ParticleSwarmOptimization.FitnessFunctions.Interface;
using ParticleSwarmOptimization.Swarm.Utilities;

namespace ParticleSwarmOptimization.Swarm
{
    public class Particle
    {
        private readonly IFitnessFunction fitnessFunction;
        
        private static readonly Random RandomPositionGenerator = new Random();
        public static Coords GlobalBestPosition { get; private set; } = new Coords(RandomPositionGenerator.NextDouble()*VelocityCalculator.SearchSpace.XVal, RandomPositionGenerator.NextDouble()*VelocityCalculator.SearchSpace.YVal);
        
        public static double GlobalBestFitness { get; set; }
        
        public Coords PersonalBestPosition { get; private set; }
        public Coords CurrentPosition { get; private set; }
        public Coords CurrentVelocity { get; private set; }

        private double personalBestFitness;

        public Particle(IFitnessFunction function)
        {
            PersonalBestPosition = new Coords(RandomPositionGenerator.NextDouble()*VelocityCalculator.SearchSpace.XVal, RandomPositionGenerator.NextDouble()*VelocityCalculator.SearchSpace.YVal);
            CurrentVelocity = new Coords(0,0);
            CurrentPosition = new Coords(RandomPositionGenerator.NextDouble()*VelocityCalculator.SearchSpace.XVal, RandomPositionGenerator.NextDouble()*VelocityCalculator.SearchSpace.YVal);
            fitnessFunction = function;
        }

        public void Update()
        {
            CurrentPosition = CurrentPosition.Add(VelocityCalculator.GetNextVelocity(this));
            var fitness = fitnessFunction.EvaluateFitness(CurrentPosition.XVal, CurrentPosition.YVal);

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