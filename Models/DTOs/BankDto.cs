namespace test_api_dotnet.Models.DTOs;

public class BankDto
{
    public int Id { get; set; }
    public string Title { get; set; }= null!;
    public string ApiKey { get; set; }= null!;
    public string RedirectUrl { get; set; }= null!;
    public string GetCodeUrl { get; set; }= null!;
    public string CheckResultUrl { get; set; }= null!;
    public BankStatus Status { get; set; }
}

public class CreateBankDto
{
    public string Title { get; set; }= null!;
    public string ApiKey { get; set; }= null!;
    public string RedirectUrl { get; set; }= null!;
    public string GetCodeUrl { get; set; }= null!;
    public string CheckResultUrl { get; set; }= null!;
    public BankStatus Status { get; set; }
}

public class UpdateBankDto
{
    public string Title { get; set; }= null!;
    public string ApiKey { get; set; }= null!;
    public string RedirectUrl { get; set; }= null!;
    public string GetCodeUrl { get; set; }= null!;
    public string CheckResultUrl { get; set; }= null!;
    public BankStatus Status { get; set; }
} 