using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PaymentSystem.DTO;
using PaymentSystem.Models;

namespace PaymentSystem.Interfaces
{
    public interface ITransactionService
    {
        Task<List<Transaction>> GetBetweenDates(Guid userId, DateTime from, DateTime to);
        Task<Tuple<decimal, decimal>> GetStatisticByDate(Guid userId, DateTime date);
        Task<Transaction> Create(TransactionDto transaction);
    }
}