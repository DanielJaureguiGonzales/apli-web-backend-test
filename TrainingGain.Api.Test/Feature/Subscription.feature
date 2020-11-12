Feature: Subscription


@subscription
	
Scenario: user does not have a subscription 
	Given the user has not bought any subscription
	When trying to adquire  a subscription
	Then the system registers the  new subscription

Scenario: user wants to see yours past subscription
    Given the user wants to see all the subscriptions  
    When trying to see the subscription
    Then the system shows a list of the subscription
