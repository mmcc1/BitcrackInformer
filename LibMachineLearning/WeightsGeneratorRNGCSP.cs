using System.Security.Cryptography;

namespace LibMachineLearning
{
    public class WeightsGeneratorRNGCSP
    {
        private readonly RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();

        public WeightsGeneratorRNGCSP()
        {
        }

        public double[] CreateRandomWeights(int numElements)
        {
            double[] weights = new double[numElements];

            for (int i = 0; i < numElements; i++)
                weights[i] = RandomNumber();

            return weights;
        }


        private double RandomNumber()
        {
            bool inRange = false;
            byte[] b = new byte[8];

            while (!inRange)
            {
                randomNumberGenerator.GetBytes(b);
                double c = BitConverter.ToDouble(b, 0);

                if (c < 1 && c > -1)
                    return c;
            }

            return 9;  //Never reached
        }

    }
}
