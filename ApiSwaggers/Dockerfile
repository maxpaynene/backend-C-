FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["ApiSwaggers/ApiSwaggers.csproj", "ApiSwaggers/"]
RUN dotnet restore "ApiSwaggers/ApiSwaggers.csproj"
COPY . .
WORKDIR "/src/ApiSwaggers"
RUN dotnet build "ApiSwaggers.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "ApiSwaggers.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ApiSwaggers.dll"]