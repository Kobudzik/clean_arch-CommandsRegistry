FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
COPY /src /app
WORKDIR /app
RUN dotnet build WebUI/WebUI.csproj -c Release -o output


#mcr.microsoft.com/dotnet/aspnet:5.0
#mcr.microsoft.com/dotnet/runtime:5.0

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime
COPY --from=build /app/output .

ENTRYPOINT ["dotnet", "WeatherApp.WebUI.dll"]