namespace ParticleSwarmOptimization.Swarm.Utilities
{
    public class Coords
    {
        public double XVal { get; private set; }
        public double YVal { get; private set; }

        public Coords(double x, double y)
        {
            XVal = x;
            YVal = y;
        }

        public Coords(Coords coords)
        {
            XVal = coords.XVal;
            YVal = coords.YVal;
        }

        public Coords Add(Coords coords)
        {
            return new Coords(XVal+coords.XVal, YVal+coords.YVal);
        }

        public Coords Minus(Coords coords)
        {
            return new Coords(XVal-coords.XVal, YVal-coords.YVal);
        }

        public Coords Multiply(Coords coords)
        {
            return new Coords(XVal*coords.XVal, YVal*coords.YVal);
        }
        
        public Coords Multiply(double cooefficient)
        {
            return new Coords(XVal*cooefficient, YVal*cooefficient);
        }
    }
}