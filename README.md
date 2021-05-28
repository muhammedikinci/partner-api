# PartnerAPI - Supplier And Stock Management

PartnerAPI is a mini supplier management system for e-commerce platforms. Developed in an enterprise structure. The suppliers can log in the system with their credentials, create new product request, edit their product stocks and review their order. You can easily connect all specified features with the e-commerce platform of your choice

You will need to make various development in the e-commerce platform used to connect the features of PartnerAPI. You can contact [me](https://github.com/muhammedikinci) via e-mail for necessary development.

The API already has a react-frontend. You can access it [here](https://github.com/muhammedikinci/partner-frontend).

## Project Features

### Suppliers can;
- Preview Orders
    - Suppliers can only view details of orders that include their own products.

- Preview System Products

- Update Stock Of System Products

- Change Identity Informations

- Create New Product Request

- Update Denied Product Request

- Preview Their Product Requests

### Admins can;
- Create New Supplier Account

- Delete And Update Supplier Acoount

- Update Supplier Product List from E-Commerce Platform

- Confirm or Decline a Product Request

## Structure

- WebApi
    - Dotnet Core Api Project

- Application
    - Includes Logic (Services)
    - Has own provider
    - Includes Exception and Auth

- Domain
    - Includes Models and ValueObjects

- Persistence
    - Includes the First Touch of the Database

- Repository
    - Includes Generic Repository Pattern
    - Has own provider

- Tests
    - Includes Tests
    - Changes database during testing
    ```
    // src/Tests/DependencyHelperResolver
    .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStartup<Startup>();
            // test param
            webBuilder.UseSetting("TestProp", "true");
        }).Build();

    // src/WebApi/Startup.cs
    var testProp = Configuration.GetValue<string>("TestProp");

    if (testProp == "true")
    {
        services.Configure<MongoDBSettings>(
            Configuration.GetSection("MongoDBTestSettings")
        );
    }
    ```

## Start The API

```sh
git clone https://github.com/muhammedikinci/partner-api
cd partner-api/src
dotnet run --project WebApi
```

## Run The Tests

Requires data in database for tests. You can run first add tests for the data.

```sh
cd partner-api/src/Tests
dotnet test
```

---

Logging: NLog

Database: MongoDB
