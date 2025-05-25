namespace Blockchain;

public class Blockchain
{
    public List<Block> Chain { get; set; }
    public int Difficulty { get; set; } = 2;

    public decimal MiningReward { get; set; } = 10; 

    public List<Transaction> PendingTransactions { get; set; } = [];

    public void CreateTransaction(Transaction transaction)
    {
        PendingTransactions.Add(transaction);
    }
  
    public void MinePendingTransactions(string minerAddress)
    {        
        Block block = new(DateTime.Now, PendingTransactions, GetLatestBlock().Hash);
        block.MineBlock(Difficulty);

        Chain.Add(block);

        PendingTransactions =
        [
            new Transaction("SYSTEM", minerAddress, MiningReward)  
        ];
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
            new Transaction("SYSTEM", "Genesis", 0) 
        ];

        Chain.Add(new Block(DateTime.Now, genesisTransactions, "0"));
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