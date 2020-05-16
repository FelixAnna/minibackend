#!/bin/bash  

cd /webapps/boapi/

sudo pgrep -f "dotnet BookingOffline.Web.dll"
sudo pkill -f "dotnet BookingOffline.Web.dll"

sudo touch startup.log
sudo chmod 777 startup.log
dotnet BookingOffline.Web.dll >>startup.log 2>&1 &
