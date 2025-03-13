FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5096

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["AppGestaoDeResiduos/AppGestaoDeResiduos.csproj", "AppGestaoDeResiduos/"]
RUN dotnet restore "./AppGestaoDeResiduos/AppGestaoDeResiduos.csproj"
COPY . .
WORKDIR "/src/AppGestaoDeResiduos"
RUN dotnet build "./AppGestaoDeResiduos.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./AppGestaoDeResiduos.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AppGestaoDeResiduos.dll"]
