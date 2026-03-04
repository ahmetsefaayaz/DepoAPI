# Çalışma zamanı (Runtime) imajı
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

# Derleme (Build) imajı
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Clean Architecture olduğu için tüm katmanları kopyalıyoruz
COPY . .

# ASIL ÇALIŞAN PROJENİN İÇİNE GİRİYORUZ
WORKDIR "/src/DepoAPI.Presentation"
RUN dotnet restore "./DepoAPI.Presentation.csproj"
RUN dotnet build "./DepoAPI.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Yayınla (Publish)
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./DepoAPI.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Son aşama: Gerçek çalışan DLL'i ayağa kaldır
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DepoAPI.Presentation.dll"]