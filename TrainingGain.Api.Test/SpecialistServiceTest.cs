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
    public class SpecialistServiceTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task ListAsyncWhenNoSpecialistsReturnEmptyCollection() // escenario
        {
            // Arrange
            var mockSpecialistRepository = GetDefaultISpecialistRepositoryInstance();
           mockSpecialistRepository.Setup(r =>r.ListAsync())
               .ReturnsAsync(new List<Specialist>());
            var mockUnitOfWork = GetDefaultUnitOfWorkInstance();
            var service = new SpecialistService(mockSpecialistRepository.Object, mockUnitOfWork.Object);
            // Act
            List<Specialist> result =(List<Specialist>) await service.ListAsync();
            int specialistsCount = result.Count;
            // Assert
            specialistsCount.Should().Equals(0);
        }


        [Test]
        public async Task GetByIdAsyncWhenInvalidReturnsSpecialistsNotFoundResponse()
        {
            //Arrange
            var mockSpecialistRepository = GetDefaultISpecialistRepositoryInstance();
            var specialistId = 1;
            mockSpecialistRepository.Setup(r => r.FindById(specialistId))
                .Returns(Task.FromResult<Specialist>(null));

             var mockUnitOfWork = GetDefaultUnitOfWorkInstance();
            var service = new SpecialistService(mockSpecialistRepository.Object, mockUnitOfWork.Object);
            //Act
            SpecialistResponse result = await service.GetByIdAsync(specialistId);
            var message = result.Message;
            //Assert
            message.Should().Be("Specialist not found");


        }



        private Mock<ISpecialistRepository> GetDefaultISpecialistRepositoryInstance()
        {
            return new Mock<ISpecialistRepository>();
        }
      
        private Mock<IUnitOfWork> GetDefaultUnitOfWorkInstance()
        {
            return new Mock<IUnitOfWork>();
        }



    }
}