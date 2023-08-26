
### Daniel Aguilar - Technical Test

## Description

This project is a simple web application that allows you to create, edit, delete and list applicants and their interview process steps. The project was developed using the following technologies:

* ASP.NET 7
* Angular 14
* EF Core 7
* MediatR
* AutoMapper
* FluentValidation
* NUnit

This project is fully adhered the following principles/patterns/practices:

* Clean Architecture
* TDD
* CQRS
* DDD

## How to run the project

* Install .NET 7 SDK and Node LTS
* Clone the repository
* Go to the folder `src\WebUI`
* Run `dotnet run`

Note: By default, the project will run and store data under an in-memory database. If you want to use a SQL Server database, you must change the connection string in the `appsettings.json` file.

# User Histories

## 01 - Applicant List

As a user, I want to add and see a list of applicants, so that I manage all the applicants that have been added to the system.

### Business Rules

* The user can see a list of applicants
* The user can add a new applicant
* The user can edit an applicant
* The user can delete an applicant
* The user can see the interview process steps of an applicant

### Acceptance Criteria

* The Applicant's list must be displayed in rows
* The user must be able to see the remaining interview steps for each candidate next to their name.
* The user must be able to add a new process to the list by entering the name of the process in the "New Step" field and then clicking on the "Add" button.


## 02 - Interview Step List

As a user, I want to add and see a list of applicant's interview steps, so that I can follow the interview process of a particular applicant.

### Business Rules

* The user can see a list of interview steps
* The user can add a new interview step
* The user can edit an interview step
* The user can delete an interview step
* The list of interview steps must be displayed in a clear and concise manner.
* The user must be able to select steps from the list by clicking on the checkbox next to the step.
* The user must be able to add steps to the list by entering the name of the step in the "New step" field and then clicking on the "Add" button.

### Acceptance Criteria

* The list of interview steps should be displayed in rows with the following elements:
 1. Step name
 2. Status
* The user must be able to select a step from the list by clicking on the checkbox next to the step.
* The user must be able to add a new step to the list by entering the name of the step in the "New step" field and then clicking on the "Add" button.
* The user should be able to remove a step from the list by clicking the option box next to the step and the clicking the “Delete” button.


## 03 - Create and login new users

As a user, I want to create and login new users, so that I can manage the access and interact as an authenticated user.

### Business Rules

* The new user must enter their valid email address and password when creating their account
* The new user must confirm their password when creating their account
* The user must be able to successfully create a new account
* The user must be able to successfully login to their account
* The user’s data must be stored in a database

### Acceptance Criteria

* The user must be able to create a new account by entering their email address and password in the "Register Form" form and then clicking on the "Register" button.
* The user must be able to login to their account by entering their email address and password in the "Login Form" form and then clicking on the "Login" button.
* The user must be able to logout of their account by clicking on the "Logout" button.
* The user must be able to see their email address in the top right corner of the screen when they are logged in.
