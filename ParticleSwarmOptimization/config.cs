using System;
using MersenneTwister;
using ParticleSwarmOptimization.FitnessFunctions;
using ParticleSwarmOptimization.FitnessFunctions.Interface;
using ParticleSwarmOptimization.Swarm;
using ParticleSwarmOptimization.Swarm.Interfaces;

namespace ParticleSwarmOptimization
{
    public class Config
    {
        public static readonly Random RandomNumberGenerator = MersenneTwister.DsfmtRandom.Create();
        private readonly IFitnessFunction fitnessFunction;
        private IVelocityCalculator velocityCalculator;

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

        public IVelocityCalculator GetVelocityCalculator()
        {
            return velocityCalculator;
        }

        public void MakeInertiaVelocityCalculator()
        {
            velocityCalculator = new InertiaVelocityCalculator(Inertia, Cognitive, Social);
        }

        public void MakeSPSO2011VelocityCalculator()
        {
            velocityCalculator = new Spso2011VelocityCalculator(Inertia, Cognitive, Social);
        }
    }
}