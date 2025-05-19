namespace Blockchain;

public class Blockchain
{
    public List<Block> Chain { get; set; }
    public int Difficulty { get; set; } = 2;

    public Blockchain()
    {
        Chain = new List<Block>();
        AddGenesisBlock();
    }

    private void AddGenesisBlock()
    {
        Chain.Add(new Block(DateTime.Now, "Genesis Block", "0"));
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