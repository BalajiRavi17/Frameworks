@login
Feature: Login

@ValidUsernamePassword
Scenario: Valid Login
	Given User is in Swag Labs login page
	When I enter valid username as "standard_user"
	And valid password as "secret_sauce"
	And when I clicked Login button
	Then user should be navigated to home screen

@InValidUsernamePassword
Scenario Outline: InValid Login
	Given User is in Swag Labs login page
	When I enter valid username as <username>
	And valid password as <password>
	And when I clicked Login button
	Then user should be not navigated to home screen

	Examples: 
	| username       | password			 |
	| "standard_user"| "Invalid Password"|
	| "Invalid user" | "secret_sauce"    |
	| "Invalid user" | "Invalid Password"|