using System;
using ParticleSwarmOptimization.FitnessFunctions.Interface;
using ParticleSwarmOptimization.Swarm.Utilities;

namespace ParticleSwarmOptimization.Swarm
{
    public class Particle
    {
        private readonly IFitnessFunction fitnessFunction;
        public static Coords GlobalBestPosition { get; private set; }
        public static double GlobalBestFitness { get; set; } = double.MaxValue;

        public Coords PersonalBestPosition;
        private double personalBestFitness;

        public Coords CurrentVelocity;
        public Coords CurrentPosition;


        public Particle(Config config)
        {
            fitnessFunction = config.GetFitnessFunction();
            InitCoords(config.Dimensions, fitnessFunction.GetBounds());
        }

        private void InitCoords(int dimensions, Tuple<double,double> bounds)
        {
            var (minimum, maximum) = bounds;
            GlobalBestPosition = new Coords(dimensions, minimum, maximum, false);
            PersonalBestPosition = new Coords(dimensions, minimum, maximum, false);
            CurrentVelocity = new Coords(dimensions, minimum, maximum, true);
            CurrentPosition = new Coords(dimensions, minimum, maximum, false);
        }

        public void Update()
        {
            CurrentPosition = CurrentPosition.Move(VelocityCalculator.GetNextVelocity(this));
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