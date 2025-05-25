namespace Blockchain
{
    static class Program
    {
        static void Main()
        {
            Blockchain blockchain = new();
            Console.WriteLine($"Genesis block hash: {blockchain.Chain[0].Hash}");

            blockchain.CreateTransaction(new Transaction("Alice", "Bob", 5));
            blockchain.CreateTransaction(new Transaction("Bob", "Cooper", 3));

            Console.WriteLine("Mining block 1...");
            blockchain.MinePendingTransactions("Miner1");  

            Console.WriteLine($"Balance Miner1: {blockchain.GetBalance("Miner1")}");  
                   
            blockchain.CreateTransaction(new Transaction("Cooper", "Alice", 2));
            blockchain.MinePendingTransactions("Miner1");  

            Console.WriteLine($"Balance Miner1: {blockchain.GetBalance("Miner1")}");  
            Console.WriteLine($"Balance Alice: {blockchain.GetBalance("Alice")}");    
            Console.WriteLine($"Balance Bob: {blockchain.GetBalance("Bob")}");       
            Console.WriteLine($"Balance Cooper: {blockchain.GetBalance("Cooper")}");
        }
    }
}
