#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["WebApi/WebApi.csproj", "WebApi/"]
COPY ["_Common/MailSender/MailSender.csproj", "_Common/MailSender/"]
COPY ["Domain.BusinessLogic/Domain.BusinessLogic.csproj", "Domain.BusinessLogic/"]
COPY ["_UserManagement/UserManagement.DataAccess/UserManagement.DataAccess.csproj", "_UserManagement/UserManagement.DataAccess/"]
COPY ["_Common/Utilities/Utilities.csproj", "_Common/Utilities/"]
COPY ["_Payment/Payment.DataAccess/Payment.DataAccess.csproj", "_Payment/Payment.DataAccess/"]
COPY ["_Common/Common.DataAccess/Common.DataAccess.Utilities.csproj", "_Common/Common.DataAccess/"]
COPY ["_Payment/PaymentStripe/PaymentStripe.csproj", "_Payment/PaymentStripe/"]
COPY ["Domain.DataAccess/Domain.DataAccess.csproj", "Domain.DataAccess/"]
RUN dotnet restore "WebApi/WebApi.csproj"
COPY . .
WORKDIR "/src/WebApi"
RUN dotnet build "WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApi.dll"]