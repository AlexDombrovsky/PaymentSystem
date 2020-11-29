using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PaymentSystem.Interfaces;
using PaymentSystem.Models;

namespace PaymentSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly IUserService _userService;

        public TransactionController(IUserService userService, ITransactionService transactionService)
        {
            _userService = userService;
            _transactionService = transactionService;
        }

        [HttpGet("Balance/{userId:Guid}")]
        public async Task<ActionResult<decimal>> Balance(Guid userId)
        {
            var user = await _userService.Get(userId);
            if (user == null) return NotFound();
            return Ok(user.Balance);
        }

        [HttpGet("TransactionHistory/{userId:Guid}/{from:DateTime}/{to:DateTime}")]
        public async Task<ActionResult<List<Transaction>>> TransactionHistory(Guid userId, DateTime from, DateTime to)
        {
            var transactions = await _transactionService.GetBetweenDates(userId, from, to);
            if (transactions == null) return NotFound();
            return Ok(transactions);
        }

        [HttpPost("AddTransaction/{userId:Guid}/{transactionTime:DateTime}/{amount:decimal}/{notes}")]
        public async Task<ActionResult<Transaction>> AddTransaction(Guid userId, DateTime transactionTime,
            decimal amount,
            string notes)
        {
            if (!ModelState.IsValid) return BadRequest();

            var transaction = new Transaction
            {
                UserId = userId,
                Date = transactionTime,
                Amount = amount,
                Notes = notes
            };
            try
            {
                await _transactionService.Create(transaction);
            }
            finally
            {
                await _userService.UpdateBalance(userId, amount);
            }

            return Ok(transaction);
        }

        [HttpGet("Statistic/{userId:Guid}/{date:DateTime}")]
        public async Task<ActionResult<Tuple<decimal, decimal>>> Statistic(Guid userId, DateTime date)
        {
            var transactions = await _transactionService.GetStatisticByDate(userId, date);
            if (transactions == null) return NotFound();
            return Ok(transactions);
        }
    }
}