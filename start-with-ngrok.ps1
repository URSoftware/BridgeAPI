# 🚀 START API WITH NGROK - 100% FREE, NO LOGIN, NO CREDIT CARD
# Executa API localmente + expõe online com ngrok (GRATUITO)

param(
    [string]$NgrokPath = "C:\ngrok\ngrok.exe",
    [string]$Port = "5000"
)

Write-Host "
╔══════════════════════════════════════════════════════════════════╗
║           🚀 BRIDGEAPI + NGROK - 100% GRATUITO                   ║
║              Sem cartão • Sem login • Sempre online               ║
╚══════════════════════════════════════════════════════════════════╝
" -ForegroundColor Cyan

# 1. VERIFICAR SE NGROK EXISTE
if (-not (Test-Path $NgrokPath)) {
    Write-Host "
❌ NGROK NÃO ENCONTRADO EM: $NgrokPath

📥 PARA INSTALAR NGROK (1 minuto):
──────────────────────────────────────────────────────────────────
1. Acesse: https://ngrok.com/download
2. Download Windows: ngrok-v3-stable-windows-amd64.zip
3. Descompacte em: C:\ngrok
4. Execute este script novamente

ℹ️  NGROK É 100% GRATUITO - Sem cartão, sem login necessário!
" -ForegroundColor Red
    exit 1
}

Write-Host "✅ ngrok encontrado em $NgrokPath" -ForegroundColor Green

# 2. COMPILAR CÓDIGO
Write-Host "`n🔨 Compilando BridgeAPI..." -ForegroundColor Yellow
dotnet build -c Release 2>&1 | Select-String "succeeded|error" | Write-Host

# 3. INICIAR API LOCALMENTE
Write-Host "`n🌍 Iniciando API na porta $Port..." -ForegroundColor Cyan
$apiProcess = Start-Process -FilePath dotnet `
                            -ArgumentList "run --no-build -c Release" `
                            -PassThru `
                            -NoNewWindow

Start-Sleep -Seconds 3

# 4. INICIAR NGROK
Write-Host "🔗 Abrindo ngrok para expor API online..." -ForegroundColor Cyan
$ngrokProcess = Start-Process -FilePath $NgrokPath `
                              -ArgumentList "http $Port" `
                              -PassThru

Start-Sleep -Seconds 5

# 5. OBTER URL NGROK
Write-Host "`n" -ForegroundColor Green
Write-Host "╔══════════════════════════════════════════════════════════════════╗" -ForegroundColor Green
Write-Host "║                    ✅ API ONLINE!                               ║" -ForegroundColor Green
Write-Host "╚══════════════════════════════════════════════════════════════════╝" -ForegroundColor Green

Write-Host "
🌍 URLs PARA TESTAR:

LOCAL (seu computador):
  http://localhost:$Port/api/health

ONLINE (qualquer lugar do mundo):
  Verifique em: http://localhost:4040
  (ngrok abre um dashboard em localhost:4040 mostrando a URL pública)

📊 EXEMPLOS DE USO:
  • Health Check:  http://seu-url-ngrok/api/health
  • Database:      http://seu-url-ngrok/api/database
  • Query:         http://seu-url-ngrok/api/query

⏱️  NGROK FORNECE URL PÚBLICA POR 2 HORAS (gratuitamente)
    Se precisar permanente: https://railway.app (próxima etapa)

✨ PRESSIONE CTRL+C PARA PARAR AMBOS (API + NGROK)
" -ForegroundColor Green

# 6. AGUARDAR ENCERRAMENTO
Write-Host "Aguardando... (CTRL+C para parar)" -ForegroundColor Yellow
$apiProcess.WaitForExit()
$ngrokProcess.WaitForExit()
