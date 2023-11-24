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