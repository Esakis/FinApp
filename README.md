# BudgetTracker

Aplikacja webowa do zarządzania finansami osobistymi, umożliwiająca śledzenie transakcji, kategoryzację wydatków i przychodów oraz zarządzanie budżetem.

## 📋 Spis treści

- [Technologie](#-technologie)
- [Architektura](#-architektura)
- [Funkcjonalności](#-funkcjonalności)
- [Wymagania](#-wymagania)
- [Instalacja i uruchomienie](#-instalacja-i-uruchomienie)
- [Struktura projektu](#-struktura-projektu)
- [API Endpoints](#-api-endpoints)
- [Baza danych](#-baza-danych)
- [Deployment](#-deployment)

## 🚀 Technologie

### Backend
- **.NET 6.0** - Framework aplikacji webowej
- **ASP.NET Core Web API** - RESTful API
- **Entity Framework Core 6.0** - ORM do zarządzania bazą danych
- **SQL Server (LocalDB)** - Baza danych
- **Swagger/OpenAPI** - Dokumentacja API
- **CsvHelper** - Obsługa plików CSV
- **Identity.PasswordHasher** - Hashowanie haseł

### Frontend
- **Angular 16** - Framework frontendowy
- **TypeScript 5.0** - Język programowania
- **Angular Material 16** - Biblioteka komponentów UI
- **RxJS 7.8** - Programowanie reaktywne
- **SCSS** - Preprocessor CSS
- **Jasmine & Karma** - Testy jednostkowe

### DevOps
- **GitHub Actions** - CI/CD pipeline
- **Azure App Service** - Hosting produkcyjny
- **Azure OIDC** - Autoryzacja deployment

## 🏗️ Architektura

Aplikacja wykorzystuje architekturę **Client-Server** z wyraźnym podziałem na:

### Backend (ASP.NET Core)
```
Backend/Store/Store/
├── Controllers/          # Kontrolery API (TransactionsController, UsersController)
├── Services/            # Logika biznesowa (TransactionService, UserService)
├── Models/              # Modele danych (TransactionModel, UserModel, CategoryModel)
├── Data/                # DbContext i konfiguracja bazy danych
├── Migrations/          # Migracje Entity Framework
└── Bootstrap/           # Dependency Injection
```

**Wzorce projektowe:**
- **Repository Pattern** - poprzez Entity Framework Core
- **Dependency Injection** - natywne wsparcie ASP.NET Core
- **Service Layer Pattern** - separacja logiki biznesowej

### Frontend (Angular)
```
Frontend/BudgetTrackerFront/src/app/
├── components/          # Komponenty aplikacji
│   ├── transaction-form/
│   └── transaction-list/
├── services/            # Serwisy HTTP (TransactionService, UserService)
├── models/              # Modele TypeScript
└── shared/              # Komponenty współdzielone (Header, Modals)
```

**Komunikacja:**
- Frontend → Backend: HTTP REST API (port 7119)
- CORS: Włączony dla wszystkich źródeł w trybie deweloperskim

## ✨ Funkcjonalności

### Zarządzanie transakcjami
- ✅ Dodawanie nowych transakcji (wydatki/przychody)
- ✅ Wyświetlanie listy wszystkich transakcji
- ✅ Usuwanie transakcji
- ✅ Filtrowanie transakcji według kategorii
- ✅ Edycja transakcji (modal)

### Kategorie
Predefiniowane kategorie:
- **Wydatki:** Jedzenie, Rozrywka, Rachunki, Transport, Zdrowie, Edukacja, Zakupy, Podróże, Inne
- **Przychody:** Przychód, Oszczędności

### Zarządzanie użytkownikami
- ✅ Tworzenie użytkowników
- ✅ Aktualizacja danych użytkownika
- ✅ Usuwanie użytkowników
- ✅ Śledzenie salda konta
- ✅ Hashowanie haseł

## 📦 Wymagania

### Wymagania systemowe
- **Windows** (zalecane, skrypt `start.bat`)
- **Node.js** 18.x lub nowszy
- **npm** (instalowany z Node.js)
- **.NET 6.0 SDK** lub nowszy
- **SQL Server LocalDB** (instalowany z Visual Studio lub osobno)

### Narzędzia deweloperskie (opcjonalne)
- Visual Studio 2022 / Visual Studio Code
- SQL Server Management Studio (SSMS)
- Postman (testowanie API)

## 🔧 Instalacja i uruchomienie

### Metoda 1: Automatyczne uruchomienie (Windows)

Użyj dołączonego skryptu `start.bat`:

```bash
start.bat
```

Skrypt automatycznie:
1. Instaluje zależności frontend (npm install)
2. Buduje aplikację Angular (npm run build)
3. Uruchamia backend .NET (dotnet run)

### Metoda 2: Manualne uruchomienie

#### Backend

```bash
cd Backend\Store\Store
dotnet restore
dotnet ef database update  # Utworzenie bazy danych
dotnet run
```

Backend będzie dostępny pod adresem: `https://localhost:7119`
Swagger UI: `https://localhost:7119/swagger`

#### Frontend

```bash
cd Frontend\BudgetTrackerFront
npm install
npm start
```

Frontend będzie dostępny pod adresem: `http://localhost:4200`

### Metoda 3: Tryb produkcyjny

```bash
cd Backend\Store\Store
dotnet publish -c Release
```

Aplikacja Angular zostanie automatycznie zbudowana i skopiowana do `wwwroot/` podczas publikacji.

## 📁 Struktura projektu

```
BudgetTracker/
├── .github/
│   └── workflows/
│       └── main_budgettracker.yml      # CI/CD pipeline dla Azure
├── Backend/
│   └── Store/
│       ├── Store/               # Projekt główny
│       │   ├── Controllers/
│       │   ├── Services/
│       │   ├── Models/
│       │   ├── Data/
│       │   ├── Migrations/
│       │   ├── Bootstrap/
│       │   ├── Program.cs
│       │   ├── Startup.cs
│       │   └── Store.csproj
│       └── Store.sln
├── Frontend/
│   └── BudgetTrackerFront/
│       ├── src/
│       │   ├── app/
│       │   │   ├── components/
│       │   │   ├── services/
│       │   │   ├── models/
│       │   │   └── shared/
│       │   ├── environments/
│       │   └── assets/
│       ├── angular.json
│       └── package.json
├── start.bat                    # Skrypt uruchomieniowy
├── .gitignore
└── README.md
```

## 🔌 API Endpoints

### Transactions API (`/Transactions`)

| Metoda | Endpoint | Opis |
|--------|----------|------|
| POST | `/Transactions` | Dodaj nową transakcję |
| GET | `/Transactions` | Pobierz wszystkie transakcje |
| DELETE | `/Transactions/{id}` | Usuń transakcję |
| GET | `/Transactions/by-category/{categoryId}` | Pobierz transakcje według kategorii |
| GET | `/Transactions/getAllCategories` | Pobierz wszystkie kategorie |

### Users API (`/Users`)

| Metoda | Endpoint | Opis |
|--------|----------|------|
| POST | `/Users` | Dodaj nowego użytkownika |
| GET | `/Users` | Pobierz wszystkich użytkowników |
| GET | `/Users/{id}` | Pobierz użytkownika po ID |
| PUT | `/Users/{id}` | Zaktualizuj użytkownika |
| DELETE | `/Users/{id}` | Usuń użytkownika |

### Przykładowe żądania

**Dodanie transakcji:**
```json
POST /Transactions
{
  "amount": 150.50,
  "date": "2024-04-03T09:00:00",
  "description": "Zakupy spożywcze",
  "categoryId": 1,
  "userId": 1
}
```

**Dodanie użytkownika:**
```json
POST /Users
{
  "username": "jankowalski",
  "email": "jan@example.com",
  "passwordHash": "hashedpassword123",
  "firstName": "Jan",
  "lastName": "Kowalski",
  "balance": 5000.00
}
```

## 🗄️ Baza danych

### Connection String
```
Server=(localdb)\\MSSQLLocalDB;Database=DataBase;Trusted_Connection=True;
```

### Modele danych

**TransactionModel**
- `Id` (int, PK)
- `Amount` (decimal)
- `Date` (DateTime)
- `Description` (string, nullable)
- `CategoryId` (int, FK)
- `UserId` (int, FK)

**UserModel**
- `Id` (int, PK)
- `Username` (string)
- `Email` (string)
- `PasswordHash` (string)
- `FirstName` (string)
- `LastName` (string)
- `Balance` (decimal)

**CategoryModel**
- `Id` (int, PK)
- `Name` (string)
- `CategoryIncome` (bool) - true dla przychodów, false dla wydatków

### Dane początkowe

Aplikacja automatycznie tworzy:
- **11 predefiniowanych kategorii** (Jedzenie, Rozrywka, Rachunki, etc.)
- **Domyślnego użytkownika** (username: `defaultuser`, balance: 1,000,000)

### Migracje

```bash
# Utworzenie nowej migracji
dotnet ef migrations add NazwaMigracji

# Aktualizacja bazy danych
dotnet ef database update

# Cofnięcie migracji
dotnet ef database update PreviousMigrationName
```

## 🚀 Deployment

### Azure App Service (Produkcja)

Aplikacja jest automatycznie wdrażana na Azure App Service przy każdym push do gałęzi `main`.

**Pipeline CI/CD:**
1. **Build:**
   - Instalacja Node.js 18.x
   - Instalacja .NET 8 SDK
   - Build Angular (production mode)
   - Kopiowanie dist → wwwroot
   - Build i publish .NET

2. **Deploy:**
   - Autoryzacja Azure OIDC
   - Deploy do Azure Web App (slot: Production)

**Konfiguracja:**
- App Name: `BudgetTracker`
- Runtime: .NET 6/8
- Plan: F1 (Free tier)

### Zmienne środowiskowe (Azure)

Należy skonfigurować w Azure Portal:
- `AZUREAPPSERVICE_CLIENTID_*`
- `AZUREAPPSERVICE_TENANTID_*`
- `AZUREAPPSERVICE_SUBSCRIPTIONID_*`
- Connection String dla SQL Database

## 📝 Konfiguracja

### Backend (`appsettings.json`)
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=DataBase;Trusted_Connection=True;"
  },
  "AllowedHosts": "*"
}
```

### Frontend (`environment.ts`)
```typescript
export const environment = {
  production: false,
  apiUrl: 'https://localhost:7119'
};
```

## 🧪 Testowanie

### Frontend
```bash
cd Frontend\BudgetTrackerFront
npm test              # Uruchom testy jednostkowe
npm run test:coverage # Testy z pokryciem kodu
```

### Backend
```bash
cd Backend\Store\Store
dotnet test
```

## 📄 Licencja

Projekt jest dostępny na licencji określonej w pliku `LICENSE`.

## 👥 Autorzy

Projekt BudgetTracker - Aplikacja do zarządzania finansami osobistymi

---

**Status projektu:** ✅ Aktywny  
**Ostatnia aktualizacja:** Kwiecień 2026