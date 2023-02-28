@Browser_Firefox
@Browser_Edge
@Browser_Chrome
Feature: Everlight UI Automation Tests
		The scenario covers UI Tests for Create and View Orders

@C001 @regression
Scenario Outline: Create, View and Delete Orders
	Given I am on "https://localhost:44449/" and navigate to orders page
	When I create the order for "<orgCode>", <siteId>, "<modality>"
	And view the created order in the search results for "<orgCode>", <siteId>, "<modality>"
	Then I delete the created order and verify order is deleted
	Examples:
		| orgCode | siteId | modality |
		| LUM     | 102    |   CT     |

@C002 @regression
Scenario: View existing orders and delete one of them
	Given I am on "https://localhost:44449/" and navigate to orders page
	When I verify the orders from order list table to know the order count
	Then delete order, verify to know the order count

@C003 @regression
Scenario: View existing orders list, teleradiology several statuses, Modality and OrgCode  
	Given I am on "https://localhost:44449/" and navigate to orders page
	When I verify the orders from order list table to know the order count 
	Then verify teleradiology several statuses, Modality and OrgCode

@C004 @regression
Scenario Outline: Create Order - Field Validations
	Given I am on "https://localhost:44449/" and navigate to orders page
	When I check for all mandatory fields
	Then I check all other validations like duplicate, incorrect data format for "<orgCode>", <siteId>, "<modality>", <accessionNumber>,"<mrn>"
	Examples:
		| orgCode | siteId | modality | accessionNumber | mrn   |
		| LUM     | 102    | CT       |      00507      |  P338 |