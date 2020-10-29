
using Moq;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TrainingGain.Api.Domain.Models;
using TrainingGain.Api.Domain.Repositories;
using TrainingGain.Api.Domain.Services.Communication;
using TrainingGain.Api.Services;

namespace TrainingGain.Api.Test.Step
{
    [Binding]
    public class UserFeatureSteps
    {

     

        [Given(@"I have a repository with users")]
        public async Task IHaveUsers()
        {
           var mockUserRepository = GetDefaultIUserRepositoryInstance();
            var userId = 1;
            mockUserRepository.Setup(r => r.FindById(userId))
                .Returns(Task.FromResult<User>(null));

            var mockUnitOfWork = GetDefaultUnitOfWorkInstance();
            var service = new UserService(mockUserRepository.Object, mockUnitOfWork.Object);
      
        
        
        
        
        }
        [When(@"I place an order with an id that does not exist")]

        public async Task GetId()
        {

            

          //  UserResponse result = await service.GetByIdAsync(userId);
         //   var message = result.Message;
        }



        [Then(@"I should give an answer")]

        public async Task ShowMessage()
        {
            
           // message.Should().Be("User not found");
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
