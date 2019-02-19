using System;
using MersenneTwister;
using ParticleSwarmOptimization.FitnessFunctions;
using ParticleSwarmOptimization.FitnessFunctions.Interface;

namespace ParticleSwarmOptimization
{
    public class Config
    {
        public static Random RandomNumberGenerator = MersenneTwister.DsfmtRandom.Create();
        private readonly IFitnessFunction fitnessFunction;
        public const double InitialValues = double.MaxValue;

        public int Dimensions { get; }

        public Config(int dimensions)
        {
            Dimensions = dimensions;
            fitnessFunction = new AbsVal();
        }

        public IFitnessFunction GetFitnessFunction()
        {
            return fitnessFunction;
        }
    }
}