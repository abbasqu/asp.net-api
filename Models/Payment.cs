using System;

namespace test_api_dotnet.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public PaymentStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? ReferenceCode { get; set; }

        public string UserId { get; set; } = null!;
        public virtual AppUser User { get; set; } = null!;

        public int BankId { get; set; }
        public Bank Bank { get; set; } = null!;
    }
} 