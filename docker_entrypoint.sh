#!/bin/bash
cd /pipeline/source/app/publish
ls
# dotnet ef database update
dotnet StatlerWaldorfCorp.LocationService.dll --server.urls=http://0.0.0.0:${PORT-"8080"}
