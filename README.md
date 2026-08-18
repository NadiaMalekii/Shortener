
---

### 2. Shortener

```markdown
# Shortener

<p align="center">
  <img src="https://img.shields.io/badge/.NET-10-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" alt=".NET" />
  <img src="https://img.shields.io/badge/Orleans-10-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" alt="Orleans" />
  <img src="https://img.shields.io/badge/Swagger-OpenAPI-85EA2D?style=for-the-badge&logo=swagger&logoColor=black" alt="Swagger" />
  <img src="https://img.shields.io/badge/Architecture-Actor%20Model-orange?style=for-the-badge" alt="Actor Model" />
</p>

**URL Shortener powered by Microsoft Orleans**

A high-performance URL shortener built with **ASP.NET Core** and **Microsoft Orleans** (Actor Model).  
Each short code is managed by its own grain for scalability and isolation.

---

## Features

- Shorten any valid URL
- Redirect using short code (301 Permanent Redirect)
- Click statistics per short code
- Actor-based design with Orleans grains
- In-memory grain storage (easy to replace with persistent storage)
- Swagger UI for testing

---

## Tech Stack

| Technology              | Purpose                          |
|-------------------------|----------------------------------|
| ASP.NET Core (.NET 10)  | Web API                          |
| Microsoft Orleans       | Distributed actor model          |
| Swashbuckle             | Swagger / OpenAPI                |
| Minimal APIs            | Lightweight endpoint definition  |

---

## Project Structure
