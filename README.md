[![Build Status](https://travis-ci.com/InvestingOak/InvestingOak.svg?branch=master)](https://travis-ci.com/InvestingOak/InvestingOak)
[![Codacy Badge](https://api.codacy.com/project/badge/Grade/0cda63a48acd40c9b4332b4995e6194b)](https://app.codacy.com/gh/InvestingOak/InvestingOak?utm_source=github.com&utm_medium=referral&utm_content=InvestingOak/InvestingOak&utm_campaign=Badge_Grade)

# InvestingOak

InvestingOak provides trade suggestions to users based on questions about their risk tolerance, 
buy/sell ratings, and other factors. InvestingOak will also provide users with real-time stock 
market data. Users will be able to search for information quickly and see relevant news at a 
glance with our simple, intuitive, and easy-to-use interface. The ultimate goal is to make 
finding trade ideas as simple and digestible as possible for beginning investors.

## Running

### Installing Tools

Make sure you have the [.NET 5.0 SDK](https://dotnet.microsoft.com/download/dotnet/5.0) and 
[npm](https://www.npmjs.com/get-npm) installed.

### Running the Client

The Angular client needs to run before the server. In the "ClientApp" directory, 
run `npm start` to start the client development server and install dependencies.

In production, the client is precompiled and served by ASP.NET and this step is 
not necessary.

### Running the Server

Back in the project root, run `dotnet run`. This will install dependencies for the 
server and run.

### Accessing the Site

In development, the site can be accessed on `https://localhost:5001`. If running the client 
only, use `http://localhost:4200`.

If you run the development server and get an error that says something like 
`No connection could be made because the target machine actively refused it.`, you most 
likely need to start the client development server.

In production, ASP.NET will listen on `port 80` and `port 443` by default.
