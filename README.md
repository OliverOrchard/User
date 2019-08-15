# User
Restful API for creating, updating and retrieving user information. 

Welcome to my project I have used cosmos db with a dotnetcore web api.
I have added the unit tests as asked

I hadn't before but have had a good attempt to try to get it working.
Wasn't able to find any good guides to get docker yaml working with cosmos-db.

However I Successfully created dockerfile for my project minus cosmos db aswell as a compose file which will build the file.
I have not included these in the repo however as it breaks the connection to cosmosdb.

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
