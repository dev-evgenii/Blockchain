namespace Blockchain;
public class Transaction(string sender, string recipient, decimal amount)
{
    public string Sender { get; set; } = sender;
    public string Recipient { get; set; } = recipient;
    public decimal Amount { get; set; } = amount;
}