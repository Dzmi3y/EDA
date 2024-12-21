#!/bin/bash

while ! nc -z localhost 9092; do
  echo 'Waiting for Kafka to start...';
  sleep 1;
done;

declare -A topicsPartitions 
topicsPartitions=( 
	["ProductPageResponse"]=1 
	["ProductPageRequest"]=1 
	["SignUpRequest"]=1 
	["SignUpResponse"]=1 
	["SignInRequest"]=1 
	["SignInResponse"]=1 
	["SignOutRequest"]=1 
	["SignOutResponse"]=1 
	["TokenRefreshRequest"]=1 
	["TokenRefreshResponse"]=1 
	["DeleteAccountRequest"]=1 
	["DeleteAccountResponse"]=1 )

for topic in "${!topicsPartitions[@]}"; do 
	kafka-topics.sh --create --topic "$topic" --partitions "${topicsPartitions[$topic]}" --replication-factor 1 --bootstrap-server localhost:9092 
done

echo 'Topics created successfully'
sleep infinity
