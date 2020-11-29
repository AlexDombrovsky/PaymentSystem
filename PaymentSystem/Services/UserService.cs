using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PaymentSystem.Interfaces;
using PaymentSystem.Models;

namespace PaymentSystem.Services
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;

        public UserService(DataContext context)
        {
            _context = context;
        }

        public async Task<IList<User>> GetAll()
        {
            var entities = await _context.Users.ToListAsync();
            return entities;
        }

        public async Task<User> Get(Guid id)
        {
            var entity = await _context.Users.FirstOrDefaultAsync(e => e.Id == id);
            return entity;
        }

        public async Task<decimal> UpdateBalance(Guid id, decimal amount)
        {
            var entity = await _context.Users.FirstOrDefaultAsync(e => e.Id == id);
            if (amount < 0 && Math.Abs(amount) > entity.Balance)
                entity.Balance = 0;
            else
                entity.Balance += amount;
            await _context.SaveChangesAsync();
            return entity.Balance;
        }
    }
}