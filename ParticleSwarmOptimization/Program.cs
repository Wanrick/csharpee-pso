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
            var countMaxIter = 10000;
            
            for (var i = 0; i < swarm.Length; i++)
            {
                swarm[i] = new Particle(config);
            }

            Particle.GlobalBestFitness = double.MaxValue;
            while (!StopConditionMet(countMaxIter))
            {
                foreach (var particle in swarm)
                {
                    particle.Update();
                }

                countMaxIter--;
                Console.WriteLine(Math.Round(Particle.GlobalBestFitness, 2));
            }
        }

        private static bool StopConditionMet(int countMaxIter)
        {
            return (Particle.GlobalBestFitness < 0.001 || countMaxIter < 0);
        }
    }
}