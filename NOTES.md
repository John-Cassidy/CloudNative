# Lecture Notes

## Create new xunit test project using bunit project template

Created xunit project in Visual Studio

NOTE: You can also create a new xunit test project that includes bunit

dotnet new bunit --framework xunit -o ProductServiceTests
dotnet sln .\CloudNative.sln add .\microservices\ProductServiceTests\ProductServiceTests.csproj
dotnet add .\ProductServiceTests.csproj reference ..\ProductService\ProductService.csproj

dotnet build ProductService.sln

## Automating build and test

[GitHub Action dotnet](https://docs.github.com/en/actions/automating-builds-and-tests/building-and-testing-net)

Next steps could be:

- Add a step to publish the project
- Add a step to deploy the project

## Customize Docker containers in Visual Studio

[Build/Debugging Containers in VS 2022](https://learn.microsoft.com/en-us/visualstudio/containers/container-build?view=vs-2022)
Learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

Create Dockerfile in csproj folder using Visual Studio Tools

```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["microservices/ProductService/ProductService.csproj", "microservices/ProductService/"]
RUN dotnet restore "./microservices/ProductService/./ProductService.csproj"
COPY . .
WORKDIR "/src/microservices/ProductService"
RUN dotnet build "./ProductService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ProductService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProductService.dll"]
```

Docker commands run by Visual Studio when you select 'Docker' debug

```powershell

docker build -f "C:\DEV\github.com\CloudNative\microservices\ProductService\Dockerfile" --force-rm -t productservice:dev --target base  --build-arg "BUILD_CONFIGURATION=Debug" --label "com.microsoft.created-by=visual-studio" --label "com.microsoft.visual-studio.project-name=ProductService" "C:\DEV\github.com\CloudNative"

docker run -dt -v "C:\Users\jpcas\vsdbg\vs2017u5:/remote_debugger:rw" -v "C:\Users\jpcas\AppData\Roaming\Microsoft\UserSecrets:/root/.microsoft/usersecrets:ro" -v "C:\Users\jpcas\AppData\Roaming\Microsoft\UserSecrets:/home/app/.microsoft/usersecrets:ro" -v "C:\Users\jpcas\AppData\Roaming\ASP.NET\Https:/root/.aspnet/https:ro" -v "C:\Users\jpcas\AppData\Roaming\ASP.NET\Https:/home/app/.aspnet/https:ro" -v "C:\Program Files\Microsoft Visual Studio\2022\Professional\MSBuild\Sdks\Microsoft.Docker.Sdk\tools\TokenService.Proxy\linux-x64\net6.0:/TokenService.Proxy:ro" -v "C:\Program Files\Microsoft Visual Studio\2022\Professional\MSBuild\Sdks\Microsoft.Docker.Sdk\tools\HotReloadProxy\linux-x64\net6.0:/HotReloadProxy:ro" -v "C:\Program Files\Microsoft Visual Studio\2022\Professional\Common7\IDE\CommonExtensions\Microsoft\HotReload:/HotReloadAgent:ro" -v "C:\DEV\github.com\CloudNative\microservices\ProductService:/app" -v "C:\DEV\github.com\CloudNative:/src/" -v "C:\Users\jpcas\.nuget\packages\:/.nuget/fallbackpackages" -e "ASPNETCORE_LOGGING__CONSOLE__DISABLECOLORS=true" -e "ASPNETCORE_ENVIRONMENT=Development" -e "DOTNET_USE_POLLING_FILE_WATCHER=1" -e "NUGET_PACKAGES=/.nuget/fallbackpackages" -e "NUGET_FALLBACK_PACKAGES=/.nuget/fallbackpackages" -P --name ProductService --entrypoint tail productservice:dev -f /dev/null
9545763849b964cb8df6fd933e230e8cdfe17420010ccf9d4b6ff5cdf7780fbe

docker rm -f 9545763849b964cb8df6fd933e230e8cdfe17420010ccf9d4b6ff5cdf7780fbe

```

## Customize Docker containers in VS Code

Create Dockerfile in csproj folder in VS Code

```dockerfile
# Use the official image as a parent image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory
WORKDIR /source

# copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# copy and publish app and libraries
COPY . .
RUN dotnet publish -o /app

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .

# Make sure the app binds to port 80
ENV ASPNETCORE_URLS http://*:80
EXPOSE 80

#set the entry point to the application
ENTRYPOINT ["dotnet", "ProductService.dll"]
```

Docker commands:

Create Docker Image for Product Microservices

Build Docker Image

Run command at same directory level of Dockerfile

```powershell
# Run command at same directory level of Dockerfile
docker build -t productservice .
#See a list of all images available on your machine
docker images
# same command exclude any lines that contain the word "k8s" and 'docker'
docker images --format "{{.Repository}}:{{.Tag}}" | Where-Object {$_ -notmatch 'k8s' -and $_ -notmatch 'docker'}

#Run Docker image: run detached mode (-d)
docker run -d -p 8080:80 --name productservicecontainer productservice
#Another good approach to run docker image:
#This command tells Docker to run a container in -it interactive mode
docker run -it --rm -p 8080:80 --name productservicecontainer productservice


#Check if the container is running by executing:
docker ps

#Open your browser and navigate to
http://localhost:8080/api/products

#Stop container
docker stop productservicecontainer
#Remove container
docker rm productservicecontainer
#Delete image
docker rmi -f productservice:latest

```

## Docker Vulnerability Checks

```powershell

#View a summary of image vulnerabilities and recommendations →
docker scout quickview local://productservice:latest
#View vulnerabilities →
docker scout cves local://productservice:latest
#View base image update recommendations →
docker scout recommendations local://productservice:latest
#Include policy results in your quickview by supplying an organization →
docker scout quickview local://productservice:latest --org <organization>
```

## Docker Learning about building .NET container images

[Learn about building .NET container images:](https://github.com/dotnet/dotnet-docker/blob/main/samples/README.md)

## Push to Docker Hub

> Step 1: Log in to Docker Hub
> Open a terminal and run the following command to log in to Docker Hub:

docker login

Enter your Docker Hub username and password when prompted.

Username: xxxx
Password:
Login Succeeded

> Step 2: Tag your Docker image

Find the IMAGE ID of your local Docker image by running:
docker images

Tag your Docker image with your Docker Hub username and the desired repository name:
docker tag productservice jpcassidy/productservice:latest

See tagged image
docker images

> Step 3: Push the Docker image to Docker Hub
> Push the tagged Docker image to your Docker Hub repository:

docker push jpcassidy/productservice:latest

Check
https://hub.docker.com/
See
https://hub.docker.com/repository/docker/jpcassidy/productservice/general
