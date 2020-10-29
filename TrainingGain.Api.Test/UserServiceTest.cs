using FluentAssertions;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrainingGain.Api.Domain.Models;
using TrainingGain.Api.Domain.Repositories;
using TrainingGain.Api.Domain.Services.Communication;

using TrainingGain.Api.Services;

namespace TrainingGain.Api.Test
{
    public class UserServiceTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task ListAsyncWhenNoUsersReturnEmptyCollection() // escenario
        {
            // Arrange
            var mockUserRepository = GetDefaultIUserRepositoryInstance();
            mockUserRepository.Setup(r => r.ListAsync())
               .ReturnsAsync(new List<User>());
            var mockUnitOfWork = GetDefaultUnitOfWorkInstance();
            var service = new UserService(mockUserRepository.Object, mockUnitOfWork.Object);
            // Act
            List<User> result =(List<User>) await service.ListAsync();
            int usersCount = result.Count;
            // Assert
            usersCount.Should().Equals(0);
        }


        [Test]
        public async Task GetByIdAsyncWhenInvaldIdReturnsUsersNotFoundResponse()
        {
            //Arrange
            var mockUserRepository = GetDefaultIUserRepositoryInstance();
            var userId = 1;
            mockUserRepository.Setup(r => r.FindById(userId))
                .Returns(Task.FromResult<User>(null));

            var mockUnitOfWork = GetDefaultUnitOfWorkInstance();
            var service = new UserService(mockUserRepository.Object, mockUnitOfWork.Object);
            //Act
            UserResponse result = await service.GetByIdAsync(userId);
            var message = result.Message;
            //Assert
            message.Should().Be("User not found");


        }



        private Mock<IUserRepository> GetDefaultIUserRepositoryInstance()
        {
            return new Mock<IUserRepository>();
        }
       
        private Mock<IUnitOfWork> GetDefaultUnitOfWorkInstance()
        {
            return new Mock<IUnitOfWork>();
        }



    }
}