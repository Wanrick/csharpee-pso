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
            const int dimensions = 20;
            const int swarmSize = 20;
            const int iterations = 1000;
            const int runs = 1;
            
            var (inertiaParam, cognitiveParam, socialParam) = GetVelocityParams();
            
            var config = new Config()
            {
                SwarmSize = swarmSize,
                Iterations = iterations,
                Dimensions = dimensions,
                Inertia = inertiaParam,
                Cognitive = cognitiveParam,
                Social = socialParam
            };
            
            for (var i = 0; i < runs; i++)
            {
                RunPso(config);
            }
        }

        private static Tuple<double, double, double> GetVelocityParams()
        {
            return new Tuple<double, double, double>(0.7,1.4,1.4);
        }

        private static void RunPso(Config config)
        {
            var swarm = new Particle[config.SwarmSize];
            var countMaxIter = config.Iterations;
            
            for (var i = 0; i < swarm.Length; i++)
            {
                swarm[i] = new Particle(config);
            }

            while (!StopConditionMet(countMaxIter))
            {
                foreach (var particle in swarm)
                {
                    particle.Update();
                }

                countMaxIter--;
                Console.WriteLine(Particle.GlobalBestFitness);
            }
            Console.WriteLine();
        }

        private static bool StopConditionMet(int countMaxIter)
        {
            return (Particle.GlobalBestFitness < 0.001 || countMaxIter < 0);
        }
    }
}