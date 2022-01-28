			Micro service course (Purpose: face detection)
-------------------------------------------------------
					Introducing
-------------------------------------------------------
Technologies => {
.net core 5.0
docker
rabbitmq
mass transist
signalR core 
}
- how parts of a application will comunicate with each other ?
+ there is two type of comunication style
+ 1: Rpc(Remote procedure call)  A micro service directly communicate with another micro service through http requests
+ 2: Asynchronous messaging  

A single microservice wont achive much but when it comunicate with other micro services then it will create value 

Certain rules for the type of comunicationsbetween microservices . we cannot have communication between them without setting any rules
Other wise we'll run into trouble or the structure will start to behaving like a monolothic architecture
In a distributed system like a microservice based application wiht so many artifacts moving around and with distributed services across many servers orhosts, components will eventually fail 
So we need to design our microservices and the communications across them considering common risks this type of distributed system.
A popular approach is to implement HTTP Rest base microservices due to their simplicity and http based approach is prefectly acceptable The only issue is the way we using them
if you use http requests and response just to interact with your microservices from clinet application or from API gateway , thats fine .
but if we create long chains of synchronous http calls across microservices communicating across their boundaries as if the microservices were objects in a monolothic application your application
will eventually run into problems.
--------------------------------------------------------------
Jump in to project =>

OpenCv =>
{
	a free opensource image proccesing library which we are gonna use from it in this face detection api(open computer vision)
}
Continue =>
{
	https://github.com/amirhosseinhosseinzadeh/FaceDetectionApi
}

----------------------------------------------------------------
Rabit MQ

There is alot of procedures for implement interaction with rabit mq =>
{
	Producer(done something)[send message] ---> Rabit mq (collect message with any kind of informaion) [Put it in queue] ---> Consumer (Receive the message and consume it)
}

The  rabit mq could contain any information  we need for example
bite_arrays , message for result of some proccess , an simple code etc. 

What should be known as exchanges =>
{
	In figure 1 Direct => we have shown as if a message is sent directly to a queue from where consumers take the message in fact rabit mq ,messagees are not published directly to a queue 
	|	Instead the producers sends messages to an exchange. and the exchange is responsible for routing the messages to different queues with the help of bindings and routing keys
		A binding is a link between a queue abd an exchange.
	The second one is "Topic exchange" => the message form an exchange to relevant queue or queues find their way is wild card match between the routing key and the routing pattern 
	|	specified ind the binding 
	And finally the "Fanout type" => in this type of exchange the messages are delivered to all the queues and the mass transit service bus that we are going to be 
	|	using on top of rabbitmq that is the type of exchange which is used .
}

In ordere facilitate messaging betweeen our application component and the rabit mq we usually create a component that call and event bus in our application (EasyNetQ)
------------------------------------------------------------

Mass transit and EasyNetQ are pen source frameworks which we can utilize easily for building event buses for our messaging applications quit easily. 
Mass tranist also provides in memory sevice bus and azure service bus which make it an idealy gateway package for handling queues

Producers concept (with mass tranist): 
	1 : message can be send 
	2 : message can be published
in every one behavior is completely differenet but easy to understand by lookng to type o message is sent. its delivered to a specifi endpoint. 
when a message is send its delivered to a specific endpoint using a destination address
but whne a message is published its nt send to a specific endpoint but instead its been broadcated to any consumers which had subscribe to the message type

we considered message sent as "command" and message publish as "events"

As we saw the message cannot direcly get in a queue but instead its need to pass to a exchange and then get in to queue so we need a an exchange defination 
but with mass tranist this level will be ignore because mass tranist built'in exchange defination iself 
------------------------------------------------------------------------------------------------------------
Docker 

commands =>
{
	cmd : docker images =>
	{
		get list of images
	}
	cmd : docker run [image_name]:[tag] =>
	{
		run the imge as a container if exists 
		|	tag will mention the version of docker image we need 
				latest is a tag which set the latest vrsion to be pulled
			
		* => if we wasnt download the image of container it will combine the pull and run commands
	}
	cmd : docker rmi [image_name]:[tag_name]
	{
		will remove the speified image 
	}
	cmd : docker ps -a
	{
		get all containers
	}
	cmd : docker rm [container_id] =>
	{
		this command will destroy the container which specified with id (only first three letter would be enough)
		|	the id can be found with get list of containers
	}
	cmd : docker stop [container_name]=>
	{
		will stop the container
	}
}
switches =>
{
	sw : -it =>
	{
		interactive : it means that container will be available even after closing the bash or other things which made container running
	}
	sw : -a =>
	{
		all
	}
	sw : -d =>
	{
		it make no og on screen except will shown us logs on terminal
	}
	sw : -p =>
	{
		determine port number to un container on
	}
	sw : --name =>
	{
		determine name of container
	}
}
------------------------------------------------------------------------------------------------------------------------------------------
Rabit mq docker container 

first we need to set some settings for docker engine include memory and cpu consuming max and min amount
done ! :) (made me cry to solve issues after adding .wslconfig)

we need an specific version of rabbit mq which provide us a tool to monitoring the queues 
command to get specified version of rmq with an extra plugin =>
{
	docker run -p 15672:15672 -p 5672:5672 --name [any_string] rabbit-mq:3-management
}
-------------------------------------------------------------------------------------------------------------------------------------------
Create event bus 

through the application events will raise and we send it to the event bus 

-------------------------------------------------------------------------------------------------------------------------------------------
The rabbit mq have 8 exhange when it starts and they are default excahnges for system 

-------------------------------------------------------------------------------------------------------------------------------------------

Microsoft sql container make command =>
{
	docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=[any_string](passw0rd(!))" --name ordermssql -p 1445:1433 mcr.microsoft.com/mssql/server:2019-latest
}
Connecting to database through ssms =>
{
	Server Name : localhost,1445
	Login : sa
	Password : password
}
--------------------------------------------------------------------------------------------------------------------------------------------
7 -> 5