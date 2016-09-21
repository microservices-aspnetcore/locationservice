#!/bin/bash
cd /pipeline/source/app/publish
echo "starting"
mkdir /pipeline/source/app/publish/tmp
export TEMP=/pipeline/source/app/publish/tmp
export TMPDIR=/pipeline/source/app/publish/tmp
dotnet StatlerWaldorfCorp.LocationService.dll --server.urls=http://0.0.0.0:${PORT-"8080"}
