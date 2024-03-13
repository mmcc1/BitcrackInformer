using LibDAL;
using LibDAL.Tables;
using Microsoft.EntityFrameworkCore;

namespace BitCrackCandidateInformer
{
    public enum ByteSelection
    {
        Byte0 = 0,
        Byte1,
        Byte2,
        Byte3,
        Byte4,
        Byte5,
        Byte6,
        Byte7,
        Byte8,
        Byte9,
        Byte10,
        Byte11,
        Byte12,
        Byte13,
        Byte14,
        Byte15,
        Byte16,
        Byte17,
        Byte18,
        Byte19,
        Byte20,
        Byte21,
        Byte22,
        Byte23,
        Byte24,
        Byte25,
        Byte26,
        Byte27,
        Byte28,
        Byte29,
        Byte30,
        Byte31
    }
    public class Logic
    {
        public WeightPositions GetCandidateNN(ByteSelection bs)
        {
            BTCDBContext context = new BTCDBContext();
            WeightPositions wp = new WeightPositions();

            switch (bs)
            {
                case ByteSelection.Byte0:
                    {
                        wp = context.WeightPositions.Include(x => x.NeuralNets).ThenInclude(x => x.Weights).OrderByDescending(x => x.Byte0).First();
                        break;
                    }
                case ByteSelection.Byte1:
                    {
                        wp = context.WeightPositions.Include(x => x.NeuralNets).ThenInclude(x => x.Weights).OrderByDescending(x => x.Byte1).First();
                        break;
                    }
                case ByteSelection.Byte2:
                    {
                        wp = context.WeightPositions.Include(x => x.NeuralNets).ThenInclude(x => x.Weights).OrderByDescending(x => x.Byte2).First();
                        break;
                    }
                case ByteSelection.Byte3:
                    {
                        wp = context.WeightPositions.Include(x => x.NeuralNets).ThenInclude(x => x.Weights).OrderByDescending(x => x.Byte3).First();
                        break;
                    }
                case ByteSelection.Byte4:
                    {
                        wp = context.WeightPositions.Include(x => x.NeuralNets).ThenInclude(x => x.Weights).OrderByDescending(x => x.Byte4).First();
                        break;
                    }
                case ByteSelection.Byte5:
                    {
                        wp = context.WeightPositions.Include(x => x.NeuralNets).ThenInclude(x => x.Weights).OrderByDescending(x => x.Byte5).First();
                        break;
                    }
                case ByteSelection.Byte6:
                    {
                        wp = context.WeightPositions.Include(x => x.NeuralNets).ThenInclude(x => x.Weights).OrderByDescending(x => x.Byte6).First();
                        break;
                    }
                case ByteSelection.Byte7:
                    {
                        wp = context.WeightPositions.Include(x => x.NeuralNets).ThenInclude(x => x.Weights).OrderByDescending(x => x.Byte7).First();
                        break;
                    }
                case ByteSelection.Byte8:
                    {
                        wp = context.WeightPositions.Include(x => x.NeuralNets).ThenInclude(x => x.Weights).OrderByDescending(x => x.Byte8).First();
                        break;
                    }
                case ByteSelection.Byte9:
                    {
                        wp = context.WeightPositions.Include(x => x.NeuralNets).ThenInclude(x => x.Weights).OrderByDescending(x => x.Byte9).First();
                        break;
                    }
                case ByteSelection.Byte10:
                    {
                        wp = context.WeightPositions.Include(x => x.NeuralNets).ThenInclude(x => x.Weights).OrderByDescending(x => x.Byte10).First();
                        break;
                    }
                case ByteSelection.Byte11:
                    {
                        wp = context.WeightPositions.Include(x => x.NeuralNets).ThenInclude(x => x.Weights).OrderByDescending(x => x.Byte11).First();
                        break;
                    }
                case ByteSelection.Byte12:
                    {
                        wp = context.WeightPositions.Include(x => x.NeuralNets).ThenInclude(x => x.Weights).OrderByDescending(x => x.Byte12).First();
                        break;
                    }
                case ByteSelection.Byte13:
                    {
                        wp = context.WeightPositions.Include(x => x.NeuralNets).ThenInclude(x => x.Weights).OrderByDescending(x => x.Byte13).First();
                        break;
                    }
                case ByteSelection.Byte14:
                    {
                        wp = context.WeightPositions.Include(x => x.NeuralNets).ThenInclude(x => x.Weights).OrderByDescending(x => x.Byte14).First();
                        break;
                    }
                case ByteSelection.Byte15:
                    {
                        wp = context.WeightPositions.Include(x => x.NeuralNets).ThenInclude(x => x.Weights).OrderByDescending(x => x.Byte15).First();
                        break;
                    }
                case ByteSelection.Byte16:
                    {
                        wp = context.WeightPositions.Include(x => x.NeuralNets).ThenInclude(x => x.Weights).OrderByDescending(x => x.Byte16).First();
                        break;
                    }
                case ByteSelection.Byte17:
                    {
                        wp = context.WeightPositions.Include(x => x.NeuralNets).ThenInclude(x => x.Weights).OrderByDescending(x => x.Byte17).First();
                        break;
                    }
                case ByteSelection.Byte18:
                    {
                        wp = context.WeightPositions.Include(x => x.NeuralNets).ThenInclude(x => x.Weights).OrderByDescending(x => x.Byte18).First();
                        break;
                    }
                case ByteSelection.Byte19:
                    {
                        wp = context.WeightPositions.Include(x => x.NeuralNets).ThenInclude(x => x.Weights).OrderByDescending(x => x.Byte19).First();
                        break;
                    }
                case ByteSelection.Byte20:
                    {
                        wp = context.WeightPositions.Include(x => x.NeuralNets).ThenInclude(x => x.Weights).OrderByDescending(x => x.Byte20).First();
                        break;
                    }
                case ByteSelection.Byte21:
                    {
                        wp = context.WeightPositions.Include(x => x.NeuralNets).ThenInclude(x => x.Weights).OrderByDescending(x => x.Byte21).First();
                        break;
                    }
                case ByteSelection.Byte22:
                    {
                        wp = context.WeightPositions.Include(x => x.NeuralNets).ThenInclude(x => x.Weights).OrderByDescending(x => x.Byte22).First();
                        break;
                    }
                case ByteSelection.Byte23:
                    {
                        wp = context.WeightPositions.Include(x => x.NeuralNets).ThenInclude(x => x.Weights).OrderByDescending(x => x.Byte23).First();
                        break;
                    }
                case ByteSelection.Byte24:
                    {
                        wp = context.WeightPositions.Include(x => x.NeuralNets).ThenInclude(x => x.Weights).OrderByDescending(x => x.Byte24).First();
                        break;
                    }
                case ByteSelection.Byte25:
                    {
                        wp = context.WeightPositions.Include(x => x.NeuralNets).ThenInclude(x => x.Weights).OrderByDescending(x => x.Byte25).First();
                        break;
                    }
                case ByteSelection.Byte26:
                    {
                        wp = context.WeightPositions.Include(x => x.NeuralNets).ThenInclude(x => x.Weights).OrderByDescending(x => x.Byte26).First();
                        break;
                    }
                case ByteSelection.Byte27:
                    {
                        wp = context.WeightPositions.Include(x => x.NeuralNets).ThenInclude(x => x.Weights).OrderByDescending(x => x.Byte27).First();
                        break;
                    }
                case ByteSelection.Byte28:
                    {
                        wp = context.WeightPositions.Include(x => x.NeuralNets).ThenInclude(x => x.Weights).OrderByDescending(x => x.Byte28).First();
                        break;
                    }
                case ByteSelection.Byte29:
                    {
                        wp = context.WeightPositions.Include(x => x.NeuralNets).ThenInclude(x => x.Weights).OrderByDescending(x => x.Byte29).First();
                        break;
                    }
                case ByteSelection.Byte30:
                    {
                        wp = context.WeightPositions.Include(x => x.NeuralNets).ThenInclude(x => x.Weights).OrderByDescending(x => x.Byte30).First();
                        break;
                    }
                case ByteSelection.Byte31:
                    {
                        wp = context.WeightPositions.Include(x => x.NeuralNets).ThenInclude(x => x.Weights).OrderByDescending(x => x.Byte31).First();
                        break;
                    }
                default:
                    break;
            }

            return wp;

        }
    }
}
