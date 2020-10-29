using FluentAssertions;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrainingGain.Api.Domain.Models;
using TrainingGain.Api.Domain.Repositories;
using TrainingGain.Api.Domain.Services.Communication;
using TrainingGain.Api.Domain.Services;
using TrainingGain.Api.Services;

namespace TrainingGain.Api.Test
{
    public class CustomerServiceTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task ListAsyncWhenNoCustomersReturnEmptyCollection() // escenario
        {
            // Arrange
            var mockCustomerRepository = GetDefaultICustomerRepositoryInstance();
            mockCustomerRepository.Setup(r => r.ListAsync())
               .ReturnsAsync(new List<Customer>());
            var mockSubscriptionRepository = GetDefaultISubscriptionRepositoryInstance();
            var mockUnitOfWork = GetDefaultUnitOfWorkInstance();
            var service = new CustomerService(mockCustomerRepository.Object, mockUnitOfWork.Object,
                mockSubscriptionRepository.Object);
            // Act
            List<Customer> result =(List<Customer>) await service.ListAsync();
            int customersCount = result.Count;
            // Assert
            customersCount.Should().Equals(0);
        }


        [Test]
        public async Task GetByIdAsyncWhenInvalidReturnsCustomersNotFoundResponse()
        {
            //Arrange
            var mockCustomerRepository = GetDefaultICustomerRepositoryInstance();
            var customerId = 1;
            mockCustomerRepository.Setup(r => r.FindById(customerId))
                .Returns(Task.FromResult<Customer>(null));

            var mockSubscriptionRepository = GetDefaultISubscriptionRepositoryInstance();
            var mockUnitOfWork = GetDefaultUnitOfWorkInstance();
            var service = new CustomerService(mockCustomerRepository.Object, mockUnitOfWork.Object, mockSubscriptionRepository.Object);
            //Act
            CustomerResponse result = await service.GetByIdAsync(customerId);
            var message = result.Message;
            //Assert
            message.Should().Be("Customer not found");


        }



        private Mock<ICustomerRepository> GetDefaultICustomerRepositoryInstance()
        {
            return new Mock<ICustomerRepository>();
        }
        private Mock<ISubscriptionRepository> GetDefaultISubscriptionRepositoryInstance()
        {
            return new Mock<ISubscriptionRepository>();
        }

        private Mock<IUnitOfWork> GetDefaultUnitOfWorkInstance()
        {
            return new Mock<IUnitOfWork>();
        }



    }
}