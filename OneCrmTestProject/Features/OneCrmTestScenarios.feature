Feature: OneCrmTestScenarios

A few tests for https://demo.1crmcloud.com/ portal

@UI
Scenario Outline: Create contact with unique name
	# This scenario generates random number to use it as part of contact's first name.
	# Without it, when scenario is executed 2 times in a row it fails because given contact has been created already.
	# I'm aware there was nothing about 'random' contact in task description, but test is 100% reliable with it.
	Given Random number between '10000' and '99999' is generated
	And User navigates to OneCrm web app login page
	And User logs into OneCrm web app
	When User navigates to 'Sales & Marketing -> Contacts' menu item
	Then User should see list of all contacts
	When User opens 'New contact' form using 'Create' button 
	And User enters new contact's details with random number in 'First Name' to avoid contact duplicates
	| FirstName   | LastName   | BusinessRole   | Categories              | 
	| <FirstName> | <LastName> | <BusinessRole> | <Category1>,<Category2> |
	And User saves new contact
	Then User should see saved '<FirstName> <LastName>' contact 
	When User opens '<FirstName> <LastName>' contact
	Then Contact details are correct
	| FirstName   | LastName   | BusinessRole   | Categories              | 
	| <FirstName> | <LastName> | <BusinessRole> | <Category1>,<Category2> |

	Examples: 
	| FirstName | LastName | BusinessRole | Category1 | Category2 |
	| John      | Doe      | CEO          | Business  | Customers |

@UI
Scenario: Run 'Project Profitability' report
	Given User navigates to OneCrm web app login page
	And User logs into OneCrm web app
	When User navigates to 'Reports & Settings -> Reports' menu item
	Then User should see list of all reports
	When User opens 'Project Profitability' report
	Then User should see 'Project Profitability' report
	And User should see '0' result rows
	When User runs opened report
	Then User should see '20' result rows

@UI
Scenario: Remove multiple events from activity log
	# This scenario sometimes fails, because there are multiple activity log rows with the same content.
	# So after activity log rows removal, validation fail because duplicated activity row may be visible in the table.
	Given User navigates to OneCrm web app login page
	And User logs into OneCrm web app
	When User navigates to 'Reports & Settings -> Activity Log' menu item
	Then User should see list of all acivity logs
	When User selects first '3' rows in the activity table
	And User selects 'Delete' option from 'Actions' dropdown
	Then Selected activity rows have been deleted