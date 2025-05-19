namespace Blockchain
{
    static class Program
    {
        static void Main()
        {
            Blockchain myBlockchain = new();

            Console.WriteLine("Mining block 1...");
            myBlockchain.AddBlock(new Block(DateTime.Now, "Block 1 Data"));

            Console.WriteLine("Mining block 2...");
            myBlockchain.AddBlock(new Block(DateTime.Now, "Block 2 Data"));

            Console.WriteLine("\nBlockchain valid?: " + myBlockchain.IsChainValid());

            // Попытка подмены данных
            Console.WriteLine("\nTrying to tamper data...");
            myBlockchain.Chain[1].Data = "Modified Data";
            Console.WriteLine("Blockchain valid?: " + myBlockchain.IsChainValid());
        }
    }
}
