# User
Restful API for creating, updating and retrieving user information. 

Welcome to my project I have used cosmos db with a dotnetcore web api.
I have added the unit tests as asked

**Set up**

1. Please follow this link, download and install the emulator
https://docs.microsoft.com/en-us/azure/cosmos-db/local-emulator#installation
2. Launch the emulator
3. Once launched the explorer should start (if not you can right click the icon in the system tray and click open data explorer...)
4. Click explorer
5. Click the new collection button
6. Fill in form (please note the names are case sensitive)
```
  * Database id: User
  * Collection id: Users
  * Partition key: /id
  * Throughput: 10000
```
7. Press ok

You now have set up the database and collection that the user service will use.
Please follow these next steps to get the project running locally

1. Download the repo
2. Open the project in visual studio
3. Restore the nuget packages
4. Build and run the project using IISExpress

The website will now be launched this will load the swagger ui from where you can make requests to the user api.

**Task 2**

I have not used docker before but I have had a good attempt to get it working.
Wasn’t able to find any good guides to get docker yaml working with cosmos-db.
However, I did successfully create a dockerfile for my project minus cosmos db and a compose file which will build the file.

Please see my Dockerfile
``` dockerfile
FROM microsoft/dotnet:2.2-aspnetcore-runtime-nanoserver-1803 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk-nanoserver-1803 AS build
WORKDIR /src
COPY User.api/User.Api.csproj User.api/
COPY User.Domain/User.Domain.csproj User.Domain/
COPY User.Business/User.Business.csproj User.Business/
COPY User.Repository/User.Repository.csproj User.Repository/
RUN dotnet restore User.api/User.Api.csproj
COPY . .
WORKDIR /src/User.api
RUN dotnet build User.Api.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish User.Api.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "User.Api.dll"]
```
And my docker-compose.yml file:
```
version: '3.4'

services:
  user.api:
    image: ${DOCKER_REGISTRY-}userapi
    build:
      context: .
      dockerfile: User.api\Dockerfile
  azure-cosmosdb-emulator:
    image: microsoft/azure-cosmosdb-emulator
    container_name: cosmosdb-emulator
    restart: on-failure    
```
