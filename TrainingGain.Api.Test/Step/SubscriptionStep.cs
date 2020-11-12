using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

using TechTalk.SpecFlow;
using TrainingGain.Api.Domain.Models;
using TrainingGain.Api.Persistance.Repositories;
using TrainingGain.Api.Services;

namespace TrainingGain.Api.Test.Step


{
    [Binding]
    public class SubscriptionStep
    {



        public UserRepository userRepository;
        public CustomerRepository customerRepository;
        public CustomerService CustomerService;
        public SubscriptionPlanRepository subscriptionPlanRepository;
        public SubscriptionRepository subscriptionRepository;
        public SubscriptionService subscriptionService;
        public SubscriptionPlanService subscriptionPlanService;
        public User user = new User
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
        public Customer customer = new Customer { Id = 1, Description = "none", UserId = 1 };
        public SubscriptionPlan subscriptionPlan = new SubscriptionPlan { Id = 1, Description = "Amateur", Cost = 35 };
        public Subscription subscription = new Subscription
        {
            CustomerId = 1,
            SubscriptionPlanId = 1,
            ExpiryDate = new DateTime(2021 - 01 - 02),
            StartDate = new DateTime(2020 - 12 - 02)
        };
        public bool have = false;


        public async void Create()
        {
            await userRepository.AddAsync(user);
            await customerRepository.AddAsync(customer);
            await subscriptionPlanRepository.AddAsync(subscriptionPlan);
            await subscriptionRepository.AssingSubscription(customer.Id, subscriptionPlan.Id);


        }

        [Given(@"the user has not bought any subscription")]
        public async void GivenTheUserHasNotBoughtAnySubscription()
        {

            IEnumerable<Subscription> list = await subscriptionRepository.ListBySubscriptionPlanIdAsync(user.Id);

            if (!list.Any())
                have = false;
            else
                have = true;
        }



        [When(@"trying to adquire  a subscription")]
        public async void WhenTryingToAdquireaSubscription()
        {

            await subscriptionService.AssignSubscriptionAsync(customer.Id, subscriptionPlan.Id);

        }

        [Then(@"the system registers the  new subscription")]
        public async void ThenTheSystemRegistersTheNewSubscription()
        {
            IEnumerable<Subscription> list = await subscriptionRepository.ListByCustomerIdAsync(customer.Id);
            Assert.That(list.Count() == 1, Is.True);

        }


        [Given(@"the user wants to see all the subscriptions")]
        public async void GivenTheUserWantsToSee()
        {
            Create();

        }

        [When(@"trying to see the subscription")]

        public async void WhenTryingToSeeTheSubscription()
        {
            IEnumerable<Subscription> list = await subscriptionRepository.ListBySubscriptionPlanIdAsync(customer.Id);
        }

        [Then(@"the system shows a list of the subscription")]
        public async void ThenTheSystemShowsAListOfTheSubscription()
        {
            IEnumerable<Subscription> list = await subscriptionRepository.ListByCustomerIdAsync(customer.Id);
            Assert.That(list == subscriptionRepository.ListByCustomerIdAsync(customer.Id));

        }
    }
    }
