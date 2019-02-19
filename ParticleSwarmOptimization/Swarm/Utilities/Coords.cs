using System;

namespace ParticleSwarmOptimization.Swarm.Utilities
{
    public class Coords
    {
        public double[] CoordinateArray { get; set; }

        public Coords(int dimensions)
        {
            CoordinateArray = new double[dimensions];
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
                output[i] = CoordinateArray[i] + cooefficient;
            }
            
            return new Coords(output);
        }

        public void InitBestPosition(double initialValues)
        {
            for (var i = 0; i < CoordinateArray.Length; i++)
            {
                CoordinateArray[i] = initialValues;
            }
        }

        public void InitVelocity()
        {
            for (var i = 0; i < CoordinateArray.Length; i++)
            {
                CoordinateArray[i] = 0.0;
            }
        }

        public void InitPosition(double bounds)
        {
            for (var i = 0; i < CoordinateArray.Length; i++)
            {
                var pos = Config.RandomNumberGenerator.NextDouble() * bounds;
                if (Config.RandomNumberGenerator.Next(0, 2) == 1)
                {
                    pos *= -1;
                }

                CoordinateArray[i] = pos;
            }
        }
    }
}