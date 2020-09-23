# InvestingOak

## Running

#### Installing Tools

Make sure you have the [.NET 5.0 SDK](https://dotnet.microsoft.com/download/dotnet/5.0), 
[npm](https://www.npmjs.com/get-npm), and [SQL Server Express](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) installed.

#### Setting Up the Database
Create copies of `appsettings.sample.json` and `appsettings.Development.sample.json`, 
and name them `appsettings.json` and `appsettings.Development.json`, respectively.

Ensure you have a database instance running and paste the connection string in 
`appsettings.json` and `appsettings.Development.json` (OptionSheetConnectionString value).

A quick way to do this is to run `sqllocaldb create OptionSheet` and use the 
connection strings provided in the `appsettings.json` files.

#### Running the Client

The Angular client needs to run before the server. In the "ClientApp" directory, 
run `npm install` to install dependencies for the client.

Run `npm start` to start the client development server.

#### Running the Server

Back in the project root, run `dotnet run`. This will install dependencies for the 
server and run.

By default, the site can be accessed on `https://localhost:5001`.
