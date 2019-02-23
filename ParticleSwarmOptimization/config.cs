using System;
using MathNet.Numerics.Distributions;
using MathNet.Numerics.Random;
using ParticleSwarmOptimization.Calculators;
using ParticleSwarmOptimization.FitnessFunctions;
using ParticleSwarmOptimization.FitnessFunctions.Interface;
using ParticleSwarmOptimization.Swarm.Calculators;
using ParticleSwarmOptimization.Swarm.Interfaces;

namespace ParticleSwarmOptimization
{
    public class Config
    {
        public static readonly Random RandomNumberGenerator = new MersenneTwister(true);
        public static readonly Normal NormalRandom = new Normal(0.0,1.0, RandomNumberGenerator);
        private IFitnessFunction fitnessFunction;
        private IVelocityCalculator velocityCalculator;

        public int Iterations { get; set; }
        public int SwarmSize { get; set; }
        public int Dimensions { get; set; }
        public double Inertia { get; set; }
        public double Cognitive { get; set; }
        public double Social { get; set; }

        public Config()
        {
            
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

        public void MakeSpso2011VelocityCalculator()
        {
            velocityCalculator = new Spso2011VelocityCalculator(Inertia, Cognitive, Social);
        }
        
        public void SetAbs()
        {
            fitnessFunction = new AbsVal();
        }
        
        public void SetAckley()
        {
            fitnessFunction = new Ackley();
        }
        
        public void SetSphere()
        {
            fitnessFunction = new Sphere();
        }
        
        public void SetMichalewicz()
        {
            fitnessFunction = new Michalewicz();
        }
        
        public void SetKatsuura()
        {
            fitnessFunction = new Katsuura();
        }
        
        public void SetSchubert()
        {
            fitnessFunction = new Schubert();
        }
    }
}