# Lecture Notes

## Create new xunit test project

Created xunit project in Visual Studio

NOTE: You can also create a new xunit test project that includes bunit

dotnet new bunit --framework xunit -o ProductServiceTests
dotnet sln .\CloudNative.sln add .\microservices\ProductServiceTests\ProductServiceTests.csproj
dotnet add .\ProductServiceTests.csproj reference ..\ProductService\ProductService.csproj

dotnet build ProductService.sln


