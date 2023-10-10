# Use the official .NET Core SDK image as the base image
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build

# Set the working directory in the container
WORKDIR /app

# Copy the project files to the container
COPY . .

# Build the application
RUN dotnet publish -c Release -o out

# Use the official .NET Core Runtime image as the base image for the runtime container
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime

# Set the working directory in the runtime container
WORKDIR /app

# Copy the published application from the build container to the runtime container
COPY --from=build /app/out .

# Expose the port your application listens on (replace 80 with your application's port)
EXPOSE 5001

# Start the application
ENTRYPOINT ["dotnet", "OTPApp.dll"]