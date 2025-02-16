namespace test_api_dotnet.Models;

public class Bank
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string ApiKey { get; set; }= null!;
    public string RedirectUrl { get; set; }= null!;
    public string GetCodeUrl { get; set; }= null!;
    public string CheckResultUrl { get; set; }= null!;
    public BankStatus Status { get; set; }

    // Navigation property
    public ICollection<Payment> Payments { get; set; }= null!;
}

public enum BankStatus
{
    Activate,
    Deactivate
} 