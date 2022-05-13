#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Interview.Api/Interview.Api.csproj", "src/Interview.Api/"]
COPY ["src/Interview.Infrastructure/Interview.Infrastructure.csproj", "src/Interview.Infrastructure/"]
COPY ["src/Interview.Domain/Interview.Domain.csproj", "src/Interview.Domain/"]
RUN dotnet restore "src/Interview.Api/Interview.Api.csproj"
COPY . .
WORKDIR "/src/src/Interview.Api"
RUN dotnet build "Interview.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Interview.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Interview.Api.dll"]
