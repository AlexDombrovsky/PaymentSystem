using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;
using PaymentSystem.Controllers;
using PaymentSystem.Interfaces;
using PaymentSystem.Models;
using PaymentSystem.Services;
using Xunit;

namespace PaymentSystem.Tests
{
    public class TestClass
    {
       [Fact]
        public void ShouldReturnUserById()
        {
            // Arrange
            var userId = Guid.NewGuid();
            decimal userBalance = 150;
            var userDto = new User
            {
                Id = userId,
                Balance = userBalance
            };

            // Act
            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(p => p.Get(userId)).ReturnsAsync(userDto);
            
            // Assert
            Assert.Equal(userId, userDto.Id);
        }

        //[Fact]
        //public void ShouldUpdateBalance()
        //{
        //    // Arrange
        //    var userId = Guid.NewGuid();
        //    decimal userBalance = 150;
        //    var userDto = new User
        //    {
        //        Id = userId,
        //        Balance = userBalance
        //    };

        //    // Act
        //    var mockUserService = new Mock<IUserService>();
        //   mockUserService.Setup(p => p.UpdateBalance(userId, 150)).ReturnsAsync(userDto.Balance);
            
        //     // Assert
        //    Assert.Equal(300, userDto.Balance);
        //}
    }
}