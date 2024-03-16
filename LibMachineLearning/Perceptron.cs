namespace LibMachineLearning
{
    public class Perceptron
    {
        public double Execute(double[] weights, double[] inputs)
        {
            double sum = 0;

            for (int i = 0; i < inputs.Length; i++)
                sum += weights[i] * inputs[i];

            return sum;
        }
    }
}
