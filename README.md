# 📦 Shipping Request and Delivery System

A backend web application built using **ASP.NET Core** that enables merchants to request deliveries and delivery personnel to manage those deliveries. The system follows **Clean Architecture** principles and is fully containerized using **Docker**.

---

## 📁 Project Structure

The solution consists of four main projects:


### ✅ Architecture Pattern
This project applies **Clean Architecture**, which brings:
- Separation of concerns
- Independent business logic
- Easier testing and maintenance
- Scalability and better organization

---

## 🚀 Features

### 👤 Merchant
- Register and login
- Create delivery requests
- Provide package details:
  - Size
  - Weight
  - Delivery address

### 🚚 Delivery Person
- View assigned delivery requests
- Update delivery status:
  - Picked Up
  - Delivered

---

## 🔐 Authentication & Authorization

- **JWT** based authentication
- Role-based authorization:
  - `Merchant`
  - `DeliveryPerson`

---

## 🧱 Technologies Used

- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- Clean Architecture
- JWT Authentication
- Docker

---

## 📦 Main Components

### ✅ Controllers

- `AccountController.cs` – Register & login
- `RolesController.cs` – Role management
- `PackageController.cs` – Submit new delivery requests
- `DeliveryController.cs` – Manage assigned deliveries

### 🧠 Application Layer

- Uses **CQRS** style with separate Command and Query folders
- DTOs used for input/output across layers

### 🧾 Domain Layer

- Pure domain models (Entities and Enums)
- No dependencies on other layers

### 🏗️ Infrastructure Layer

- `CloudDbContext.cs` – EF Core database context
- Identity configuration
- Data persistence and services

---

## 🐳 Docker Support

### 📄 Dockerfile

A Dockerfile is already included to containerize the application.

