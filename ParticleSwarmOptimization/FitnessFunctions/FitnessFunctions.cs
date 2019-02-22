using System;
using ParticleSwarmOptimization.FitnessFunctions.Interface;
using ParticleSwarmOptimization.Swarm;

namespace ParticleSwarmOptimization.FitnessFunctions
{
    public class AbsVal : IFitnessFunction
    {
        public double EvaluateFitness(Particle particle)
        {
            var position = particle.CurrentPosition.CoordinateArray;
            var sum = 0.0;
            for (var i = 0; i < position.Length; i++)
            {
                sum += Math.Abs(position[i]);
            }
            return sum;
        }

        public Tuple<double, double> GetBounds()
        {
            return new Tuple<double, double>(-100, 100);
        }
    }
    
    //Converges on 0
    public class Sphere : IFitnessFunction
    {
        public double EvaluateFitness(Particle particle)
        {
            var position = particle.CurrentPosition.CoordinateArray;
            var sum = 0.0;
            for (var i = 0; i < position.Length; i++)
            {
                sum += Math.Pow(position[i], 2);
            }
            return sum;
        }

        public Tuple<double, double> GetBounds()
        {
            return new Tuple<double, double>(-5.12, 5.12);
        }
    }

    //Converges on 0 for x = [0,0,...]
    public class Ackley : IFitnessFunction
    {
        public double EvaluateFitness(Particle particle)
        {
            var result = 0.0;
            var array = particle.CurrentPosition.CoordinateArray;

            result = -20 * GetFirstExponent(array) - GetSecondExponent(array) + 20 + Math.Exp(1);
            return result;
        }

        private double GetSecondExponent(double[] array)
        {
            var summation = 0.0;
            var result = 0.0;
            var n = array.Length;

            for (var i = 0; i < n; i++)
            {
                summation += Math.Cos(2*Math.PI*array[i]);
            }

            result = Math.Exp(1.0 / n * summation);
            return result;
        }

        private double GetFirstExponent(double[] array)
        {
            var summation = 0.0;
            var result = 0.0;
            var n = array.Length;

            for (var i = 0; i < n; i++)
            {
                summation += Math.Pow(array[i], 2);
            }

            result = Math.Exp(-0.2 * Math.Sqrt(1.0 / n * summation));
            return result;
        }

        public Tuple<double, double> GetBounds()
        {
            return new Tuple<double, double>(-32.768, 32.768);
        }
    }
    
    public class Michalewicz : IFitnessFunction
    {
        public double EvaluateFitness(Particle particle)
        {
            var result = 0.0;
            var arr = particle.CurrentPosition.CoordinateArray;
            var n = arr.Length;
            var m = 10.0;

            for (var i = 0; i < n; i++)
            {
                result += Math.Sin(arr[i]) * Math.Pow(Math.Sin(i * Math.Pow(arr[i], 2) / Math.PI), 2 * m);
            }
            
            return -1 * result;
        }

        public Tuple<double, double> GetBounds()
        {
            return new Tuple<double, double>(0, Math.PI);
        }
    }
    
    //Converges on 1 for x = 0
    public class Katsuura : IFitnessFunction //http://infinity77.net/global_optimization/test_functions_nd_K.html
    {
        public double EvaluateFitness(Particle particle)
        {
            var result = 1.0;
            var arr = particle.CurrentPosition.CoordinateArray;
            var n = arr.Length;

            for (var i = 0; i < n-1; i++)
            {
                result *= (1 + (i + 1) * KatsuuraSum(arr[i]));
            }
            
            return result;
        }

        private double KatsuuraSum(double x)
        {
            var d = 32;
            var result = 0.0;
            
            for (var k = 1; k < d; k++)
            {
                result += Math.Floor((Math.Pow(2, k) * x)) * Math.Pow(2, -k);
            }

            return result;
        }

        public Tuple<double, double> GetBounds()
        {
            return new Tuple<double, double>(0, 100);
        }
    }
    
    public class Schubert : IFitnessFunction
    {
        public double EvaluateFitness(Particle particle)
        {
            var result = 1.0;
            var arr = particle.CurrentPosition.CoordinateArray;
            var n = arr.Length;

            for (var i = 0; i < n; i++)
            {
                result *= SchubertSum(arr[i]);
            }
            
            return result;
        }

        private double SchubertSum(double x)
        {
            var result = 0.0;
            
            for (var i = 1; i < 5; i++)
            {
                result += i * Math.Cos(x * (i + 1) + i);
            }

            return result;
        }

        public Tuple<double, double> GetBounds()
        {
            return new Tuple<double, double>(-10.0, 10.0);
        }
    }
    
    public class AckleyShRot : IFitnessFunction
    {
        public double EvaluateFitness(Particle particle)
        {
            var result = 0.0;
            var arr = particle.CurrentPosition.CoordinateArray;
            var n = arr.Length;
            
            return result;
        }

        public Tuple<double, double> GetBounds()
        {
            return new Tuple<double, double>(-32.768, 32.768);
        }
    }
    
    public class MichalewiczShRot : IFitnessFunction
    {
        public double EvaluateFitness(Particle particle)
        {
            var result = 0.0;
            var arr = particle.CurrentPosition.CoordinateArray;
            var n = arr.Length;
            
            return result;
        }

        public Tuple<double, double> GetBounds()
        {
            return new Tuple<double, double>(0, Math.PI);
        }
    }
    
    public class KatsuuraShRot : IFitnessFunction
    {
        public double EvaluateFitness(Particle particle)
        {
            var result = 0.0;
            var arr = particle.CurrentPosition.CoordinateArray;
            var n = arr.Length;
            
            return result;
        }

        public Tuple<double, double> GetBounds()
        {
            return new Tuple<double, double>(0, 100);
        }
    }
    
    public class SchubertShRot : IFitnessFunction
    {
        public double EvaluateFitness(Particle particle)
        {
            var result = 0.0;
            var arr = particle.CurrentPosition.CoordinateArray;
            var n = arr.Length;
            
            return result;
        }

        public Tuple<double, double> GetBounds()
        {
            return new Tuple<double, double>(-10.0, 10.0);
        }
    }
}