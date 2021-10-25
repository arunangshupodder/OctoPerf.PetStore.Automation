Feature: Verfiy the user management feature of JPetStore

Background:
	Given Application is launched
	When I click on Enter the Store link
	Then I land on the home page of the application

@UITests @Chrome @SmokeTest @RegressionTest
Scenario: Register a new user in the application
	Given I click on Sign In 
    When  I register a new user
	Then I should be navigated to Home Page
	And I should be able to login to application