# CloudNative

## Description: Cloud-Native: Microservices, Kubernetes, Service Mesh, CI/CD

[Udemy Course](https://www.udemy.com/course/cloud-native-microservices-kubernetes-service-mesh-cicd/)

[GitHub Repository](https://github.com/mehmetozkaya/CloudNative)

[Medium Article](https://medium.com/design-microservices-architecture-with-patterns)

[Mehmet Ozkaya - Trainer's GitHub Site](https://github.com/mehmetozkaya)

NOTE: Moving Forward, a branch will be created cooresponding to a lecture where new or modified code is added to project. If applicable, the branch will be merged into main.

Example: lecture69 branch created to create microservices/ProductService.csproj

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

[Configuration Best Practices of Kubernetes](https://kubernetes.io/docs/concepts/configuration/overview/)
[Java, NodeJS containers not required, Asp.Net need explicit configure port on Dockerfile or code](https://github.com/dotnet/dotnet-docker/issues/3968)

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
- Create Ingress for External Access of Microservice
  When exposing a web application to the internet, you should use an Ingress.
  [Kubernetes documentation of Ingress](https://kubernetes.io/docs/concepts/services-networking/ingress/#what-is-ingress)
- Create ConfigMaps and Secrets for Microservice
