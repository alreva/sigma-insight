FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release

#RUN curl -fsSL https://deb.nodesource.com/setup_14.x | bash - \
#    && apt-get install -y \
#        nodejs \
#    && rm -rf /var/lib/apt/lists/* 

RUN apt-get update -yq 
RUN apt-get install -yq curl gnupg
RUN curl -sL https://deb.nodesource.com/setup_20.x | bash -
RUN apt-get install -yq nodejs

WORKDIR /src
COPY ./SigmaInsight.Web.csproj .
RUN dotnet restore
COPY . .
RUN dotnet build -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=true

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV HTTP_PORTS=80
ENTRYPOINT ["./SigmaInsight.Web"]
