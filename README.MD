# Proof of concept for docker-compose

This repo demonstrates a working [docker-compose.yml](./docker-compose.yml) that includes:

- [Azure Cosmos DB Emulator for Linux](https://learn.microsoft.com/en-us/azure/cosmos-db/docker-emulator-linux)
- [Azurite](https://learn.microsoft.com/en-us/azure/storage/common/storage-use-azurite?tabs=docker-hub)
- [RabbitMQ](https://www.rabbitmq.com) ([Docker image](https://hub.docker.com/r/masstransit/rabbitmq) provided by [MassTransit.io](https://masstransit.io/quick-starts/rabbitmq))

The purpose of this is to provide an example of how to configure docker-compose for a local development environment that only requires the developer to run one command to have all required services running on their local machine in docker containers.

The aim of this is to demonstrate a working example.

## Requirements

- .Net 7 SDK
- Docker for Windows (or regular docker on Linux)

## Developer setup

### Services currently configured

- `cosmosdb`: Azure Cosmos DB Emulator for Linux
- `azurite`: Azurite (Azure storage emulator)
- `rabbitmq`: RabbitMQ AMQP services

### Assumptions

1. You have the requirements listed above installed
2. You have no other services running on any of the following ports:
    - 5672
    - 8081
    - 8900-8901
    - 8979
    - 10000-10002
    - 10250-10256
    - 10350
    - 15672
3. You do not already have Azurite running elsewhere.
4. You do not have the (now deprecated) Azure Storage Emulator running

> ***Note:** You may have an instance of Azurite or the older storage emulator running without realising it if you have opened another project that has a connection string `UseDevelopmentStorage=true`. Visual Studio will start its own instance automatically when it detects it is needed. Note that the Visual Studio Azurite/Emulator instance does not shut down when you close Visual Studio - you must restart your computer.*

### Running services for the first time

1. Clone the repository
2. Open your command line client that can use docker CLI commands and `cd` to the repository root
3. Run the command `docker compose up -d`

This will start all of the services, all running on their default ports.

If you only wish to start one of the services you can do so by specifying the service name on the end of the command:

```bash
# Only start Cosmos DB
docker compose up -d cosmosdb

# Only start azurite
docker compose up -d azurite

# Only start RabbityMQ
docker compose up -d rabbitmq
```

### Closing down services when finished

When you've finished using the services for the day it's a good idea to spin down the containers to free up system resources. There are two ways to do this.

#### 1 - Close down and remove the containers from docker

1. Open your command line client that can use docker CLI commands and `cd` to the repository root
2. Run the command `docker compose down`

> ***Note:** This will **not** remove any data from docker volumes*

#### 2 - Close down but leave the containers registered in docker in a "stopped" state

1. Open your command line client that can use docker CLI commands and `cd` to the repository root
2. Run the command `docker compose stop`

### Restarting the services the next time

How you restart the services the next time depends on how you closed them down.

#### Closed them down with `docker compose down`

Use `docker compose up -d` to spin them up again as this will recreate the containers.

#### Closed them down with `docker compose stop`

Use `docker compose start` instead.
