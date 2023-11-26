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
   This command starts a Minikube cluster using the Docker driver.

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
    ▪ Using image gcr.io/k8s-minikube/storage-provisioner:v5
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
