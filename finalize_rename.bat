@echo off
echo ============================================
echo  Finalizacja zmiany nazwy na BudgetTracker
echo ============================================
echo.

echo UWAGA: Ten skrypt wykona nastepujace kroki:
echo 1. Zmieni nazwe glownego folderu FinApp na BudgetTracker
echo 2. Zaktualizuje Git remote URL (jesli podasz nowy URL)
echo.
echo WAZNE: Zamknij wszystkie programy uzywajace tego folderu!
echo (IDE, terminale, eksplorator plikow)
echo.
pause

echo.
echo Czy zmieniles juz nazwe repozytorium na GitHub?
echo Jesli nie, zrob to teraz:
echo 1. Wejdz na: https://github.com/TWOJ_USERNAME/FinApp/settings
echo 2. Zmien nazwe na: BudgetTracker
echo 3. Kliknij Rename
echo.
set /p github_done="Czy zmieniles nazwe na GitHub? (tak/nie): "

if /i "%github_done%"=="tak" (
    echo.
    set /p username="Podaj swoja nazwe uzytkownika GitHub: "
    
    echo.
    echo Aktualizuje Git remote URL...
    git remote set-url origin https://github.com/!username!/BudgetTracker.git
    
    echo.
    echo Sprawdzam remote URL...
    git remote -v
    
    echo.
    echo Wypycham zmiany do GitHub...
    git push origin main
)

echo.
echo ============================================
echo Teraz zamknij to okno i wykonaj recznie:
echo ============================================
echo.
echo 1. Zamknij WSZYSTKIE programy (IDE, terminale)
echo 2. Otworz PowerShell w katalogu d:\Programowanie
echo 3. Wykonaj:
echo    Rename-Item -Path "FinApp" -NewName "BudgetTracker"
echo 4. Przejdz do nowego folderu:
echo    cd BudgetTracker
echo 5. Otworz projekt w IDE
echo.
echo Gotowe! Projekt zostal przemianowany na BudgetTracker
echo.
pause
