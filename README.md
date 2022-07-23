
# HeyBaldur Companybase API

Muchos APIs son costosos y en el equipo de HeyBaldur hemos decidido crear un API de libre uso para desarrolladores y usuarios que deseen hacer uso de. La forma de hacer uso del API es totalmente librede costo.



## Tech Stack

**ASP.NET CORE 5.0:** The ASP.NET Core Runtime enables you to run existing web/server applications. On Windows, we recommend installing the Hosting Bundle, which includes the .NET Runtime and IIS support. https://dotnet.microsoft.com/en-us/download/dotnet/5.0

**MongoDB C#/.NET Driver:** Welcome to the documentation site for the official MongoDB C#/.NET driver. You can add the driver to your application to work with MongoDB in C#/.NET. Download it using 
NuGet
 or set up a runnable project by following our Getting Started guide. https://www.mongodb.com/docs/drivers/csharp/

 


## Authors

- [@HeyBaldur](https://twitter.com/HeyBaldur)


## FAQ

#### It's free?

Yes, totally free, keep in mind that the API does not contain any information, the information that you are going to manipulate is totally private, we are working on a totally free version with real information, but we need help from other developers who want to participate in the project by contributing your knowledge.

#### I am a user, I can use the API

If the user has knowledge in software development with the necessary requirements to be able to run the project, yes, otherwise it is necessary to have a certain level of technical knowledge to get the most out of the system.


## Installation

First of all download the Master version of the repository, after that make sure you have Mongo Atlas and Mongo proper installed on your computer, then make sure you have ASP.NET 5.0 version installed on your computer as well

```bash
  git clone https://github.com/HeyBaldur/Company.API.git
```

After having the code in its respective folder, open the following address

```bash
  <root>\Company.API\Company.API
```

Then you can run the application with the following command

```bash
  dotnet watch run
```

It is also important that you change the credentials in the connections file

```bash
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "AppSettings": {
    "Token": "YOUR_TOKEN"
  },
  "ServiceDatabase": {
    "ConnectionString": "MONGO_CONNECTION_URL",
    "DatabaseName": "CompanyDB"
  },
  "AllowedHosts": "*"
}

```

    
## Acknowledgements
If there is any question about it, please feel free to write me or contact me at 😉
 - [@HeyBaldur](https://twitter.com/HeyBaldur)

