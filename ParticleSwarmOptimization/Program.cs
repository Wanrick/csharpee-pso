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
            var config = new Config(20);
            var swarm = new Particle[20];
            var countMaxIter = 1000;
            
            for (var i = 0; i < swarm.Length; i++)
            {
                swarm[i] = new Particle(config);
            }

            Particle.GlobalBestFitness = double.MaxValue;
            while (!StopConditionMet() && countMaxIter > 0)
            {
                foreach (var particle in swarm)
                {
                    particle.Update();
                }

                countMaxIter--;
                Console.WriteLine(Particle.GlobalBestFitness);
            }
        }

        private static bool StopConditionMet()
        {
            return Particle.GlobalBestFitness < 0.001;
        }
    }
}