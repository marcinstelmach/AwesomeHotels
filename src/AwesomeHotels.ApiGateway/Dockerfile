FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/AwesomeHotels.ApiGateway/AwesomeHotels.ApiGateway.csproj", "src/AwesomeHotels.ApiGateway/"]
RUN dotnet restore "src/AwesomeHotels.ApiGateway/AwesomeHotels.ApiGateway.csproj"
COPY . .
WORKDIR "/src/src/AwesomeHotels.ApiGateway"
RUN dotnet build "AwesomeHotels.ApiGateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AwesomeHotels.ApiGateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AwesomeHotels.ApiGateway.dll"]