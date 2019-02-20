using System;
using ParticleSwarmOptimization.Swarm.Interfaces;
using ParticleSwarmOptimization.Swarm.Utilities;

namespace ParticleSwarmOptimization.Swarm
{
    public class Spso2011VelocityCalculator : IVelocityCalculator
    {
        private readonly double inertiaWeight;
        private readonly double cognitiveCoefficient;
        private readonly double socialCoefficient;
        public Spso2011VelocityCalculator(double inertia, double cognitive, double social)
        {
            inertiaWeight = inertia;
            cognitiveCoefficient = cognitive;
            socialCoefficient = social;
        }

        public Coords GetNextVelocity(Particle particle)
        {
            return GetInertia(particle).Add(getPositionFromSphere(particle)).Minus(particle.CurrentPosition);
        }

        private Coords getPositionFromSphere(Particle particle)
        {
            Coords center = getSphereCenter(particle);
        }
        
        private Coords GetInertia(Particle particle)
        {
            return particle.CurrentVelocity.Multiply(inertiaWeight);
        }
    }
}