Feature: Payments

A short summary of the feature

@tag1
Scenario: Confirm Payment
	Given That I have an order waiting for payment
	And That I generated a QR Code
	When The payment is confirmed
	Then The order should be at Confirmed status
