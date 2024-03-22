FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "BackendService/BackendService.csproj"
WORKDIR "/src/BackendService"
RUN dotnet build "BackendService.csproj" -c Release -o /app/build

FROM build AS publish
WORKDIR "/src/BackendService"
RUN dotnet publish "BackendService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BackendService.dll"]