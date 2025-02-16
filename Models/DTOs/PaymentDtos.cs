using System;

namespace test_api_dotnet.Models.DTOs
{
    public class CreatePaymentDto
    {
        public decimal Price { get; set; }
        public string ReferenceCode { get; set; }
    }

    public class UpdatePaymentDto
    {
        public decimal Price { get; set; }
        public string ReferenceCode { get; set; }
    }

    public class ChangePaymentStatusDto
    {
        public PaymentStatus Status { get; set; }
    }

    public class PaymentDto
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public decimal Price { get; set; }
        public PaymentStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ReferenceCode { get; set; }
    }
} 