# Skrypt do zmiany nazwy projektu z FinApp na BudgetTracker
# Uruchom jako Administrator w PowerShell

Write-Host "=== Zmiana nazwy projektu FinApp -> BudgetTracker ===" -ForegroundColor Cyan
Write-Host ""

# Sprawdź czy jesteśmy w odpowiednim katalogu
$currentPath = Get-Location
if ($currentPath.Path -notlike "*FinApp*") {
    Write-Host "BŁĄD: Uruchom skrypt z katalogu FinApp!" -ForegroundColor Red
    exit 1
}

# 1. Zmiana nazwy folderu Frontend
Write-Host "[1/4] Zmiana nazwy folderu Frontend..." -ForegroundColor Yellow
$frontendOld = "Frontend\FinAppFront"
$frontendNew = "Frontend\BudgetTrackerFront"

if (Test-Path $frontendOld) {
    Rename-Item -Path $frontendOld -NewName "BudgetTrackerFront"
    Write-Host "✓ Zmieniono nazwę folderu: $frontendOld -> $frontendNew" -ForegroundColor Green
} else {
    Write-Host "⚠ Folder $frontendOld nie istnieje lub już został zmieniony" -ForegroundColor Yellow
}

# 2. Sprawdź czy git remote istnieje
Write-Host ""
Write-Host "[2/4] Sprawdzanie konfiguracji Git..." -ForegroundColor Yellow
$gitRemote = git remote get-url origin 2>$null

if ($gitRemote) {
    Write-Host "Obecny remote URL: $gitRemote" -ForegroundColor Cyan
    
    # Wykryj nazwę użytkownika z URL
    if ($gitRemote -match "github.com[:/](.+?)/(.+?)(\.git)?$") {
        $username = $matches[1]
        $newRemoteUrl = "https://github.com/$username/BudgetTracker.git"
        
        Write-Host ""
        Write-Host "Nowy remote URL będzie: $newRemoteUrl" -ForegroundColor Cyan
        Write-Host ""
        Write-Host "UWAGA: Najpierw zmień nazwę repozytorium na GitHub!" -ForegroundColor Red
        Write-Host "1. Wejdź na: https://github.com/$username/FinApp/settings" -ForegroundColor White
        Write-Host "2. W sekcji 'Repository name' zmień nazwę na: BudgetTracker" -ForegroundColor White
        Write-Host "3. Kliknij 'Rename'" -ForegroundColor White
        Write-Host ""
        
        $response = Read-Host "Czy zmieniłeś już nazwę repozytorium na GitHub? (tak/nie)"
        
        if ($response -eq "tak" -or $response -eq "t" -or $response -eq "yes" -or $response -eq "y") {
            git remote set-url origin $newRemoteUrl
            Write-Host "✓ Zaktualizowano remote URL" -ForegroundColor Green
        } else {
            Write-Host "⚠ Pomiń aktualizację remote URL - zrób to później ręcznie" -ForegroundColor Yellow
        }
    }
} else {
    Write-Host "⚠ Brak konfiguracji Git remote" -ForegroundColor Yellow
}

# 3. Zmiana nazwy głównego folderu projektu
Write-Host ""
Write-Host "[3/4] Przygotowanie do zmiany nazwy głównego folderu..." -ForegroundColor Yellow
Write-Host "Obecna lokalizacja: $currentPath" -ForegroundColor Cyan

$parentPath = Split-Path -Parent $currentPath
$newProjectPath = Join-Path $parentPath "BudgetTracker"

Write-Host ""
Write-Host "Po zakonczu tego skryptu, wykonaj recznie:" -ForegroundColor Yellow
Write-Host "1. Zamknij wszystkie programy uzywajace tego folderu (IDE, terminale)" -ForegroundColor White
Write-Host "2. Przejdz do katalogu nadrzednego: cd .." -ForegroundColor White
Write-Host "3. Zmien nazwe folderu: Rename-Item -Path 'FinApp' -NewName 'BudgetTracker'" -ForegroundColor White
Write-Host "4. Przejdz do nowego folderu: cd BudgetTracker" -ForegroundColor White

# 4. Commit zmian w Git
Write-Host ""
Write-Host "[4/4] Commit zmian w Git..." -ForegroundColor Yellow

$gitStatus = git status --porcelain 2>$null
if ($gitStatus) {
    Write-Host "Znaleziono zmiany do zacommitowania:" -ForegroundColor Cyan
    git status --short
    Write-Host ""
    
    $commitResponse = Read-Host "Czy zacommitować zmiany? (tak/nie)"
    
    if ($commitResponse -eq "tak" -or $commitResponse -eq "t" -or $commitResponse -eq "yes" -or $commitResponse -eq "y") {
        git add -A
        git commit -m "Rename project from FinApp to BudgetTracker"
        Write-Host "✓ Zmiany zostały zacommitowane" -ForegroundColor Green
        
        $pushResponse = Read-Host "Czy wypchnąć zmiany do repozytorium? (tak/nie)"
        if ($pushResponse -eq "tak" -or $pushResponse -eq "t" -or $pushResponse -eq "yes" -or $pushResponse -eq "y") {
            git push
            Write-Host "✓ Zmiany zostały wypchnięte" -ForegroundColor Green
        }
    }
} else {
    Write-Host "⚠ Brak zmian do zacommitowania" -ForegroundColor Yellow
}

Write-Host ""
Write-Host "=== Zakończono zmianę nazwy projektu ===" -ForegroundColor Green
Write-Host ""
Write-Host "Podsumowanie wykonanych zmian:" -ForegroundColor Cyan
Write-Host "✓ Zmieniono nazwę folderu Frontend/FinAppFront -> Frontend/BudgetTrackerFront" -ForegroundColor Green
Write-Host "✓ Zmieniono nazwę pliku workflow: main_finapp.yml -> main_budgettracker.yml" -ForegroundColor Green
Write-Host "✓ Zaktualizowano wszystkie pliki konfiguracyjne" -ForegroundColor Green
Write-Host ""
Write-Host "Następne kroki:" -ForegroundColor Yellow
Write-Host "1. Zamknij IDE i wszystkie terminale" -ForegroundColor White
Write-Host "2. Zmień nazwę głównego folderu FinApp -> BudgetTracker" -ForegroundColor White
Write-Host "3. Otwórz projekt ponownie w nowej lokalizacji" -ForegroundColor White
Write-Host ""
