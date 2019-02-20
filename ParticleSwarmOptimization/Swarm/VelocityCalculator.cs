using System;
using ParticleSwarmOptimization.Swarm.Utilities;

namespace ParticleSwarmOptimization.Swarm
{
    public class VelocityCalculator
    {
        private readonly double inertiaWeight;
        private readonly double cognitiveCoefficient;
        private readonly double socialCoefficient;

        public VelocityCalculator(double inertia = 0.7, double cognitive = 1.4, double social = 1.4)
        {
            inertiaWeight = inertia;
            cognitiveCoefficient = cognitive;
            socialCoefficient = social;
        }
        
        public Coords GetNextVelocity(Particle particle)
        {
            var inertia = GetInertia(particle);
            var cognitive = GetCognitiveComponent(particle);
            var social = GetSocialComponent(particle);
            return inertia.Add(cognitive).Add(social);
        }

        private Coords GetSocialComponent(Particle particle)
        {
            return Particle.GlobalBestPosition
                .Minus(particle.CurrentPosition)
                .Multiply(socialCoefficient * Config.RandomNumberGenerator.NextDouble());
        }

        private Coords GetCognitiveComponent(Particle particle)
        {
             return particle.PersonalBestPosition
                .Minus(particle.CurrentPosition)
                .Multiply(cognitiveCoefficient * Config.RandomNumberGenerator.NextDouble());
        }

        private Coords GetInertia(Particle particle)
        {
            return particle.CurrentVelocity.Multiply(inertiaWeight);
        }
    }
}