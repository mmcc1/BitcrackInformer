namespace LibBitcoinUtils
{
    public class BTCKeysDataSet
    {
        public double[] PublicAddressDouble { get; set; }
        public string PublicAddress { get; set; }
        public byte[] PrivateKey { get; set; }
        public byte[] CrackedPrivateKey { get; set; }
    }
}
