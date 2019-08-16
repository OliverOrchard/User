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
``` yml
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

**Task 3**
Answer to questions in task 3:

**Be deployed to a live environment**

This application could be deployed using Teamcity and Octopus deploy.
Teamcity could reference the github repo and be used to build changes and run unit tests.
Then another build could be set up on Teamcity to take the successful builds and create a release in octopus deploy that would be automatically deployed to azure vm.
Transformations could be used in Octopus deploy that would create the correct references to the hosted cosmo db.
The cosmos db would just need hosting on azure and have a collection for users.﻿

**Handle a large volume of requests, including concurrent creation and update operations**

I have built the api to asynchronous this will already help with performance. We could also look at the partition keys in cosmos db. Logical partitions have an upper size limit of 10GB, Request units per second (RU/s) are shared across partitions. Multiple requests to the same partition cannot exceed the allocated throughput for the partition.
If the performance hit of cross partition queries is troublesome, then it is possible to mitigate this with the use of lookup collections. These are collections that duplicate data in the main collection to facilitate querying by a different partition key.

**Continue operating in the event of problems reading and writing from the database**

We could use a library such as https://github.com/App-vNext/Polly 
To create policies in the webapi so that when we make requests to cosmos db we have various around retries. If the service is down we should consider returning informative response types and potentially messages to the consumer of the api .This could be used by the consumer of the application to implement the appropriate ux.

**Ensure the security of the user information.**

We should ensure we do not have any Insecure Direct Object References exposed via the api. We should also ensure that user passwords are appropriately encrypted. Using the latest hashing functions. We should make sure we use a hashing function such as PBKDF2 . As we want CPU-intensive hash function to help prevent brute forcing of passwords. We should also use an iteration count which should be stored in the database with the salt and hash. We should also use tokens when logging in which can be authorised in the api. We could use the token to determine the permissions in the api. For further security we could consider penetration testing the api.

**References**

1. Microsoft Docs. 2019. Choosing a partition key. https://docs.microsoft.com/en-us/azure/cosmos-db/partitioning-overview#choose-partitionkey 
2. Microsoft Azure Blog. 2018. Azure Cosmos DB partitioning design patterns – Part 1. https://azure.microsoft.com/en-us/blog/azure-cosmos-db-partitioning-design-patterns-part-1/ 
3. Microsoft Docs. 2018. Change feed in Azure Cosmos DB - overview. https://docs.microsoft.com/en-us/azure/cosmos-db/change-feed
