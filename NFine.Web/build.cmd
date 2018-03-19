#dotnet publish -c Release -o publish
echo off
echo "当前docker中"
docker images nfine

echo "请输入版本号:"
set /p version=

dotnet clean
dotnet publish -c Release -o publish


docker build -t nfine:%version% .

echo "创建Repository:nfine  Tag:%version%完成！！！"


