#!/bin/bash
mkdir -p /root/.nuget/packages/.tools
mv /pipeline/source/app/ef-tools ~/.nuget/packages/.tools/Microsoft.EntityFrameworkCore.Tools 
cd /pipeline/source/app/publish
dotnet ef database update
dotnet StatlerWaldorfCorp.LocationService.dll --server.urls=http://0.0.0.0:${PORT-"8080"}
