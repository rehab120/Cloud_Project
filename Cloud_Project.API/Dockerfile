# Use the official .NET SDK image as the base image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory inside the container
WORKDIR /src

# Copy the .csproj files into the appropriate directories
COPY ["Cloud_Project.API/Cloud_Project.API.csproj", "Cloud_Project.API/"]
COPY ["Cloud_Project.Application/Cloud_Project.Application.csproj", "Cloud_Project.Application/"]
COPY ["Cloud_Project.Domain/Cloud_Project.Domain.csproj", "Cloud_Project.Domain/"]
COPY ["Cloud_Project.Infrastructure/Cloud_Project.Infrastructure.csproj", "Cloud_Project.Infrastructure/"]

# Run dotnet restore for the Cloud_Project.API project
RUN dotnet restore "Cloud_Project.API/Cloud_Project.API.csproj"

# Copy all other files and build the app
COPY . .
RUN dotnet publish "Cloud_Project.API/Cloud_Project.API.csproj" -c Release -o /app

# Set the base image for the runtime environment
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

# Fix for: Cannot load library libgssapi_krb5.so.2
RUN apt-get update && apt-get install -y libkrb5-3 libkrb5-dev && apt-get clean
RUN find / -name "libgssapi_krb5.so.2"

# RUN apt-get update && apt-get install -y krb5-user && apt-get clean

# Set the working directory for the final image
WORKDIR /app

# Copy the published files from the build image
COPY --from=build /app .

# Set the entry point for the application
ENTRYPOINT ["dotnet", "Cloud_Project.API.dll"]