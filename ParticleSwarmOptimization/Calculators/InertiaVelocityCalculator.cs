using ParticleSwarmOptimization.Swarm;
using ParticleSwarmOptimization.Swarm.Interfaces;
using ParticleSwarmOptimization.Swarm.Utilities;

namespace ParticleSwarmOptimization.Calculators
{
    public class InertiaVelocityCalculator : IVelocityCalculator
    {
        private readonly double inertiaWeight;
        private readonly double cognitiveCoefficient;
        private readonly double socialCoefficient;

        public InertiaVelocityCalculator(double inertia = 0.7, double cognitive = 1.4, double social = 1.4)
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
            inertia.Add(cognitive); 
            inertia.Add(social);
            return inertia;
        }

        private Coords GetSocialComponent(Particle particle)
        {
            var socialComponent = new Coords(Particle.GlobalBestPosition);
            socialComponent.Minus(particle.CurrentPosition);
            socialComponent.Multiply(cognitiveCoefficient * Config.RandomNumberGenerator.NextDouble());
            return socialComponent;
        }

        private Coords GetCognitiveComponent(Particle particle)
        {
            var cognitiveComponent = new Coords(particle.PersonalBestPosition);
            cognitiveComponent.Minus(particle.CurrentPosition);
            cognitiveComponent.Multiply(cognitiveCoefficient * Config.RandomNumberGenerator.NextDouble());
            return cognitiveComponent;
        }

        private Coords GetInertia(Particle particle)
        {
            var inertia = new Coords(particle.CurrentVelocity);
            inertia.Multiply(inertiaWeight);
            return inertia;
        }
    }
}