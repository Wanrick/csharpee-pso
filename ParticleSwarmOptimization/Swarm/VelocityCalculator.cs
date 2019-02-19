using System;
using ParticleSwarmOptimization.Swarm.Utilities;

namespace ParticleSwarmOptimization.Swarm
{
    public static class VelocityCalculator
    {
        private static readonly Random RandomNumberGenerator = new Random();
        private const double InertiaWeight = 0.7;
        private const double CognitiveCoefficient = 1.4;
        private const double SocialCoefficient = 1.4;
        
        public static Coords GetNextVelocity(Particle particle)
        {
            var inertia = GetInertia(particle);
            var cognitive = GetCognitiveComponent(particle);
            var social = GetSocialComponent(particle);
            return inertia.Add(cognitive).Add(social);
        }

        private static Coords GetSocialComponent(Particle particle)
        {
            return Particle.GlobalBestPosition
                .Minus(particle.CurrentPosition)
                .Multiply(SocialCoefficient * RandomNumberGenerator.NextDouble());
        }

        private static Coords GetCognitiveComponent(Particle particle)
        {
             return particle.PersonalBestPosition
                .Minus(particle.CurrentPosition)
                .Multiply(CognitiveCoefficient * RandomNumberGenerator.NextDouble());
        }

        private static Coords GetInertia(Particle particle)
        {
            return particle.CurrentVelocity.Multiply(InertiaWeight);
        }
        
        
    }
}