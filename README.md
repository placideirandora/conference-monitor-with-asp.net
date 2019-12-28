# CONFERENCE MONITOR (ASP.NET CORE)
A back-end RESTful Web API for publishing and managing conferences.

## GETTING STARTED

### Clone The Project

```
$ git clone https://github.com/placideirandora/conference-monitor-with-asp.net
```

### Install C# Extension

- Open the extensions tab, find and install the **C#** extension by OmniSharp. 

- Extension name: **C# for Visual Studio Code (powered by OmniSharp)**


### Install Required NuGet Packages

```
$ dotnet restore
```

### Make And Apply Migrations

```
$ dotnet ef migrations add Initial 
```
```
$ dotnet ef database update 
```

### Start The App

```
$ dotnet run 
```

### Test The Web API Endpoints

- Local Host: **https://localhost:3000/swagger/index.html**

### API Endpoints

| METHOD | ROUTE | DESCRIPTION | ACCESS |
|--------|----------------|-------------|-----------------|
|  POST  | /api/v1/Auth/SignUp | User Registration | Public |
|  POST  | /api/v1/Auth/SignIn | User Login | Public |
|  POST  | /api/v1/Conference | Publish a Conference | Protected |
|  GET  | /api/v1/Conference | Retrieve All Conferences | Public |
|  GET  | /api/v1/Conference/{Id} | Retrieve a Specific Conference | Public |
|  PUT  | /api/v1/Conference/{Id} | Update a Specific Conference | Protected |
|  DELETE | /api/v1/Conference/{Id} | Delete a Specific Conference | Protected |

### Run Unit Tests

```
$ dotnet test
```