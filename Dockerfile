FROM mcr.microsoft.com/dotnet/core/runtime:2.2-alpine

COPY artifacts/Cshrix.Bot.Console/ /data
ENTRYPOINT dotnet /data/Cshrix.Bot.Console.dll
