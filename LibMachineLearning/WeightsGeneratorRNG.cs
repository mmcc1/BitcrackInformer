namespace LibMachineLearning
{
    public class WeightsGeneratorRNG
    {
        private readonly Random randomNumberGenerator = new Random();

        public WeightsGeneratorRNG()
        {
        }

        public double[] CreateRandomWeights(int numElements)
        {
            double[] weights = new double[numElements];

            for (int i = 0; i < numElements; i++)
                weights[i] = randomNumberGenerator.NextDouble();

            return weights;
        }
    }
}
