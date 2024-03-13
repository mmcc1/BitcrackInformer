namespace LibDAL.Tables
{
    public class NN
    {
        public Guid Id { get; set; }
        public int LayerNumber { get; set; }
        public int NetworkNumber { get; set; }
        public List<Weight> Weights { get; set; }
        public double Bias { get; set; }
    }
}
