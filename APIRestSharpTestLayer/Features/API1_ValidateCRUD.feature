Feature: API1_ValidateCRUD

A short summary of the feature

Scenario Outline: Create Update Delete Post
	Given I create a new post uisng user id <userId> id <iD> title <title> and body <body>
	When I update a created post with user id '100' title 'title 5'
	Then I delete the created post with id '<userId>'

Examples:
	| userId | iD    | title   | body   |
	| 10111  | 10111 | title 1 | body 1 |

Scenario Outline: Create Update Delete Post negative test scenarios
	When I get the requested post with user id '111'
	Then I delete the created post with id '<userId>'

Examples:
	| userId | iD  | title   | body   |
	| 101    | 101 | title 1 | body 1 |