# STAGE01 - Build application and its dependencies
FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /app
COPY *.csproj ./
COPY . ./
RUN dotnet restore

# STAGE02 - Publish the application
FROM build as publish
RUN dotnet publish -c Release -o /app

# STAGE03 - Create the final image
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim
WORKDIR /app
EXPOSE 80
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "PaymentService.dll"]
