#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 7138:80
EXPOSE 5109:443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ToDoApp.Server/ToDoApp.Server.csproj", "ToDoApp.Server/"]
RUN dotnet restore "ToDoApp.Server/ToDoApp.Server.csproj"
COPY . .
WORKDIR "/src/ToDoApp.Server"
RUN dotnet build "ToDoApp.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ToDoApp.Server.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ToDoApp.Server.dll"]