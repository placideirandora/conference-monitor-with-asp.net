# CONFERENCE MONITOR (ASP.NET CORE)
A back-end RESTful Web API for publishing and managing conferences.

## GETTING STARTED

### Clone The Project

```
$ git clone https://github.com/placideirandora/conference-monitor-with-asp.net
```

### Install C# Extension

```
$ Open the extensions tab, find and install the C# extension by OmniSharp. 
```
```
$ Extension name: C# for Visual Studio Code (powered by OmniSharp).
```

### Install Required NuGet Packages

```
$ Run the following command with VSCode Terminal: dotnet restore
```

### Make And Apply Migrations

```
$ dotnet ef migrations add Initial (make migrations)
```
```
$ dotnet ef database update (apply migrations)
```

### Start The App

```
$ dotnet watch run (start the app in watch mode)
```

### Testing The Web API Endpoints

- Local Host: navigate to *https://localhost:3000/swagger/index.html* and test the endpoints.

### API Endpoint Routes

| METHOD | ROUTE | DESCRIPTION | ACCESS |
|--------|----------------|-------------|-----------------|
|  POST  | /api/v1/Auth/SignUp | User Registration | Public |
|  POST  | /api/v1/Auth/SignIn | User Login | Public |
|  POST  | /api/v1/Conference | Publish a Conference | Protected |
|  GET  | /api/v1/Conference | Retrieve All Conferences | Public |
|  GET  | /api/v1/Conference/{Id} | Retrieve a Specific Conference | Public |
|  PUT  | /api/v1/Conference/{Id} | Update a Specific Conference | Protected |
|  DELETE | /api/v1/Conference/{Id} | Delete a Specific Conference | Protected |