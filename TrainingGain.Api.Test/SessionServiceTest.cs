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
    public class SessionServiceTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task ListAsyncWhenNoSessionsReturnEmptyCollection() // escenario
        {
            // Arrange
            var mockSessionRepository = GetDefaultISessionRepositoryInstance();
            mockSessionRepository.Setup(r => r.ListAsync())
               .ReturnsAsync(new List<Session>());
            var mockUnitOfWork = GetDefaultUnitOfWorkInstance();
            var service = new SessionService(mockSessionRepository.Object, mockUnitOfWork.Object);
            // Act
            List<Session> result =(List<Session>) await service.ListAsync();
            int sessionsCount = result.Count;
            // Assert
           sessionsCount.Should().Equals(0);
        }


    


        private Mock<ISessionRepository> GetDefaultISessionRepositoryInstance()
        {
            return new Mock<ISessionRepository>();
        }
       
        private Mock<IUnitOfWork> GetDefaultUnitOfWorkInstance()
        {
            return new Mock<IUnitOfWork>();
        }



    }
}