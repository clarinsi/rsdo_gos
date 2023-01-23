# GOS (GOvorni korpus Slovenščine)
This repository contains the source code of the concordancer for GOS (corpus of spoken Slovene).

There are two main parts:
- Web application written in ASP.NET Core 6
- Console application which is written in .NET 6 and is used to initially set up the system

## Prerequisites
Web application requires following external dependencies
- OpenSearch v2.3.0 (use docker image: `opensearchproject/opensearch:2.3.0`)
- PostgreSQL (use docker image: `postgres`)

## Building source code
To build JavaScript required by Web application, first, [Node.js](https://nodejs.org/en/) must be installed (at least version 16).
Web application is located in the [Web](src/Gos.Web) subfolder.
Use `npm install` to install all required dependencies, and `npm run build` to build the application.

Open [Gos.sln](src/Gos.sln) inside of your IDE and build the solution, or use `dotnet build` command to build the solution. All required dependencies will be installed via NuGet packages.

## Creating docker images
There are two Dockerfile's in the source repository for each part. Both images should be compiled within the context of the project root folder.

### Web application
Use following [Dockerfile](src/Gos.Web/Dockerfile) to build for Web Application.

`docker build -t gos-web -f src/Gos.Web/Dockerfile .`

### Console application
Use following [Dockerfile](src/Gos.Console/Dockerfile) to build System Manager.

`docker build -t gos-console -f src/Gos.Console/Dockerfile .`

## Environment variables
Web and console application must be configured with the following Environment variables and values:
- `GOS:Database:ConnectionString` holds connection string to the database; e.g.: `Host=gos_db;Username=gos;Password=strongpassword;Database=gos`
- `GOS:Elastic:ConnectionString` holds connection string to OpenSearch server; e.g.: `http://gos_opensearch:9200`
- `GOS:SoundFilesFolder` holds location of a folder with an audio files; e.g.: `/data/audio`
- `GOS:Logging:LogPath` holds location for a log file; e.g.: `/data/logs/gos.log`

## Using Console application
To initialy setup the system, you have to use Console application and run following commands:
1. `dotnet Gos.Console.dll import --path=/data/corpus/gos.xml` which will import corpus data into the database
2. `dotnet Gos.Console.dll index` which will index data for searching into the OpenSearch server
3. `dotnet Gos.Console.dll counters --path=/data/corpus/gos.xml` which will create corpus statistics
4. `dotnet Gos.Console.dll lexicon --path=/data/sloleks/sloleks_clarin_2.0.xml` which will import Sloleks (which is used for better searching). XML file is not part of this repository, but it can be downloaded from the following [URL](https://www.clarin.si/repository/xmlui/bitstream/handle/11356/1230/Sloleks2.0.LMF.zip?sequence=3&isAllowed=y)
