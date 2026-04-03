@echo off
echo Starting FinApp locally...
echo.

echo [1/3] Installing frontend dependencies...
cd "Frontend\FinAppFront"
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
dotnet run
if %ERRORLEVEL% neq 0 (
    echo Failed to start backend server
    pause
    exit /b 1
)

pause
