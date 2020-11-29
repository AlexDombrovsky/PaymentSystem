using System;
using System.Collections.Generic;

namespace PaymentSystem.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public decimal Balance { get; set; }
        public List<Transaction> Transactions { get; set; }
    }
}