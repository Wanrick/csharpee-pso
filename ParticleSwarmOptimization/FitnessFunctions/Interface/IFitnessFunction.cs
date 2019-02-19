using ParticleSwarmOptimization.Swarm;

namespace ParticleSwarmOptimization.FitnessFunctions.Interface
{
    public interface IFitnessFunction
    {
        double EvaluateFitness(Particle particle);
        double GetBounds();
    }
}