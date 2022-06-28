#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Cko.PaymentGateway.WebApi/Cko.PaymentGateway.WebApi.csproj", "Cko.PaymentGateway.WebApi/"]
COPY ["Cko.PaymentGateway.Core/Cko.PaymentGateway.Core.csproj", "Cko.PaymentGateway.Core/"]
COPY ["Cko.PaymentGateway.Service/Cko.PaymentGateway.Service.csproj", "Cko.PaymentGateway.Service/"]
COPY ["Cko.PaymentGateway.Data/Cko.PaymentGateway.Data.csproj", "Cko.PaymentGateway.Data/"]
RUN dotnet restore "Cko.PaymentGateway.WebApi/Cko.PaymentGateway.WebApi.csproj"
COPY . .
WORKDIR "/src/Cko.PaymentGateway.WebApi"
RUN dotnet build "Cko.PaymentGateway.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Cko.PaymentGateway.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Cko.PaymentGateway.WebApi.dll"]
