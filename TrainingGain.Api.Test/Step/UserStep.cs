using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using TrainingGain.Api.Domain.Models;
using TrainingGain.Api.Persistance.Repositories;
using TrainingGain.Api.Services;

namespace TrainingGain.Api.Test.Step
{
    [Binding]
    public sealed class UserStep
    {
        public UserRepository userRepository;
        public CustomerRepository customerRepository;
        public CustomerService customerService;
        public SpecialistRepository specialistRepository;

        public User user1 = new User
     { 
        Id = 1,
            Name = "Jose",
            LastName = "Pardo",
            Birth = new DateTime(1985 - 05 - 02),
            Address = "av.los heroes",
            Phone = 963125438,
            Age = 35,
            Email = "pardo7@gmail.com",
            Country = "Peru",
            Gender = "M",
            Password = "start2020"
        };
    public User user2 = new User
    {
        Id = 2,
        Name = "Manuel",
        LastName = "Jimenez",
        Birth = new DateTime(1991 - 07 - 02),
        Address = "av.aviacion",
        Phone = 962543915,
        Age = 29,
        Email = "jimenes20@gmail.com",
        Country = "Peru",
        Gender = "M",
        Password = "finish2020"
    };

     public Customer customer = new Customer { Id = 1, Description = "deportista", UserId = 1 };
     public Specialist specialist = new Specialist { Id = 1, Specialty = "fisioterapia", UserId = 2 };

        public async void Create()
        {
            await userRepository.AddAsync(user1);
            await userRepository.AddAsync(user2);
            await  customerRepository.AddAsync(customer);
            await specialistRepository.AddAsync(specialist);

        }



    [Given(@"the user wants to register as a customer")]
        public async void GivenTheUserWantsToRegisterAsACustomer()
        {
            await customerRepository.AddAsync(customer);
        }

     
        [When(@"the user registers as a customer")]
        public async void WhenTheUserRegistersAsACustomer()
        {

          var existingUser = await userRepository.FindById(user1.Id);
            existingUser.Name = user1.Name;
            existingUser.LastName = user1.LastName;
            existingUser.Birth = user1.Birth;
            existingUser.Address = user1.Address;
            existingUser.Phone = user1.Phone;
            existingUser.Age = user1.Age;
            existingUser.Password = user1.Password;
            existingUser.Email = user1.Email;
            existingUser.Country = user1.Country;
            existingUser.Gender = user1.Gender;

            userRepository.Update(existingUser);

        }

        [Then(@"The system registers the account generated as a customer")]
        public async void ThenTheSystemRegistersTheAccountGeneratedAsACustomer()
        {
            IEnumerable<Customer> list = await customerRepository.ListAsync();
            Assert.That(list.Count() == 1, Is.True);


        }




        [Given(@"the user wants to register as specialist")]
        public async void GivenTheUserWantsToRegisterAsSpecialist()
        {
            await specialistRepository.AddAsync(specialist);
        }


        [When(@"the user registers as specialist")]
        public async void WhenTheUserRegistersAsSpecialist()
        {

            var existingUser = await userRepository.FindById(user2.Id);
            existingUser.Name = user2.Name;
            existingUser.LastName = user2.LastName;
            existingUser.Birth = user2.Birth;
            existingUser.Address = user2.Address;
            existingUser.Phone = user2.Phone;
            existingUser.Age = user2.Age;
            existingUser.Password = user2.Password;
            existingUser.Email = user2.Email;
            existingUser.Country = user2.Country;
            existingUser.Gender = user2.Gender;

            userRepository.Update(existingUser);

        }

        [Then(@"The system registers the account generated as a specialist")]
        public async void ThenTheSystemRegistersTheAccountGeneratedAsASpecialist()
        {
            IEnumerable<Specialist> list = await specialistRepository.ListAsync();
            Assert.That(list.Count() == 1, Is.True);


        }

    }
}
