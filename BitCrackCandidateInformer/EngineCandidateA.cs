using LibBitcoinUtils;
using LibDAL.Tables;
using LibMachineLearning;
using System.Text;

namespace BitCrackCandidateInformer
{
    public class EngineCandidateA
    {
        List<PKeystore> pKeystores;
        List<WeightStore> weightStores;
        private List<NeuralNetwork> neuralNetwork;
        private ScalingFunction scalingFunction;
        private ActivationFunctions activationFunctions;
        private Perceptron perceptron;

        public EngineCandidateA()
        {
            Init();
        }

        #region Public Methods

        public void Execute()
        {
            GenerateDataset();

            for(int i = 0; i < 32; i++)
            {
                DesignNN(i);

                for(int j = 0; j < pKeystores.Count(); j++)
                {
                    ExecuteNN(j, i);
                }
            }

            Print();
        }

        #endregion

        #region Init

        private void Init()
        {
            Console.WriteLine("Init...");

            //List of public addresses to provide candidates for
            //Add to this list to obtain candidate private key ranges
            string[] publicAddresses = new string[]
            {
                "1CounterpartyXXXXXXXXXXXXXXXUWLpVr"
            };

            pKeystores = new List<PKeystore>();

            for(int i = 0; i < publicAddresses.Count(); i++)
            {
                PKeystore pKeystore = new PKeystore();
                pKeystore.PublicAddress = publicAddresses[i];
                pKeystores.Add(pKeystore);
            }

            weightStores = new List<WeightStore>();
            Logic logic = new Logic();

            for(int i = 0; i < 32; i++)
            {
                WeightStore ws = new WeightStore();
                ws.ByteNum = i;
                ws.WP = logic.GetCandidateNN((ByteSelection)i);
                weightStores.Add(ws);
            }

            neuralNetwork = new List<NeuralNetwork>();
            scalingFunction = new ScalingFunction();
            activationFunctions = new ActivationFunctions();
            perceptron = new Perceptron();
        }

        #endregion

        #region Generate Dataset

        private void GenerateDataset()
        {
            Console.WriteLine("Generating Dataset...");

            for (int i = 0; i < pKeystores.Count; i++)
            {
                pKeystores[i].CandidatePrivKeys = new byte[32];

                byte[] pap = BTCPrep.PrepareAddress(pKeystores[i].PublicAddress);

                if (pap.Length != 20)
                    continue;

                pKeystores[i].PublicAddressDouble = new double[20];

                for (int j = 0; j < pap.Length; j++)
                    pKeystores[i].PublicAddressDouble[j] = pap[j];

                pKeystores[i].PublicAddressDouble = scalingFunction.LinearScaleToRange(pKeystores[i].PublicAddressDouble, new MinMax() { min = 0, max = 255 }, new MinMax() { min = -1, max = 1 });
            }
        }

        #endregion

        #region Design Neural Network

        private void DesignNN(int byteNum)
        {
            Console.WriteLine("Designing Neural Networks...");

            WeightStore ws = weightStores[byteNum];
            List<NN> nns = ws.WP.NeuralNets;
            neuralNetwork.Clear();

            List<NN> layer0 = nns.FindAll(x => x.LayerNumber == 0).OrderBy(x => x.NetworkNumber).ToList();

            //Layer 0
            for (int i = 0; i < layer0.Count(); i++)
            {
                double[] weights = new double[layer0[i].Weights.Count()];

                for(int j = 0; j < layer0[i].Weights.Count(); j++)
                {
                    weights[j] = layer0[i].Weights[j].Entry;
                }
                
                NeuralNetwork nn = new NeuralNetwork()
                {
                    LayerNumber = layer0[i].LayerNumber,
                    NetworkNumber = layer0[i].NetworkNumber,
                    Bias = layer0[i].Bias,
                    Weights = weights
                };

                neuralNetwork.Add(nn);
            }

            List<NN> layer1 = nns.FindAll(x => x.LayerNumber == 1).OrderBy(x => x.NetworkNumber).ToList();

            //Layer 1
            for (int i = 0; i < layer1.Count(); i++)
            {
                double[] weights = new double[layer1[i].Weights.Count()];

                for (int j = 0; j < layer1[i].Weights.Count(); j++)
                {
                    weights[j] = layer1[i].Weights[j].Entry;
                }

                NeuralNetwork nn = new NeuralNetwork()
                {
                    LayerNumber = layer1[i].LayerNumber,
                    NetworkNumber = layer1[i].NetworkNumber,
                    Bias = layer1[i].Bias,
                    Weights = weights
                };
                neuralNetwork.Add(nn);
            }

            List<NN> layer2 = nns.FindAll(x => x.LayerNumber == 2).OrderBy(x => x.NetworkNumber).ToList();

            //Layer 2
            for (int i = 0; i < layer2.Count(); i++)
            {
                double[] weights = new double[layer2[i].Weights.Count()];

                for (int j = 0; j < layer2[i].Weights.Count(); j++)
                {
                    weights[j] = layer2[i].Weights[j].Entry;
                }

                NeuralNetwork nn = new NeuralNetwork()
                {
                    LayerNumber = layer2[i].LayerNumber,
                    NetworkNumber = layer2[i].NetworkNumber,
                    Bias = layer2[i].Bias,
                    Weights = weights
                };
                neuralNetwork.Add(nn);
            }

            List<NN> layer3 = nns.FindAll(x => x.LayerNumber == 3).OrderBy(x => x.NetworkNumber).ToList();

            //**Layer 3
            for (int i = 0; i < layer3.Count(); i++)
            {
                double[] weights = new double[layer3[i].Weights.Count()];

                for (int j = 0; j < layer3[i].Weights.Count(); j++)
                {
                    weights[j] = layer3[i].Weights[j].Entry;
                }

                NeuralNetwork nn = new NeuralNetwork()
                {
                    LayerNumber = layer3[i].LayerNumber,
                    NetworkNumber = layer3[i].NetworkNumber,
                    Bias = layer3[i].Bias,
                    Weights = weights
                };
                neuralNetwork.Add(nn);
            }

            List<NN> layer4 = nns.FindAll(x => x.LayerNumber == 4).OrderBy(x => x.NetworkNumber).ToList();

            //Output Layer
            for (int i = 0; i < layer4.Count(); i++)
            {
                double[] weights = new double[layer4[i].Weights.Count()];

                for (int j = 0; j < layer4[i].Weights.Count(); j++)
                {
                    weights[j] = layer4[i].Weights[j].Entry;
                }

                NeuralNetwork nn = new NeuralNetwork()
                {
                    LayerNumber = layer4[i].LayerNumber,
                    NetworkNumber = layer4[i].NetworkNumber,
                    Bias = layer4[i].Bias,
                    Weights = weights
                };
                neuralNetwork.Add(nn);
            }
        }

        #endregion

        #region Execute Neural Network

        private void ExecuteNN(int pkey, int byteNum)
        {
            Console.WriteLine("Executing Neural Networks...");

            //Layer 0
            List<NeuralNetwork> hiddenLayer1 = neuralNetwork.FindAll(x => x.LayerNumber == 0).OrderBy(x => x.NetworkNumber).ToList();
            double[] weightedSum1 = new double[20];
            for (int j = 0; j < hiddenLayer1.Count; j++)
                weightedSum1[j] = perceptron.Execute(hiddenLayer1[j].Weights, pKeystores[pkey].PublicAddressDouble, hiddenLayer1[j].Bias);

            for (int k = 0; k < weightedSum1.Length; k++)
                weightedSum1[k] = activationFunctions.BinaryStep(weightedSum1[k]);

            //Layer 1
            List<NeuralNetwork> hiddenLayer2 = neuralNetwork.FindAll(x => x.LayerNumber == 1).OrderBy(x => x.NetworkNumber).ToList();
            double[] weightedSum2 = new double[64];
            for (int j = 0; j < hiddenLayer2.Count; j++)
                weightedSum2[j] = perceptron.Execute(hiddenLayer2[j].Weights, weightedSum1, hiddenLayer2[j].Bias);

            for (int k = 0; k < weightedSum2.Length; k++)
                weightedSum2[k] = activationFunctions.BinaryStep(weightedSum2[k]);

            //Layer 2
            List<NeuralNetwork> hiddenLayer3 = neuralNetwork.FindAll(x => x.LayerNumber == 2).OrderBy(x => x.NetworkNumber).ToList();
            double[] weightedSum3 = new double[128];
            for (int j = 0; j < hiddenLayer3.Count; j++)
                weightedSum3[j] = perceptron.Execute(hiddenLayer3[j].Weights, weightedSum2, hiddenLayer3[j].Bias);

            for (int k = 0; k < weightedSum3.Length; k++)
                weightedSum3[k] = activationFunctions.BinaryStep(weightedSum3[k]);

            //**Layer 2**
            List<NeuralNetwork> hiddenLayer4 = neuralNetwork.FindAll(x => x.LayerNumber == 3).OrderBy(x => x.NetworkNumber).ToList();
            double[] weightedSum5 = new double[128];
            for (int j = 0; j < hiddenLayer4.Count; j++)
                weightedSum5[j] = perceptron.Execute(hiddenLayer4[j].Weights, weightedSum3, hiddenLayer4[j].Bias);

            for (int k = 0; k < weightedSum3.Length; k++)
                weightedSum5[k] = activationFunctions.BinaryStep(weightedSum5[k]);

            //Output Layer
            List<NeuralNetwork> outputLayer = neuralNetwork.FindAll(x => x.LayerNumber == 4).OrderBy(x => x.NetworkNumber).ToList();
            double[] weightedSum4 = new double[256];
            for (int j = 0; j < outputLayer.Count; j++)
                weightedSum4[j] = perceptron.Execute(outputLayer[j].Weights, weightedSum5, outputLayer[j].Bias);

            for (int k = 0; k < weightedSum4.Length; k++)
                weightedSum4[k] = activationFunctions.BinaryStep(weightedSum4[k]);

            double[] cfbtd = ConvertFromBinaryToDouble(weightedSum4);
            pKeystores[pkey].CandidatePrivKeys[31 - byteNum] = (byte)cfbtd[31 - byteNum];
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

        private void Print()
        {
            Console.WriteLine("Candidate KeySpaces...");

            for (int i = 0; i < pKeystores.Count; i++)
            {
                string toHex = Helpers.ByteArrayToString(pKeystores[i].CandidatePrivKeys);

                StringBuilder sb = new StringBuilder();
                StringBuilder sb2 = new StringBuilder();

                for (int k = 0; k < toHex.Length - 10; k++)
                {
                    sb.Append(toHex[k]);
                    sb2.Append(toHex[k]);
                }

                sb.Append("0000000000");
                sb2.Append("FFFFFFFFFF");

                Console.WriteLine(sb.ToString());
                Console.WriteLine(sb2.ToString());
                Console.WriteLine(Environment.NewLine);
            }
        }

        #endregion
    }
}
