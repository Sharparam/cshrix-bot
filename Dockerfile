FROM microsoft/dotnet:2.2-runtime-alpine3.9

COPY artifacts/Cshrix.Bot.Console/ /data
ENTRYPOINT dotnet /data/Cshrix.Bot.Console.dll
