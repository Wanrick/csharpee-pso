using System;

namespace ParticleSwarmOptimization.Swarm.Utilities
{
    public class Coords
    {
        public double[] CoordinateArray { get; }
        public readonly double LowerBound;
        public readonly double UpperBound;

        public Coords(int dimensions, double minimum, double maximum, bool zeroArray)
        {
            LowerBound = minimum;
            UpperBound = maximum;
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

            var range = UpperBound - LowerBound;

            for (var i = 0; i < CoordinateArray.Length; i++)
            {
                var pos = (Config.RandomNumberGenerator.NextDouble() * range) + LowerBound;
                while (Math.Abs(pos) < 0.000000000000001)
                {
                    pos = (Config.RandomNumberGenerator.NextDouble() * range) + LowerBound;
                }
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

            LowerBound = coords.LowerBound;
            UpperBound = coords.UpperBound;
        }

        public Coords(double[] coordsArr, double minimum, double maximum)
        {
            CoordinateArray = new double[coordsArr.Length];
            for (var i = 0; i < CoordinateArray.Length; i++)
            {
                CoordinateArray[i] = coordsArr[i];
            }

            if (maximum > minimum)
            {
                UpperBound = maximum;
                LowerBound = minimum;
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

            return new Coords(output, coords.LowerBound, coords.UpperBound);
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

            return new Coords(output, coords.LowerBound, coords.UpperBound);
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

            return new Coords(output, coords.LowerBound, coords.UpperBound);
        }

        public Coords Multiply(double cooefficient)
        {
            var output = new double[CoordinateArray.Length];

            for (var i = 0; i < CoordinateArray.Length; i++)
            {
                output[i] = CoordinateArray[i] * cooefficient;
            }

            return new Coords(output, LowerBound, UpperBound);
        }
        
        public Coords Divide(double cooefficient)
        {
            var output = new double[CoordinateArray.Length];

            for (var i = 0; i < CoordinateArray.Length; i++)
            {
                output[i] = CoordinateArray[i] / cooefficient;
            }

            return new Coords(output, LowerBound, UpperBound);
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
                if (newPosition >= LowerBound && newPosition <= UpperBound)
                {
                    output[i] = newPosition;
                }
                else
                {
                    output[i] = CoordinateArray[i];
                }
            }

            return new Coords(output, velocity.LowerBound, velocity.UpperBound);
        }
    }
}