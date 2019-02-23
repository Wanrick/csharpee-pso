using System;

namespace ParticleSwarmOptimization.Swarm.Utilities
{
    public class Coords
    {
        public double[] CoordinateArray { get; set; }
        public readonly double LowerBound;
        public readonly double UpperBound;

        public Coords(int dimensions, double minimum, double maximum, bool zeroArray)
        {
            LowerBound = minimum;
            UpperBound = maximum;
            CoordinateArray = new double[dimensions];

            InitCoordinateArray(zeroArray);
        }

        public Coords(Coords coords)
        {
            CoordinateArray = CopyArray(coords.CoordinateArray);

            LowerBound = coords.LowerBound;
            UpperBound = coords.UpperBound;
        }

        public Coords(double[] coordsArr, double minimum, double maximum)
        {
            CoordinateArray = CopyArray(coordsArr);

            if (maximum > minimum)
            {
                UpperBound = maximum;
                LowerBound = minimum;
            }
        }

        private double[] CopyArray(double[] coordsArr)
        {
            var result = new double[coordsArr.Length];
            for (var i = 0; i < result.Length; i++)
            {
                result[i] = coordsArr[i];
            }

            return result;
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

        public double[] GetCoordinateArrayCopy()
        {
            var result = CopyArray(CoordinateArray);
            return result;
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

            CoordinateArray = output;
            return this;
        }
        
        public Coords Add(double scalar)
        {
            var output = new double[CoordinateArray.Length];

            for (var i = 0; i < CoordinateArray.Length; i++)
            {
                output[i] = CoordinateArray[i] + scalar;
            }

            CoordinateArray = output;
            return this;
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

            CoordinateArray = output;
            return this;
        }
        
        public Coords Minus(double scalar)
        {
            var output = new double[CoordinateArray.Length];

            for (var i = 0; i < CoordinateArray.Length; i++)
            {
                output[i] = CoordinateArray[i] - scalar;
            }

            CoordinateArray = output;
            return this;
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

            CoordinateArray = output;
            return this;
        }

        public Coords Multiply(double scalar)
        {
            var output = new double[CoordinateArray.Length];

            for (var i = 0; i < CoordinateArray.Length; i++)
            {
                output[i] = CoordinateArray[i] * scalar;
            }

            CoordinateArray = output;
            return this;
        }
        
        public Coords Divide(Coords coords)
        {
            if (coords.CoordinateArray.Length != CoordinateArray.Length)
            {
                return null;
            }

            var output = new double[coords.CoordinateArray.Length];

            for (var i = 0; i < CoordinateArray.Length; i++)
            {
                if (coords.CoordinateArray[i] > 0)
                {
                    output[i] = CoordinateArray[i] / coords.CoordinateArray[i];
                }
            }

            CoordinateArray = output;
            return this;
        }

        public Coords Divide(double scalar)
        {
            var output = new double[CoordinateArray.Length];

            for (var i = 0; i < CoordinateArray.Length; i++)
            {
                if (scalar > 0)
                {
                    output[i] = CoordinateArray[i] / scalar;
                }
            }

            CoordinateArray = output;
            return this;
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

            CoordinateArray = output;
            return this;
        }
    }
}