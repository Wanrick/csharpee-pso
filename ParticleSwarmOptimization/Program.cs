using System;
using System.IO;
using System.Text;
using System.Xml;
using ParticleSwarmOptimization.FitnessFunctions;
using ParticleSwarmOptimization.Swarm;
using ParticleSwarmOptimization.Swarm.Utilities;

namespace ParticleSwarmOptimization
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            string psoPath = Path.GetFullPath( @"/home/wanrick/PSOResults/PSOattempt.csv");
            /*if (!File.Exists(psoPath))
            {
                File.Create(psoPath);
            }*/
            var psoResults = new StreamWriter(psoPath, true);
            
            string spsoPath = Path.GetFullPath(@"/home/wanrick/PSOResults/SPSOattempt.csv");
            /*if (!File.Exists(spsoPath))
            {
                File.Create(spsoPath);
            }*/
            var spsoResults = new StreamWriter(spsoPath, true);
            
            const int dimensions = 20;
            const int swarmSize = 20;
            const int iterations = 1000;
            const int runs = 1;
            var functionName = string.Empty;

            var (inertiaParam, cognitiveParam, socialParam) = GetVelocityParams();

            var psoconfig = new Config()
            {
                SwarmSize = swarmSize,
                Iterations = iterations,
                Dimensions = dimensions,
                Inertia = inertiaParam,
                Cognitive = cognitiveParam,
                Social = socialParam
            };

            var spsoconfig = new Config()
            {
                SwarmSize = swarmSize,
                Iterations = iterations,
                Dimensions = dimensions,
                Inertia = inertiaParam,
                Cognitive = cognitiveParam,
                Social = socialParam
            };
            for (var j = 0; j < 5; j++)
            {
                switch (j)
                {
                    case 0:
                        spsoconfig.SetAckley();
                        psoconfig.SetAckley();
                        functionName = "Ackley";
                        break;
                    case 1:
                        spsoconfig.SetSphere();
                        psoconfig.SetSphere();
                        functionName = "Sphere";
                        break;
                    case 2:
                        spsoconfig.SetKatsuura();
                        psoconfig.SetKatsuura();
                        functionName = "Katsuura";
                        break;
                    case 3:
                        spsoconfig.SetSchubert();
                        psoconfig.SetSchubert();
                        functionName = "Schubert";
                        break;
                    case 4:
                        spsoconfig.SetMichalewicz();
                        psoconfig.SetMichalewicz();
                        functionName = "Michalewicz";
                        break;
                    default:
                        spsoconfig.SetAbs();
                        psoconfig.SetAbs();
                        functionName = "Absolute Value";
                        break;
                }

                Console.WriteLine();
                Console.WriteLine(functionName);
                Console.WriteLine("-------------------------------------");
                Console.WriteLine();

                for (var i = 0; i < runs; i++)
                {
                    RunPso(psoconfig, psoResults);
                    RunSpso(spsoconfig, spsoResults);
                }
            }
            psoResults.Close();
            spsoResults.Close();
        }

        private static Tuple<double, double, double> GetVelocityParams()
        {
            return new Tuple<double, double, double>(0.7, 1.4, 1.4);
        }

        private static void RunSpso(Config config, StreamWriter spsoResults)
        {
            var spsoResult = new StringBuilder();
            config.MakeSpso2011VelocityCalculator();
            var swarm = new Particle[config.SwarmSize];
            var countMaxIter = config.Iterations;
            Particle.GlobalBestFitness = double.MaxValue;
            Particle.GlobalBestPosition = new Coords(config.Dimensions,
                config.GetFitnessFunction().GetBounds().Item1,
                config.GetFitnessFunction().GetBounds().Item2, false);

            for (var i = 0; i < swarm.Length; i++)
            {
                swarm[i] = new Particle(config);
            }

            while (!StopConditionMet(countMaxIter))
            {
                foreach (var particle in swarm)
                {
                    particle.Update(spsoResult);
                }

                countMaxIter--;
                Console.WriteLine(Particle.GlobalBestFitness);
                
                spsoResult.AppendLine();
                spsoResult.Append(Particle.GlobalBestFitness + "|" + string.Join("|", Particle.GlobalBestPosition.CoordinateArray));
                spsoResult.AppendLine();
                spsoResult.AppendLine();
            }
            spsoResults.Write(spsoResult);
            Console.WriteLine();
        }

        private static void RunPso(Config config, StreamWriter psoResults)
        {
            var psoResult = new StringBuilder();
            config.MakeInertiaVelocityCalculator();
            var swarm = new Particle[config.SwarmSize];
            var countMaxIter = config.Iterations;
            Particle.GlobalBestFitness = double.MaxValue;
            Particle.GlobalBestPosition = new Coords(config.Dimensions,
                config.GetFitnessFunction().GetBounds().Item1,
                config.GetFitnessFunction().GetBounds().Item2, false);

            for (var i = 0; i < swarm.Length; i++)
            {
                swarm[i] = new Particle(config);
            }

            while (!StopConditionMet(countMaxIter))
            {
                foreach (var particle in swarm)
                {
                    particle.Update(psoResult);
                }

                countMaxIter--;

                psoResult.AppendLine();
                psoResult.Append(Particle.GlobalBestFitness + "|" + string.Join("|", Particle.GlobalBestPosition.CoordinateArray));
                psoResult.AppendLine();
                psoResult.AppendLine();
                
                Console.WriteLine(Particle.GlobalBestFitness);
            }
            
            psoResults.Write(psoResult);
            
            Console.WriteLine();
        }

        private static bool StopConditionMet(int countMaxIter)
        {
            return (Particle.GlobalBestFitness < 0.001 || countMaxIter < 0);
        }
    }
}