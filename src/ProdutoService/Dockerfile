FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["ProdutoService.csproj", "."]
RUN dotnet restore "ProdutoService.csproj"
COPY . .
RUN dotnet build "ProdutoService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProdutoService.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProdutoService.dll"]