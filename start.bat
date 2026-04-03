@echo off
echo Starting BudgetTracker locally...
echo.

REM Przejdz do katalogu gdzie znajduje sie ten skrypt
cd /d "%~dp0"

echo [1/2] Starting Frontend (Angular) in new window...
start "BudgetTracker - Frontend" cmd /k "cd /d "%~dp0Frontend\BudgetTrackerFront" && npm start"

echo [2/2] Starting Backend (.NET) in new window...
start "BudgetTracker - Backend" cmd /k "cd /d "%~dp0Backend\Store\Store" && dotnet run"

echo.
echo ========================================
echo  BudgetTracker is starting...
echo ========================================
echo.
echo Frontend: http://localhost:4200
echo Backend:  https://localhost:7119
echo.
echo Dwa nowe okna zostaly otwarte:
echo  - Frontend (Angular dev server)
echo  - Backend (.NET API)
echo.
echo Aby zatrzymac serwery, zamknij te okna.
echo.
pause
