Feature: ShoppingCart

A short summary of the feature

@tag1
Scenario: Create shopping cart
	Given That i have a customer
	And that i'm authenticated at shopping cart service
	When i create a shopping cart
	Then the shopping cart must be created
