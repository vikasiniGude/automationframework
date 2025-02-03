Feature: TC1_UserLoginSuccessful

Validate the user is logged in successfully

@ui
Scenario: Validate the login functionality is successfull with right credentials
	Given I am in the orange HRM login page
	When I input the username, password and click on login
	Then I should see the dashboard page
