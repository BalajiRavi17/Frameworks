#Create a new branch and merge this changes.
Feature: Users

@GetUser
Scenario: Get user
	Given Endpoint for users
	When I set up a get request for "?page=2"
	And the API is executed
	Then Get List user response should be OK and response payload should match

@PostUser
Scenario: Post user
	Given Endpoint for users
	When I set up a Post request
	And the API is executed
	Then Response should be Created and response payload should match

@PutUser
Scenario: Put user
	Given Endpoint for users
	When I set up a Put request for "1"
	And the API is executed
	Then Response should be OK and response payload should match

@DeleteUser
Scenario: Deleteut user
	Given Endpoint for users
	When I set up a Delete request for "2"
	And the API is executed
	Then Response should be No content