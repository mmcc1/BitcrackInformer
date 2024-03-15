using LibBitcoinUtils;
using LibMachineLearning;

namespace BitcrackByteInformer
{
    public class EngineByteA
    {
        private ScalingFunction scalingFunction;
        private ActivationFunctions activationFunctions;
        private GeneticAlgorithm geneticAlgorithm;
        private Perceptron perceptron;
        private WeightsGeneratorRNGCSP weightsGenerator;
        private List<BTCKeyStore> keyStore;
        private List<BTCKeysDataSet> trainingDataSet;
        private List<NeuralNetwork> neuralNetwork;
        private int deathRate;
        private int currentDeathRate;

        public EngineByteA()
        {
            Init();
        }

        #region Init

        private void Init()
        {
            Console.WriteLine("Init...");

            scalingFunction = new ScalingFunction();
            weightsGenerator = new WeightsGeneratorRNGCSP();
            activationFunctions = new ActivationFunctions();
            geneticAlgorithm = new GeneticAlgorithm(weightsGenerator);
            neuralNetwork = new List<NeuralNetwork>();
            perceptron = new Perceptron();
            keyStore = new List<BTCKeyStore>();
            trainingDataSet = new List<BTCKeysDataSet>();
            neuralNetwork = new List<NeuralNetwork>();
            deathRate = 10;  //Change this value to increase how many time evolution will occur before abandoning the current weights

            GenerateTrainingDataset();
        }

        #endregion

        #region Design Neural Networks

        private void DesignNN()
        {
            Console.WriteLine("Designing Neural Nets...");

            //Layer 0
            for (int i = 0; i < 20; i++)
            {
                NeuralNetwork nn = new NeuralNetwork()
                {
                    LayerNumber = 0,
                    NetworkNumber = i,
                    Bias = weightsGenerator.CreateRandomWeights(1)[0],
                    Weights = weightsGenerator.CreateRandomWeights(20)
                };

                neuralNetwork.Add(nn);
            }

            //Layer 1
            for (int i = 0; i < 64; i++)
            {
                NeuralNetwork nn = new NeuralNetwork()
                {
                    LayerNumber = 1,
                    NetworkNumber = i,
                    Bias = weightsGenerator.CreateRandomWeights(1)[0],
                    Weights = weightsGenerator.CreateRandomWeights(20)
                };

                neuralNetwork.Add(nn);
            }

            //Layer 2
            for (int i = 0; i < 128; i++)
            {
                NeuralNetwork nn = new NeuralNetwork()
                {
                    LayerNumber = 2,
                    NetworkNumber = i,
                    Bias = weightsGenerator.CreateRandomWeights(1)[0],
                    Weights = weightsGenerator.CreateRandomWeights(64)
                };

                neuralNetwork.Add(nn);
            }

            //Layer 3
            for (int i = 0; i < 128; i++)
            {
                NeuralNetwork nn = new NeuralNetwork()
                {
                    LayerNumber = 3,
                    NetworkNumber = i,
                    Bias = weightsGenerator.CreateRandomWeights(1)[0],
                    Weights = weightsGenerator.CreateRandomWeights(128)
                };

                neuralNetwork.Add(nn);
            }

            //Layer 4
            for (int i = 0; i < 256; i++)
            {
                NeuralNetwork nn = new NeuralNetwork()
                {
                    LayerNumber = 4,
                    NetworkNumber = i,
                    Bias = weightsGenerator.CreateRandomWeights(1)[0],
                    Weights = weightsGenerator.CreateRandomWeights(128)
                };

                neuralNetwork.Add(nn);
            }

            //Output Layer
            for (int i = 0; i < 256; i++)
            {
                NeuralNetwork nn = new NeuralNetwork()
                {
                    LayerNumber = 5,
                    NetworkNumber = i,
                    Bias = weightsGenerator.CreateRandomWeights(1)[0],
                    Weights = weightsGenerator.CreateRandomWeights(256)
                };

                neuralNetwork.Add(nn);
            }
        }

        #endregion

        #region Generate Training Data

        private void GenerateTrainingDataset()
        {
            keyStore.Clear();
            trainingDataSet.Clear();

            Console.WriteLine("Generating Training Dataset...");

            //Generate 100000 Bitcoin keypairs
            for (int i = 0; i < 100000; i++)
            {
                keyStore.Add(BTCBasicFunctions.CreateKeyPair());
            }

            Console.WriteLine("Converting Training Dataset...");

            //Convert them into a format suitable for processing by Neural network.
            for (int i = 0; i < keyStore.Count; i++)
            {
                BTCKeysDataSet ds = new BTCKeysDataSet() { PublicAddressDouble = new double[20], PrivateKey = keyStore[i].PrivateKeyByteArray, PublicAddress = keyStore[i].PublicAddress };
                byte[] pap = BTCPrep.PrepareAddress(keyStore[i].PublicAddress);

                if (pap.Length != 20)
                    continue;

                for (int j = 0; j < pap.Length; j++)
                    ds.PublicAddressDouble[j] = pap[j];

                ds.PublicAddressDouble = scalingFunction.LinearScaleToRange(ds.PublicAddressDouble, new MinMax() { min = 0, max = 255 }, new MinMax() { min = -1, max = 1 });
               
                trainingDataSet.Add(ds);
            }
        }

        #endregion

        #region Public Methods

        public void Execute()
        {
            //Engine runs on a loop with no end.
            Console.WriteLine(string.Format("Engine Byte A Starting..."));
            DesignNN();
            bool shouldRun = true;

            while (shouldRun)
            {
                Train();
            }
        }

        #endregion

        #region Neural Net Training

        private void Train()
        {
            Console.WriteLine("Training...");

            List<double[]> outputWeights = new List<double[]>();

            for (int i = 0; i < trainingDataSet.Count; i++)
            {
                //Layer 0
                List<NeuralNetwork> hiddenLayer1 = neuralNetwork.FindAll(x => x.LayerNumber == 0).OrderBy(x => x.NetworkNumber).ToList();
                double[] weightedSum1 = new double[hiddenLayer1.Count()];
                for (int j = 0; j < hiddenLayer1.Count; j++)
                    weightedSum1[j] = perceptron.Execute(hiddenLayer1[j].Weights, trainingDataSet[i].PublicAddressDouble, hiddenLayer1[j].Bias);

                for (int k = 0; k < weightedSum1.Length; k++)
                    weightedSum1[k] = activationFunctions.BinaryStep(weightedSum1[k]);

                //Layer 1
                List<NeuralNetwork> hiddenLayer2 = neuralNetwork.FindAll(x => x.LayerNumber == 1).OrderBy(x => x.NetworkNumber).ToList();
                double[] weightedSum2 = new double[hiddenLayer2.Count()];
                for (int j = 0; j < hiddenLayer2.Count; j++)
                    weightedSum2[j] = perceptron.Execute(hiddenLayer2[j].Weights, weightedSum1, hiddenLayer2[j].Bias);

                for (int k = 0; k < weightedSum2.Length; k++)
                    weightedSum2[k] = activationFunctions.BinaryStep(weightedSum2[k]);

                //Layer 2
                List<NeuralNetwork> hiddenLayer3 = neuralNetwork.FindAll(x => x.LayerNumber == 2).OrderBy(x => x.NetworkNumber).ToList();
                double[] weightedSum3 = new double[hiddenLayer3.Count()];
                for (int j = 0; j < hiddenLayer3.Count; j++)
                    weightedSum3[j] = perceptron.Execute(hiddenLayer3[j].Weights, weightedSum2, hiddenLayer3[j].Bias);

                for (int k = 0; k < weightedSum3.Length; k++)
                    weightedSum3[k] = activationFunctions.BinaryStep(weightedSum3[k]);

                //Layer 3
                List<NeuralNetwork> hiddenLayer4 = neuralNetwork.FindAll(x => x.LayerNumber == 3).OrderBy(x => x.NetworkNumber).ToList();
                double[] weightedSum4 = new double[hiddenLayer4.Count()];
                for (int j = 0; j < hiddenLayer4.Count; j++)
                    weightedSum4[j] = perceptron.Execute(hiddenLayer4[j].Weights, weightedSum3, hiddenLayer4[j].Bias);

                for (int k = 0; k < weightedSum4.Length; k++)
                    weightedSum4[k] = activationFunctions.BinaryStep(weightedSum4[k]);

                //Layer 4
                List<NeuralNetwork> hiddenLayer5 = neuralNetwork.FindAll(x => x.LayerNumber == 4).OrderBy(x => x.NetworkNumber).ToList();
                double[] weightedSum5 = new double[hiddenLayer5.Count()];
                for (int j = 0; j < hiddenLayer5.Count; j++)
                    weightedSum5[j] = perceptron.Execute(hiddenLayer5[j].Weights, weightedSum4, hiddenLayer5[j].Bias);

                for (int k = 0; k < weightedSum5.Length; k++)
                    weightedSum5[k] = activationFunctions.BinaryStep(weightedSum5[k]);

                //Output Layer
                List<NeuralNetwork> outputLayer = neuralNetwork.FindAll(x => x.LayerNumber == 5).OrderBy(x => x.NetworkNumber).ToList();
                double[] weightedSum6 = new double[outputLayer.Count()];
                for (int j = 0; j < outputLayer.Count; j++)
                    weightedSum6[j] = perceptron.Execute(outputLayer[j].Weights, weightedSum5, outputLayer[j].Bias);

                for (int k = 0; k < weightedSum6.Length; k++)
                    weightedSum6[k] = activationFunctions.BinaryStep(weightedSum6[k]);

                outputWeights.Add(weightedSum5);
            }

            Assess(outputWeights);
        }

        #endregion

        #region Assess

        private void Assess(List<double[]> outputWeights)
        {
            Console.WriteLine("Assessing...");
            int[] matchCount = new int[32];
            bool shouldSave = false;

            //Check output weights against private keys
            for (int i = 0; i < outputWeights.Count; i++)
            {
                outputWeights[i] = ConvertFromBinaryToDouble(outputWeights[i]);

                for (int j = 0; j < trainingDataSet[i].PrivateKey.Length; j++)
                {
                    if (trainingDataSet[i].PrivateKey[j] == (int)outputWeights[i][j])
                    {
                        matchCount[j]++;
                    }
                }
            }

            //If any of the position counts is greater than 440 then save
            //Chance is 1 in 256.  For 100000 private keys that's about 390.
            for(int i = 0; i < matchCount.Length;i++)
            {
                if (matchCount[i] > 440)  //can be changed to increase/decrease amount saved.
                    shouldSave = true;
            }

            if (shouldSave)
            {
                Console.WriteLine("Saving...");
                currentDeathRate = 0;
                Save(matchCount);
            }

            NeuralNetEvaluate();
        }
        
        #endregion

        #region Evaluate Neural Nets

        private void NeuralNetEvaluate()
        {
            if (currentDeathRate >= deathRate)  //Trash weights and recreate
            {
                Console.WriteLine("Resetting...");
                neuralNetwork.Clear();
                DesignNN();
                currentDeathRate = 0;
            }
            else //Evolve
            {
                Console.WriteLine("Evolving...");
                currentDeathRate++;

                List<NeuralNetwork> hiddenLayer1 = neuralNetwork.FindAll(x => x.LayerNumber == 0).OrderBy(x => x.NetworkNumber).ToList();

                for (int j = 0; j < hiddenLayer1.Count; j++)
                {
                    bool[] toChange = geneticAlgorithm.EvaluateFitness(hiddenLayer1[j].Weights);
                    hiddenLayer1[j].Weights = geneticAlgorithm.CrossOverAndMutation(toChange, hiddenLayer1[j].Weights);
                }

                List<NeuralNetwork> hiddenLayer2 = neuralNetwork.FindAll(x => x.LayerNumber == 1).OrderBy(x => x.NetworkNumber).ToList();

                for (int j = 0; j < hiddenLayer2.Count; j++)
                {
                    bool[] toChange = geneticAlgorithm.EvaluateFitness(hiddenLayer2[j].Weights);
                    hiddenLayer2[j].Weights = geneticAlgorithm.CrossOverAndMutation(toChange, hiddenLayer2[j].Weights);
                }

                List<NeuralNetwork> hiddenLayer3 = neuralNetwork.FindAll(x => x.LayerNumber == 2).OrderBy(x => x.NetworkNumber).ToList();

                for (int j = 0; j < hiddenLayer3.Count; j++)
                {
                    bool[] toChange = geneticAlgorithm.EvaluateFitness(hiddenLayer3[j].Weights);
                    hiddenLayer3[j].Weights = geneticAlgorithm.CrossOverAndMutation(toChange, hiddenLayer3[j].Weights);
                }

                List<NeuralNetwork> hiddenLayer4 = neuralNetwork.FindAll(x => x.LayerNumber == 3).OrderBy(x => x.NetworkNumber).ToList();

                for (int j = 0; j < hiddenLayer4.Count; j++)
                {
                    bool[] toChange = geneticAlgorithm.EvaluateFitness(hiddenLayer4[j].Weights);
                    hiddenLayer4[j].Weights = geneticAlgorithm.CrossOverAndMutation(toChange, hiddenLayer4[j].Weights);
                }

                List<NeuralNetwork> hiddenLayer5 = neuralNetwork.FindAll(x => x.LayerNumber == 4).OrderBy(x => x.NetworkNumber).ToList();

                for (int j = 0; j < hiddenLayer5.Count; j++)
                {
                    bool[] toChange = geneticAlgorithm.EvaluateFitness(hiddenLayer5[j].Weights);
                    hiddenLayer5[j].Weights = geneticAlgorithm.CrossOverAndMutation(toChange, hiddenLayer5[j].Weights);
                }

                List<NeuralNetwork> outputLayer = neuralNetwork.FindAll(x => x.LayerNumber == 5).OrderBy(x => x.NetworkNumber).ToList();

                for (int j = 0; j < outputLayer.Count; j++)
                {
                    bool[] toChange = geneticAlgorithm.EvaluateFitness(outputLayer[j].Weights);
                    outputLayer[j].Weights = geneticAlgorithm.CrossOverAndMutation(toChange, outputLayer[j].Weights);
                }
            }
        }

        #endregion

        #region Helpers

        private double[] ConvertFromBinaryToDouble(double[] data)
        {
            double[] value = new double[32];
            int index = 0;

            for (int i = 0; i < 32; i++)
            {
                if (data[index++] == 1)
                    value[i] += 1;
                if (data[index++] == 1)
                    value[i] += 2;
                if (data[index++] == 1)
                    value[i] += 4;
                if (data[index++] == 1)
                    value[i] += 8;
                if (data[index++] == 1)
                    value[i] += 16;
                if (data[index++] == 1)
                    value[i] += 32;
                if (data[index++] == 1)
                    value[i] += 64;
                if (data[index++] == 1)
                    value[i] += 128;
            }

            return value;
        }

        private void Save(int[] positionCount)
        {
            Logic logic = new Logic();
            logic.AddToDB(neuralNetwork, positionCount);
        }

        #endregion
    }
}
