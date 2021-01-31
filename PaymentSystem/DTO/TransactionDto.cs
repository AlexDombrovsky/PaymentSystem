using System;

namespace PaymentSystem.DTO
{
    public class TransactionDto
    {
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Notes { get; set; }
        public Guid UserId { get; set; }
    }
}
