FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

RUN dotnet tool install -g Microsoft.Web.LibraryManager.Cli
RUN ln -s ~/.dotnet/tools/libman /usr/local/bin/libman

WORKDIR /src
COPY ["src/EmployeeProject1.Web.Mvc/EmployeeProject1.Web.Mvc.csproj", "src/EmployeeProject1.Web.Mvc/"]
COPY ["src/EmployeeProject1.Web.Core/EmployeeProject1.Web.Core.csproj", "src/EmployeeProject1.Web.Core/"]
COPY ["src/EmployeeProject1.Application/EmployeeProject1.Application.csproj", "src/EmployeeProject1.Application/"]
COPY ["src/EmployeeProject1.Core/EmployeeProject1.Core.csproj", "src/EmployeeProject1.Core/"]
COPY ["src/EmployeeProject1.EntityFrameworkCore/EmployeeProject1.EntityFrameworkCore.csproj", "src/EmployeeProject1.EntityFrameworkCore/"]
WORKDIR "/src/src/EmployeeProject1.Web.Mvc"
RUN dotnet restore 

WORKDIR /src
COPY ["src/EmployeeProject1.Web.Mvc", "src/EmployeeProject1.Web.Mvc"]
COPY ["src/EmployeeProject1.Web.Core", "src/EmployeeProject1.Web.Core"]
COPY ["src/EmployeeProject1.Application", "src/EmployeeProject1.Application"]
COPY ["src/EmployeeProject1.Core", "src/EmployeeProject1.Core"]
COPY ["src/EmployeeProject1.EntityFrameworkCore", "src/EmployeeProject1.EntityFrameworkCore"]
WORKDIR "/src/src/EmployeeProject1.Web.Mvc"
RUN libman restore
RUN dotnet publish -c Release -o /publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:7.0
EXPOSE 80
WORKDIR /app
COPY --from=build /publish .
ENTRYPOINT ["dotnet", "EmployeeProject1.Web.Mvc.dll"]
