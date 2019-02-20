using ParticleSwarmOptimization.Swarm.Utilities;

namespace ParticleSwarmOptimization.Swarm.Interfaces
{
    public interface IVelocityCalculator
    {
        Coords GetNextVelocity(Particle particle);
    }
}