using System;

namespace ParticleSwarmOptimization.Swarm.Utilities
{
    public class Coords
    {
        public double[] CoordinateArray { get; }
        private readonly double lowerBound;
        private readonly double upperBound;

        public Coords(int dimensions, double minimum, double maximum, bool zeroArray)
        {
            lowerBound = minimum;
            upperBound = maximum;
            CoordinateArray = new double[dimensions];

            InitCoordinateArray(zeroArray);
        }

        private void InitCoordinateArray(bool zeroArray)
        {
            if (zeroArray)
            {
                for (var i = 0; i < CoordinateArray.Length; i++)
                {
                    CoordinateArray[i] = 0.0;
                }

                return;
            }

            var range = upperBound - lowerBound;

            for (var i = 0; i < CoordinateArray.Length; i++)
            {
                var pos = (Config.RandomNumberGenerator.NextDouble() * range) + lowerBound;
                CoordinateArray[i] = pos;
            }
        }

        public Coords(Coords coords)
        {
            CoordinateArray = new double[coords.CoordinateArray.Length];
            for (var i = 0; i < CoordinateArray.Length; i++)
            {
                CoordinateArray[i] = coords.CoordinateArray[i];
            }
        }

        public Coords(double[] coordsArr)
        {
            CoordinateArray = new double[coordsArr.Length];
            for (var i = 0; i < CoordinateArray.Length; i++)
            {
                CoordinateArray[i] = coordsArr[i];
            }
        }

        public Coords Add(Coords coords)
        {
            if (coords.CoordinateArray.Length != CoordinateArray.Length)
            {
                return null;
            }

            var output = new double[coords.CoordinateArray.Length];

            for (var i = 0; i < CoordinateArray.Length; i++)
            {
                output[i] = CoordinateArray[i] + coords.CoordinateArray[i];
            }

            return new Coords(output);
        }

        public Coords Minus(Coords coords)
        {
            if (coords.CoordinateArray.Length != CoordinateArray.Length)
            {
                return null;
            }

            var output = new double[coords.CoordinateArray.Length];

            for (var i = 0; i < CoordinateArray.Length; i++)
            {
                output[i] = CoordinateArray[i] - coords.CoordinateArray[i];
            }

            return new Coords(output);
        }

        public Coords Multiply(Coords coords)
        {
            if (coords.CoordinateArray.Length != CoordinateArray.Length)
            {
                return null;
            }

            var output = new double[coords.CoordinateArray.Length];

            for (var i = 0; i < CoordinateArray.Length; i++)
            {
                output[i] = CoordinateArray[i] * coords.CoordinateArray[i];
            }

            return new Coords(output);
        }

        public Coords Multiply(double cooefficient)
        {
            var output = new double[CoordinateArray.Length];

            for (var i = 0; i < CoordinateArray.Length; i++)
            {
                output[i] = CoordinateArray[i] * cooefficient;
            }

            return new Coords(output);
        }

        public void InitPosition(Tuple<double, double> bounds)
        {
        }

        public Coords Move(Coords velocity)
        {
            if (velocity.CoordinateArray.Length != CoordinateArray.Length)
            {
                return null;
            }

            var output = new double[velocity.CoordinateArray.Length];

            for (var i = 0; i < CoordinateArray.Length; i++)
            {
                var newPosition = CoordinateArray[i] + velocity.CoordinateArray[i];
                if (newPosition >= lowerBound && newPosition <= upperBound)
                {
                    output[i] = newPosition;
                }
            }

            return new Coords(output);
        }
    }
}