# Docker enabled 

#For Production ==>
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-nanoserver-1809 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

#Only for Development ==>
FROM mcr.microsoft.com/dotnet/core/sdk:3.1-nanoserver-1809 AS build
WORKDIR /src
COPY ["JoysCakeShop.csproj", ""]
RUN dotnet restore "./JoysCakeShop.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "JoysCakeShop.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "JoysCakeShop.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "JoysCakeShop.dll"]