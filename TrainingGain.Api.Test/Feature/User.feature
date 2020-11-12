Feature: User
	



@User1
Scenario: User registration as a customer
	Given the user wants to register as a customer
	When the user registers as a customer
	Then The system registers the account generated as a customer

@User2
	Scenario: User registration as a specialist
	Given the user wants to register as specialist
	When the user registers as specialist
	Then The system registers the account generated as a specialist 