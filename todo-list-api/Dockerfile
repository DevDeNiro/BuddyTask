# Stage de build
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src


COPY *.csproj .
RUN dotnet restore

COPY . .
RUN dotnet publish -c Release -o /app/publish

# Stage de runtime
FROM mcr.microsoft.com/dotnet/aspnet:6.0

WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_ENVIRONMENT=Development

EXPOSE 7174

ENTRYPOINT ["dotnet", "todo-list-api.dll"]
