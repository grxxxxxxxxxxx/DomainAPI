
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["BookingProject.csproj", "."]
RUN dotnet restore "BookingProject.csproj"
COPY . .
RUN dotnet build "BookingProject.csproj" -c Release -o /app/build
RUN dotnet publish "BookingProject.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "BookingProject.dll"]