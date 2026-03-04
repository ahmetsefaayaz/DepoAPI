# DepoAPI 🚀

DepoAPI is a comprehensive backend API for warehouse and storage management. It is built adhering to Clean Architecture principles, ensuring a scalable, testable, and maintainable codebase. The entire infrastructure is fully dockerized for seamless deployment and development.

## 🛠 Tech Stack

* **Framework:** .NET / C#
* **Architecture:** Clean Architecture (Domain, Application, Infrastructure, Persistence, Presentation)
* **Relational Database:** PostgreSQL / SQL Server *(Hangisini kullandıysan burayı güncelleyebilirsin)*
* **Containerization:** Docker & Docker Compose
* **Testing:** xUnit & Moq *(Test yazdıysan ekleyebilirsin)*

---

## 🚀 Getting Started

You don't need to install any databases locally to run this project. Everything is containerized!

## Prerequisites

* [Docker Desktop](https://www.docker.com/products/docker-desktop/) installed and running.

## Installation & Run

1. Clone the repository:
```bash
git clone https://github.com/ahmetsefaayaz/DepoAPI.git
```
2. Open Projects root
   ```bash
   cd DepoAPI
   ```
## Spin up the infrastructure and the API:
```bash
docker-compose up -d --build
```

* The API will automatically apply EF Core migrations and seed the initial data.

### API Usage & Swagger
Once the containers are up and running, you can access the Swagger UI to test the endpoints:
* **Swagger URL**: http://localhost:8080/swagger

### Database Connections (For Local Inspection)

## PostgreSQL (Main Data & Identity)
* **Host:** localhost
* **Port:** 5433
* **Database:** CryptoFlowDb
* **Username:** postgres
* **Password:** mysecretpassword

### Admin Information
* **Email:** admin@gmail.com
* **Password:** _AdminPassword0

