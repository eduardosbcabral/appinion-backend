FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /app

COPY . .

CMD ASPNETCORE_URLS=http://*:$PORT dotnet Appinion.Api.dll