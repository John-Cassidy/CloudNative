# CloudNative

## Description: Cloud-Native: Microservices, Kubernetes, Service Mesh, CI/CD

[Udemy Course](https://www.udemy.com/course/cloud-native-microservices-kubernetes-service-mesh-cicd/)

[GitHub Repository](https://github.com/mehmetozkaya/CloudNative)

[Medium Article](https://medium.com/design-microservices-architecture-with-patterns)

[Mehmet Ozkaya - Trainer's GitHub Site](https://github.com/mehmetozkaya)

NOTE: Moving Forward, a branch will be created cooresponding to a lecture where new or modified code is added to project. If applicable, the branch will be merged into main.

Example: lecture69 branch created to create microservices/ProductService.csproj

## Kubernetes shortcuts

kubectl cluster-info
kubectl get po -A
kubectl get all

Start with watch pods
kubectl get pod -w
kubectl get pods

## Install Kubernetes Dashboard in Docker Desktop

[Andrew Lock Article (Windows/wsl2 and Install Kube Dashboard)](https://andrewlock.net/running-kubernetes-and-the-dashboard-with-docker-desktop/)

[Teten Nugraha Article (skip login)](https://medium.com/@teten.nugraha/install-kubernetes-dashboard-in-docker-desktop-469a2fb7c87)

[Use Bearer Token to login](https://medium.com/@dijin123/kubernetes-and-the-ui-dashboard-with-docker-desktop-5ad4799b3b61)

## Minikube shortcuts

Following shortcuts used to run instance of Minikube on Windows 11 with Docker Desktop and Kubernetes installed.

- Start Minikube: This command starts a Minikube cluster container using the Docker driver.

```powershell
minikube start --driver=docker
```

- Set the Kubernetes context to Minikube or docker-desktop(kubernetes), use the following command:

```powershell
kubectl config use-context minikube
# TO CHANGE BACK TO docker-desktop version of kubernetes
kubectl config use-context docker-desktop
```

- Verify the installation:

```powershell
minikube status
```

- See dashboard

```powershell
minikube dashboard
```

- Start/Stop Application using yaml file

```powershell
kubectl apply -f product.yaml
kubectl delete -f product.yaml
```

- Access DNS address of application: http://product.local/api/products

- Stop Minikube

```powershell
minikube stop
```

## Pillar 1: Microservices

[Martin Fowlers Microservices Article](https://martinfowler.com/articles/microservices.html)

### Characteristics of a Microservice Architecture:

- Componentization via Services
- Organized around Business Capabilities
- Products not Projects
- Smart endpoints and dumb pipes
- Decentralized Governance
- Decentralized Data Management
- Infrastructure Automation
- Design for failure
- Evolutionary Design

### Understand E-Commerce Domain: Functional Requirements

- List products
- Filter products as per brand and categories
- Put products into the shopping cart
- Apply coupon for discounts and see the total cost all for all of the items in shopping cart
- Checkout the shopping cart and create an order
- List my old orders and order items history

### Understand E-Commerce Domain: User Stories (Use Cases)

- As a user I want to list products
- As a user I want to filter products as per brand and categories
- As a user I want to put products into the shopping cart so that I can check out quickly later
- As a user I want to apply coupon for discounts and see the total cost all for all of the items that are in my cart
- As a user I want to checkout the shopping cart and create an order
- As a user I want to list my old orders and order items history
- As a user I want to login the system as a user and the system should remember my shopping cart items

### Analysis E-Commerce Domain - Nouns and Verbs

- As a user I want to list products
- As a user I want to be able to filter products as per brand and categories
- As a user I want to see the supplier of product in the product detail screen with all characteristics of product
- As a user I want to be able to put products that I want to purchase in to the shopping cart so I can check out
- As a user I want to see the total cost all for all of the items that are in my cart so that I see if I can afford to buy
- As a user I want to see the total cost of each item in the shopping cart so that I can re-check the price for items
- As a user I want to be able to specify the address of where all of the products are going to be sent to
- As a user I want to be able to add a note to the delivery address so that I can provide special instructions
- As a user I want to be able to specify my credit card information during check out so I can pay for the items
- As a user I want system to tell me how many items are in stock so that I know how many items I can purchase
- As a user I want to receive order confirmation email with order number so that I have proof of purchase
- As a user I want to list my old orders and order items history
- As a user I want to login the system as a user and the system should remember my shopping cart items

![Analysis E-Commerce Domain - Nouns](./resources/001-design-analysis-e-commerce-domain-nouns.png)

![Analysis E-Commerce Domain - Verbs](./resources/002-design-analysis-e-commerce-domain-verbs.png)

![Object Relational Diagram (Object Responsibilities)](./resources/003-object-responsibility-diagram.png)

![Main Use Case of Our E-Commerce Application](./resources/004-main-use-case-of-our-e-commerce-application.png)

![Identifying and Decomposing Microservices for E-Commerce](./resources/005-identifying-decomposing-microservices-for-domain.png)

![Design: Cloud-Native E-commerce Microservices](./resources/006-design-cloud-native-e-commerce-microservices.png)

## Pillar 2: Containers

E-Commerce App Design

Containerize application:

- Write Dockerfile
- Build Docker Image
- Run Docker Container
- Test running docker container on local docker env
- Tag Docker Image
- Publish image to a Registry: Docker Hub, AWS ECR
- Configure and Deploy ProductService to AWS AppRunner

## Pillar 3: Container Orchestrators

### Getting Started with Minikube and kubectl

- Getting Started with Minikube and kubectl
- Pods, Deployments, Services, Incress, ConfigMaps, Secrets on Kubernetes using Minikube
- Best Practice for Creating Deployment and Services for Microservices
- Helm Charts - Managing Kubernetes Applications with Helm

### Kubernetes Configuration Best Practices for Containers

- By default, many applications bind to localhost (127.0.0.1) which makes them only accessible from within the container.
- To be accessible from outside the container, your application should bind to 0.0.0.0.
- To resolve the issue, update your application to bind to 0.0.0.0 instead of localhost or 127.0.0.1.
- The process of changing the binding IP address will depend on your application and its configuration.
- For example, in a .NET 7 web application using Kestrel, you can update the Program.cs file to bind to 0.0.0.0.
  Any port which is listening on the default "0.0.0.0" address inside a container will be accessible from the network.

- [Configuration Best Practices of Kubernetes](https://kubernetes.io/docs/concepts/configuration/overview/)
- [Java, NodeJS containers not required, Asp.Net need explicit configure port on Dockerfile or code](https://github.com/dotnet/dotnet-docker/issues/3968)

#### ASP.NET Container Expose Port - CONFIGURE TO LISTEN - 0.0.0.0:8080

- Edit Program.cs
- Build Docker Image
- Run Docker Container
- Test running docker container on local docker env
- Tag Docker Image
- Publish image to a Registry: Docker Hub
- Create Pod Definition k8s/product-pod.yaml
- Create and Apply Deployment on Kubernetes k8s/product-deploy.yaml
- Create and Apply a Service in Kubernetes k8s/product-service.yaml
  ![4 Types of Kubernetes Services](./resources/138_4-types-of-kubernetes-services.png)
- Combined Way of Creating Deployment and Services for Microservices - product.yaml

### Kubernetes Configuration Best Practices for Containers Continued

- Create Ingress for External Access of Microservice
  When exposing a web application to the internet, you should use an Ingress.
  [Kubernetes documentation of Ingress](https://kubernetes.io/docs/concepts/services-networking/ingress/#what-is-ingress)
- Create ConfigMaps and Secrets for Microservice
- Scale a Container Instance in Kubernetes
- Kubernetes Deploy and Service with Minikube
  ![Kubernetes Deploy and Service with Minikube](./resources/144-deploy-kubernetes-with-minikube.png)

### Helm: Managing Kubernetes Applications with Helm

#### What is Helm and Helm Charts?

Helm is a package manager for Kubernetes (K8s) that simplifies the deployment, scaling, and management of applications. It provides a way to define, install, and upgrade complex Kubernetes applications.

Helm uses a packaging format called Helm Charts. A Helm Chart is a collection of files that describe a related set of Kubernetes resources. These files are written in YAML and describe the application's Kubernetes resources, such as deployments, services, config maps, and secrets.

A single chart might consist of everything needed to deploy something simple, like a memcached pod, or something complex, like a full web app stack with HTTP servers, databases, caches, and so on.

It allows you to automate the deployment of complex applications in a
Kubernetes cluster. Instead of manually creating and managing each component of your application separately, can use a single Helm chart to define all of the components and their relationships. This makes it much easier to manage and update your application over time

For your next steps, you might want to:

- Install Helm and try deploying a Helm Chart
- Learn how to create your own Helm Charts
- Explore the public Helm Chart repository for pre-made charts of common applications

#### What is Artifact Hub for Helm Charts?

Artifact Hub is a web-based platform where Helm users and contributors can publish and share their Helm Charts. It's essentially a marketplace for finding and sharing cloud-native artifacts.

Artifact Hub provides a centralized place for discovering and distributing Kubernetes packages, such as Helm Charts, Operators, and more. It's supported by the Cloud Native Computing Foundation (CNCF) and is the recommended place to find Helm Charts since the deprecation of the Helm stable and incubator repositories.

For your next steps, you might want to:

- Visit Artifact Hub and explore the available Helm Charts
- Learn how to use a Helm Chart from Artifact Hub
- Learn how to publish your own Helm Chart on Artifact Hub

#### How can I search for and install Helm Charts from Artifact Hub?

To search for and install Helm Charts from Artifact Hub, follow these steps:

Visit the Artifact Hub website and use the search bar to find the Helm Chart you want to install.

Click on the Helm Chart you're interested in. You'll be taken to a page with more details about the chart, including installation instructions.

To install a Helm Chart, you typically need to add the chart's repository to your Helm client first. This can be done with the helm repo add command. The command will be provided on the chart's page on Artifact Hub.

```bash
helm repo add <repo-name> <repo-url>
```

After adding the repository, you can install the chart using the helm install command. Again, the exact command will be provided on the chart's page on Artifact Hub.

```bash
helm install <release-name> <repo-name>/<chart-name>
```

Remember to replace <repo-name>, <repo-url>, <release-name>, and <chart-name> with the actual values.

For your next steps, you might want to:

- Check the status of your Helm release
- Learn how to update and manage Helm releases
- Explore other Helm Charts on Artifact Hub deployment

#### How do Helm Charts Work?

[What is a Helm Chart? A Tutorial for Kubernetes Beginners](https://www.freecodecamp.org/news/what-is-a-helm-chart-tutorial-for-kubernetes-beginners/)

Helm Charts work by providing a structured format to define, install, and upgrade Kubernetes applications. Here's a step-by-step explanation:

Chart Creation: A Helm Chart is created as a collection of files inside a directory. The directory name is the name of the chart. It includes a Chart.yaml file that contains metadata about the chart, a values.yaml file that lists the default configuration values, and one or more template files that define Kubernetes resources.

Chart Packaging: The Helm Chart directory is packaged into a versioned chart archive file (.tgz) using the helm package command. This file can be distributed to other Helm users.

Chart Distribution: Packaged Helm Charts can be distributed via a chart repository, which is a basic HTTP server that can serve chart archive files. Users can search for and download charts from these repositories.

Chart Installation: Users can install a Helm Chart using the helm install command. Helm combines the chart and configuration (from values.yaml or provided by the user) to generate a release. A release is an instance of a chart running in a Kubernetes cluster.

Chart Upgrades and Rollbacks: Helm tracks each release and allows users to upgrade the release with new versions of the chart or configuration, or rollback to a previous release.

![Helm Chart Structure](./resources/151-helm-chart-structure.png)

For your next steps, you might want to:

Create your own Helm Chart
Learn how to package and distribute a Helm Chart
Explore how to manage Helm releases, including upgrades and rollbacks

#### Install Helm

See NOTES.md file for information on installing Helm

### Create Helm Chart for ProductService

See NOTES.md for information on creating helm chart for ProductService and running in either:

- Docker Desktop/kubernetes
- MiniKube

## Pillar 4: Cloud-Native Communications

- What are Cloud-Native Communications?
- How microservices communicate in Cloud-Native environments?
- What are patterns & best practices of communications in CN environments?
- API Gateways, Sidecar and Service Mesh Pattern
- Design & Implement our E-Commerce application with Communication Tools

### What are Cloud-Native Communications?

#### 12-Factor App – Cloud-Native Communications

- Processes: Emphasizes that applications should be stateless and share nothing. Encourages apps to use external services for sharing state, which often involves communication between services.
- Port binding: Enables apps to be deployed and scaled independently. By binding to a specific port, an app can expose its functionality as a service, allowing other services to communicate with it without being tightly coupled.
- Concurrency: Promotes scaling applications by running multiple processes concurrently. Communication and coordination between these processes and services become critical.
- Disposability: Fast startup and graceful shutdown for app processes. Services may need to communicate with other components during startup or shutdown to coordinate their actions or update their status.
- Service Mesh: Dedicated infrastructure layer handles service-to-service communication in a cloud-native env. It provides features like load balancing, service discovery, observability, and security.
- Service Discovery: Services need to dynamically discover and communicate with each other. Help to locate and connect to other services within the system.
- API Gateway: Entry point for external requests to the microservices. It routes the requests to appropriate services, handles load balancing, and provides additional functionalities like auth and rate limiting.
- Messaging Systems: Async communication between microservices by using message queues that decouples services, allows to evolve independently and ensures resilience against temporary service failures.

### How microservices communicate in Cloud-Native environments?

Microservices communication types:

- Synchronous communications: Request/Response

Synchronous communication is using HTTP or gRPC protocol for returning synchronous response. 
The client sends a request and waits for a response from the service.

- Asynchronous communications: Message broker event buses

The client sends a request but it doesn't wait for a response from the service.
The client should not have blocked a thread while waiting for a response. 
AMQP (Advanced Message Queuing Protocol)
Using AMQP protocols, the client sends the message with using message broker systems like Kafka and RabbitMQ queue.
The message producer does not wait for a response.
Message consume from the subscriber systems in async way, and no one waiting for response suddenly.
Asynchronous communication also divided by 2:

- one-to-one(queue)

In one-to-one(queue) implementation there is a single producer and single receiver.
Command Patterns offers to receive one queue object and after that execute the command with incoming message. 
This process restarts with receiving new command queue item.

- one-to-many (topic)

In one-to-many (topic) implementation has Multiple receivers. Each request can be processed by zero to multiple receivers. 
Event-bus or message broker system is publishing events between multiple microservices and communication provide with subscribing these events in an async way.
Publish/subscribe mechanism used in Event-driven microservices architecture

Microservices Communication Styles

- Request/response communication with HTTP and REST Protocol (extends gRPC and GraphQL)
gRPC protocol communication mechanisms to provide high performance and low latency
- Push and real-time communication based on HTTP, WebSocket Protocol
Use case about real-time and one-to-many communication like chat application, use Push Model with HTTP and WebSocket Protocols.
Build real-time two-way communication applications, such as chat apps and streaming dashboards like the score of a ports game, with WebSocket APIs.
The client and the server can both send messages to each other at any time. Backend servers can easily push data to connected users and devices.
- Pull communication based on HTTP and AMQP (short polling - long polling)
Also called "Polling" and it's basically the same as refreshing your mail inbox every 5 minutes to check for new mail. It is a call and ask model. This model is become a waste of bandwidth if there are no new messages and responses comes from the server. Opening and closing connections is expensive. And we can say that this model doesn't scale well.
- Event-Driven communication with Publish/Subscribe Mode

### What are patterns & best practices of communications in Cloud-Native environments?

#### Microservices Synchronous Communications and Best Practices

- The client sends a request with using http protocols and waits for a response from the service.
- The synchronous communication protocols can be HTTP or HTTPS. 
- Request/response communication with HTTP and REST Protocol (extends gRPC and GraphQL)
- REST HTTP APIs when exposing from microservices
- gRPC APIs when communicate internal microservices
- GraphQL APIs when structured flexible data in microservices
- WebSocket APIs when real-time bi-directional communication

#### 2 Main Approaches for Public and Backend APIs

- RESTful API Design over HTTP using JSON
- gRPC binary protocol API Design

### API Gateways, Sidecar and Service Mesh Pattern

- API Gateway is a single point of entry to the client applications, sits between the client and multiple backend.
- Similar to the facade pattern from object-oriented design, but it is a distributed system reverse proxy or gateway routing for using in synchronous communication.
- The pattern provides a reverse proxy to redirect or route requests to your internal microservices endpoints.
- API Gateway provides a single endpoint for the client applications, and it maps the requests to internal microservices.
- Provide cross-cutting concerns like authentication, SSL termination, and cache.
- Best practices is splitting the API Gateway in multiple services or multiple smaller API Gateways: BFF-Backend-for-Frontend Pattern.
- Should segregated based on business boundaries of the client applications.

#### Main Features of API Gateway Pattern

- Reverse Proxy and Gateway Routing

Reverse proxy to redirect requests to the endpoints of the internal microservices. Using Layer 7 routing for 
HTTP requests for redirections. Decouple client applications from the internal microservices. Separating
responsibilities on network layer and abstracting internal operations. 

- Requests Aggregation and Gateway Aggregation

Aggregate multiple internal microservices into a single client request. Client application sends a single request 
to the API Gateway and it dispatches several requests to the internal microservices and then aggregates the 
results and sends back to the client application in 1 single response. Reduce chattiness communication.

- Cross-cutting Concerns and Gateway Offloading

Best practice to implement cross-cutting functionality on the API Gateways. Cross-cutting functionalities can be;
Authentication and authorization, Service discovery, Response caching, Retry policies, Circuit Breaker, Rate 
limiting and throttling, Load balancing, Logging, tracing, IP allowlisting

#### Service Registry/Discovery Pattern

![What is Service Discovery Pattern?](./resources/362-service-discovery-pattern.png)

![How Does Service Discovery Work?](./resources/363-how-service-discovery-work.png)

#### Sidecar Pattern

![Sidecar Pattern](./resources/364-sidecar-pattern.png)

##### Drawbacks of Sidecar Pattern

- Increased complexity - Adding an additional layer of complexity to your deployment, more difficult to understand and troubleshoot issues that rise.
- Increased resource usage - Running an additional container in a pod will increase the resource usage of the pod. 
- Decreased performance - Pod can potentially decrease the performance of the pod, as the sidecar container will be competing for resources with the main container.
- Limited flexibility - Can be inflexible in some cases, as it requires that the main container and the sidecar container run in the same pod.

##### When to use Sidecar Pattern

- When you want to add functionality to an existing container image
- When you want to decouple the main container from the additional functionality
- When you want to run multiple containers in a pod that need to communicate with each other
- When you want to add common functionality to multiple microservices

#### Service Mesh Pattern

![Service Mesh Pattern Communication](./resources/370-service-mesh-communication.png)

##### What is Service Mesh Pattern?

- Service mesh pattern is managing the communication between microservices in a distributed system. 
- Designed to provide a uniform way to route traffic between microservices, handle load balancing, and monitor the healths.
- Consists of a set of proxy servers (sidecars) that are deployed alongside the microservices. Proxy servers handle the communication between the microservices.
- Responsible for tasks such as routing requests, load balancing, and monitoring the health of the system.
- Abstract away the complexities of managing communication between microservices. 
- Instead of having to manage these details at the application level, use the service mesh to handle them automatically. 
- Popular Service mesh implementations, including Istio and Linkerd, set up and manage a service mesh in a Kubernetes cluster.

##### When to use Service Mesh Pattern

- When you want to abstract away the complexities of managing communication between microservices.
- When you want to centralize the management of communication between microservices.
- When you want to add features such as load balancing, trafficmanagement, and monitoring to your microservices.
- Using a service mesh, can add these features to your microservices without having to modify the microservices themselves.
- Service mesh pattern is a useful tool for managing the communication between microservices in a distributed system. 
- Build and maintain complex microservices-based systems by abstracting away the complexities of managing communication between the microservices.

##### Service Mesh Communication

- Minimizing inter-service communication is ideal.
- Microservice communication: synchronous HTTP and gRPC communication and asynchronous messaging. 
- Service mesh is a dedicated infrastructure layer that manages communication between microservices. 
- Secure the interactions between services without adding complexity to the individual microservices themselves.
- Built-in capabilities for service-to-service communication, resiliency, and handling cross-cutting concerns.
- Communication concerns is moved out of the microservices and into the service mesh layer. 
- This abstraction allows communication to be handled independently from your microservices.
- Support for service discovery and load balancing. Handles traffic management, communication, and networking concerns.

### Design & Implement our E-Commerce application with Communication Tools

#### Communication Tools

- Service Proxy (envoy, nginx, haproxy)
- API Gateway (Kong, krakend, kubeGateway)
- Service Meshes (istio, linkerd, kuma)

#### Examples

AWS Service Proxy - AWS App Mesh is a service mesh that uses Envoy as a proxy, providing application-level networking for microservices on AWS.

AWS API Gateway - AWS API Gateway is a fully managed service for creating, publishing, and managing APIs. It handles features like authentication, rate limiting, and caching.

AWS Service Meshes - AWS App Mesh can also be considered a managed service mesh, as it provides observability, traffic control, and security for microservices running on AWS.

Azure Service Proxy - Azure API Management can act as a service proxy, providing API gateway functionality with load balancing, rate limiting, and caching.

Azure API Gateway - Azure API Management is a fully managed service for creating, publishing, and managing APIs. It provides features like authentication, rate limiting, caching, and monitoring.

Azure Service Meshes - Azure Service Fabric Mesh is a fully managed service mesh for building and deploying microservices. It provides features like service discovery, load balancing, and communication security. Additionally, AKS supports the integration of Istio, Linkerd, and other service meshes.

Istio dominates Service meshes - Service meshes provide service discovery, load balancing, timeouts, and retries, and allow administrators to manage the cluster's security and monitor its performance.

#### Service Mesh

Microservices Architecture

- App should be split into small, independent, and loosely coupled services, each responsible for a specific functionality.
- Allow to develop, deploy, and scale these services independently.

Containerization

- Package your microservices into lightweight containers, which can be easily deployed and managed in a cloud-native environment.

Container Orchestration

- Kubernetes to manage the deployment, scaling, and operation of your containerized microservices. Kubernetes provides features like self-healing, load balancing, and rolling updates.

Istio Service Mesh

- Implement Istio as a service mesh to provide a uniform way to connect, secure, control, and observe microservices. Istio with Envoy proxies, handle traffic management, security, and observability without modifying app code.

#### Ingress

Traffic Management

- Use Istio's traffic management capabilities: load balancing, request routing, and fault injection, to ensure services are highly available and resilient.

Security

- Leverage Istio's built-in security features, including mutual TLS, authorization policies, and authentication, to secure communication between microservices.

Observability

- Utilize Istio's observability features, like distributed tracing, monitoring, and logging, to gain insights into application's performance and identify issues.

Ingress Gateway

- Use an Istio ingress gateway to manage incoming traffic to application, enabling to define routing rules and expose services to the outside world.

#### Design Serverless E-commerce Microservices Architecture with AWS API Gateway, AWS App Mesh

![Design with AWS API Gateway, AWS App Mesh](./resources/385-AWS-serverless-design.png)

[GitHub Repo](https://github.com/awsrun/aws-microservices)

[Udemy Course](https://www.udemy.com/course/aws-serverless-microservices-lambda-eventbridge-sqs-apigateway/)

##### Managed API Gateway

Microservices Architecture

- Design app as a collection of small, independent, and loosely coupled microservices, each responsible for a specific functionality.

Serverless Compute

- Use serverless compute services like AWS Lambda, Azure Functions, or Google Cloud Functions to host microservices.
- Automatically manage the scaling, patching, and capacity planning of microservices, allows to focus on application logic.

Managed API Gateway

- Utilize managed API Gateway services: Amazon API Gateway, Azure API Management or Google Cloud API Gateway to expose microservices.
- Provide features like request routing, authentication, caching, throttling, and monitoring.

Managed Service Mesh

- Use managed service meshes like AWS App Mesh, Google Cloud Traffic Director, or Azure

Service Mesh

- Integrate with serverless compute services and provide features like traffic routing, load balancing, and end-to-end encryption.

##### Observability

Event-driven Architecture

- Design microservices to be event-driven and use managed services like Amazon EventBridge, Azure Event Grid, or Google Cloud Pub/Sub for asynchronous communication between services.

Data Storage

- Use managed serverless storage services: Amazon DynamoDB, Azure Cosmos DB, or Google Cloud Firestore to store application data.
- Provide low-latency, scalable, and fully managed data storage options.

Security

- Leverage managed Identity and Access Management (IAM) services provided by the cloud providers to control access to microservices and ensure secure communication.

Observability

- Use managed monitoring, logging, and tracing services: Amazon CloudWatch, Azure Monitor, or Google Cloud Operations Suite to gain insights into application's performance and troubleshoot issues.

#### Deploy Microservices to Kubernetes with Service Mesh Istio and Envoy

![Service Mesh Istio and Envoy](./resources/389-ISTIO-ENVOY.png)

![Deploy Microservices to Kubernetes with Service Mesh Istio and Envoy](./resources/390-ISTIO-ENVOY.png)

## Pillar 5: Backing Services - Data Management, Caching, Message Brokers

- What are Cloud-Native Backing Services?
- Which Backing Services for Cloud-Native Microservices?
- How microservices use Backing Services in Cloud-Native environments?
- What are patterns & best practices of Backing Services in Cloud-native apps?
- Implement Hands-on Development of Backing Services in Cloud-native microservices

### Backing Services

- K8s Databases (MySQL, Postgres), Serverless Databases
- File storage (NFS, FTP, Cloud Filestore)
- Message Brokers, Enterprise Message Queues, (Kafka, EventBridge)
- Event Streaming Services
- Object Storage: S3, Azure Blob Storage
- AUthenticcation, Authorization Services
- Security Identity Management: AWS IAM, Azure AD
- API Gateways
- Service Meshes
- Distributed Caches, Serverless Caching
- Monitoring and Analytic tools
- Security Identity Services
- Logging services (syslog endpoints, Cloud Logging)
- Search Analytics

Distributed Databases

- Popular databases used in cloud-native apps include relational databases (PostgreSQL, MySQL), NoSQL databases (MongoDB, Cassandra), and in-memory databases (Redis)
- Managed relational databases like Amazon RDS and Google Cloud SQL
- NoSQL databases like Amazon DynamoDB Azure Cosmos DB, and Google Cloud Firestore
- NewSQL or Cloud-native Databases are modern relational database management systems that aim to combine the best features of traditional SQL databases and NoSQL databases. Examples are:  Google Spanner, CockroachDB, VoltDB, TiDB and Vitess

Messaging and Eventing Systems

- Enable asynchronous communication between microservices, allowing to exchange messages or events. This improves scalability and decouples microservices
- Message Brokers such as Apache Kafka, NATS, and RabbitMQ are popular for building event-driven architectures in cloud-native applications
- Cloud Message Brokers - Amazon SQS/SNS, EventBridgeGoogle Cloud Pub/Sub, and Azure Service Bus, Azure Event Hubs

Messaging and Event Streaming

- Managed cloud messaging and event streaming services like Amazon SQS, Amazon SNS, Amazon Kinesis, Azure Service Bus, Azure Event Hubs, or Google Cloud Pub/Sub enable asynchronous communication between microservices

Object Storage

- Managed object storage services: Amazon S3, Azure Blob Storage, or Google Cloud Storage provide highly scalable, durable, and cost-effective storage options for storing and retrieving unstructured data like images, videos

Districtured Caches

- Store frequently accessed data to improve microservice performance and reduce resource-intensive operations like database queries. Redis and Memcached are popular caching systems
- Managed caching services like Amazon ElastiCache, Azure Cache for Redis, or Google Cloud Memorystore provide in-memory data storage for faster data access and reduced latency, improving the performance of microservices

Authentication and Authorization Services

- User authentication and access control management for microservices. Examples include OAuth 2.0, OpenID Connect, and LDAP

Security and Identity Management

- Managed Identity and Access Management (IAM) services provided by the cloud providers help control access to your microservices, secure communication, and manage authentication and authorization

API Gateways

- Load balancing, authentication, rate limiting, and request routing. Examples include Kong, Ambassador, and Amazon API Gateway
- Managed API Gateway services like Amazon API Gateway, Azure API Management, or Google Cloud API Gateway provide request routing, authentication, caching, throttling, and monitoring for your microservices apis

Service Meshes

- Infrastructure layer for managing and controlling communication between microservices, handling tasks like load balancing, service discovery, and observability. Examples of service meshes include Istio, Linkerd, and Kuma

Logging, Monitoring, and Tracing

- Collect, store, and analyze logs and metrics for microservices, helping developers and operators gain insights into performance and troubleshoot issues. Examples Elasticsearch, Logstash, Kibana (ELK stack), Prometheus, and Grafana
- Managed services like Amazon CloudWatch, Azure Monitor, or Google Cloud

Search and Analytics

- Process, analyze, and search large datasets. Elasticsearch is a popular choice for this purpose
- Managed search services like Amazon Elasticsearch, Azure Cognitive Search, or Google Cloud Search provide indexing and searching capabilities for microservices

#### Databases

How to Choose a Database for Microservices?

- Data Consistency Level: Do we need Strict-Strong consistency or Eventual consistency?
- Do we need ACID compliance? Should follow Eventual consistency in microservices to gain high scalability and availability.
- Fixed or Flexible Schema Choise, Predictable or Dynamic Data: Are we work with fixed or flexible schema that need to change frequently, dynamically changed data?
- Are we have Predictable Data or Dynamic Data?
- High or Low Data Volume, Predictable or Un-predictable Data: Are we work with High Volume Data or Low Volume Data?
- Can we have predictable data that we store our microservices database?
- NoSQL Databases prioritize partition tolerance that handling large amount of data or data coming in high velocity
- Read Requirements, Relational or non-Relational Data, Complex Join Queries: Our data is highly structured and requires referential integrity or not required for relationships that is dynamic and frequently changes?
- Should it work with complex queries, table joins and run SQL queries on normalized data models or Retrieve data operations are simple and performs without table joins?
- Deployments, Centralized or De-centralized Structure: Do we deployed to large and one or few locations with centralized structure? or Do we need to deploy and replicate data across different geographical zones?
- High Performance Requirements: Do we need to achieve fast read-write performance?
- High Scalability Requirements: Do we need High Scalability Requirements both vertical and horizontally scaling?
- To accomodate millions of request should sacrifice strong consistency
- High Availability and Low Latency Requirements: Do we need High Availability and Low Latency Requirements that need to separate data across different geographical zones?
- Can we provide ALL OF THESE FEATURES at the same time? Is it possible to provide High Scalability, High Availability and Low Latency with High Performance and able to run Complex Join Queries providing with ACID principles strong data consistency? New SQL Databases offer these features

#### NewSQL Databases: Compare to SQL and NoSQL

| Distinguishing Feature | SQL                 | NoSQL     | NewSQL          |
|------------------------|---------------------|-----------|-----------------|
| Relational             | Yes                 | No        | Yes             |
| ACID                   | Yes                 | No        | Yes             |
| SQL                    | Yes                 | No        | Yes             |
| OLTP                   | Not fully Supported | Supported | Fully Supported |
| Horizontal Scaling     | No                  | Yes       | Yes             |
| Query Complexity       | Low                 | High      | Very High       |
| Distributed            | No                  | Yes       | Yes             |

### Databases used in Ecommerce Microservice Application

![Sql NoSql databases / Ecommerce Microservice Application](./resources/458-sql-nosql-databases-ecommerce-microservice-application.png)

![NewSql databases / Ecommerce Microservice Application](/resources/459-newsql-databases-ecommerce-microservice-application.png)

![Cloud Serverless databases / Ecommerce Microservice Application](/resources/461-cloud-serverless-databases-ecommerce-microservice-application.png)

### Caching (K8s and Serverless Caching)

What are Cloud-Native Backing Services for Caching?
How microservices use Caching in Cloud-Native environments?
How to Choose a Caching for Microservices?
Which Caching should select for Backing Services for Cloud-Native Microservices?
What are patterns & best practices of using Cache in Cloud-native environments?

#### Types of Caching

In-memory cache

- Stores data in the main memory of a computer. In-memory caches are typically the fastest type of cache, but the data is lost when the cache is restarted or the machine is shut down.

Disk cache

- Stores data on a hard drive or solid-state drive. Disk caches are slower than in-memory caches, but they can persist data.

Distributed cache

- Cache is distributed across multiple machines and is typically used in distributed systems, such as microservices architectures.
- Distributed caches can improve the performance and scalability of a system by allowing data to be stored and accessed from multiple locations.

#### Cache Hit and Cache Miss

- Cache hit occurs when the requested data can be found in the cache.
- Cache miss occurs when the requested data is not in the cache and must be retrieved from a slower storage db.
- Cache hits are desirable because they can improve the performance of a system by reducing the number of requests.
- Cache misses can have a negative impact on performance, because they require additional time and resources to retrieve the requested data.
- The cache hit rate is a measure of how often a cache is able to fulfill requests from its own store.
- High cache hit rate indicates that the cache is effective at storing frequently accessed data.
- Low cache hit rate may indicate that the cache is not large enough.

#### Caching Strategies in Distributed Caching

There are several caching strategies that can be used in distributed microservices:

- Cache Aside Strategy: 
Client checking the cache for data before making a request to the backend service. When microservices needs to read data from the database, it checks the cache first to determine whether the data is available.
If the data is available (a cache hit), the cached data is returned. If the data isn’t available (a cache miss), the database is queried for the data.
The client will retrieve the data from the backend service and store it in the cache for future requests.
Data is lazy loaded into cache by client application.

- Read-Through Strategy: 
When there is a cache miss, it loads missing data from the database, populates the cache and returns it to the application.
When a client requests data that is not found in the cache, the cache will automatically retrieve the data from the underlying database and store it in the cache for future requests.
Cache-aside strategy, when a client requests data that is not found in the cache, the client is responsible for retrieving the data from the database.
Read-through cache strategy, when a client requests data that is not found in the cache, the cache will automatically retrieve the data from the database.
Cache always stays consistent with the database.

- Write-Through Strategy: 
Update the cache whenever data is written to the backend service. Cache always has the most up-to-date data, but it can also result in a higher number of write operations.
Instead of lazy-loading the data in the cache after a cache miss, the cache is proactively updated immediately following the primary database update.
Data is first written to the cache and then to the database.

- Write-Back or Write-Behind Strategy: 
Delays updating the cache until a later time. This reduce the number of write operations, but the cache may not have the most up-to-date data.
In Write-Through, the data written to the cache is synchronously updated in the main database.
In Write-Back or Write-Behind, the data written to the cache is asynchronously updated in the main database.

#### Cache-Aside Pattern for Microservices

- (1) When a client needs to access data, it first checks to see if the data is in the cache.
- (2) If the data is in the cache, the client retrieves it from the cache and returns it to the caller.
- (3) If the data is not in the cache, the client retrieves it from the database, stores it in the cache, and then returns it to the caller.

#### Microservices Distributed Caching apply Cache-Aside Pattern on API Gateway

![Microservices Distributed Caching apply Cache-Aside Pattern on API Gateway](./resources/506-distributed-cache.png)

#### Best Practices of using Cache in Cloud-native Apps

Understand Your Data
Use the right Caching Strategy
Set appropriate Cache Expiration - Cache Invalidation
Use Distributed Caching for Scalability and High Availability
Be careful of Data Consistency
Monitor and Optimize Cache Performance
Plan for Cache Failures
Test your Caching Strategy

#### Cloud-Native Distributed Caches

Distributed Caches

- Redis
- Memcached
- Hazelcast
- TiKV
- etcd

Cloud Serverless Caching Services

- Amazon ElastiCache
- Azure Cache for Redis
- Google Cloud Memorystore
- Upstash Redis

### Message Brokers (Event-driven communication)

What are Message Brokers in Event-driven communication?

Message brokers in event-driven communication are intermediary software that translate messages between different applications. They allow systems to communicate and share data without needing to know details about each other's design.

In an event-driven architecture, these brokers handle the distribution of messages (events) from event producers to event consumers. They can also provide additional features like message persistence, message routing, and load balancing.

Here's a simple pseudocode of how a message broker might work in an event-driven system:

An event producer sends a message to the broker.
The broker receives the message and stores it temporarily.
The broker determines which consumers are interested in this type of message.
The broker sends the message to all interested consumers.
In actual code, the implementation would depend on the specific message broker software being used, such as RabbitMQ, Apache Kafka, or AWS SQS.

#### What are some popular open-source message brokers for event-driven communication?

There are several popular open-source message brokers that are commonly used for event-driven communication. Here are a few:

RabbitMQ: This is a widely used open-source message broker that supports multiple messaging protocols.

Apache Kafka: Kafka is a distributed event streaming platform that is used for high-performance data pipelines, streaming analytics, data integration, and mission-critical applications.

ActiveMQ: This is a message broker written in Java together with a full Java Message Service (JMS) client.

NATS: A high performance, open-source messaging system. It is lightweight and easy to deploy.

Redis Pub/Sub: While not a full-fledged message broker, Redis has publish/subscribe capabilities that can be used for event-driven architectures.

Mosquitto: An open-source message broker that implements the MQTT protocol, a lightweight method of carrying out messaging using a publish/subscribe model.

EMQ X: An open-source MQTT message broker for IoT (Internet of Things) and Edge computing.

#### What are Cloud-Native Backing Services for Message Brokers?

Cloud-native backing services for message brokers are services that provide messaging capabilities and are designed to take full advantage of cloud computing frameworks. These services are scalable, resilient, and managed, meaning you don't have to worry about the underlying infrastructure.

Examples of such services include:

Amazon SQS (Simple Queue Service): A fully managed message queuing service that enables you to decouple and scale microservices, distributed systems, and serverless applications.

Google Cloud Pub/Sub: A global, real-time messaging service that allows you to send and receive messages between independent applications.

Azure Service Bus: A fully managed enterprise integration message broker. Service Bus can decouple applications and services.

RabbitMQ on CloudAMQP: CloudAMQP automates every part of setup, running, and scaling of RabbitMQ clusters.

Confluent Cloud (Apache Kafka as a Service): A fully managed, cloud-native Kafka service.

These services provide the benefits of message brokers without the need to manage the underlying infrastructure, making them a good fit for cloud-native applications.

#### What are some key considerations when choosing a message broker for event-driven communication in a cloud-native application?

When choosing a message broker for event-driven communication in a cloud-native application, consider the following key factors:

Scalability: The message broker should be able to handle the load as your application scales. It should be able to distribute messages efficiently even as the number of producers and consumers increases.

Durability: If your application requires that no messages are lost, you need a message broker that supports message persistence.

Delivery Guarantees: Depending on your use case, you might need at-least-once, at-most-once, or exactly-once delivery semantics.

Latency: If your application requires real-time communication, you need a message broker with low latency.

Throughput: The message broker should be able to handle the volume of messages your application will produce.

Integration: The message broker should integrate well with your existing systems and technology stack.

Support and Community: Consider the level of support available, either from a vendor or an open-source community.

Cost: Consider both the direct costs (licensing or usage fees) and indirect costs (development, operations, support).

Operational Complexity: Some message brokers might require significant effort to operate and maintain.

Security: The message broker should support the necessary security features like encryption and access control.

#### What are some popular cloud-native messaging patterns used in event-driven architectures?

There are several popular cloud-native messaging patterns used in event-driven architectures:

Publish/Subscribe (Pub/Sub): In this pattern, messages are sent to all subscribers, or to those that have subscribed to specific topics. This is useful for broadcasting information to multiple services.

Event Sourcing: This pattern involves storing all changes to application state as a sequence of events. These events can then be replayed to recreate the application state.

Command Query Responsibility Segregation (CQRS): This pattern separates read and update operations for a data store, which can be beneficial in complex business systems.

Message Queue: In this pattern, messages are stored on the queue until they are processed and deleted by a receiver. This is useful for handling asynchronous tasks.

Event-Driven Data Management: This pattern involves updating a database in response to events in the system. This can be used to keep multiple databases in sync.

Saga: This pattern is a sequence of local transactions where each transaction updates data within a single service. The Saga pattern coordinates and sequences these local transactions, and handles failure in any of them.

#### Streaming

Streaming is a pattern in message broker systems that is particularly useful when dealing with large volumes of data or real-time data processing. It fits into the broader category of publish/subscribe patterns, but with a focus on continuous data flow.

In a streaming pattern, producers continuously generate data and send it to the message broker. Consumers then process the data in real-time or near real-time. This is different from traditional message broker patterns where messages are discrete and often processed individually.

Apache Kafka is a popular message broker that supports the streaming pattern. It allows for real-time processing of data streams and can handle very high volumes of data. This makes it suitable for use cases like real-time analytics, log aggregation, and event sourcing.

- Data streaming, where a continuous flow of data is processed and often analyzed in real time.
- Use cases like real-time analytics, anomaly detection, and live leaderboards, among others.
- Apache Kafka, Amazon Kinesis, or Google Pub/Sub to manage these streams of data.
- Handle vast amounts of data in near-real time, necessary for microservices-based apps that require high-throughput and low-latency processing.

#### Messaging

Messaging is a fundamental part of message broker patterns. It's the method by which data is transferred between services in a distributed system. Here's how it fits into the overall pattern:

Producers: Services that generate messages are known as producers. A message could represent a variety of things, such as a new piece of data, an update to existing data, or a command that a service should perform an operation.

Message Broker: The message broker receives messages from producers and routes them to the appropriate consumers. The broker can also provide additional features like message persistence, message ordering, and backpressure management.

Consumers: Services that receive messages from the broker are known as consumers. They process the messages and perform any necessary actions.

Messaging Patterns: There are several patterns that can be used for messaging, including request/reply, publish/subscribe, and streaming. The best choice depends on the specific use case.

- Asynchronous communication between services.
- Decoupling between microservices, allowing them to interact without being directly connected or aware of each other.
- Messaging systems are often categorized into two types: message queues and publish/subscribe (pub/sub) systems.
- Message queues: messages are stored on the queue until they are processed and deleted by a receiver.
- Pub/sub systems: messages are sent to all subscribers, or to those that have subscribed to specific topics.
- Publish messages that are then consumed by other services, without the services needing to be aware of each other's existence.
- Greatly decouples services and allows them to evolve independently.
- Message brokers such as RabbitMQ, Apache Kafka, AWS SQS/SNS, and Google Pub/Sub, provide robust messaging capabilities.
- Use cases such as event notification, workflow processing, and decoupling services in a microservices architecture.

#### Design Checkout Shopping Cart Use Case with Message Broker using Kafka

1. A customer adds products to their shopping cart using the Cart-Service.
2. The Cart-Service publishes a CheckoutEvent to a Kafka topic. Event includes: customer ID, shopping cart ID, and the list of products in the cart.
3. The Order-Service is subscribed to this Kafka topic and consumes the CheckoutEvent. It creates a new order and reserves the products included in the shopping cart.
4. If successful, it publishes an OrderCreatedEvent to another Kafka topic and starts to Order fullfilment process.
5. In Order fullfilment process, Invetory, Shipment and Payment services consume this event and perform fullfilment actions.

![Design Checkout Shopping Cart Use Case with Kafka](./resources/564-kafka-checkout-shopping-cart-use-case.png)

## Pillar6: Scalability: Kubernetes Horizontal Pod Autoscaler(HPA) and KEDA

- What are Scalability? Why need to Scale?
- Vertical-Horizontal Scaling, VPA, HPA, Cluster Autoscaler
- Best Practices of Scaling Cloud-native Applications in Kubernetes
- KEDA Event-driven Autoscaling Cloud-native Applications in Kubernetes
- Cloud Serverless Scalability: AWS Fargate, Azure Container Apps, Google CloudRun, Knative, Openshift Serverless
- Hands-on: Cloud-Native Scalability(Vertical-Horizontal Scaling, VPA, HPA, Cluster Autoscaler, KEDA) on a Kubernetes Cluster

### Scaling Cloud-native Applications in Kubernetes

Vertical Scaling, Horizontal Scaling, Vertical Pod Autoscaler (VPA), Horizontal Pod Autoscaler (HPA), Cluster Autoscaler, Kubernetes Event-Driven Autoscaling (KEDA)

Vertical Scaling

- Increase the resources (CPU and memory) allocated to your app. This can be done by updating the resources field in your container specifications in your deployment configuration.

Horizontal Scaling

- Increase the number of instances (pods) of your app to handle more load. To manually scale the number of replicas, you can use the kubectl scale command or update the replicas field in your deployment configuration.

Vertical Pod Autoscaler (VPA)

- VPA adjusts the resources (CPU and memory) allocated to a pod based on its usage. Improve resource utilization and prevent overprovisioning or underprovisioning of resources.

Horizontal Pod Autoscaler (HPA)

- HPA automatically scales the number of pod replicas based on predefined resource utilization (CPU, memory) or custom metrics. To use HPA, create a HorizontalPodAutoscaler resource that targets your deployment and specifies the desired CPU utilization or custom metric thresholds.

#### Scaling Cloud-native Applications in Kubernetes - KEDA

- Cluster Autoscaler: Adjusts the size of the Kubernetes cluster based on the current resource utilization, adding or removing nodes as needed. It ensures that there are enough resources for your app to scale out while minimizing the number of underutilized nodes.
- Kubernetes Event-Driven Autoscaling (KEDA): KEDA is an extension to Kubernetes that provides event-driven autoscaling for applications. It can scale applications based on external events like the length of a message queue or the number of incoming HTTP requests.
- KEDA acts as an extension to the Kubernetes Horizontal Pod Autoscaler (HPA), which usually scales based on CPU and memory utilization metrics.
- KEDA is an open-source project that enables event-driven autoscaling for Kubernetes workloads.
- It allows you to scale your applications based on various event sources and custom metrics, such as message queue length or HTTP requests per second. It enables the deployment of serverless containers that can automatically scale based on events and external metrics.

![Kubernetes Event-Driven Autoscaling (KEDA) Scaling in Kubernetes](./resources/601-keda.png)

##### KEDA Architecture Components

- KEDA provides 2 main components: KEDA Operator and Metrics Server
- KEDA Operator: KEDA operator allows end-users to scale workloads in/out from 0 to N instances with support for Kubernetes Deployments, obs, StatefulSets or any custom resource that defines /scale subresource.
- Metrics Server: Metrics server exposes external metrics to Horizontal Pod Autoscaler (HPA) in Kubernetes for autoscaling purposes such as messages in a Kafka topic, or number of events in an Azure event hub.
- KEDA must be the only installed metric adapter.
- KEDA provides wide range of rich catalog of 50+ KEDA scalers.
- Goto Official Website - Kubernetes Event-driven Autoscaling: [https://keda.sh](https://keda.sh)

![Shopping Cart – Kafka – Ordering – KEDA Workflow](./resources/617-design-shopping-cart-use-case-scailing-of-order-microservice.png)

![Shopping Cart – Kafka – Ordering – Fargate Workflow](./resources/620-design-shopping-cart-use-case-scailing-of-order-microservice.png)

## Pillar7: Devops, CI/CD, IaC and GitOps

- What are Devops CI/CD?
- How Devops and CI/CD applied in Cloud-Native microservices?
- What are patterns & best practices of Devops and CI/CD in Cloud-Native microservices?
- What is Infrastructure as Code (IaC)?
- How IaC uses to create Kubernetes clusters?
- What is GitOps?
- How GitOps uses in Cloud-Native Microservices deployments?
- Explore Devops CI/CD tools
- Implement Hands-on labs for Devops and CI/CD in Cloud-Native Kubernetes cluster

### Terraform

What is Terraform

- Terraform is an open-source Infrastructure as Code (IaC) tool developed by HashiCorp.
- Define and provision data center infrastructure using a declarative configuration language known as HashiCorp Configuration Language (HCL), or optionally JSON. 
- Terraform can manage infrastructure across various cloud providers, as well as on-premises resources.
- Terraform enables you to safely and predictably provision and manage infrastructure in any cloud.
- Terraform is used primarily by DevOps teams to automate various infrastructure tasks like provisioning of cloud resources.

How Terraform uses in Cloud-native

- Provisioning Kubernetes Clusters: Write scripts to automate the creation of Kubernetes clusters across various cloud providers such as AWS (EKS), Azure (AKS), Google Cloud (GKE), or even on-premises.
- Managing Kubernetes Resources: Terraform has a Kubernetes provider which allows you to manage Kubernetes resources such as namespaces, deployments, and services.
- Microservices Deployment: Defining Kubernetes deployments and services in Terraform scripts, ensure microservices have the necessary configurations, scaling policies, and networking.
- Scaling and Auto-scaling: Define auto-scaling policies for your microservices.
- Integration with Continuous Delivery Pipelines: Terraform scripts can be integrated into CI/CD pipelines, allows for automated deployment and management of infrastructure and applications.
- State Management: Terraform maintains a state file which tracks the current state of the infrastructure. Crucial for managing configurations over time and is beneficial in a microservices env.

#### Terraform IaC Steps - How Terraform Works?

![Terraform IaC Steps - How Terraform Works](./resources/688-terraform.png)

### Design Microservices with DevOps,CI/CD, IaC and GitOps

#### Steps of DevOps,CI/CD, IaC and GitOps Architecture

1. Infrastructure Provisioning: Terraform configuration files to describe your AWS infrastructure including the EKS cluster, ECR repositories.
2. Set Up ArgoCD: Deploy ArgoCD in your EKS cluster. Configure ArgoCD to monitor your GitHub repositories for changes in Kubernetes manifests.
3. Develop Microservices: Writes code for the microservices and pushes to GitHub.
4. GitHub Actions Trigger: It builds a Docker image of the microservice.
Pushes the Docker image to ECR. Updates the Kubernetes manifest files with the new image tag.
5. ArgoCD Observes and Deploys: ArgoCD notices the changes in Kubernetes manifests. It pulls these manifest files. Deploys the updated microservice to the EKS cluster by applying the manifests.
6. Monitor and Observe: Set up monitoring and logging solutions like Prometheus and Grafana, AWS CloudWatch for observing the performance and health of your microservices

![Design Microservices with DevOps,CI/CD, IaC and GitOps](./resources/703-Github-Workflow-to-AWS-EKS-deployment.png)

## Pillar8: Monitoring & Observability with Distributed Logging and Tracing

- What are Monitoring Observability?
- What are best practices of Monitoring and Observability in CN microservices?
- How Monitoring and Observability applied in Cloud-Native microservices?
- What are Health Monitoring?
- How Health Monitoring uses in Kubernetes clusters?
- What are Distributed Logging-Tracing?
- How Distributed Logging-Tracing uses in Kubernetes clusters?
- Explore Tools: Prometheus, Grafana, ELK, Jeager
- Design our E-Commerce application with Monitoring and Observability
- Implement Hands-on labs for Monitoring and Observability in CN Kubernetes cluster with Prometheus, Grafana, ELK ...

### Monitoring & Observibility in Cloud-native Applications

Topics divided by 2 main and sub topics:

Monitoring

- System Infrastructure Monitoring: CPU, Memory usage
- Backing Services Monitoring: Redis, Postgres, RabbitMQ, Kafka…
- Application Monitoring: Health Checks, Performance and Business Metrics (APM, ABM)

Observability

- Distributed Logging
- Distributed Tracing
- Chaos Engineering

#### Why Monitoring Tools?

Complexity of Microservices Architecture

- Organizations increasingly adopt microservices so the complexity of managing and monitoring microservices grows.
- Communicate with one another over a network, and it’s crucial to understand the health and performance of these interactions.

Scalability and Dynamic Nature

- Containers can be spun up and down quickly, and applications can be scaled on demand.
- This dynamic nature requires sophisticated monitoring tools to ensure that the system is performing as expected and to catch any issues before they impact the users.

Troubleshooting and Root Cause Analysis

- In complex distributed system like K8s, understanding the root cause of an issue is challenging.
- Observability tools help in collecting data and metrics that can be critical in troubleshooting issues.

Compliance and Auditing

- In regulated industries, monitoring is essential not just for performance but for compliance reasons.
- They need to have detailed logs and metrics to satisfy regulatory requirements.

#### Key Components of Monitoring

Metrics

- Numerical data points that represent the state of a system at a point in time.
- Common metrics in microservices monitoring include request rate, error rate, response times, CPU usage, memory usage, and network throughput.

Logs

- Records of events that occur within an application or system.
- Invaluable when it comes to debugging and understanding the behavior of
microservices.

Traces

- Tracking requests useful in a microservices architecture where a single user request might involve multiple services.
- Understanding the flow of requests and identifying performance bottlenecks.

Alerting

- Receive alerts when something goes wrong or when certain thresholds are crossed.
- Notified of issues in real-time, allowing for quick resolution.

Visualization

- Creating dashboards that can display metrics, logs, and traces in a user-friendly manner.
- Gaining insights at a glance and makes it easier to understand the state of the microservices and the underlying infrastructure.

Health Checks

- Regularly checking the health of microservices is important.
- Involve simple checks to see if a service is up and running, or more complex checks like testing if a service can connect to a database.
- Three Types of Health Checks:
  - Liveness Checks
  - Readiness Checks
  - Performance Checks

Integration with CI/CD

- Monitoring should be integrated with CI/CD pipelines.
- Allows for a feedback loop where issues detected in monitoring can be addressed in continuous integration and deployment processes.

#### Cloud-Native Monitoring Tools

- Prometheus
- Grafana
- Dynatrace
- Datadog
- Cortex
- OpenMetrics

#### Cloud Serverless Monitoring tools

- Amazon CloudWatch and AWS X-Ray
- Google Stackdriver
- Microsoft Azure Monitor

### Prometheus

- Prometheus is an open-source monitoring and alerting toolkit designed for reliability and scalability.
- It collects metrics from configured targets at given intervals, evaluates rule
expressions, displays the results, and can trigger alerts when specified conditions are observed.
- Prometheus collects and stores its metrics as time series data, i.e. metrics information is stored with the timestamp at which it was recorded, alongside optional key-value pairs called labels.
- Prometheus is a monitoring platform that collects metrics from monitored targets by scraping metrics HTTP endpoints on these targets.
- Widely adopted in cloud-native environments and is especially popular for monitoring Kubernetes clusters and microservices.

#### Prometheus Features

- A multi-dimensional data model (time series defined by metric name and set of key/value dimensions)
- PromQL, a powerful and flexible query language to leverage this dimensionality
- No dependency on distributed storage; single server nodes are autonomous
- An HTTP pull model for time series collection
- Pushing time series is supported via an intermediary gateway for batch jobs
- Targets are discovered via service discovery or static configuration
- Multiple modes of graphing and dashboarding support
- Support for hierarchical and horizontal federation

#### How is Prometheus used in Cloud-native Kubernetes and Microservices?

Data Collection and Storage

- Scrapes metrics from instrumented jobs
- Stores scraped samples locally
- Runs rules over data for aggregation or alerts

Service Discovery

- Automatically discovers scraping targets in Kubernetes
- Adapts to dynamically scheduled services

Metrics Exposition

- Microservices expose metrics via HTTP endpoint (e.g. /metrics)
- Uses libraries like client_golang, client_java

Flexible Query Language (PromQL)

- Select and aggregate metric data
- Powerful for querying large number of services

Visualization

- Basic visualization via web UI
- Often used with Grafana for advanced dashboards

Alerting

- Define alert conditions based on Prometheus expressions
- Sends notifications through Alertmanager

Integration with Kubernetes

- Works with Kubernetes labels for flexible queries
- Aligns with Kubernetes metadata definitions

Use Cases

- Comprehensive observability of microservices in Kubernetes
- Integrated with Grafana for visualization and Alertmanager for notifications

![Prometheus Architecture](./resources/761-prometheus.png)

#### How Prometheus Works?

Setup and Configuration of Prometheus in Kubernetes

- Deploy Prometheus to your Kubernetes cluster.
- Configure Prometheus to discover and scrape metrics from your microservices.

Instrumenting Microservices

- Implement metric exposition in your microservices using Prometheus client libraries.
- Expose metrics through an HTTP endpoint (usually /metrics).

Service Discovery

- Use Prometheus’s Kubernetes service discovery to dynamically discover services to monitor.

Defining Recording Rules and Alerts

- Use recording rules to precompute frequently needed or computationally expensive expressions.

Querying Metrics with PromQL

- Use PromQL, Prometheus’s flexible query language, to query your metrics.

Visualizing Data

- Integrate Prometheus with Grafana.
- Setup dashboards in Grafana to visualize the metrics collected by Prometheus.

Setting up Alertmanager

- Configure Alertmanager to group, suppress, and route the alerts to the appropriate channel like email, Slack, PagerDuty, etc.

Gaining Insights and Taking Action

- Use the insights gained from monitoring to understand the behavior of your microservices.
- Take actions like scaling, optimizing, or troubleshooting based on the monitoring data.

![How Prometheus Works](./resources/762-prometheus.png)

### Distributed Logging and Tracing

Why Distributed Logging?

- Distributed apps are split into multiple independent services that can run on different containers, making the centralized collection and analysis of logs a fundamental requirement for efficient debugging, monitoring, and alert tracking.
- Distributed logging provides capturing, storing, and analyzing log data from various services that are distributed across multiple machines and containers.

Why Distributed Tracing?

- In a monolithic application, a traditional debugger enough to understand application performance. But in a distributed system, a request can criss-cross multiple services, making it challenging to diagnose performance bottlenecks or failures.
- When you have dozens of services interacting, understanding how a request traverses these services becomes crucial for effective debugging and optimization.
- Distributed tracing provides a method to track the journey of a request across various microservices.

#### Microservices Observability with Distributed Logging and Distributed Tracing

- Microservice have a strategy for monitoring and managing the complex dependencies on microservices
- Need to implement microservices observability with using distributed logging and tracing features.
- Microservices Observability gives us greater operational insight.
- Monitor and understand the behavior and performance of a system made up of microservices.
- Distributed Logging and Distributed Tracing are two key tools that improve observability in microservices.
- Distributed Logging is a practice of collecting, storing, and analyzing log data from multiple service instances.
- Behavior of the system over time, identifying patterns and trends, and troubleshooting issues.
Distributed Tracing is tracking the flow of requests through a microservices architecture, to see how the different service instances interact with each other.
- See the performance of the system, identifying bottlenecks, and troubleshooting issues.

![Real-world Example of Microservices Observability with Distributed Logging and Distributed Tracing](/./resources/771-distributed-logging-tracing.png)

#### Cloud-Native Distributed Logging-Tracing Tools

Cloud-Native Distributed Logging tools

- Elasticsearch, Logstash, and Kibana (ELK Stack)
- Fluentd & Fluent Bit
- Loki
- Graylog

Cloud-Native Distributed Tracing tools

- Jaeger
- Zipkin
- OpenTelemetry
- Lightstep

Cloud Serverless Logging-Tracing tools

- Amazon CloudWatch Logs and AWS X-Ray
- Google Stackdriver and Cloud Trace
- Azure Monitor Logs and Azure Application Insights

#### Elastic Stack for Microservices Observability with Distributed Logging

![Elastic Stack for Microservices Observability with Distributed Logging](./resources/775-ELK-stack.png)

Why logging with ElasticSearch and Kibana in Microservices?

- Logging is critical to implement to identify problems in distributed architecture.
- Elastic Stack used to collect, store, and analyze log data from multiple service instances in a microservices architecture.
- Useful for understanding the behavior of the system over time, identifying patterns and trends, and troubleshooting issues.
- Elastic Stack might be used to collect log data from each service instance.
- Kibana to visualize and analyze the data in order to identify  patterns or trends.

![Real-world Example with ElasticStack](./resources/777-ELK-stack.png)

#### Distributed Tracing with OpenTelemetry using Zipkin

- Distributed Tracing is used to track the flow of a request as it is processed by different microservices in a system.
- Different microservices are interacting and identify issues or bottlenecks and troubleshooting issues in the system.
- OpenTelemetry is an open-source project that provides a set of APIs for collecting and exporting telemetry data; traces, metrics, and logs.
- Zipkin is a distributed tracing system that collects and stores trace data from microservices, provides a web UI for viewing and analyzing trace data.
- To use OpenTelemetry with Zipkin for microservices distributed tracing, OpenTelemetry SDK would be integrated.
- Allows to collect trace data as requests flow through the system, and send data to a Zipkin server for storage and visualization.

##### Real-world Example of Distributed Tracing with OpenTelemetry using Zipkin

- Use OpenTelemetry with Zipkin to collect trace data as requests flow through the system.
- When a user adds an item to their shopping cart, the trace include information about the request.
- The trace data collected by the OpenTelemetry SDKs integrated into each service instance, and then sent to a Zipkin server for storage and visualization.
- Use the Zipkin UI to visualize and analyze the trace data, to identify patterns or trends.

![Real-world Example of Distributed Tracing with OpenTelemetry using Zipkin](./resources/779-opentelemetry-zipkin.png)

#### Design with Monitoring & Observibility Tools

Tools: Cloud-Native Monitoring & Observability

- Monitoring: Prometheus - Grafana
- Distributed Logging: Elasticsearch, Logstash, and Kibana (ELK Stack)
- Distributed Tracing: Jaeger

![Microservices with Prometheus, ELK Stack and Jeager](./resources/786-ELK-stack.png)

- Centralized Configuration: Use a tool like ConfigMap in Kubernetes or Spring Cloud Config for centralized configuration.
- Health Checks: Implement health check endpoints (/health or /status) for services. Integrate with Prometheus for alerting on service health.
- Alerting: Use Alertmanager (part of the Prometheus stack) to manage alerts based on predefined conditions in the metrics.
- Correlation IDs: Generate unique IDs at the entry points of your systems (like APIs) and pass these through service calls. This helps in correlating logs and traces across services.

