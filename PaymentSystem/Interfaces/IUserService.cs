using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PaymentSystem.Models;

namespace PaymentSystem.Interfaces
{
    public interface IUserService
    {
        Task<IList<User>> GetAll();
        Task<User> Get(Guid id);
        Task<decimal> UpdateBalance(Guid id, decimal amount);
    }
}