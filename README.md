# Globo Service Template for .NET

The Globo Service Templates for .NET package allows you to quickly bootstrap a .NET application with the recommended
structure and services. You can use the Globo Service Templates with the .NET CLI and Visual Studio.

## Getting started

* .NET Core 8

### Installation

You can install the Globo Service Templates from [Artifactory]("https://artifactory.globoi.com/ui/")

```bash
 dotnet nuget add source "https://artifactory.globoi.com/artifactory/api/nuget/v3/nuget-local" --name Globo
```

Then running the following command in your terminal window:

```bash
dotnet new install Globo.Template --add-source Globo
```

Once installed, the templates are available in .NET CLI, Visual Studio for Windows, Visual Studio for Mac, and JetBrains
Rider.

```bash
dotnet new globo-service --ServiceName MyServiceName -n MySolutionName
```
