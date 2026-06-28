<div align="center">

```
███████╗      █████╗ ██████╗  ██████╗  ██████╗ █████╗ ██╗  ██╗   ██╗██████╗ ███████╗███████╗
╚══███╔╝     ██╔══██╗██╔══██╗██╔═══██╗██╔════╝██╔══██╗██║  ╚██╗ ██╔╝██╔══██╗██╔════╝██╔════╝
  ███╔╝█████╗███████║██████╔╝██║   ██║██║     ███████║██║   ╚████╔╝ ██████╔╝███████╗█████╗
 ███╔╝ ╚════╝██╔══██║██╔═══╝ ██║   ██║██║     ██╔══██║██║    ╚██╔╝  ██╔═══╝ ╚════██║██╔══╝
███████╗     ██║  ██║██║     ╚██████╔╝╚██████╗██║  ██║███████╗██║   ██║     ███████║███████╗
╚══════╝     ╚═╝  ╚═╝╚═╝      ╚═════╝  ╚═════╝╚═╝  ╚═╝╚══════╝╚═╝   ╚═╝     ╚══════╝╚══════╝
```

**Survive. Adapt. Persist.**

[![Three.js](https://img.shields.io/badge/Three.js-000000?style=for-the-badge&logo=three.js&logoColor=white)](https://threejs.org/)
[![C#](https://img.shields.io/badge/C%23-.NET_Core-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![SQL Server](https://img.shields.io/badge/SQL_Server-CC2927?style=for-the-badge&logo=microsoftsqlserver&logoColor=white)](https://www.microsoft.com/en-us/sql-server)
[![JavaScript](https://img.shields.io/badge/JavaScript-F7DF1E?style=for-the-badge&logo=javascript&logoColor=black)](https://developer.mozilla.org/en-US/docs/Web/JavaScript)

</div>

---

## 📌 About The Project

**Z-Apocalypse** is a browser-based 3D zombie survival game built as a full-stack university project. The game runs entirely in the browser using **Three.js** for 3D rendering, while player data and progress are saved to a **SQL Server** database through a **C# .NET Core** REST API backend.

Players fight waves of zombies, earn scores, level up, and have all their progress saved across sessions.

---

## 🛠️ Built With

| Layer | Technology |
|---|---|
| 3D Graphics | Three.js + WebGL |
| Frontend | HTML5, CSS3, JavaScript |
| Backend | C# .NET Core (REST API) |
| Database | Microsoft SQL Server |
| ORM | Entity Framework Core |

---

## 🗂️ Project Structure

```
Z-Apocalypse/
│
├── Backend/
│   ├── Controllers/        # API endpoints (Player, Game, Stats)
│   ├── Models/             # Database entity classes
│   ├── Services/           # Business logic
│   └── Migrations/         # EF Core database migrations
│
├── Frontend/
│   ├── scripts/            # Three.js game logic
│   ├── styles/             # CSS files
│   └── index.html          # Main game page
│
└── Database/
    ├── schema.sql          # Table creation scripts
    └── seed.sql            # Sample/test data
```

---

## ⚙️ System Architecture

```
[ Browser / Three.js ]
        │
        │  HTTP REST API Calls
        ▼
[ C# .NET Core Backend ]
        │
        │  Entity Framework Core
        ▼
[ SQL Server Database ]
```

---

## ✨ Features

- 🎮 Real-time 3D zombie survival gameplay in the browser
- 💾 Player progress saved across sessions (kills, score, level)
- 👤 Player registration and login system
- 📊 Live score and level tracking via REST API
- 🔒 Secure data storage with encrypted wallet balances
- 📱 Responsive layout that works on different screen sizes

---

## 🗄️ Database Tables

| Table | Purpose |
|---|---|
| `Players` | Stores username and login credentials |
| `Sessions` | Tracks each game session (start/end time) |
| `PlayerStats` | Saves kills, score, level, and playtime |
| `Levels` | Records which levels a player has completed |
| `Wallets` | Stores in-game currency (encrypted) |

---

## 📡 API Endpoints

| Method | Endpoint | Description |
|---|---|---|
| `POST` | `/api/player/register` | Create a new player account |
| `POST` | `/api/player/login` | Login and start a session |
| `GET` | `/api/player/{id}/stats` | Get player stats |
| `PUT` | `/api/player/{id}/level` | Update player level |
| `POST` | `/api/game/session/start` | Start a new game session |
| `PUT` | `/api/game/session/end` | End session and save data |
| `GET` | `/api/game/leaderboard` | Get top players |

---

## 🚀 How to Run

### 1. Clone the Repository
```bash
git clone https://github.com/asharibgaming4-ai/Z-Apocalypse.git
cd Z-Apocalypse
```

### 2. Set Up the Database
```bash
sqlcmd -S localhost -i Database/schema.sql
```

### 3. Run the Backend
```bash
cd Backend
dotnet restore
dotnet ef database update
dotnet run
```

### 4. Open the Game
```bash
cd ../Frontend
# Open index.html in your browser or run:
npx serve .
```

---

## 👨‍💻 Developer

**Asharib** — Computer Science Student
🔗 [GitHub Profile](https://github.com/asharibgaming4-ai)

---

<div align="center">

*Built with dedication as a university full-stack project.*
⭐ Leave a star if you liked it!

</div>
