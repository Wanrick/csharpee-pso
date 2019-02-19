using System;
using ParticleSwarmOptimization.Swarm;

namespace ParticleSwarmOptimization.FitnessFunctions.Interface
{
    public interface IFitnessFunction
    {
        double EvaluateFitness(Particle particle);
        Tuple<double, double> GetBounds();
    }
}