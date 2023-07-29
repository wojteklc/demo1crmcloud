Feature: OneCrmTestScenarios

Tests for https://demo.1crmcloud.com/ portal

@UI
Scenario Outline: Create contact
	Given User logs in using 'admin' user name and 'admin' password
	When User navigates to 'Sales & Marketing' main menu item then selects 'Contacts' sub menu item
	And User creates new contact
	| FirstName   | LastName   | Role   | Category1   | Category2   |
	| <FirstName> | <LastName> | <Role> | <Category1> | <Category2> |
	And User opens contact 
	Then Contact details are correct
	| FirstName   | LastName   | Role   | Category1   | Category2   |
	| <FirstName> | <LastName> | <Role> | <Category1> | <Category2> |

	Examples: 
	| FirstName | LastName | Role  | Category1 | Category2 |
	| John      | Doe      | Admin | Business  | Customers |

@UI
Scenario: Run report
	Given User logs in using 'admin' user name and 'admin' password
	When User navigates to 'Reports & Settings' main menu item then selects 'Reports' sub menu item
	And User runs 'Project Profitability' report
	Then '20' result rows are visible on the page

@UI
Scenario: Remove evenes from activity log
	Given User logs in using 'admin' user name and 'admin' password
	When User navigates to 'Reports & Settings' main menu item then selects 'Activity Log' sub menu item
	And User selects first 3 rows in the table
	And User selects 'Delete' option from 'Actions' dropdown
	Then 3 rows have been deleted
	
