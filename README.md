# UIAutomation

## The Practice Test covers the following: 
UI and API Tests are automated to cover the following scenarios with Selenium and RestSharp (C#)

## Feature: Everlight UI Automation Tests
The scenario covers UI Tests for Create and View Orders

- Scenario: Create, View and Delete Orders
- Scenario: View existing orders and delete one of them
- Scenario: View existing orders list, teleradiology several statuses, Modality and OrgCode  
- Scenario: Create Order - Field Validations

## Feature: Everlight API Automation Tests
The scenario covers API Tests for Create, View Orders, domain field values and validations 

- Scenario: View Orders, Client and Modality and verify schema responses for all APIs
- Scenario: Create, View and Delete Order 
- Scenario Outline: Create Order validate api errors

## Tools: -Visual Studio -C#.Net -Libraries: RestSharp, Specflow, Allure Reports

## Steps to Execute:
```sh
-Navigate to the Project Folder: 
 cd EverlightTestAutomation 
-Build the Project: dotnet build 
-Execute the Project: dotnet test ./EverlightTestAutomation.csproj
```

## Allure Report is generated and results are stored in the Project\Bin\Debug Folder which can be hosted as a Node.js App

    
