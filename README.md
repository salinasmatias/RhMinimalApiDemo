## RhMinimalApiDemo

RhMinimalApiDemo is a Minimal API project that serves as an API for human resources information. It provides endpoints to retrieve information about countries and jobs, as well as the ability to create new job entries. This project is built with .NET 6 and leverages various technologies and patterns for efficient development and data management.

## Features

    Countries API: Retrieve information about countries.
    Jobs API: Get details about jobs and create new job entries.
    Entity Framework Core: Utilizes Entity Framework Core for database connectivity.
    Fluent Validation: Implements Fluent Validation for data validation and integrity.
    AutoMapper: Utilizes AutoMapper for object-to-object mapping.
    Repository Pattern: Implements a generic repository pattern with lambda expressions for flexible data access.
    Eager Loading: Allows for the inclusion of related data with eager loading.

## Getting Started (Database Creation and Population)

To get started with RhMinimalApiDemo using the provided SQL scripts, follow these steps:

1. **Clone the repository**:

   ```bash
   git clone https://github.com/salinasmatias/RhMinimalApiDemo.git
   cd RhMinimalApiDemo
   ```

2. **Create the Database**:
Use the SQL scripts to create the database and populate it with data. You will need to run these SQL scripts in your SQL Server environment.

    Create the tables: https://www.sqltutorial.org/wp-content/uploads/2020/04/sqlserver.txt
    Load data into the tables: https://www.sqltutorial.org/wp-content/uploads/2020/04/sqlserver-data.txt

You can execute these scripts using SQL Server Management Studio (SSMS) or a similar tool. Ensure that the database and tables are created and data is loaded successfully.

3. **Configure the Database Connection**:
Update the database connection string in the appsettings.json file to point to the newly created database.

```json
  {
    "Logging": {
      "LogLevel": {
        "Default": "Information",
        "Microsoft.AspNetCore": "Warning"
      }
    },
    "ConnectionString": "Server=localhost; Database=HrDb; Trusted_Connection=True; Encrypt=False;",
    "AllowedHosts": "*"
  }
```
4. **Generate Models**:
If you don't already have Entity Framework models generated from your database, you may need to use tools like the Entity Framework Core CLI to generate models based on your database schema. You can use the dotnet ef dbcontext scaffold command to scaffold models based on your existing database.

Example:

```bash
dotnet ef dbcontext scaffold "Server=localhost; Database=HrDb; Trusted_Connection=True; Encrypt=False;" Microsoft.EntityFrameworkCore.SqlServer -o Models
```
5. **Build and Run**:
By following these steps, you'll set up the project with the provided SQL scripts and an example database for RhMinimalApiDemo. Make sure to adjust the connection string and paths to the SQL scripts as needed for your specific environment.

## API Endpoints

    GET /api/countries: Retrieve a list of countries.
    GET /api/jobs: Retrieve a list of jobs.
    POST /api/jobs: Create a new job entry.

## License

This project is licensed under the MIT License. See the [LICENSE](https://opensource.org/license/mit/) file for details.
