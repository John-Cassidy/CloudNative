# Lecture Notes

## Create new xunit test project using bunit project template

Created xunit project in Visual Studio

NOTE: You can also create a new xunit test project that includes bunit

dotnet new bunit --framework xunit -o ProductServiceTests
dotnet sln .\CloudNative.sln add .\microservices\ProductServiceTests\ProductServiceTests.csproj
dotnet add .\ProductServiceTests.csproj reference ..\ProductService\ProductService.csproj

dotnet build ProductService.sln

## Automating build and test

[GitHub Action dotnet](https://docs.github.com/en/actions/automating-builds-and-tests/building-and-testing-net)

Next steps could be:

- Add a step to publish the project
- Add a step to deploy the project

## Customize Docker containers in Visual Studio

[Build/Debugging Containers in VS 2022](https://learn.microsoft.com/en-us/visualstudio/containers/container-build?view=vs-2022)
Learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

Create Dockerfile in csproj folder using Visual Studio Tools

```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["microservices/ProductService/ProductService.csproj", "microservices/ProductService/"]
RUN dotnet restore "./microservices/ProductService/./ProductService.csproj"
COPY . .
WORKDIR "/src/microservices/ProductService"
RUN dotnet build "./ProductService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ProductService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProductService.dll"]
```

Docker commands run by Visual Studio when you select 'Docker' debug

```powershell

docker build -f "C:\DEV\github.com\CloudNative\microservices\ProductService\Dockerfile" --force-rm -t productservice:dev --target base  --build-arg "BUILD_CONFIGURATION=Debug" --label "com.microsoft.created-by=visual-studio" --label "com.microsoft.visual-studio.project-name=ProductService" "C:\DEV\github.com\CloudNative"

docker run -dt -v "C:\Users\jpcas\vsdbg\vs2017u5:/remote_debugger:rw" -v "C:\Users\jpcas\AppData\Roaming\Microsoft\UserSecrets:/root/.microsoft/usersecrets:ro" -v "C:\Users\jpcas\AppData\Roaming\Microsoft\UserSecrets:/home/app/.microsoft/usersecrets:ro" -v "C:\Users\jpcas\AppData\Roaming\ASP.NET\Https:/root/.aspnet/https:ro" -v "C:\Users\jpcas\AppData\Roaming\ASP.NET\Https:/home/app/.aspnet/https:ro" -v "C:\Program Files\Microsoft Visual Studio\2022\Professional\MSBuild\Sdks\Microsoft.Docker.Sdk\tools\TokenService.Proxy\linux-x64\net6.0:/TokenService.Proxy:ro" -v "C:\Program Files\Microsoft Visual Studio\2022\Professional\MSBuild\Sdks\Microsoft.Docker.Sdk\tools\HotReloadProxy\linux-x64\net6.0:/HotReloadProxy:ro" -v "C:\Program Files\Microsoft Visual Studio\2022\Professional\Common7\IDE\CommonExtensions\Microsoft\HotReload:/HotReloadAgent:ro" -v "C:\DEV\github.com\CloudNative\microservices\ProductService:/app" -v "C:\DEV\github.com\CloudNative:/src/" -v "C:\Users\jpcas\.nuget\packages\:/.nuget/fallbackpackages" -e "ASPNETCORE_LOGGING__CONSOLE__DISABLECOLORS=true" -e "ASPNETCORE_ENVIRONMENT=Development" -e "DOTNET_USE_POLLING_FILE_WATCHER=1" -e "NUGET_PACKAGES=/.nuget/fallbackpackages" -e "NUGET_FALLBACK_PACKAGES=/.nuget/fallbackpackages" -P --name ProductService --entrypoint tail productservice:dev -f /dev/null
9545763849b964cb8df6fd933e230e8cdfe17420010ccf9d4b6ff5cdf7780fbe

docker rm -f 9545763849b964cb8df6fd933e230e8cdfe17420010ccf9d4b6ff5cdf7780fbe

```

## Customize Docker containers in VS Code

Create Dockerfile in csproj folder in VS Code

```dockerfile
# Use the official image as a parent image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory
WORKDIR /source

# copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# copy and publish app and libraries
COPY . .
RUN dotnet publish -o /app

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .

# Make sure the app binds to port 80
ENV ASPNETCORE_URLS http://*:80
EXPOSE 80

#set the entry point to the application
ENTRYPOINT ["dotnet", "ProductService.dll"]
```

Docker commands:

Create Docker Image for Product Microservices

Build Docker Image

Run command at same directory level of Dockerfile

```powershell
# Run command at same directory level of Dockerfile
docker build -t productservice .
#See a list of all images available on your machine
docker images
# same command exclude any lines that contain the word "k8s" and 'docker'
docker images --format "{{.Repository}}:{{.Tag}}" | Where-Object {$_ -notmatch 'k8s' -and $_ -notmatch 'docker'}

#Run Docker image: run detached mode (-d)
docker run -d -p 8080:80 --name productservicecontainer productservice
#Another good approach to run docker image:
#This command tells Docker to run a container in -it interactive mode
docker run -it --rm -p 8080:80 --name productservicecontainer productservice


#Check if the container is running by executing:
docker ps

#Open your browser and navigate to
http://localhost:8080/api/products

#Stop container
docker stop productservicecontainer
#Remove container
docker rm productservicecontainer
#Delete image
docker rmi -f productservice:latest

```

## Docker Vulnerability Checks

```powershell

#View a summary of image vulnerabilities and recommendations →
docker scout quickview local://productservice:latest
#View vulnerabilities →
docker scout cves local://productservice:latest
#View base image update recommendations →
docker scout recommendations local://productservice:latest
#Include policy results in your quickview by supplying an organization →
docker scout quickview local://productservice:latest --org <organization>
```

## Docker Learning about building .NET container images

[Learn about building .NET container images:](https://github.com/dotnet/dotnet-docker/blob/main/samples/README.md)

## Push to Docker Hub

> Step 1: Log in to Docker Hub
> Open a terminal and run the following command to log in to Docker Hub:

docker login

Enter your Docker Hub username and password when prompted.

Username: xxxx
Password:
Login Succeeded

> Step 2: Tag your Docker image

Find the IMAGE ID of your local Docker image by running:
docker images

Tag your Docker image with your Docker Hub username and the desired repository name:
docker tag productservice jpcassidy/productservice:latest

See tagged image
docker images

> Step 3: Push the Docker image to Docker Hub
> Push the tagged Docker image to your Docker Hub repository:

docker push jpcassidy/productservice:latest

Check
https://hub.docker.com/
See
https://hub.docker.com/repository/docker/jpcassidy/productservice/general

## Configure and Deploy ProductService to AWS AppRunner

[Download and Configure AWS CLI](https://docs.aws.amazon.com/cli/latest/userguide/getting-started-install.html)

Check AWS CLI
aws --version

[Configure AWS CLI](https://docs.aws.amazon.com/cli/latest/userguide/cli-authentication-user.html#cli-authentication-user-get)
aws configure

- Secret Access Key
- Access Key ID
- AWS Region
- Output format

[AWS Configure Commands](https://docs.aws.amazon.com/cli/latest/userguide/cli-configure-files.html)

- aws configure list

![Deploy ProductService Container to 
AWS Apprunner](./resources/103_deploy-ProductService-container-to-AWS-AppRunner.png)

### Pushing Docker Image to Amazon Elastic Container Registry (ECR)

- Step 1: Create an Amazon ECR repository (name: productservice)
- Step 2: Authenticate Docker to your Amazon ECR registry

aws ecr get-login-password --region us-east-1 | docker login --username AWS --password-stdin xxxx.dkr.ecr.us-east-1.amazonaws.com/productservice

- Step 3: Build Docker image
  productservice:latest
- Step 4: Tag Docker image
  docker tag productservice:latest xxxx.dkr.ecr.us-east-1.amazonaws.com/productservice
- Step 5: Push your Docker image to Amazon ECR
  docker push xxxx.dkr.ecr.us-east-1.amazonaws.com/productservice:latest

- List images
  aws ecr list-images --repository-name productservice --output json --no-cli-pager

### Deploy ProductService Container to AWS Apprunner

- Step 1: Create a new service in AWS App Runner
- Step 2: Configure the container image
- Step 3: Configure the deployment settings
- Step 4: Review your configuration and create the service
- Step 5: Deploy to AWS App Runner that pull image from ECR

### Clear AWS Resources

- AWS App Runner
- AWS ECR and delete images
- Role – AppRunnerECRAccessRole

## Install Minikube and kubectl

- [Manually - Install Minikube](https://minikube.sigs.k8s.io/docs/start/)
  [Video - Install Minikube in Windows 10 via Docker Desktop](https://www.youtube.com/watch?v=u2684TT9XHo)
- [Install kubectl](https://kubernetes.io/docs/tasks/tools/install-kubectl/)
- Test to ensure the version of kubectl
  kubectl version -o json

### Can you run Kubernetes and MiniKube at same time on Docker Desktop on Windows 11

Yes, you can run both Kubernetes and Minikube at the same time on Docker Desktop on Windows 11. However, they will create separate Kubernetes clusters and won't interfere with each other.

Docker Desktop's Kubernetes feature and Minikube both create a local Kubernetes cluster on your machine. Docker Desktop's Kubernetes runs as part of the Docker Desktop application, while Minikube runs as a separate virtual machine or a Docker container, depending on the driver you choose.

Remember to manage your system resources (CPU, memory) wisely, as running both at the same time can be resource-intensive.

### How to install minikube on docker desktop on windows 11

To install Minikube on Docker Desktop on Windows 11, follow these steps:

1. Ensure Docker Desktop is installed and running.
2. Install Chocolatey, a package manager for Windows. Open PowerShell as an administrator and run:

```powershell
Set-ExecutionPolicy Bypass -Scope Process -Force; [System.Net.ServicePointManager]::SecurityProtocol = [System.Net.ServicePointManager]::SecurityProtocol -bor 3072; iex ((New-Object System.Net.WebClient).DownloadString('https://chocolatey.org/install.ps1'))
```

3. Install Minikube via Chocolatey:

```powershell
choco install minikube
```

4. Start Minikube:
   This command starts a Minikube cluster container using the Docker driver.

```powershell
minikube start --driver=docker

# Results
W1126 11:33:54.053549   15628 main.go:291] Unable to resolve the current Docker CLI context "default": context "default": context not found: open C:\Users\xxxx\.docker\contexts\meta\37a8eec1ce19687d132fe29051dca629d164e2c4958ba141d5f4133a33f0688f\meta.json: The system cannot find the path specified.
😄  minikube v1.32.0 on Microsoft Windows 11 Pro 10.0.22631.2787 Build 22631.2787
✨  Using the docker driver based on user configuration
📌  Using Docker Desktop driver with root privileges
👍  Starting control plane node minikube in cluster minikube
🚜  Pulling base image ...
💾  Downloading Kubernetes v1.28.3 preload ...
    > preloaded-images-k8s-v18-v1...:  403.35 MiB / 403.35 MiB  100.00% 70.39 M
    > gcr.io/k8s-minikube/kicbase...:  453.90 MiB / 453.90 MiB  100.00% 51.02 M
🔥  Creating docker container (CPUs=2, Memory=16300MB) ...
🐳  Preparing Kubernetes v1.28.3 on Docker 24.0.7 ...
    ▪ Generating certificates and keys ...
    ▪ Booting up control plane ...
    ▪ Configuring RBAC rules ...
🔗  Configuring bridge CNI (Container Networking Interface) ...
🔎  Verifying Kubernetes components...
    ▪ Using image docker.io/kubernetesui/dashboard:v2.7.0
    ▪ Using image docker.io/kubernetesui/metrics-scraper:v1.0.8
    ▪ Using image gcr.io/k8s-minikube/storage-provisioner:v5
💡  Some dashboard features require the metrics-server addon. To enable all features please run:

        minikube addons enable metrics-server

🌟  Enabled addons: storage-provisioner, default-storageclass
🏄  Done! kubectl is now configured to use "minikube" cluster and "default" namespace by default
```

5. Verify the installation:

```powershell
minikube status
```

6. Check MiniKube Cluster

```powershell
kubectl cluster-info
kubectl get po -A
```

NOTE: if you have Kubernetes already installed in Docker-Desktop follow these instructions to set minikube configuration in docker cli

How to set kubectl to use minikube configuration in docker cli?

To set kubectl to use Minikube's configuration in Docker CLI, you can use the minikube kubectl command or set the Kubernetes context to Minikube. Here's how you can do it:

To use minikube kubectl, you can run your commands like this:

```powershell
minikube kubectl -- get po -A
```

7. To set the Kubernetes context to Minikube, use the following command:

```powershell
kubectl config use-context minikube
# TO CHANGE BACK TO docker-desktop
kubectl config use-context docker-desktop
```

8. See dashboard

```powershell
minikube dashboard

#Results
W1126 12:11:45.207027    2436 main.go:291] Unable to resolve the current Docker CLI context "default": context "default": context not found: open C:\Users\jpcas\.docker\contexts\meta\37a8eec1ce19687d132fe29051dca629d164e2c4958ba141d5f4133a33f0688f\meta.json: The system cannot find the path specified.
🔌  Enabling dashboard ...
    ▪ Using image docker.io/kubernetesui/metrics-scraper:v1.0.8
    ▪ Using image docker.io/kubernetesui/dashboard:v2.7.0
💡  Some dashboard features require the metrics-server addon. To enable all features please run:

        minikube addons enable metrics-server


🤔  Verifying dashboard health ...
🚀  Launching proxy ...
🤔  Verifying proxy health ...
🎉  Opening http://127.0.0.1:63420/api/v1/namespaces/kubernetes-dashboard/services/http:kubernetes-dashboard:/proxy/ in your default browser...
```

After running this command, kubectl commands will interact with the Minikube cluster.

#### Stop and Delete MiniKube

This should delete the .minikube and .kube directories usually under:
C:\users\{user}\.minikube
C:\users\{user}\.kube

```powershell
minikube stop
minikube delete
choco uninstall minikube
choco uninstall kubectl
```

## Kubernetes Configuration Best Practices for Containers

### ASP.NET Container Expose Port - CONFIGURE TO LISTEN - 0.0.0.0:8080

#### CONFIGURE Container Expose Port TO LISTEN - 0.0.0.0:8080

- Edit Program.cs

```csharp
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<List<Product>>();

	// Add the following for Kubernetes Deployment
	var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
	var url = $"http://0.0.0.0:{port}";
	builder.WebHost.UseUrls(url);

var app = builder.Build();
```

- dockerfile > [add ENV varible into DockerFile](https://github.com/dotnet/dotnet-docker/issues/3968)

```dockerfile
"environmentVariables": {
"ASPNETCORE_URLS": "https://+:443;http://+:80",
"ASPNETCORE_HTTPS_PORT": "44360"
},
```

- bash - [k8s deployment ENV variable inject](https://www.youtube.com/watch?v=63FLcPHUCPM)

```bash
kestrel**endpoints**http\_\_url
http://0.0.0.0:80
```

#### Build and Deploy Docker Image to Docker Hub

Build Image

```powershell
docker build -t productservice .
```

Run Docker Image
You can run your app in a container using the following command:

```powershell
docker run -d -p 8080:80 --name productservicecontainer productservice
```

Hit Endpoint:
http://localhost:8080/api/products

Tag your Docker image with your Docker Hub username and the desired repository name:

```powershell
docker tag productservice jpcassidy/productservice:latest
```

Push the tagged Docker image to your Docker Hub repository:

```powershell
docker push jpcassidy/productservice:latest
```

### Create POD

#### While you can create standalone Pods on Kubernetes, it is not recommended

because Pods are the lowest-level abstraction in Kubernetes.
▪ Lack of self-healing
If a Pod fails, is terminated, or becomes unhealthy, it will not be automatically
replaced. In contrast, higher-level abstractions like Deployments automatically
manage the desired number of replicas and replace any failed Pods.
▪ No scaling support
Need to manually create and manage multiple Pod YAML files to scale your
application. Deployments make scaling easy by allowing you to simply update
the desired number of replicas.
▪ Should make the Pods Resilient with Deployments

#### Creating a Pod Definition product-pod.yaml

product-pod.yaml

> Open 2. terminal to watch created pods on K8s

    kubectl get pods -w

> Apply the configuration

    kubectl apply -f product-pod.yaml

> See Watch

kubectl get pods -w
NAME READY STATUS RESTARTS AGE
my-app-pod 0/1 Pending 0 0s
my-app-pod 0/1 Pending 0 0s
my-app-pod 0/1 ContainerCreating 0  
my-app-pod 1/1 Running 0

> kubectl get pod
> NAME READY STATUS RESTARTS AGE
> my-app-pod 1/1 Running 0 63s

> Expose the Pod

    kubectl port-forward product-pod 8080:8080

Forwarding from 127.0.0.1:8080 -> 8080
Forwarding from [::1]:8080 -> 8080

You can now access the microservice at http://localhost:8080.

> SEE DEPLOYED MICROSERVICE ON K8S WITH POD

http://localhost:8080/api/products

> Stop Pod

    CTRL+C

> Clean Up

kubectl delete pod product-pod
or
kubectl delete -f .\product-pod.yaml

#### Create and Apply Deployment on Kubernetes k8s/product-deploy.yaml

product-deploy.yaml

Open 2. terminal to watch created pods on K8s
kubectl get pods -w

> Apply the configuration

    kubectl apply -f product-deploy.yaml

> See Watch

kubectl get pods -w
NAME READY STATUS RESTARTS AGE
my-app-pod 0/1 Pending 0 0s
my-app-pod 0/1 Pending 0 0s
my-app-pod 0/1 ContainerCreating 0  
my-app-pod 1/1 Running 0

> kubectl get pod
> NAME READY STATUS RESTARTS AGE
> my-app-pod 1/1 Running 0 63s

> > Expose the Pod

#### Create and Apply a Service in Kubernetes k8s/product-service.yaml

roduct-service.yaml

> Apply the configuration

```powershell
kubectl apply -f product-service.yaml
```

> See all

```powershell
kubectl get all
NAME                           READY   STATUS    RESTARTS   AGE
pod/product-7b7c849898-chzr6   1/1     Running   0          17m
pod/product-7b7c849898-hgkzm   1/1     Running   0          17m
pod/product-7b7c849898-kwrs6   1/1     Running   0          17m

NAME                      TYPE           CLUSTER-IP      EXTERNAL-IP   PORT(S)          AGE
service/kubernetes        ClusterIP      10.96.0.1       <none>        443/TCP          22h
service/product-service   LoadBalancer   10.98.131.168   <pending>     8080:31677/TCP   4m57s

NAME                      READY   UP-TO-DATE   AVAILABLE   AGE
deployment.apps/product   3/3     3            3           17m

NAME                                 DESIRED   CURRENT   READY   AGE
replicaset.apps/product-7b7c849898   3         3         3       17m
```

> Notice the service/product-service --type argument has a value of LoadBalancer.
> This service type is implemented by the cloud-controller-manager which is part of the Kubernetes control plane.

> Expose the Service
> To invoke our pod with this service definition, we need a tunnel for K8s LoadBalancer IP.

```powershell
kubectl port-forward service/product-service 7080:8080
```

> SEE RESULT

http://127.0.0.1:7080/api/products

> Expose Service with Minikube

First close port-forward service - CTRL+C
Run the following command to expose the service with Minikube:

```powershell
minikube service product-service

|-----------|-----------------|-------------|---------------------------|
| NAMESPACE |      NAME       | TARGET PORT |            URL            |
|-----------|-----------------|-------------|---------------------------|
| default   | product-service |        8080 | http://192.168.49.2:31677 |
|-----------|-----------------|-------------|---------------------------|
🏃  Starting tunnel for service product-service.
|-----------|-----------------|-------------|------------------------|
| NAMESPACE |      NAME       | TARGET PORT |          URL           |
|-----------|-----------------|-------------|------------------------|
| default   | product-service |             | http://127.0.0.1:53668 |
|-----------|-----------------|-------------|------------------------|
🎉  Opening service default/product-service in default browser...
❗  Because you are using a Docker driver on windows, the terminal needs to be open to run it.
```

#### Combined Way of Creating Deployment and Services for Microservices - product.yaml

Apply the configuration

```powershell
kubectl apply -f .\product.yaml

deployment.apps/product unchanged
service/product-service unchanged
```

Delete and re-create your deployment and service objects with kubectl delete command

```powershell
kubectl delete -f .\product.yaml
```

#### Create Ingress for External Access of Microservice

No longer need a service type of LoadBalancer since the service does not need to be accessible from the internet.
It only needs to be accessible from the Ingress Controller (internal to the cluster)
so we can change the service type to ClusterIP.

Update your service.yaml file to look like this:

    Remove type
    Create Ingress into product.yaml file

We have created Ingress object and referring to our service object which is product-service.
and host address is product.local

> So we should add this dns address into our host file.

Update your hosts file (/etc/hosts on Linux and macOS or C:\Windows\System32\drivers\etc\hosts on Windows) to add the following line:

First get IP of minikube
minikube ip
192.168.49.2

> Add this line:

Added by Minikube Custom Domain
192.168.49.2 product.local

> Active Ingress addons into our minikube installment.

See all list
minikube addons list

Activate Ingress for our local minikube
minikube addons enable ingress

> Re-apply the app service manifest.
> Re-create all objects:

kubectl delete -f product.yaml
kubectl apply -f product.yaml

> See all
> kubectl get all

kubectl get ingress
NAME CLASS HOSTS ADDRESS PORTS AGE
product-ingress nginx product.local 192.168.49.2 80 3m29s

> Access dns adress

product.local

> SEE RESULT:
> http://product.local/api/products

list ingress pods:
kubectl get pods -n ingress-nginx

describe pod:
kubectl describe pod ingress-nginx-controller-7c6974c4d8-nlhqk -n ingress-nginx

view logs from ingress controller:
kubectl logs ingress-nginx-controller-7c6974c4d8-nlhqk -n ingress-nginx

#### Create ConfigMaps and Secrets for Microservice

Create a ConfigMap to store a basic configuration parameter, such as the log_level.
Create a file named log-level-configmap.yaml

GOTO
product.yaml

---

apiVersion: v1
kind: ConfigMap
metadata:
name: log-level-configmap
data:
log_level: "Information"

---

Create the Secret
use a Secret to create api-key secret inside the pod.
encode the secret value in base64:

> open bash on vscode

echo -n 'product-api-key' | base64
cHJvZHVjdC1hcGkta2V5

> Copy the output and create secret into product.yaml file

---

apiVersion: v1
kind: Secret
metadata:
name: api-key-secret
type: Opaque
data:
api_key: cHJvZHVjdC1hcGkta2V5

> > Update the deployment object to use the ConfigMap and Secret values as environment variables in the container:

--product.yaml

added below part into deployment:

env: - name: LOG_LEVEL
valueFrom:
configMapKeyRef:
name: log-level-configmap
key: log_level - name: API_KEY
valueFrom:
secretKeyRef:
name: api-key-secret
key: api_key

See that our pod injected 2 ENV Variables, 1 from configmap another from secret.

> > Apply the updated all Deployment:\k8s>

    kubectl apply -f .\product.yaml

    deployment.apps/product configured
    service/product-service unchanged
    ingress.networking.k8s.io/product-ingress unchanged
    configmap/log-level-configmap created
    secret/api-key-secret created

> Check

kubectl get secret
NAME TYPE DATA AGE
api-key-secret Opaque 1 27s

kubectl get configmap
NAME DATA AGE
kube-root-ca.crt 1 25h
log-level-configmap 1 40s

> > Modify .NET application to read these environment variables and use them as needed.
> > These are typical ENV variables that we can use into our application.

goto Program.cs

Show how to get these values:

var builder = WebApplication.CreateBuilder(args);

var logLevel = Environment.GetEnvironmentVariable("LOG_LEVEL");
var apiKey = Environment.GetEnvironmentVariable("API_KEY");

So we can ConfigureLogging as per these logLevel and add api key into controllers.

#### Scale a Container Instance in Kubernetes

Start with watch pods
kubectl get pod -w

Use the kubectl scale command to update the deployment with a number of pods to create.
kubectl scale --replicas=5 deployment/product

> See watch - new 2 pod creating
> product-deploy-5d5ccb7569-khfs6 0/1 ContainerCreating 0 0s
> product-deploy-5d5ccb7569-qkrzn 0/1 ContainerCreating 0 0s

> See latest pods

    kubectl get pod

> > if there's a failure Kubernetes will automatically restart the pods that were running before the failure.
> > Let's see this resilience in action by deleting pod and then verifying that Kubernetes has restarted it.

kubectl get pods
product-deploy-5d5ccb7569-khfs6 0/1 ContainerCreating 0 0s
product-deploy-5d5ccb7569-qkrzn 0/1 ContainerCreating 0 0s

> Delete the pod by using the kubectl delete command.
> kubectl delete pod product-5b6cc765c4-hjpx4

> see
> immediately stating the pod has been deleted.
> kubectl get pods

random string following the pod name has changed.
Indicating the pod is a new instance.

> > To scale the instance back down, run the following command.

    kubectl scale --replicas=1 deployment/product

See WATCH

product-deploy-5d5ccb7569-j9gvl 0/1 Terminating 0 2m26s
product-deploy-5d5ccb7569-khfs6 0/1 Terminating 0 78s
product-deploy-5d5ccb7569-khfs6 0/1 Terminating 0 78s
product-deploy-5d5ccb7569-khfs6 0/1 Terminating 0 78s
product-deploy-5d5ccb7569-4dcr8 0/1 Terminating 0 2m27s
product-deploy-5d5ccb7569-4dcr8 0/1 Terminating 0 2m27s
product-deploy-5d5ccb7569-4dcr8 0/1 Terminating 0 2m27s
product-deploy-5d5ccb7569-qkrzn 0/1 Terminating 0 78s
product-deploy-5d5ccb7569-qkrzn 0/1 Terminating 0 79s

Both of these approaches modify the running configuration
manually run kubectl scale
or change replica number.

Solution: Auto-Scaling

#### Kubernetes Deploy and Service with Minikube

> Start
> minikube start

> Interact with your cluster
> minikube dashboard

> Deploy applications
> Create a sample deployment and expose it on port 8080:
> kubectl create deployment hello-minikube --image=jpcassidy/productservice:latest
> kubectl expose deployment hello-minikube --type=NodePort --port=8080

> Check service
> The easiest way to access this service is to let minikube launch a web browser for you:

    kubectl get services hello-minikube

    minikube service hello-minikube

SEE - WORKED !
http://127.0.0.1:62162/api/products

> Alternatively, use kubectl to forward the port:

    kubectl port-forward service/hello-minikube 7080:8080

Your application is now available at http://localhost:7080/.

SEE- WORKED !
http://localhost:7080/api/products

#### Clean up Minikube resources

> 1
> Delete host address

    C:\Windows\System32\drivers\etc\hosts

> 2

    kubectl delete deployment my-app
    kubectl delete service my-app-service
    kubectl delete pod my-app-pod
    kubectl delete ingress my-app-ingress

Since we can't just delete the pods, we have to delete the deployments.
kubectl delete -f ./product.yaml

> 3
> Finally, stop Minikube with the command:

    minikube stop

## Helm Charts
