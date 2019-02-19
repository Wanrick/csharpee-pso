using System;
using ParticleSwarmOptimization.FitnessFunctions.Interface;
using ParticleSwarmOptimization.Swarm;

namespace ParticleSwarmOptimization.FitnessFunctions
{
    public class Sphere : IFitnessFunction
    {
        public double EvaluateFitness(Particle particle)
        {
            var position = particle.CurrentPosition.CoordinateArray;
            var sum = 0.0;
            for (var i = 0; i < position.Length; i++)
            {
                sum += Math.Pow(position[i], 2);
            }
            return sum;
        }

        public double GetBounds()
        {
            return 5.12;
        }
    }
}