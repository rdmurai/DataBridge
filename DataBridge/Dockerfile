#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DataBridge/DataBridge.csproj", "DataBridge/"]
RUN dotnet restore "DataBridge/DataBridge.csproj"
COPY . .
WORKDIR "/src/DataBridge"
RUN dotnet build "DataBridge.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DataBridge.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DataBridge.dll"]