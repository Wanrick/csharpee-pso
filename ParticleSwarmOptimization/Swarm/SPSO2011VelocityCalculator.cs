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
            return GetInertia(particle).Add(GetPositionFromSphere(particle)).Minus(particle.CurrentPosition);
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

        private Coords SetRandomPosition(Coords vector, double scalar, Coords center)
        {
            return vector.Multiply(scalar).Add(center);
        }

        private double GetScalar(double radius)
        {
            return Config.RandomNumberGenerator.NextDouble()*radius;
        }

        private double GetRadius(Coords center, Particle particle)
        {
            return GetMagnitude(center.Minus(particle.CurrentPosition).CoordinateArray);
        }

        private Coords GetSphereCenter(Particle particle)
        {
            return particle.CurrentPosition.Add(GetAlpha(particle)).Add(GetBeta(particle)).Divide(3);
        }

        private Coords GetBeta(Particle particle)
        {
            return particle.PersonalBestPosition
                .Minus(particle.CurrentPosition)
                .Multiply(cognitiveCoefficient * Config.RandomNumberGenerator.NextDouble())
                .Add(particle.CurrentPosition);
        }

        private Coords GetAlpha(Particle particle)
        {
            return Particle.GlobalBestPosition
                .Minus(particle.CurrentPosition)
                .Multiply(socialCoefficient * Config.RandomNumberGenerator.NextDouble())
                .Add(particle.CurrentPosition);
        }

        private Coords Normalize(Coords vector)
        {
            var magnitude = GetMagnitude(vector.CoordinateArray);
            
            for (var i = 0; i < vector.CoordinateArray.Length; i++)
            {
                if (magnitude > 0)
                {
                    vector.CoordinateArray[i] = vector.CoordinateArray[i] / magnitude;
                }
            }

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

        private Coords GenerateVector(Particle particle)
        {
            var scalars = new double[particle.Dimensions];
            for (var i = 0; i < scalars.Length; i++)
            {
                scalars[i] = Config.NormalRandom.Sample();
            }

            return new Coords(scalars, particle.CurrentPosition.LowerBound, particle.CurrentPosition.UpperBound);
        }


        private Coords GetInertia(Particle particle)
        {
            return particle.CurrentVelocity.Multiply(inertiaWeight);
        }
    }
}