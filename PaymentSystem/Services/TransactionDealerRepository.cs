using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PaymentSystem.Interfaces;

namespace PaymentSystem.Services
{
    public class TransactionDealerRepository : ITransactionDealerRepository
    {
        private readonly DataContext _context;

        public TransactionDealerRepository(DataContext context)
        {
            _context = context;
        }

        public async Task BeginTransactionAsync()
        {
            await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
           await _context.Database.CommitTransactionAsync();
        }

        public async Task RollbackTransactionAsync()
        {
            await _context.Database.RollbackTransactionAsync();
        }

        public async Task DisposeTransactionAsync()
        {
            if (_context.Database.CurrentTransaction != null)
            {
                await _context.Database.CurrentTransaction.DisposeAsync();
            }
        }
    }
}
