# RabbitMQ

## Introduction RabbitMQ
### What is RabbitMQ?  
 - Open source messaging system with messages and queues    
 - Implements Advanced Message Queueing Protocol(AMQP)  0-9-1  
 - Written in Erlang    
 - Distributed fault tolerant design  
 - RabbitmQ server is message broker  
 - RabbitMQ is reliable(persisted to disk, message acknowledments), routing(via exchanges and bindings), clustering(high availabilty), management web ui and CLI(RabbitMQ ctrl and admin)    
 - RabbitMQ is cross-platform  
 - RabbitMQ supports various technologies via client libraries  
 - RabbitMQ is open-source but enterprise support is also available(via http:///pivotal.io)  

### Rabbitmq vs MSMQ
- centralized vs decentralized  
- multi-platform vs windows only   
- amqp(default) standards based vs proprietary standard  
### Management Options
- web ui to manage message broker 
- HTTP api to manage rabbitmq server 
- Command line interface 

## Introduction to RabbitMQ Exchanges
### AMQP Protocol
- Networking protocol  
- RabbitMQ supports version AMQP 0-9-1  
- enables client applications to communicate with compatible messaging system  
- publisher-->exchange--with routes/bindings-->queue<--subscribes/acks-->consumer    
### Exchanges 
- Exchanges are AMQP entities where messages are sent to the message broker  
- Exchanges take a message and routes to one or more queues. The type of routing routing depends on exchange type used and exchanging rules called bindings  
- Exchanges are generally defined with attributes  
- Examples of attributes are   
-- Name  
-- Durablility (persist messages to disk)  
-- Auto-delete  
-- Arguments  
- AMQP message broker has a default direct exchange prebuilt with no name(empty string)  
- Every queue that is created say "testqueue" is bound to this default exchange with the routing key same as queue name i.e. "testqueue"  
- this may seem like the messages are send to queue directly which is not the case  
#### Direct Exchanges
- an queue  binds to the exchange via a routing key  
- all messages that arrive at the exchange with same routing key are sent to the queue  
#### Fanout Exchanges
- routes messages to all queues that are bound to it   
- routing key is ignored  
- ideal for broadcasting messages   
#### Topic Exchanges
- routes messages to one or more queues via by matching routing key patterns between the exchanges and all the queues that are bound to it  
- " * " (star) can substitute for exactly one word
- " # " (hash) can substitute for zero or more words
- ideal for selectively multicasting messages  
#### Headers Exchanges
- routes messages to one or more queues via attributes in the header  
- routing key is ignored  
- ideal when one routing key is not sufficient for sending messages to queues  
- ideal when routing key is not a string  
### Queues
- Messages are placed in queues and are worked in FIFO basis  
- Queue must be declared first to be used  
- Queue declartion is idempotent  
- Queues can be user-named or auto-named  
- Queue names can be 255 chars in length  
- Queues are generally defined with attributes  
- Examples of attributes are 
-- Name  
-- Durable(persist queue to disk;not messages)  
-- Exclusive  
-- Auto-delete  
### Bindings
- are the rules that define how messages are routed from exchange to queues
- bindings may have optional routing key attribute for certain type of exchanges which acts as a filter in sending the messages from the exchange to the bound queue
- if the message cannot be routed to queue due to invalid binding, it is either dropped or sent back to the publisher depending on the message attributes   
### Consumers and Message Acks
- consumers subscribe to the messages on the queue  
- AMQP gives two options to remove a message from the queue  
-- message can be removed when queue sends it to the consumer  
-- message can be removed when queue receives acknowledgement from the consumer that message is received  
- if consumer fails to ack, the
-- message can be requeued  
-- message can be sent to other consumer  
-- message can be discarded  
### Standard Queues
### Worker Queues
- Work queue is used to distribute messages among multiple workers
- In work queues messages will be shared between them
### Publish and Subscribe
- In publish/subscribe pattern, single message is delivered to mutiple consumers 
- The messages will be lost if no queue is bound to the exchange yet
### Direct Routing 
- In direct routing, message from exchange goes to the queues whose binding key exactly matches the routing key of the message
- It is perfectly legal to bind multiple queues with the same binding key.  In that case, the direct exchange will behave like fanout and will broadcast the message to all the matching queues
- Also multiple routing keys can be used to bind the queue to exchange
### Topic Routing
- Messages sent to a topic exchange can't have an arbitrary routing_key - it must be a list of words, delimited by dots upto 255 bytes
- " * " (star) can substitute for exactly one word
- " # " (hash) can substitute for zero or more words
- When a queue is bound with " # " (hash) binding key - it will receive all the messages, regardless of the routing key - like in fanout exchange
- When special characters " * " (star) and " # " (hash) aren't used in bindings, the topic exchange will behave just like a direct one
### Remote Procedure Calls(RPC)

### Microservices and Queuing

### Installation and Configuration

### Synchronous and Asynchronous Communication with RabbitMQ

### RabbitMQ .Net Client API 

## Architecture Notes
### Notes
- RabbitMQ initial spin up can be done with a batch job    
- Queue and Exchange declaration in RabbitMQ is idempotent  
- Queues should be durable  
- Queue messages should be persisted  
### Useful Commands
docker container run -d --hostname my-rabbit --name rmq rabbitmq:3  
docker container run -d --hostname my-rabbit -p 8080:15672 --name rmqm rabbitmq:3-management  

docker container run -d -p 5672:5672 -v /Users/laxmimanojkumarpoonati/Projects/c1/rabbitmq.conf :/etc/rabbitmq/rabbitmq.config --hostname myrabbit rmq  
