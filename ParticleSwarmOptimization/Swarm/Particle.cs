using System;
using System.Text;
using ParticleSwarmOptimization.FitnessFunctions.Interface;
using ParticleSwarmOptimization.Swarm.Interfaces;
using ParticleSwarmOptimization.Swarm.Utilities;

namespace ParticleSwarmOptimization.Swarm
{
    public class Particle
    {
        private readonly IFitnessFunction fitnessFunction;
        public static Coords GlobalBestPosition { get; set; }
        public static double GlobalBestFitness { get; set; } = double.MaxValue;

        public Coords PersonalBestPosition;
        private double PersonalBestFitness { get; set; } = double.MaxValue;

        public Coords CurrentVelocity;
        public Coords CurrentPosition;
        private readonly IVelocityCalculator velocityCalculator;
        public int Dimensions { get; }


        public Particle(Config config)
        {
            velocityCalculator = config.GetVelocityCalculator();
            fitnessFunction = config.GetFitnessFunction();
            InitCoords(config.Dimensions, fitnessFunction.GetBounds());
            Dimensions = config.Dimensions;
        }

        private void InitCoords(int dimensions, Tuple<double,double> bounds)
        {
            var (minimum, maximum) = bounds;
            GlobalBestPosition = new Coords(dimensions, minimum, maximum, false);
            PersonalBestPosition = new Coords(dimensions, minimum, maximum, false);
            CurrentVelocity = new Coords(dimensions, minimum, maximum, true);
            CurrentPosition = new Coords(dimensions, minimum, maximum, false);
        }

        public void Update(StringBuilder spsoResult)
        {
            var newVelocity = velocityCalculator.GetNextVelocity(this);
            CurrentPosition = CurrentPosition.Move(newVelocity);
            CurrentVelocity = newVelocity;
            var fitness = fitnessFunction.EvaluateFitness(this);

            if (IsPersonalBest(fitness))
            {
                PersonalBestPosition = new Coords(CurrentPosition);
                PersonalBestFitness = fitness;
            }

            if (IsGlobalBest(fitness))
            {
                GlobalBestPosition = new Coords(CurrentPosition);
                GlobalBestFitness = fitness;
            }

            spsoResult.Append(PersonalBestFitness + "|" + string.Join("|", PersonalBestPosition.CoordinateArray));
            spsoResult.AppendLine();
        }

        private bool IsGlobalBest(double fitness)
        {
            return fitness < GlobalBestFitness;
        }

        private bool IsPersonalBest(double fitness)
        {
            return fitness < PersonalBestFitness;
        }
    }
}