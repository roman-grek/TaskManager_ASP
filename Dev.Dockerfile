# Use Microsoft's official .NET image.
# https://hub.docker.com/r/microsoft/dotnet
FROM microsoft/dotnet:sdk

RUN mkdir app
WORKDIR /app
COPY . .

RUN dotnet restore

CMD dotnet watch run