Feature: OneCrmTestScenarios

Tests for https://demo.1crmcloud.com/ portal

@UI
Scenario Outline: Create contact with unique name
	Given Random number between '10000' and '99999' is generated
	And User logs in using 'admin' user name and 'admin' password
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

#@UI
#Scenario: Run report
#	Given User logs in using 'admin' user name and 'admin' password
#	When User navigates to 'Reports & Settings' main menu item then selects 'Reports' sub menu item
#	And User runs 'Project Profitability' report
#	Then '20' result rows are visible on the page
#
#@UI
#Scenario: Remove evenes from activity log
#	Given User logs in using 'admin' user name and 'admin' password
#	When User navigates to 'Reports & Settings' main menu item then selects 'Activity Log' sub menu item
#	And User selects first 3 rows in the table
#	And User selects 'Delete' option from 'Actions' dropdown
#	Then 3 rows have been deleted
	
