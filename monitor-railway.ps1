# Script para acompanhar deploy em Railway
# Este script testa se sua API está online após deploy no Railway

param(
    [string]$ApiUrl = "https://bridgeapi-xxx.railway.app/api/health",
    [int]$MaxAttempts = 60,
    [int]$WaitSeconds = 10
)

Write-Host "
╔════════════════════════════════════════════════════════════════╗
║              RAILWAY DEPLOYMENT MONITOR                        ║
║  Testando API a cada $WaitSeconds segundos (máx $MaxAttempts tentativas)        ║
╚════════════════════════════════════════════════════════════════╝
" -ForegroundColor Cyan

# IMPORTANTE: Substitua a URL acima pela URL real do Railway
if ($ApiUrl -contains "xxx") {
    Write-Host "❌ ERRO: Altere a URL para sua URL real do Railway!" -ForegroundColor Red
    Write-Host "Obtenha a URL em: Railway Dashboard → Project → Hostname" -ForegroundColor Yellow
    exit 1
}

Write-Host "📡 Testando: $ApiUrl`n" -ForegroundColor Yellow

$attempt = 0
$success = $false
$startTime = Get-Date

while ($attempt -lt $MaxAttempts -and -not $success) {
    $attempt++
    $elapsed = (Get-Date) - $startTime
    $minutos = [Math]::Round($elapsed.TotalMinutes, 1)
    
    # Log a cada 6 tentativas (~1 minuto)
    if ($attempt % 6 -eq 1) {
        Write-Host "[$minutos m] Tentativa $attempt/$MaxAttempts..." -ForegroundColor Cyan
    }
    
    try {
        $response = Invoke-WebRequest -Uri $ApiUrl `
                                     -UseBasicParsing `
                                     -TimeoutSec 5 `
                                     -ErrorAction Stop
        
        if ($response.StatusCode -eq 200) {
            Write-Host "`n✅ ✅ ✅ API ONLINE! ✅ ✅ ✅`n" -ForegroundColor Green
            Write-Host "Status Code: $($response.StatusCode)" -ForegroundColor Green
            Write-Host "Tempo total: $minutos minutos`n" -ForegroundColor Green
            Write-Host "Resposta:`n$($response.Content)" -ForegroundColor Green
            $success = $true
        }
    }
    catch {
        # Silenciosamente continua tentando
    }
    
    if (-not $success) {
        Start-Sleep -Seconds $WaitSeconds
    }
}

if (-not $success) {
    Write-Host "❌ API não respondeu após $(($MaxAttempts * $WaitSeconds) / 60) minutos" -ForegroundColor Red
    Write-Host "`nVerifique em Railway Dashboard:" -ForegroundColor Yellow
    Write-Host "  1. Build logs: Projeto → Logs → Deployment" -ForegroundColor Yellow
    Write-Host "  2. Environment já set: Projeto → Variables" -ForegroundColor Yellow
    Write-Host "  3. Health: Projeto → Status" -ForegroundColor Yellow
}
