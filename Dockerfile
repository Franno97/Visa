#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
RUN apt-get update && apt-get install -y apt-utils libgdiplus libc6-dev
#RUN apt-get update && apt-get install -y libgdiplus

WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build



WORKDIR /src

COPY ["src/Mre.Visas.Visa.Api/Mre.Visas.Visa.Api.csproj", "./Mre.Visas.Visa.Api/"]
COPY ["src/Mre.Visas.Visa.Application/Mre.Visas.Visa.Application.csproj", "./Mre.Visas.Visa.Application/"]
COPY ["src/Mre.Visas.Visa.Domain/Mre.Visas.Visa.Domain.csproj", "./Mre.Visas.Visa.Domain/"]
COPY ["src/Mre.Visas.Visa.Infrastructure/Mre.Visas.Visa.Infrastructure.csproj", "./Mre.Visas.Visa.Infrastructure/"]
RUN dotnet restore "Mre.Visas.Visa.Api/Mre.Visas.Visa.Api.csproj"

COPY ["src/Mre.Visas.Visa.Api", "./Mre.Visas.Visa.Api/"]
COPY ["src/Mre.Visas.Visa.Application", "./Mre.Visas.Visa.Application/"]
COPY ["src/Mre.Visas.Visa.Domain", "./Mre.Visas.Visa.Domain/"]
COPY ["src/Mre.Visas.Visa.Infrastructure", "./Mre.Visas.Visa.Infrastructure/"]
RUN dotnet build "Mre.Visas.Visa.Api/Mre.Visas.Visa.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Mre.Visas.Visa.Api/Mre.Visas.Visa.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Mre.Visas.Visa.Api.dll"]