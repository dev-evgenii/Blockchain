using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;

namespace Blockchain;

public class Block
{
    public int Index { get; set; }
    public DateTime Timestamp { get; set; }
    public List<Transaction> Transactions { get; set; }
    public string PreviousHash { get; set; }
    public string Hash { get; set; }
    public int Nonce { get; set; }

    public Block(DateTime timestamp, List<Transaction> transactions, string previousHash = "")
    {
        Index = 0;
        Timestamp = timestamp;
        Transactions = transactions;
        PreviousHash = previousHash;
        Hash = CalculateHash();
        Nonce = 0;
    }

    public string CalculateHash()
    {
        string rawData = $"{Index}{Timestamp}{JsonConvert.SerializeObject(Transactions)}{PreviousHash}{Nonce}";
        byte[] bytes = SHA256.HashData(Encoding.UTF8.GetBytes(rawData));
        return Convert.ToHexString(bytes);
    }

    public void MineBlock(int difficulty)
    {
        string target = new('0', difficulty);
        while (Hash[..difficulty] != target)
        {
            Nonce++;
            Hash = CalculateHash();
        }
        Console.WriteLine($"Block mined: {Hash}");
    }
}
