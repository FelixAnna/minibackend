#!/bin/bash  

cd /webapps/boapi/

sudo pgrep -f "dotnet BookingOffline.Web.dll"
sudo pkill -f "dotnet BookingOffline.Web.dll"

#dotnet dev-certs https --clean
#dotnet dev-certs https --verbose

sudo touch startup.log
sudo chmod 777 startup.log

sudo dotnet BookingOffline.Web.dll --urls=http://*:80 >>startup.log 2>&1 &