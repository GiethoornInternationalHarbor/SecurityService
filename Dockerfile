FROM microsoft/dotnet:2.0-sdk AS build
WORKDIR /app

# Copy the project file
COPY *.sln ./
COPY SecurityService.App/*.csproj ./SecurityService.App/
COPY SecurityService.Core/*.csproj ./SecurityService.Core/
COPY SecurityService.Infrastructure/*.csproj ./SecurityService.Infrastructure/

# Restore the packages
RUN dotnet restore

# Copy everything else
COPY . ./
WORKDIR /app/SecurityService.App

FROM build AS publish
# Build the release
RUN dotnet publish -c Release -o out

# Build the runtime image
FROM microsoft/dotnet:2.0-runtime AS runtime
WORKDIR /app

# Copy the output from the build env
COPY --from=publish /app/SecurityService.App/out ./

ENTRYPOINT [ "dotnet", "SecurityService.App.dll" ]