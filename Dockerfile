FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src


# Copy project file first
COPY HieuLongPortfolio/HieuLongPortfolio.csproj HieuLongPortfolio/


# Restore dependencies
RUN dotnet restore HieuLongPortfolio/HieuLongPortfolio.csproj


# Copy the rest of the ASP.NET project
COPY HieuLongPortfolio/ HieuLongPortfolio/


# Publish
WORKDIR /src/HieuLongPortfolio

RUN dotnet publish HieuLongPortfolio.csproj \
    -c Release \
    -o /app/publish


# =========================================
# RUNTIME
# =========================================

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final

WORKDIR /app


COPY --from=build /app/publish ./


ENTRYPOINT ["dotnet", "HieuLongPortfolio.dll"]