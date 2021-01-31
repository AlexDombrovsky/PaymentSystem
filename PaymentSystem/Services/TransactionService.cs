using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PaymentSystem.DTO;
using PaymentSystem.Interfaces;
using PaymentSystem.Models;

namespace PaymentSystem.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly DataContext _context;

        public TransactionService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Transaction>> GetBetweenDates(Guid userId, DateTime from, DateTime to)
        {
            var transactions = await _context.Transactions
                .Where(_ => _.Date >= from && _.Date <= to && _.UserId == userId).ToListAsync();
            return transactions;
        }

        public async Task<Tuple<decimal, decimal>> GetStatisticByDate(Guid userId, DateTime date)
        {
            decimal plus = 0;
            decimal minus = 0;
            var transactions = await _context.Transactions.Where(_ => _.Date.Date == date.Date && _.UserId == userId)
                .ToListAsync();
            var incrementalTransactions = transactions.Where(_ => _.Amount >= 0).Select(_ => _.Amount).ToList();
            foreach (var item in incrementalTransactions) plus += item;

            var decrementalTransactions = transactions.Where(_ => _.Amount < 0).Select(_ => _.Amount).ToList();
            foreach (var item in decrementalTransactions) minus += item;

            return Tuple.Create(plus, minus);
        }
        
        public async Task<Transaction> Create(TransactionDto transaction)
        {
            var result = await _context.Transactions.AddAsync(new Transaction
            {
                Amount = transaction.Amount,
                Date = transaction.Date,
                UserId = transaction.UserId,
                Notes = transaction.Notes,
            });
            await _context.SaveChangesAsync();

            return result.Entity;
        }
    }
}