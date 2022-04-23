FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CinemaProject/CinemaProject.csproj", "CinemaProject/"]
RUN dotnet restore "CinemaProject/CinemaProject.csproj"
COPY . .
WORKDIR "/src/CinemaProject"
RUN dotnet build "CinemaProject.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CinemaProject.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CinemaProject.dll"]