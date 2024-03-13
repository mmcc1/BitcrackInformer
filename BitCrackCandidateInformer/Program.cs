namespace BitCrackCandidateInformer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Generating candidates...");

            EngineCandidateA engineCandidateA = new EngineCandidateA();
            engineCandidateA.Execute();
        }
    }
}
