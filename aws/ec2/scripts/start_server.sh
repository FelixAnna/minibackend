#!/bin/bash  

cd /webapps/boapi/

pgrep -f "dotnet BookingOffline.Web.dll"
pkill -f "dotnet BookingOffline.Web.dll"

dotnet BookingOffline.Web.dll &