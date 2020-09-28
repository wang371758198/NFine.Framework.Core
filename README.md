# NFine.Framework.Core
asp.net core 管理系统框架
#### 示例用户名:admin 密码:000000

### Docker 
在Docker中运行时由于linux系统中有的没有libgdiplus组件，所以在创建镜像是安装libgdiplus：
##### 第一种办法
在Dockerfile中添加

apt-get update
apt-get install libgdiplus


##### 第二种办法
对现有的镜像做修改，然后保存下来。
 - docker run -it microsoft/aspnetcore
 - apt-get update
 - apt-install libgdiplus
 - exit
 - docker ps -a (找到刚才运行的容器的id)
 - docker commit 容器的id lind/aspnetcore (以后Dockerfile中的FROM用此镜像)



