using System;
using ParticleSwarmOptimization.FitnessFunctions.Interface;

namespace ParticleSwarmOptimization.FitnessFunctions
{
    public class Booth : IFitnessFunction
    {
        public double EvaluateFitness(double x, double y)
        {
            return Math.Pow((x + 2 * y - 7), 2) + Math.Pow((x + y - 5), 2);
        }
    }
}