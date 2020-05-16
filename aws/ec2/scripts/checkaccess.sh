#!/bin/bash  
set -e  


index=6
while [ $index -gt 0 ]
do
	sleep 5s
	response=$(curl http://127.0.0.1:5000/test/running)
	if grep -q "succeed" <<< "$response"; then
		echo $response
		index=0
	fi
	index=$(($index-1))
done
