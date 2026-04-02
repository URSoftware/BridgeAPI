# 🚀 DEPLOY NO RAILWAY.APP - INSTRUÇÕES

## ⚡ Resumo Rápido
Seu código foi preparado para **Railway.app** - plataforma que oferece:
- ✅ .NET 9 suportado perfeitamente
- ✅ Tier gratuito com crédito inicial
- ✅ Sempre online (sem sleep mode)
- ✅ Hospedagem confiável (melhor que Render)

## 📋 Passos para Deploy

### 1. Criar Conta Railway (se não tiver)
```
👉 Acesse: https://railway.app
👉 Sign up com GitHub (recomendado)
👉 Autorize Railway a acessar seus repositórios
```

### 2. Conectar GitHub a Railway
```
No dashboard Railway:
1. Clique em "+ New Project"
2. Selecione "Deploy from GitHub repo"
3. Selecione: URSoftware/BridgeAPI
4. Selecione branch "main"
5. Railway detectará Dockerfile automaticamente
```

### 3. Configurar Variáveis de Ambiente (IMPORTANTE!)
No painel Railway, vá para "Variables" e adicione:
```
PORT = 8080
ASPNETCORE_ENVIRONMENT = Production
```

### 4. Deploy
```
Railway fará deploy automaticamente de:
- Dockerfile (multi-stage build já otimizado)
- railway.toml (configuração Railway)
- Código .NET 9 compilado
```

### 5. Obter URL
```
Após deploy:
Dashboard Railway → Seu Project → Hostname
Exemplo: https://bridgeapi-xxx.railway.app/api/health
```

---

## 🔍 Configuração Técnica

**Dockerfile**: Multi-stage build
- SDK:9.0 → Build Release
- aspnet:9.0 → Runtime
- PORT: 8080 (padrão Railway)

**Program.cs**: Otimizado para Railway
- Lê PORT de environment variable
- ASPNETCORE_ENVIRONMENT = Production
- Sem UseHttpsRedirection (Railway tem reverse proxy TLS)
- Binding: 0.0.0.0:{PORT}

**railway.toml**: Configuração
- builder = dockerfile
- numReplicas = 1
- PORT = 8080
- ASPNETCORE_ENVIRONMENT = Production

---

## 📊 Código Atual
- ✅ Commit: `757d64d` (railway.toml adicionado)
- ✅ Build local: Status 200 OK
- ✅ Sem erros
- ✅ Pronto para produção

---

## ❓ Suporte
Se encontrar erros:
1. Verifique logs no dashboard Railway
2. Procure por erro de "PORT" ou "binding"
3. Verifique se Dockerfile se compila: `dotnet build -c Release`

**Status em 1º abril 2026**:
- Train de deploy para Railway está 100% pronto
- Seu repositório está sincronizado GitHub
- Crédito gratuito Railway: use durante testes
- Após crédito: ~$2-5/mês para sempre online

---

Qualquer dúvida, avise! 🚀
