FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["PedidoService.csproj", "."]
RUN dotnet restore "PedidoService.csproj"
COPY . .
RUN dotnet build "PedidoService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PedidoService.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PedidoService.dll"]