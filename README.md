# service-discovery
service-discovery in .net framework 4.8

DNS_SD : has the code to register the service to eurake server
ConsumeFromSD: has the code to consume registered service from eurake server

Note:
1. Before starting any of the application please make sure euraka server is running.
2. Change the euraka server service Url in appsettings for both the applications. bydefault it's "http://localhost:8761/eureka/"


to run the dockerized steeltoeoss/eurekaserver image:

Pull the image:
docker pull steeltoeoss/eurekaserver:latest

run the image:
docker run -d -p 8761:8761 --name eureka-server -it steeltoeoss/eurekaserver:latest