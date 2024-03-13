namespace LibMachineLearning
{
    public class GeneticAlgorithm
    {
        WeightsGeneratorRNGCSP wg;

        public GeneticAlgorithm(WeightsGeneratorRNGCSP wg)
        {
            this.wg = wg;
        }

        //Fitness is a stochastic process.  There is a low probability of weights being evolved
        //and is determined by chance.
        public bool[] EvaluateFitness(double[] weights)
        {
            bool[] shouldKeep = new bool[weights.Length];

            for (int i = 0; i < weights.Length; i++)
            {
                //Just some random values to drive change
                double k = wg.CreateRandomWeights(1)[0];
                double l = wg.CreateRandomWeights(1)[0];
                double m = wg.CreateRandomWeights(1)[0];

                if (Math.Abs(k) > Math.Abs(l) && Math.Abs(k) - Math.Abs(l) < m)  //Change this value to determine evolution rate.
                    shouldKeep[i] = false;
                else
                    shouldKeep[i] = true;
            }

            return shouldKeep;
        }

        public double[] CrossOverAndMutation(bool[] evaluateFitnessResult, double[] weights)
        {

            double[] _childWeights = new double[weights.Length];

            for (int i = 0; i < weights.Length; i++)
            {
                if (evaluateFitnessResult[i])
                    _childWeights[i] = weights[i];
                else
                {
                    _childWeights[i] = wg.CreateRandomWeights(1)[0];
                }
            }

            return _childWeights;
        }
    }
}
