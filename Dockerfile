#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["BookingOffline.Web/BookingOffline.Web.csproj", "BookingOffline.Web/"]
COPY ["BookingOffline.Repositories/BookingOffline.Repositories.csproj", "BookingOffline.Repositories/"]
COPY ["BookingOffline.Entities/BookingOffline.Entities.csproj", "BookingOffline.Entities/"]
COPY ["BookingOffline.Repositories.SqlServer/BookingOffline.Repositories.SqlServer.csproj", "BookingOffline.Repositories.SqlServer/"]
COPY ["BookingOffline.Services/BookingOffline.Services.csproj", "BookingOffline.Services/"]
COPY ["BookingOffline.Common/BookingOffline.Common.csproj", "BookingOffline.Common/"]
RUN dotnet restore "BookingOffline.Web/BookingOffline.Web.csproj"
COPY . .
WORKDIR "/src/BookingOffline.Web"
RUN dotnet build "BookingOffline.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BookingOffline.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BookingOffline.Web.dll"]