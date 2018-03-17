FROM lind/aspnetcore:2.0

#ARG source

#RUN apt-get update
#RUN apt-get install libgdiplus

COPY . /publish

WORKDIR /publish/NFine.Web    

EXPOSE 80

CMD ["dotnet", "publish","-c","Release","-o","publish"]

CMD ["dotnet", "publish/NFine.Web.dll"]