#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Cko.BankSimulator/Cko.BankSimulator.csproj", "Cko.BankSimulator/"]
COPY ["Cko.PaymentGateway.Service/Cko.PaymentGateway.Service.csproj", "Cko.PaymentGateway.Service/"]
COPY ["Cko.PaymentGateway.Core/Cko.PaymentGateway.Core.csproj", "Cko.PaymentGateway.Core/"]
RUN dotnet restore "Cko.BankSimulator/Cko.BankSimulator.csproj"
COPY . .
WORKDIR "/src/Cko.BankSimulator"
RUN dotnet build "Cko.BankSimulator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Cko.BankSimulator.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Cko.BankSimulator.dll"]
