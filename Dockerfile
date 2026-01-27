# Этап 1: Базовый образ для runtime
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER app 
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

# Этап 2: Сборка
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Копируем файл проекта и восстанавливаем зависимости
COPY ["BookingProject/BookingProject.csproj", "BookingProject/"]
RUN dotnet restore "BookingProject/BookingProject.csproj"

# Копируем всё остальное
COPY BookingProject/ BookingProject/
WORKDIR "/src/BookingProject"

# Собираем и публикуем
RUN dotnet build "BookingProject.csproj" -c $BUILD_CONFIGURATION -o /app/build
RUN dotnet publish "BookingProject.csproj" -c $BUILD_CONFIGURATION -o /app/publish

# Этап 3: Финальный образ
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "BookingProject.dll"]
