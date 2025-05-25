using System;

namespace Blockchain;

public class Blockchain
{
    public List<Block> Chain { get; set; }
    public int Difficulty { get; set; } = 2;
    public decimal MiningReward { get; set; } = 10; 
    public List<Transaction> PendingTransactions { get; set; } = [];
    public decimal TransactionFee { get; set; } = 0.1m;  

    public bool CreateTransaction(Transaction transaction, bool isMiningReward = false)
    {      
        if (!isMiningReward)
        {
            decimal senderBalance = GetBalance(transaction.Sender);
            if (senderBalance < transaction.Amount)
            {
                Console.WriteLine($"Error: {transaction.Sender} has insufficient funds (balance: {senderBalance})");
                return false;
            }
        }

        PendingTransactions.Add(transaction);
        Console.WriteLine($"Transaction added: {transaction.Sender} → {transaction.Recipient}: {transaction.Amount}");
        return true;
    }

    public void MinePendingTransactions(string minerAddress)
    {
        if (PendingTransactions.Count == 0)
        {
            Console.WriteLine("No pending transactions to mine!");
            return;
        }

        decimal totalFee = PendingTransactions.Count * TransactionFee;
 
        Block block = new(DateTime.Now, PendingTransactions, GetLatestBlock().Hash);
        block.MineBlock(Difficulty);
        
        Chain.Add(block);
              
        PendingTransactions =
        [
            new Transaction("SYSTEM", minerAddress, MiningReward + totalFee)
        ];

        Console.WriteLine($"Block mined! Miner {minerAddress} received: {MiningReward + totalFee}");
    }

    public decimal GetBalance(string address)
    {
        decimal balance = 0;

        foreach (var block in Chain)
        {
            foreach (var transaction in block.Transactions)
            {
                if (transaction.Sender == address)
                    balance -= transaction.Amount;
                if (transaction.Recipient == address)
                    balance += transaction.Amount;
            }
        }

        return balance;
    }

    public Blockchain()
    {
        Chain = [];
        AddGenesisBlock();
    }

    private void AddGenesisBlock()
    {        
        List<Transaction> genesisTransactions =
        [
            new Transaction("SYSTEM", "Genesis_Address", 0)  
        ];

        Block genesisBlock = new(DateTime.Now, genesisTransactions, "0");
        genesisBlock.MineBlock(Difficulty);  
        Chain.Add(genesisBlock);

        PendingTransactions.Add(new Transaction("SYSTEM", "Miner1", MiningReward));
        MinePendingTransactions("Miner1");  
    }
    
    public Block GetLatestBlock()
    {
        return Chain[^1];
    }

    public void AddBlock(Block newBlock)
    {
        newBlock.Index = Chain.Count;
        newBlock.PreviousHash = GetLatestBlock().Hash;
        newBlock.MineBlock(Difficulty);
        Chain.Add(newBlock);
    }

    public bool IsChainValid()
    {
        for (int i = 1; i < Chain.Count; i++)
        {
            Block current = Chain[i];
            Block previous = Chain[i - 1];

            if (current.Hash != current.CalculateHash())
                return false;

            if (current.PreviousHash != previous.Hash)
                return false;
        }
        return true;
    }
}