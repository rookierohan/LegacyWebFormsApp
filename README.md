# Legacy ASP.NET WebForms Modernization Assignment  
### Senior .NET Developer – Migration & Modernization Task  
### Estimated Time: 2–3 Days  
### Target Architecture: **.NET 8 MVC (Frontend) + .NET 8 Web API (Backend)**

---

## 📌 1. Introduction

You are provided with a legacy **ASP.NET WebForms (.NET Framework 4.8)** project named **LegacyWebFormsApp**.

This application uses:

- JSON file for **products**
- XML file for **users**
- XML file for **application settings**
- Inline business logic inside code-behind
- ViewState, Session, UpdatePanels
- Synchronous file I/O
- No layers, no DI, no separation of concerns

Your goal is to **modernize this application into .NET 8** using:

- **ASP.NET Core MVC (UI)**
- **ASP.NET Core Web API (.NET 8)**
- **New relational SQL database**
- **Clean architecture + best practices**

---

## ⚙️ 2. Running the Legac
- Visual Studio 2019/2022  
- .NET Framework 4.8 Developer Pack  
- Newtonsoft.Json NuGet package  

### Steps:
1. Open the solution in Visual Studio  
2. Set **LegacyWebFormsApp** as the startup project  
3. Run (`Ctrl + F5`)  
4. Navigate to: /Dashboard.aspx


### Pages:
- **Dashboard.aspx** – Combined metrics from JSON/XML  
- **Products.aspx** – JSON editing, ViewState, UpdatePanel  
- **Users.aspx** – XML loading, sorting, paging, Session filter  
- **Settings.aspx** – XML read/write  

---

## 🎯 3. Modernization Objective

You must build a **modernized .NET 8 solution** consisting of:

### 1️⃣ ModernApi — .NET 8 Web API  
### 2️⃣ ModernWebApp — .NET 8 MVC UI  
### 3️⃣ LegacyDataImporter — Data Migration Console App  
### 4️⃣ Database schema (SQL file)

The MVC application must consume the Web API for all data operations.

---

## 🧩 4. Requirements

### ✔ A) Migration Analysis Document (Mandatory)
A short (1–2 page) technical document explaining:

- Problems in the WebForms architecture  
- Technical debt & risks  
- Migration plan  
- Proposed new architecture  
- Technology choices  
- Architecture diagram  

---

## ✔ B) Database Requirements (Simplified)
You only need to submit a **schema.sql** file.

Your task:

- Design relational tables for **Products**, **Users**, and **Settings**
- Normalize (3NF recommended)
- Include:
  - Primary keys  
  - Data types  
  - Indexes (where needed)

Bonus (optional): ERD diagram

---

## ✔ C) ModernApi — .NET 8 Web API

Create a new **ASP.NET Core Web API** project.

### Required:
- Controller-based API (Products, Users, Settings)
- EF Core 8 (SQL database)
- DTOs (do not expose entities)
- AutoMapper (or manual mapping)
- Async/await everywhere
- Service + Repository layers
- Global exception handler middleware
- Swagger/OpenAPI
- Logging via ILogger
- Pagination & filtering support

### Minimum Endpoints:

#### Products

- GET /api/products
- GET /api/products/{id}
- POST /api/products
- PUT /api/products/{id}
- PATCH /api/products/{id}
- DELETE /api/products/{id}


#### Users

- GET /api/users
- GET /api/users/{id}


#### Settings

- GET /api/settings
- PUT /api/settings


---

## ✔ D) ModernWebApp — .NET 8 MVC (Frontend)

Create a new **ASP.NET Core MVC** project that consumes your API.

### Required features:
- MVC controllers + views (NO Razor Pages)
- Services that call Web API using HttpClient
- Strongly typed view models
- Centralized error handling
- Bootstrap UI (optional but recommended)

### Required pages:
#### **Dashboard**
- Show metrics:
  - Total products
  - Total users
  - Low-stock products
- Uses Web API

#### **Products**
- Paginated list
- Search
- Create
- Edit
- Delete

#### **Users**
- Paginated list
- Search/filter

#### **Settings**
- Load & update application settings

---

## ✔ E) LegacyDataImporter — .NET 8 Console App

A console app that:

- Loads `products.json`, `users.xml`, `settings.xml`
- Maps them to new DB entities
- Inserts into SQL database
- Includes a `--dry-run` option
- Logs operations
- Handles malformed data gracefully

---

## 📁 5. Required Repo Structure

Root/
│
├── LegacyWebFormsApp/ (Given)
│
├── ModernApi/ (.NET 8 Web API)
│
├── ModernWebApp/ (.NET 8 MVC UI)
│
├── LegacyDataImporter/ (.NET 8 Console App)
│
├── schema.sql (Database schema)
│
├── MigrationPlan.pdf (Architecture + analysis)
│
└── README.md


---

## 📊 6. Evaluation Criteria

| Category | Weight |
|---------|--------|
| API Architecture & Code Quality | 30% |
| MVC Application Design & UI | 30% |
| Data Migration Tool | 20% |
| Database Schema | 10% |
| Documentation & Clarity | 10% |

---

## 🧠 7. Notes for Candidates

- You may improve any part of the legacy logic.  
- You must follow clean architecture principles in .NET 8.  
- Do **NOT** place DB logic inside controllers.  
- Make reasonable assumptions and document them.  
- Focus on maintainability and structure, not pixel-perfect UI.

---

## 📤 8. Submission

Submit via:

- GitHub repository link **OR**  
- ZIP file containing the full solution

Make sure the solution builds without errors.

---

**Good luck — treat this as a real enterprise modernization project!**

