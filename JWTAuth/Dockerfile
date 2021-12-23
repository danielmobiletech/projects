 FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
 WORKDIR /app
 EXPOSE 80
 FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
 WORKDIR /src
 COPY ["JWTAuth.csproj", ""]
 RUN dotnet restore "./JWTAuth.csproj"
 COPY . .
 WORKDIR "/src/."
 RUN dotnet build "JWTAuth.csproj" -c Release -o /app/build
 FROM build AS publish
 RUN dotnet publish "JWTAuth.csproj" -c Release -o /app/publish
 FROM base AS final
 EXPOSE 3306
 WORKDIR /app
 COPY --from=publish /app/publish .
 ENTRYPOINT ["dotnet", "JWTAuth.dll"]