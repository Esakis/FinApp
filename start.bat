@echo off
echo Starting BudgetTracker locally...
echo.

REM Przejdz do katalogu gdzie znajduje sie ten skrypt
cd /d "%~dp0"

echo [1/3] Installing frontend dependencies...
cd "Frontend\BudgetTrackerFront"
if %ERRORLEVEL% neq 0 (
    echo ERROR: Folder Frontend\BudgetTrackerFront nie istnieje!
    pause
    exit /b 1
)

call npm install
if %ERRORLEVEL% neq 0 (
    echo Failed to install frontend dependencies
    pause
    exit /b 1
)

echo.
echo [2/3] Building frontend...
call npm run build
if %ERRORLEVEL% neq 0 (
    echo Failed to build frontend
    pause
    exit /b 1
)

echo.
echo [3/3] Starting backend server...
cd "..\..\Backend\Store\Store"
if %ERRORLEVEL% neq 0 (
    echo ERROR: Folder Backend\Store\Store nie istnieje!
    pause
    exit /b 1
)

dotnet run
if %ERRORLEVEL% neq 0 (
    echo Failed to start backend server
    pause
    exit /b 1
)

pause
