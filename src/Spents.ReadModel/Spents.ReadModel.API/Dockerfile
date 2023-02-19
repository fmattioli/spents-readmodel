#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Spents.ReadModel.API/Spents.ReadModel.API.csproj", "Spents.ReadModel.API/"]
RUN dotnet restore "Spents.ReadModel.API/Spents.ReadModel.API.csproj"
COPY . .
WORKDIR "/src/Spents.ReadModel.API"
RUN dotnet build "Spents.ReadModel.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Spents.ReadModel.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Spents.ReadModel.API.dll"]