using System;
using System.Runtime.Remoting.Messaging;
using ParticleSwarmOptimization.Swarm.Interfaces;
using ParticleSwarmOptimization.Swarm.Utilities;

namespace ParticleSwarmOptimization.Swarm.Calculators
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
            var inertia = GetInertia(particle);
            var positionFromSphere = GetPositionFromSphere(particle);
            var particlePosition = new Coords(particle.CurrentPosition);

            inertia.Add(positionFromSphere);
            inertia.Minus(particlePosition);
            return inertia;
        }

        private Coords GetInertia(Particle particle)
        {
            var inertia = new Coords(particle.CurrentVelocity);
            inertia.Multiply(inertiaWeight);
            return inertia;
        }

        private Coords GetPositionFromSphere(Particle particle)
        {
            var center = GetSphereCenter(particle);
            var vector = GenerateVector(particle);
            vector = Normalize(vector);
            var radius = GetRadius(center, particle);
            var scalar = GetScalar(radius);
            vector = SetRandomPosition(vector, scalar, center);
            return vector;
        }

        private Coords GetSphereCenter(Particle particle)
        {
            var sphereCenter = new Coords(particle.CurrentPosition);
            sphereCenter.Add(GetAlpha(particle));
            sphereCenter.Add(GetBeta(particle));
            sphereCenter.Divide(3);
            return sphereCenter;
        }

        private Coords GetAlpha(Particle particle)
        {
            var alpha = new Coords(Particle.GlobalBestPosition);
            alpha.Minus(particle.CurrentPosition);
            alpha.Multiply(socialCoefficient * Config.RandomNumberGenerator.NextDouble());
            alpha.Add(particle.CurrentPosition);
            return alpha;
        }

        private Coords GetBeta(Particle particle)
        {
            var beta = new Coords(particle.PersonalBestPosition);
            beta.Minus(particle.CurrentPosition);
            beta.Multiply(cognitiveCoefficient * Config.RandomNumberGenerator.NextDouble());
            beta.Add(particle.CurrentPosition);
            return beta;
        }

        private Coords GenerateVector(Particle particle)
        {
            var scalars = new double[particle.Dimensions];
            for (var i = 0; i < scalars.Length; i++)
            {
                scalars[i] = Config.NormalRandom.Sample();
            }

            return new Coords(scalars, particle.CurrentPosition.LowerBound, particle.CurrentPosition.UpperBound);
        }

        private Coords Normalize(Coords vector)
        {
            var magnitude = GetMagnitude(vector.CoordinateArray);

            vector.Divide(magnitude);

            return vector;
        }

        private double GetMagnitude(double[] vector)
        {
            var magnitude = 0.0;
            for (var i = 0; i < vector.Length; i++)
            {
                magnitude += vector[i] * vector[i];
            }

            magnitude = Math.Sqrt(magnitude);
            return magnitude;
        }

        private double GetRadius(Coords center, Particle particle)
        {
            return GetMagnitude(center.Minus(particle.CurrentPosition).CoordinateArray);
        }

        private double GetScalar(double radius)
        {
            return Config.RandomNumberGenerator.NextDouble() * radius;
        }

        private Coords SetRandomPosition(Coords vector, double scalar, Coords center)
        {
            vector.Multiply(scalar);
            vector.Add(center);
            return vector;
        }
    }
}