FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER app 
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["BookingProject/BookingProject.csproj", "BookingProject/"]
RUN dotnet restore "BookingProject/BookingProject.csproj"

COPY BookingProject/ BookingProject/
WORKDIR "/src/BookingProject"

RUN dotnet build "BookingProject.csproj" -c $BUILD_CONFIGURATION -o /app/build
RUN dotnet publish "BookingProject.csproj" -c $BUILD_CONFIGURATION -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "BookingProject.dll"]
