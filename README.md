# 🎮 Game Store API

Простий REST API для управління іграми та жанрами.  
Побудований на ASP.NET Core + Entity Framework Core.

---

## 📦 Моделі

- Games
- Genres

---

## 🎯 Ендпоінти

### 🕹️ Games

#### GET /games
Отримати список всіх ігор

Response:
[
    {
        "id": 1,
        "name": "Game name",
        "cost": 59.99,
        "genre": "Action"
    }
]

---

#### GET /games/{id}
Отримати гру по ID

Response:
{
    "id": 1,
    "name": "Game name",
    "cost": 59.99,
    "genreId": 2
}

---

#### POST /games
Створити нову гру

Request:
{
    "name": "New Game",
    "cost": 49.99,
    "genreId": 1
}

---

#### PUT /games/{id}
Оновити існуючу гру

Request:
{
    "name": "Updated Game",
    "cost": 39.99
}

---

#### DELETE /games/{id}
Видалити гру

---

### 🎭 Genres

#### GET /genres
Отримати всі жанри

Response:
[
    {
        "id": 1,
        "name": "Action"
    }
]

---

#### GET /genres/{id}
Отримати жанр по ID

Response:
{
    "id": 1,
    "name": "Action"
}

---

#### POST /genres
Створити новий жанр

Request:
{
    "name": "Adventure"
}

---

#### PUT /genres/{id}
Оновити жанр

Request:
{
    "name": "Updated Genre"
}

---

#### DELETE /genres/{id}
Видалити жанр

---

## ⚙️ Технології

- ASP.NET Core
- Entity Framework Core
- Sqlite

---

## 🚀 Запуск

dotnet run

---