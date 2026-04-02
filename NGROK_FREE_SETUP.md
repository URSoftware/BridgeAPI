# 🚀 COLOCAR API DE PÉ EM 3 MINUTOS - SEM CARTÃO, SEM LOGIN

Sua API está **100% pronta**. Execute em 3 passos simples:

---

## 📋 3 PASSOS (3 MINUTOS)

### 1️⃣ BAIXAR NGROK (1 minuto)
```
Acesse: https://ngrok.com/download
Escolha: Windows
Download: ngrok-v3-stable-windows-amd64.zip
Descompacte em: C:\ngrok
```

✅ **NGROK é totalmente gratuito** - Sem cartão, sem login!

---

### 2️⃣ EXECUTAR SCRIPT (2 cliques)
```PowerShell
# Abra PowerShell como administrador em:
# C:\Users\thyag\OneDrive\Desktop\Estudos\BridgeAPI

.\start-with-ngrok.ps1
```

Script vai:
- ✅ Compilar API (.NET 9)
- ✅ Rodar localmente na porta 5000
- ✅ Expor online via ngrok
- ✅ Mostrar URL pública

---

### 3️⃣ TESTAR API (30 segundos)
```
Dashboard ngrok abre em: http://localhost:4040
Veja sua URL pública (ex: https://1234-56-78-90-123.ngrok.io)

Teste:
https://sua-url-ngrok.ngrok.io/api/health
```

Resultado esperado:
```json
{
  "status": "healthy",
  "timestamp": "2026-04-01T...",
  "version": "1.0.0"
}
```

---

## 💻 PRÓXIMAS AÇÕES

### ✅ Testar local (AGORA)
```PowerShell
.\start-with-ngrok.ps1
```

### ⏱️ URL muda a cada 2 horas (plano gratuito)

### 🔄 Para URL permanente: Deploy em Railway (~$3/mês)
Veja: **RAILWAY_QUICK_START.md**

---

## ❓ TROUBLESHOOTING

**Erro: "ngrok not found"**
```
Solução:
1. Download ngrok em https://ngrok.com/download
2. Descompacte em C:\ngrok
3. Execute script novamente
```

**Erro: "Port 5000 already in use"**
```
PowerShell (como admin):
Get-Process | Where-Object {$_.Name -contains "dotnet"} | Stop-Process -Force
```

**Erro: "Build failed"**
```
Execute manualmente:
dotnet build -c Release
dotnet run --no-build -c Release
```

---

## 🎯 OPÇÕES

### OPÇÃO 1: NGROK (AGORA)
- Gratuito ✅
- Sem cartão ✅
- Sem login ✅
- URL temporária (2h) ❌
- Ideal para: Testes rápidos

### OPÇÃO 2: RAILWAY (PRÓXIMA ETAPA)
- Gratuito primeiros meses ✅
- Sem cartão necessário ✅
- URL permanente ✅
- Custo: ~$3/mês ✅
- Ideal para: Produção

---

## 📞 SUPORTE

1. Script não funciona?
   - Verifique ngrok em C:\ngrok
   - Execute como administrador

2. Port 5000 ocupada?
   - Mude PORT no script para 8080

3. Teste manual:
   ```
   dotnet run -c Release
   # Em outro terminal:
   ngrok http 5000
   ```

---

**Status**: ✅ Pronto!
**Método**: ngrok (100% free)
**Próximo**: Railway para produção permanente

GO! 🚀
