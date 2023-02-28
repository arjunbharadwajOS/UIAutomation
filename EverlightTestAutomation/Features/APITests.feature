Feature: Everlight API Automation Tests
		The scenario covers API Tests for Create, View Orders, domain field values and validations 

@C001 @regression
Scenario: View Orders, Client and Modality and verify schema responses for all APIs
	Given I access the endpoint using URL "https://localhost:44449/api/orders"
	And verify the schema of the api response with input file "orders.json"
	When I access the endpoint using URL "https://localhost:44449/api/clients" 
	And verify the schema of the api response with input file "client.json"
	Then I access the endpoint using URL "https://localhost:44449/api/modalities"
	And verify the schema of the api response with input file "modality.json"

@C002 @regression
Scenario: Create, View and Delete Order 
	Given I access the create order using URL "https://localhost:44449/api/orders"
	When I access the view order using URL "https://localhost:44449/api/orders"
	And verify the schema of the api response with input file "orders.json"
	Then I delete order using URL "https://localhost:44449/api/orders/"

@C002 @regression
Scenario Outline: Create Order validate api errors
	Given I access create order all field validations using URL "https://localhost:44449/api/orders"
	And verify the schema of the api response with input file "400badrequest.json"
	When I access create order with existing "<accessionNumber>" validation using URL "https://localhost:44449/api/orders"
	And verify the schema of the api response with input file "409conflict.json"
	Then I delete order using URL "https://localhost:44449/api/orders/" with invalid id "<invalidId>"
	Examples:
		| accessionNumber | invalidId |
		| 00507           | 100       |
