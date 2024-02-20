Feature: Place_Orders


@BDDFrameworkTask1
Scenario: Task home
	Given Login https://www.saucedemo.com/
	When I enter valid username as "standard_user"
	And valid password as "secret_sauce"
	And when I clicked Login button
	When Select any item and note the price from the inventroy page and add it to cart
	And Navigate to cart page and verify same price as above noted displayed.
	When Click on checkout and enter the sample details and click continue
	Then Verify the Item and Price on chekout page and click finish.
