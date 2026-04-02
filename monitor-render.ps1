# BridgeAPI Render Health Monitor
# Monitora continuamente o status da API e registra os resultados

$logFile = "$PSScriptRoot\render-monitor.log"
$url = "https://bridgeapi-9uhi.onrender.com/api/health"
$maxFailures = 0
$checkInterval = 30  # segundos

Write-Host "🚀 BridgeAPI Render Health Monitor iniciado"
Write-Host "URL: $url"
Write-Host "Intervalo: $checkInterval segundos"
Write-Host "Log: $logFile"
Write-Host ""
Write-Host "Pressione Ctrl+C para parar"
Write-Host "━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━"

$startTime = Get-Date
$checkCount = 0

while ($true) {
    $checkCount++
    $elapsed = (Get-Date) - $startTime
    $timestamp = Get-Date -Format "yyyy-MM-dd HH:mm:ss"
    
    try {
        $response = Invoke-WebRequest -Uri $url -UseBasicParsing -TimeoutSec 3 -ErrorAction Stop
        
        if ($response.StatusCode -eq 200) {
            $message = "✅ OK - Status: $($response.StatusCode)"
            Write-Host "[$timestamp] $message"
            Add-Content $logFile "[$timestamp] $message - Response: $($response.Content)"
            $maxFailures = 0
        }
        else {
            $message = "⚠️  HTTP $($response.StatusCode)"
            Write-Host "[$timestamp] $message"
            Add-Content $logFile "[$timestamp] $message"
            $maxFailures++
        }
    }
    catch {
        $errorMsg = $_.Exception.Message
        $message = "❌ ERRO: $errorMsg"
        Write-Host "[$timestamp] $message"
        Add-Content $logFile "[$timestamp] $message"
        $maxFailures++
    }
    
    # Mostra resumo a cada 10 checks
    if ($checkCount % 10 -eq 0) {
        $minutos = [Math]::Round($elapsed.TotalMinutes, 1)
        Write-Host "━━ Check $checkCount - Tempo decorrido: ${minutos}m ━━"
    }
    
    Start-Sleep -Seconds $checkInterval
}
