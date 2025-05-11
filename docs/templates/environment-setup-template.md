# FlowOrchestrator Environment Setup Guide

## Introduction

This guide provides detailed instructions for setting up the environment required to run the FlowOrchestrator system. It covers development, testing, and production environments.

## Development Environment

### Prerequisites

- Windows 10/11, macOS 11+, or Ubuntu 20.04+
- .NET SDK 6.0 or later
- Visual Studio 2022, Visual Studio Code, or JetBrains Rider
- Git
- Docker (optional, for containerized development)

### Setup Steps

1. **Install .NET SDK**

   Download and install the .NET SDK from the [official website](https://dotnet.microsoft.com/download).

   Verify the installation:

   ```bash
   dotnet --version
   ```

2. **Install an IDE**

   Choose one of the following:

   - [Visual Studio 2022](https://visualstudio.microsoft.com/vs/)
   - [Visual Studio Code](https://code.visualstudio.com/) with the [C# extension](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp)
   - [JetBrains Rider](https://www.jetbrains.com/rider/)

3. **Install Git**

   Download and install Git from the [official website](https://git-scm.com/downloads).

   Verify the installation:

   ```bash
   git --version
   ```

4. **Clone the Repository**

   Clone the FlowOrchestrator repository:

   ```bash
   git clone https://github.com/floworch/floworch.git
   cd floworch
   ```

5. **Build the Solution**

   Build the solution:

   ```bash
   dotnet build
   ```

6. **Run the Tests**

   Run the tests:

   ```bash
   dotnet test
   ```

7. **Set Up Local Configuration**

   Create a local configuration file:

   ```bash
   cp config/floworch.example.json config/floworch.local.json
   ```

   Edit the configuration file to match your local environment:

   ```bash
   nano config/floworch.local.json
   ```

8. **Run the Application**

   Run the application:

   ```bash
   dotnet run --project src/FlowOrchestrator.Api/FlowOrchestrator.Api.csproj
   ```

### Docker Development Environment

1. **Install Docker**

   Download and install Docker from the [official website](https://www.docker.com/products/docker-desktop).

   Verify the installation:

   ```bash
   docker --version
   ```

2. **Build the Docker Image**

   Build the Docker image:

   ```bash
   docker build -t floworch:dev .
   ```

3. **Run the Docker Container**

   Run the Docker container:

   ```bash
   docker run -d \
     --name floworch-dev \
     -p 5000:5000 \
     -v $(pwd)/config:/app/config \
     -v $(pwd)/data:/app/data \
     floworch:dev
   ```

4. **Verify the Container**

   Verify that the container is running:

   ```bash
   docker ps
   curl http://localhost:5000/health
   ```

## Testing Environment

### Prerequisites

- Windows Server 2019+, Ubuntu 20.04+, or macOS 11+
- .NET Runtime 6.0 or later
- Docker (optional, for containerized testing)
- CI/CD pipeline (optional, for automated testing)

### Setup Steps

1. **Install .NET Runtime**

   Download and install the .NET Runtime from the [official website](https://dotnet.microsoft.com/download).

   Verify the installation:

   ```bash
   dotnet --version
   ```

2. **Set Up Test Configuration**

   Create a test configuration file:

   ```bash
   cp config/floworch.example.json config/floworch.test.json
   ```

   Edit the configuration file to match your test environment:

   ```bash
   nano config/floworch.test.json
   ```

3. **Run the Tests**

   Run the tests:

   ```bash
   dotnet test
   ```

### Docker Testing Environment

1. **Pull the Docker Image**

   Pull the Docker image:

   ```bash
   docker pull floworch/floworch:test
   ```

2. **Run the Docker Container**

   Run the Docker container:

   ```bash
   docker run -d \
     --name floworch-test \
     -p 5000:5000 \
     -v $(pwd)/config:/app/config \
     -v $(pwd)/data:/app/data \
     floworch/floworch:test
   ```

3. **Run the Tests**

   Run the tests:

   ```bash
   docker exec floworch-test dotnet test
   ```

## Production Environment

### Prerequisites

- Windows Server 2019+, Ubuntu 20.04+, or macOS 11+
- .NET Runtime 6.0 or later
- Docker (optional, for containerized deployment)
- Kubernetes (optional, for orchestrated deployment)
- Load balancer (optional, for high availability)
- Database server (optional, for persistent storage)

### Setup Steps

1. **Install .NET Runtime**

   Download and install the .NET Runtime from the [official website](https://dotnet.microsoft.com/download).

   Verify the installation:

   ```bash
   dotnet --version
   ```

2. **Set Up Production Configuration**

   Create a production configuration file:

   ```bash
   cp config/floworch.example.json config/floworch.prod.json
   ```

   Edit the configuration file to match your production environment:

   ```bash
   nano config/floworch.prod.json
   ```

3. **Set Up the Database**

   [Detailed instructions for setting up the database]

4. **Set Up the Load Balancer**

   [Detailed instructions for setting up the load balancer]

5. **Deploy the Application**

   [Detailed instructions for deploying the application]

### Docker Production Environment

1. **Pull the Docker Image**

   Pull the Docker image:

   ```bash
   docker pull floworch/floworch:latest
   ```

2. **Run the Docker Container**

   Run the Docker container:

   ```bash
   docker run -d \
     --name floworch-prod \
     -p 5000:5000 \
     -v /etc/floworch:/app/config \
     -v /var/floworch/data:/app/data \
     floworch/floworch:latest
   ```

3. **Verify the Deployment**

   Verify that the container is running:

   ```bash
   docker ps
   curl http://localhost:5000/health
   ```

### Kubernetes Production Environment

[Detailed instructions for setting up a Kubernetes production environment]

## Environment Variables

The following environment variables can be used to configure the FlowOrchestrator system:

| Variable | Description | Default |
|----------|-------------|---------|
| FLOWORCH_ENV | Environment (Development, Testing, Production) | Development |
| FLOWORCH_CONFIG_PATH | Path to the configuration file | config/floworch.json |
| FLOWORCH_LOG_LEVEL | Log level (Debug, Information, Warning, Error) | Information |
| FLOWORCH_DATA_DIR | Data directory path | data |
| FLOWORCH_DB_CONNECTION | Database connection string | - |
| FLOWORCH_API_KEY | API key for authentication | - |

## Troubleshooting

[Common environment setup issues and their solutions]

## References

[List of references and related documentation]
