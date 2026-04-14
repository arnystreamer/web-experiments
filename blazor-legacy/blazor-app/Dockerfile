FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
ENV ASPNETCORE_HTTP_PORTS=8088
WORKDIR /app
EXPOSE 8088

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["BlazorAppTest.csproj", "./"]
RUN dotnet restore "BlazorAppTest.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "./BlazorAppTest.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./BlazorAppTest.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BlazorAppTest.dll"]
