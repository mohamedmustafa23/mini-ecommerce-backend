# Mini E-Commerce System

A full-stack mini e-commerce application built with .NET 9 and Blazor.

## 🚀 Tech Stack
- **Backend:** ASP.NET Core Web API 9
- **Frontend:** Blazor Interactive Server
- **Database:** SQL Server (LocalDB)
- **ORM:** Entity Framework Core (Code First)

## 🛠️ Architecture Patterns
- **Generic Repository Pattern**
- **Unit of Work**
- **Specification Pattern**
- **Service Layer (Business Logic Separation)**
- **DTOs (Data Transfer Objects)**

## 📋 Implemented User Stories
- **US-01:** Create Product with validation (Price > 0, Stock >= 0).
- **US-02:** List Products with **Pagination** (API & Blazor).
- **US-03:** Create Order with **Stock Validation**.
- **US-04:** Dynamic Discount calculation (5% for 2-4 items, 10% for 5+ items).
- **US-05:** Get Order details with breakdown of subtotal, discounts, and final total.

##  How to Run

1. **Database Setup:**
   - Update `DefaultConnection` in `MiniEcommerce.API/appsettings.json` to point to your SQL Server.
   - Run `Update-Database` in the Package Manager Console (Default project: API).

2. **Run Application:**
   - Set both `MiniEcommerce.API` and `MiniEcommerce.Blazor` as startup projects.
   - The API documentation (Scalar) is available at `/scalar/v1`.
   - The Blazor UI is available at `/products`.
