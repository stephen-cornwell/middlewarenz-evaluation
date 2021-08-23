# middlewarenz-evaluation

## Introduction

This repo contians a solution to the Middleware New Zealand evaluation: https://github.com/MiddlewareNewZealand/evaluation-instructions 

## Solution

The solution is intentionally simple, trying to keep to the 2(ish) hours suggested, so focuses on solving the problem cleanly and concisely, using what is out-of-the-box as much as possible. 

## Tools & Frameworks

- Visual Studio 2019 Community
- .NET 5 Web Api
- XUnit
- Swashbuckle

## Testing

Given the simplicity of the problem, and the time contraints, I've opted for simple integration tests over the CompanyController, which should prove the solution end-to-end. 

Rather than relying on the "production" endpoint I've set up a copy of the Xml Api here: https://github.com/stephen-cornwell/middlewarenz-evaluation/tree/main/TestXmlApi (set in appsettings.json) to ensure the tests don't fail should data change. 

## Running the solution

Build and run the solution using Visual Studio. The WebApi project should start, opening your browser to the hosted Swagger UI:

https://localhost:44394/swagger/index.html



