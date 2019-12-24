# CONFERENCE MONITOR (ASP.NET CORE)
A back-end REST Web API for publishing and managing conferences.

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

### Make And Apply The Migrations

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

- Use **Postman** or **Insomnia** to test the API endpoint routes

### API Endpoint Routes

| METHOD | ROUTE | DESCRIPTION | ACCESS |
|--------|----------------|-------------|-----------------|
|  POST  | /api/v1/Conferences | Register a Conference | Public |
|  GET  | /api/v1/Conferences | Retrieve All Conferences | Public |
|  GET  | /api/v1/Conferences/{Id} | Retrieve a Specific Conference | Public |
|  PUT  | /api/v1/Conferences/{Id} | Update a Specific Conference | Public |
|  DELETE | /api/v1/Conferences/{Id} | Delete a Specific Conference | Public |