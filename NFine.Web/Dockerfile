FROM lind/aspnetcore:2.0
#FROM microsoft/aspnetcore
#ARG source

#RUN apt-get update
#RUN apt-get install libgdiplus
#RUN mkdir  /usr/src/app

#COPY -R ../NFine.Application /publish/NFine.Application 
#COPY -R ../NFine.Core /publish/NFine.Core
#COPY -R ../NFine.Data /publish/NFine.Data
#COPY -R ../NFine.Domain /publish/NFine.Domain
#COPY -R ../NFine.Mapping /publish/NFine.Mapping
#COPY -R ../NFine.Repository /publish/NFine.Repository
#COPY -R ../NFine.Web /publish/NFine.Web
#COPY ../NFine.Framework.Core.sln /publish
COPY publish /publish

WORKDIR /publish

EXPOSE 80

#CMD ["dotnet","clean"]

#CMD ["dotnet", "publish","-c","Release","-o","publish"]

CMD ["dotnet", "NFine.Web.dll"]