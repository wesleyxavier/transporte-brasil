FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["src.app.webapi/src.app.webapi.csproj", "src.app.webapi/"]
RUN dotnet restore "src.app.webapi/src.app.webapi.csproj"
COPY . .
WORKDIR "/src/src.app.webapi"
RUN dotnet build "src.app.webapi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "src.app.webapi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "src.app.webapi.dll"]

CMD ASPNETCORE_URLS=http://*:$PORT dotnet src.app.webapi.dll
