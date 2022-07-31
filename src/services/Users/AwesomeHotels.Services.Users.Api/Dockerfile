FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/services/Users/AwesomeHotels.Services.Users.Api/AwesomeHotels.Services.Users.Api.csproj", "src/services/Users/AwesomeHotels.Services.Users.Api/"]
RUN dotnet restore "src/services/Users/AwesomeHotels.Services.Users.Api/AwesomeHotels.Services.Users.Api.csproj"
COPY . .
WORKDIR "/src/src/services/Users/AwesomeHotels.Services.Users.Api"
RUN dotnet build "AwesomeHotels.Services.Users.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AwesomeHotels.Services.Users.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AwesomeHotels.Services.Users.Api.dll"]