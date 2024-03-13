using LibDAL;
using LibDAL.Tables;
using LibMachineLearning;

namespace BitcrackByteInformer
{
    public class Logic
    {
        public void AddToDB(List<NeuralNetwork> nn, int[] counts)
        {
            WeightPositions weightPositions = new WeightPositions();
            weightPositions.NeuralNets = new List<NN>();

            for(int i = 0; i < nn.Count; i++)
            {
                NN nN = new NN();

                nN.Id = Guid.NewGuid();
                nN.LayerNumber = nn[i].LayerNumber;
                nN.NetworkNumber = nn[i].NetworkNumber;
                nN.Bias = nn[i].Bias;
                nN.Weights = new List<Weight>();
                
                for(int j = 0; j < nn[i].Weights.Length; j++) 
                {
                    Weight weight = new Weight();
                    weight.Id = Guid.NewGuid();
                    weight.Order = j;
                    weight.Entry = nn[i].Weights[j];
                    
                    nN.Weights.Add(weight);
                }

                weightPositions.NeuralNets.Add(nN);
            }

            weightPositions.Id = Guid.NewGuid();
            
            weightPositions.Byte0 = counts[31];
            weightPositions.Byte1 = counts[30];
            weightPositions.Byte2 = counts[29]; 
            weightPositions.Byte3 = counts[28]; 
            weightPositions.Byte4 = counts[27]; 
            weightPositions.Byte5 = counts[26]; 
            weightPositions.Byte6 = counts[25]; 
            weightPositions.Byte7 = counts[24]; 
            weightPositions.Byte8 = counts[23]; 
            weightPositions.Byte9 = counts[22]; 
            weightPositions.Byte10 = counts[21]; 
            weightPositions.Byte11 = counts[20]; 
            weightPositions.Byte12 = counts[19]; 
            weightPositions.Byte13 = counts[18]; 
            weightPositions.Byte14 = counts[17]; 
            weightPositions.Byte15 = counts[16]; 
            weightPositions.Byte16 = counts[15]; 
            weightPositions.Byte17 = counts[14]; 
            weightPositions.Byte18 = counts[13]; 
            weightPositions.Byte19 = counts[12]; 
            weightPositions.Byte20 = counts[11]; 
            weightPositions.Byte21 = counts[10]; 
            weightPositions.Byte22 = counts[9]; 
            weightPositions.Byte23 = counts[8]; 
            weightPositions.Byte24 = counts[7]; 
            weightPositions.Byte25 = counts[6]; 
            weightPositions.Byte26 = counts[5]; 
            weightPositions.Byte27 = counts[4]; 
            weightPositions.Byte28 = counts[3]; 
            weightPositions.Byte29 = counts[2]; 
            weightPositions.Byte30 = counts[1]; 
            weightPositions.Byte31 = counts[0];

            BTCDBContext dbContext = new BTCDBContext();
            dbContext.WeightPositions.Add(weightPositions);

            dbContext.SaveChanges();
        }
    }
}
