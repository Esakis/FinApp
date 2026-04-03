# Finalizacja zmiany nazwy projektu na BudgetTracker

## ✅ Co zostało już zrobione:

1. ✓ Zmieniono nazwę w README.md
2. ✓ Zaktualizowano package.json (frontend)
3. ✓ Zaktualizowano angular.json
4. ✓ Zaktualizowano Store.csproj (backend)
5. ✓ Zmieniono nazwę pliku workflow: main_finapp.yml → main_budgettracker.yml
6. ✓ Zmieniono nazwę folderu: Frontend/FinAppFront → Frontend/BudgetTrackerFront
7. ✓ Zaktualizowano wszystkie pliki konfiguracyjne
8. ✓ Zacommitowano wszystkie zmiany do Git

## 🔄 Co należy jeszcze zrobić:

### Krok 1: Zmień nazwę repozytorium na GitHub

1. Wejdź na: https://github.com/TWOJ_USERNAME/FinApp/settings
2. W sekcji **Repository name** zmień nazwę na: `BudgetTracker`
3. Kliknij **Rename**
4. GitHub automatycznie przekieruje stare URL na nowe

### Krok 2: Zaktualizuj remote URL lokalnie

Otwórz terminal w folderze projektu i wykonaj:

```bash
git remote set-url origin https://github.com/TWOJ_USERNAME/BudgetTracker.git
```

Sprawdź czy się zmieniło:
```bash
git remote -v
```

### Krok 3: Wypchnij zmiany do GitHub

```bash
git push origin main
```

### Krok 4: Zmień nazwę głównego folderu projektu

**WAŻNE:** Zamknij wszystkie programy używające tego folderu (IDE, terminale, eksplorator plików)

Następnie w PowerShell:

```powershell
# Przejdź do katalogu nadrzędnego
cd d:\Programowanie

# Zmień nazwę folderu
Rename-Item -Path "FinApp" -NewName "BudgetTracker"

# Przejdź do nowego folderu
cd BudgetTracker

# Sprawdź czy wszystko działa
git status
```

### Krok 5: Otwórz projekt w nowej lokalizacji

Otwórz folder `d:\Programowanie\BudgetTracker` w swoim IDE.

### Krok 6: Sprawdź czy aplikacja działa

Uruchom aplikację:
```bash
.\start.bat
```

Lub ręcznie:
```bash
# Backend
cd Backend\Store\Store
dotnet run

# Frontend (w nowym terminalu)
cd Frontend\BudgetTrackerFront
npm install
npm start
```

## 🎯 Podsumowanie zmian

### Nazwy folderów:
- `d:\Programowanie\FinApp` → `d:\Programowanie\BudgetTracker`
- `Frontend\FinAppFront` → `Frontend\BudgetTrackerFront`

### Nazwy w plikach:
- Projekt Angular: `FinAppFront` → `BudgetTrackerFront`
- Package name: `fin-app-front` → `budget-tracker-front`
- Output path: `dist/fin-app-front` → `dist/budget-tracker-front`
- Azure App Name: `FinApp` → `BudgetTracker`
- Workflow file: `main_finapp.yml` → `main_budgettracker.yml`

### Repozytorium Git:
- Stare URL: `https://github.com/USERNAME/FinApp.git`
- Nowe URL: `https://github.com/USERNAME/BudgetTracker.git`

## 📝 Opcjonalne: Aktualizacja Azure

Jeśli używasz Azure App Service, zaktualizuj:
1. Nazwę App Service na `BudgetTracker`
2. Connection string w Azure Portal
3. Zmienne środowiskowe (jeśli są specyficzne dla nazwy)

## ✅ Weryfikacja

Po wykonaniu wszystkich kroków, sprawdź:
- [ ] Git remote URL wskazuje na BudgetTracker
- [ ] Folder projektu nazywa się BudgetTracker
- [ ] Aplikacja uruchamia się poprawnie
- [ ] Frontend buduje się bez błędów
- [ ] Backend uruchamia się bez błędów
- [ ] GitHub Actions workflow działa poprawnie

---

**Wszystko gotowe!** Projekt został w pełni przemianowany na BudgetTracker.
