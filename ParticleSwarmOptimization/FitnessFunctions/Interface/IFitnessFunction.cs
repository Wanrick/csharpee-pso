namespace ParticleSwarmOptimization.FitnessFunctions.Interface
{
    public interface IFitnessFunction
    {
        double EvaluateFitness(double x, double y);
    }
}