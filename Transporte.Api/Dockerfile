FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["Transporte.Api/Transporte.Api.csproj", "Transporte.Api/"]
RUN dotnet restore "Transporte.Api/Transporte.Api.csproj"
COPY . .
WORKDIR "/src/Transporte.Api"
RUN dotnet build "Transporte.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Transporte.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "Transporte.Api.dll"]

CMD ASPNETCORE_URLS=http://*:$PORT dotnet Transporte.Api.dll
