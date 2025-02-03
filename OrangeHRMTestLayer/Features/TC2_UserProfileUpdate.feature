Feature: TC2_UserProfileUpdate

Validate the user profile update is successfull

@ui
Scenario Outline: Validate the profile update functionality is successfull
	Given I am in the orange HRM login page
	When I input the username, password and click on login
	And I select tab <tabName> from left pane
	And update Employee name <employeeName> and click save
	Then I validate employee name <employeeName> update is successful
Examples:
	| tabName | employeeName |
	| My Info | Test1        |
