using System;
using MersenneTwister;
using ParticleSwarmOptimization.FitnessFunctions;
using ParticleSwarmOptimization.FitnessFunctions.Interface;
using ParticleSwarmOptimization.Swarm;

namespace ParticleSwarmOptimization
{
    public class Config
    {
        public static readonly Random RandomNumberGenerator = MersenneTwister.DsfmtRandom.Create();
        private readonly IFitnessFunction fitnessFunction;
        public const double InitialValues = double.MaxValue;

        public int Iterations { get; set; }
        public int SwarmSize { get; set; }
        public int Dimensions { get; set; }
        public double Inertia { get; set; }
        public double Cognitive { get; set; }
        public double Social { get; set; }

        public Config(int swarm, int iter, int dimensions, double inertiaParameter, double cognitiveParameter, double socialParameter)
        {
            SwarmSize = swarm;
            Iterations = iter;
            Dimensions = dimensions;
            Inertia = inertiaParameter;
            Cognitive = cognitiveParameter;
            Social = socialParameter;
            fitnessFunction = new AbsVal();
        }

        public Config()
        {
            fitnessFunction = new AbsVal();
        }

        public IFitnessFunction GetFitnessFunction()
        {
            return fitnessFunction;
        }

        public VelocityCalculator GetVelocityCalculator()
        {
            return new VelocityCalculator(Inertia, Cognitive, Social);
        }
    }
}