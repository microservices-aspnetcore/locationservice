#!/bin/bash
cd /pipeline/source/app/publish
echo "starting"
dotnet StatlerWaldorfCorp.LocationService.dll --server.urls=http://0.0.0.0:${PORT-"8080"}
