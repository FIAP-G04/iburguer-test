Feature: Ordering

A short summary of the feature

@tag1
Scenario: Get current orders
	Given That I have a paid order
	When The the order is started
	Then The order should be at InProgress status
