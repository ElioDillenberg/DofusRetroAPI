FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["DofusRetroAPI/DofusRetroAPI.csproj", "DofusRetroAPI/"]
RUN dotnet restore "DofusRetroAPI/DofusRetroAPI.csproj"
COPY . .
WORKDIR "/src/DofusRetroAPI"
RUN dotnet build "DofusRetroAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DofusRetroAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DofusRetroAPI.dll"]
