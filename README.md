⚙️ Daraz Clone — Backend API

This is the backend (API) for the Daraz Clone e-commerce platform.
It powers all core functionalities including authentication, product management, order processing, and user management — all connected to a SQL database.

The frontend for this project is available publicly in a separate repository:
👉 Daraz Clone Frontend Repo  https://github.com/Sufiyan-Fiyaz/Daraz-Clone

🚀 Live API Endpoint

Base URL: https://your-live-api-link.com
(Deployed on Render / Vercel / Railway / your hosting platform)

🧰 Tech Stack

Node.js — JavaScript runtime

.NET Core — Web framework for building RESTful APIs

SQL Database — Data storage for products, users

JWT (JSON Web Tokens) — Secure authentication and authorization

Cors — Security middleware


🧪 API Endpoints Overview
🧍‍♂️ Auth Routes
Method	Endpoint	Description
POST	/api/auth/register	Register new user
POST	/api/auth/login	Login user
GET	/api/auth/profile	Get user profile (JWT required)
🛍️ Product Routes
Method	Endpoint	Description
GET	/api/products	Fetch all products
GET	/api/products/:id	Get single product details
POST	/api/products	Add new product (Admin only)
PUT	/api/products/:id	Update product (Admin only)
DELETE	/api/products/:id	Delete product (Admin only)
📦 Order Routes
Method	Endpoint	Description
POST	/api/orders	Place new order
GET	/api/orders/:userId	Get user’s order history
⚙️ Setup Instructions
1️⃣ Clone Repo
https://github.com/Sufiyan-Fiyaz/Daraz-Clone-API.git



PORT=7292 For https & 5069 For http

Your API will be live at:
👉 https://localhost:7292/Swagger/index.html


🧩 Integration with Frontend

The API is fully integrated with the Daraz Clone Frontend, allowing:

Real-time product fetching

User authentication

Cart & order management

Chat support (via Ollama 3.2 on frontend)

📧 Contact

Muhammad Sufiyan Fiyaz
📍 Lahore, Pakistan
📧 sufiyan7278@gmail.com
