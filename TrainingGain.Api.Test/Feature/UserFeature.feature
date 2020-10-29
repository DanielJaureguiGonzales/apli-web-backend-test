Feature: UserFeature


@mytag
Scenario: Add two numbers
	Given I have a repository with users
	When  I place an order with an id that does not exist
	Then  I should give an answer