FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /src
COPY ["src/EmployeeProject1.Web.Host/EmployeeProject1.Web.Host.csproj", "src/EmployeeProject1.Web.Host/"]
COPY ["src/EmployeeProject1.Web.Core/EmployeeProject1.Web.Core.csproj", "src/EmployeeProject1.Web.Core/"]
COPY ["src/EmployeeProject1.Application/EmployeeProject1.Application.csproj", "src/EmployeeProject1.Application/"]
COPY ["src/EmployeeProject1.Core/EmployeeProject1.Core.csproj", "src/EmployeeProject1.Core/"]
COPY ["src/EmployeeProject1.EntityFrameworkCore/EmployeeProject1.EntityFrameworkCore.csproj", "src/EmployeeProject1.EntityFrameworkCore/"]
WORKDIR "/src/src/EmployeeProject1.Web.Host"
RUN dotnet restore 

WORKDIR /src
COPY ["src/EmployeeProject1.Web.Host", "src/EmployeeProject1.Web.Host"]
COPY ["src/EmployeeProject1.Web.Core", "src/EmployeeProject1.Web.Core"]
COPY ["src/EmployeeProject1.Application", "src/EmployeeProject1.Application"]
COPY ["src/EmployeeProject1.Core", "src/EmployeeProject1.Core"]
COPY ["src/EmployeeProject1.EntityFrameworkCore", "src/EmployeeProject1.EntityFrameworkCore"]
WORKDIR "/src/src/EmployeeProject1.Web.Host"
RUN dotnet publish -c Release -o /publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:7.0
EXPOSE 80
WORKDIR /app
COPY --from=build /publish .
ENTRYPOINT ["dotnet", "EmployeeProject1.Web.Host.dll"]
