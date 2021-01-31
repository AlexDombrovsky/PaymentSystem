using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using PaymentSystem.DTO;
using PaymentSystem.Interfaces;
using PaymentSystem.Models;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;

namespace PaymentSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly IUserService _userService;
        private readonly ITransactionDealerRepository _transactionDealerRepository;

        public TransactionController(ITransactionService transactionService, IUserService userService, ITransactionDealerRepository transactionDealerRepository)
        {
            _transactionService = transactionService;
            _userService = userService;
            _transactionDealerRepository = transactionDealerRepository;
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

        [HttpPost("AddTransaction")]
        public async Task<ActionResult<Transaction>> AddTransaction(TransactionDto model)
        {
            if (!ModelState.IsValid) return BadRequest();
            await _transactionDealerRepository.BeginTransactionAsync();
            try
            {
                await _transactionService.Create(model);
                await _userService.UpdateBalance(model.UserId, model.Amount);

                await _transactionDealerRepository.CommitTransactionAsync();
            }
            catch (Exception)
            {
                await _transactionDealerRepository.RollbackTransactionAsync();
                throw;
            }
            finally
            {
                await _transactionDealerRepository.DisposeTransactionAsync();
            }

            return Ok(model);
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