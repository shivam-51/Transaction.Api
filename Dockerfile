FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Transaction.Api.csproj", "."]
RUN dotnet restore "Transaction.Api.csproj"
COPY . .

WORKDIR "/src/."
RUN dotnet build "Transaction.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Transaction.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Transaction.Api.dll"]