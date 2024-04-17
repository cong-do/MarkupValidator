# Description
The Markup Validator is a markup validation console application based on .NET 8.0 that currently supports validation of XML without the use of regex or the System.XML library. 

To simplify the validation, additional components such as attributes won't need to be processed. The validation will therefore treat the opening and closing tags identically. E.g. `<tutorial date="01/01/2000">XML</tutorial>` will be invalid, because the closing tag differs from the opening tag `tutorial date="01/01/2000"`.

## How to run
### Debugging mode
1. Open the MarkupValidator.sln file in Visual Studio
2. Build the solution
3. Set the MarkupValidator.ConsoleApp project as startup
4. Debug the project
5. Verify the result in the console window
6. Modify the prefilled dummy input to any other desired input (See `UnitTests` project for other cases with valid vs. invalid inputs)

### Executable mode
1. Open the MarkupValidator.sln file in Visual Studio
2. Build the solution
3. Right-click on the ConsoleApp project and select Publish...
4. Prepare the publishing profile and have the solution compiled into an executable locally
5. Run the console app with an XML string as argument in a command terminal
6. Verify the result in the console window

## Technical notes
The solution can be broken into three projects: 
### 1. MarkdownValidator.ConsoleApp
The console app project manages the "UI" and is the starting point for running the application. 

- `Program.cs` wires up the logic and sets up the dependency injection and ultimately calls the `AppService` to run the actual app. Currently, only XML validation is supported and thus the `XmlValidator` is injected.
- `AppService` loads the preconfigured injected validator and executes its validation. As the intention of the console app is to invoke it with the XML string as an argument, validation is in place to throw exceptions if the app is not started with an argument or is started with multiple arguments.

### 2. MarkdownValidator.Core
The core project consists of the business logic of the solution. In its current state, only the `XmlValidator` is created, but other markup validators such as HTML or others can hypothetically be added.

- `XmlValidator` contains the actual validation logic for the given string to verify that it is well nested with properly defined tags. 

### 3. MarkdownValidator.UnitTests
Partially because of time, but also because the interesting bit mainly revolves around the validation logic, the current UnitTests project mainly focuses on testing the `XmlValidator`.

- `XmlValidatorTests` covers tests around the validation method and has the tests specifically broken down into expected valid and invalid cases. To run the tests, go to Tests menu in the top and select to run all tests

==Note:== as mentioned in the description, as we do not take additional components such as attributes into account, the validation for the tags are currently written to just identically match on the full opening and closing tags. Therefore `<People age='1'>hello world</People age='1'>` is listed as a valid test case.