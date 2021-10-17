Feature: Verfiy the checkout feature of JPetStore

Background:
	Given Application is launched
	When I click on Enter the Store link
	Then I land on the home page of the application

@UITests @Chrome
Scenario: Checkout a new pet as an existing customer
	Given I click on Sign In 
	And I login to application as existing user
	#When I add pet to cart
	#| Category_Id | Product_Id | Item_Id |
	#| FISH        | FI-SW-01   | EST-1   |
	#| DOGS        | K9-BD-01   | EST-6   |
	#Then I should be able to perform checkout with given cart content

@UITests @Chrome
Scenario: Checkout a new pet as a new customer
	Given I click on Sign In 
    And  I login to application as new user
	#When I add pet to cart
	#| Category_Id | Product_Id | Item_Id |
	#| FISH        | FI-SW-01   | EST-1   |
	#| DOGS        | K9-BD-01   | EST-6   |
	#Then I should be able to perform checkout with given cart content