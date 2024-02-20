Feature: Users using Http client

@GetUser
Scenario: Get user
	Given Endpoint for users_H
	When I set up a get request for "?page=2"_H
	And the API is executed_H
	Then Get List user response should be OK and response payload should match_H

@PostUser
Scenario: Post user
	Given Endpoint for users_H
	When I set up a Post request_H
	And the API is executed_H
	Then Response should be Created and response payload should match_H

@PutUser
Scenario: Put user
	Given Endpoint for users_H
	When I set up a Put request for "2"_H
	And the API is executed_H
	Then Response should be OK and response payload should match_H

@DeleteUser
Scenario: Delete user
	Given Endpoint for users_H
	When I set up a Delete request for "2"_H
	And the API is executed_H
	Then Response should be No content_H