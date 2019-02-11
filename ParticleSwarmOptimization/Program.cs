using System;
using ParticleSwarmOptimization.FitnessFunctions;
using ParticleSwarmOptimization.Swarm;
using ParticleSwarmOptimization.Swarm.Utilities;

namespace ParticleSwarmOptimization
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            
            var searchSpace = new Coords(10,10);
            var swarm = new Particle[100];
            
            VelocityCalculator.SearchSpace = searchSpace;
            
            for (var i = 0; i < swarm.Length; i++)
            {
                swarm[i] = new Particle(new Booth());
            }

            Particle.GlobalBestFitness = double.MaxValue;
            while (!StopConditionMet())
            {
                foreach (var particle in swarm)
                {
                    particle.Update();
                }
                Console.WriteLine(Particle.GlobalBestFitness);
            }
        }

        private static bool StopConditionMet()
        {
            return Particle.GlobalBestFitness < 0.001;
        }
    }
}