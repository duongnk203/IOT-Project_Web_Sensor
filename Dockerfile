FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY . .

# ❗ FIX: chỉ định đúng file project
RUN dotnet restore Iot_Project/Iot_Project/Iot_Project.csproj

RUN dotnet publish Iot_Project/Iot_Project/Iot_Project.csproj -c Release -o /app/publish

# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:10000

ENTRYPOINT ["dotnet", "Iot_Project.dll"]