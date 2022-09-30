# NetwealthCurrencyInterview
This project is a Currency Exchange service with Web UI - built for an Interview Test 

# Brief

## Step 1
Using C# and any Azure resource types you think appropriate, build an API that allows end-users to convert an amount from one currency to another. If you are not familiar with these specific back-end frameworks, please use any other appropriate language and resources. If you do not have any backend experience, you may simply reference this API from your front-end:
https://fixer.io/

## Step 2
Using any appropriate front-end framework expose the API functionality to users. Ideally this should be angular, but Ionic, Blazor, or ASP.Net MVC are also fine.
Users should be able to select a source currency, enter an amount and select a destination currency. The app should then convert the amount to the destination currency and display the converted amount.
The software should be delivered as a link to a GitHub repo we can review.


# Solution

Repo: https://github.com/siegejay/NetwealthCurrencyInterview
Demo Service - Swagger UI: https://democurrencyexchangeservice.azurewebsites.net/swagger/index.html
Demo Site: https://democurrencyexchangesite.azurewebsites.net

## Solution Approach

- Build Service (Step 1) with ASP.NET Core - Web API
- Build Site (Step 2) with ASP.NET Core - MVC (Angular would have been better - but have no experience\study on this yet)
- Focus first on model of Service
    - Include Unit Tests
    - Build so that interfaces are available to switch out Internal Demo Currency and Exchange Rate providers with another data source (e.g. API, Database, etc.. )
        - Building this way for the demo purpose also allows control of edge cases, such as a pair of Currencies being registered\recognised but no current exchange rate available
- Publish Service and Site to Azure early in development (CD pipelines not required as only a demo) 
- Include Swagger UI on service for demo and checking purposes
- Use combination of new (n), to me, frameworks and tools with those I have used in the past, for e.g.
    - ASP.Net Core MVC (n)
    - FakeItEasy
    - MSTest
    - Castle Windsor
    - NSwag (Client Service autogenerator) (n)


## Follow-ups for investigation and experimentation

- Use of Automapper to map Model Entities in service to DTOs, as opposed to using constructor methods in the DTOs
- Integration of MediatR into the solution
- Selenium tests for website 
- Research alternatives\best practices for the NSwag Client generation 




