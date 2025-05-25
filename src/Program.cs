namespace Blockchain
{
    static class Program
    {
        static void Main()
        {
            Blockchain blockchain = new(); 

            Console.WriteLine("\nMiner1 sends coins to Alice and Bob...");
            blockchain.CreateTransaction(new Transaction("Miner1", "Alice", 3));
            blockchain.CreateTransaction(new Transaction("Miner1", "Bob", 2));
            blockchain.MinePendingTransactions("Miner2");

            Console.WriteLine("\nAlice sends 1 coin to Bob...");
            blockchain.CreateTransaction(new Transaction("Alice", "Bob", 1));
            blockchain.MinePendingTransactions("Miner3");

            Console.WriteLine("\nFinal balances:");
            Console.WriteLine($"Alice: {blockchain.GetBalance("Alice")}");
            Console.WriteLine($"Bob: {blockchain.GetBalance("Bob")}");
            Console.WriteLine($"Miner1: {blockchain.GetBalance("Miner1")}");
            Console.WriteLine($"Miner2: {blockchain.GetBalance("Miner2")}");
            Console.WriteLine($"Miner3: {blockchain.GetBalance("Miner3")}");
        }    
    }
}
