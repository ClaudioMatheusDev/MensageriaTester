FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["CarrinhoService.csproj", "."]
RUN dotnet restore "CarrinhoService.csproj"
COPY . .
RUN dotnet build "CarrinhoService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CarrinhoService.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CarrinhoService.dll"]