# Trezo Books

**Trezo Books** is a full-stack web application for browsing, managing, and purchasing books. Built with React, Tailwind CSS, and ASP.NET Core Web API.

---

## Tech Stack

- **Frontend:** React + TypeScript + Tailwind CSS
- **Backend:** ASP.NET Core 9 + EF Core + JWT Authentication + Http-Only Cookies
- **Database:** SQL Server

---

## Getting Started

### 1. Clone the repository

```bash
git clone https://github.com/Abdellah-saim-mamoune1/Trezo_Books.git
cd Trezo_Books
```
### 2. Run and build docker-compose files
  First make sure you have docker installed and running in your local machine.
  Then run: 

```bash
docker-compose up -d --build
```
after building and running the containers, you will be able to access the demo by pasting this URL in the browser: http://localhost:3000/.
You can olso access backend endpoints via swagger from: http://localhost:8100/swagger/index.HTML.
When you want to login as a client or employee to the system, use the email and password provided in ./Backend/Infrastructure/db_samples/sample_data.json.
Note: employee login email must be eneterd in this format: FirstName.LastName@Trezo.com, example: john.doe@Trezo.com.
you can olso add your custom employees or clients or other data in the json file.

---

## Screenshots
![Dashboard Screenshot](./Frontend/Assets/Screenshot(97).png)
![Dashboard Screenshot](./Frontend/Assets/Screenshot(101).png)
![Dashboard Screenshot](./Frontend/Assets/Screenshot(102).png)
![Dashboard Screenshot](./Frontend/Assets/Screenshot(105).png)
![Dashboard Screenshot](./Frontend/Assets/Screenshot(106).png)
![Dashboard Screenshot](./Frontend/Assets/Screenshot(107).png)
![Dashboard Screenshot](./Frontend/Assets/Screenshot(108).png)
![Dashboard Screenshot](./Frontend/Assets/Screenshot(109).png)
![Dashboard Screenshot](./Frontend/Assets/Screenshot(110).png)
![Dashboard Screenshot](./Frontend/Assets/Screenshot(111).png)
![Dashboard Screenshot](./Frontend/Assets/Screenshot(112).png)
![Dashboard Screenshot](./Frontend/Assets/Screenshot(113).png)

---

## Features
 User login & JWT authentication with HTTP-only cookies

 Browse books via Google Books API

 Add to cart / manage orders

 Role-based access (admin,seller, user)

 Responsive UI with Tailwind

---

 ## Notes
 JWT tokens are stored in HTTP-only cookies.
 
 CORS and cookie settings are configured for production with SameSite=Strict.

---

 ## Contributions
 Contributions are welcomed.
 
---

 ## Contact
 If you have any questions or suggestions, reach out at: abdellahsaimmamoune1@gmail.com.
 




