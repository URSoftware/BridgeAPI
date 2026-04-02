# 🚀 RENDER DEPLOYMENT - BRIDGEAPI

## Status: ✅ Otimizado para Render

Este projeto está configurado para **Render.com** com suporte total ao .NET 9.

---

## 📋 Como Deployar

### 1️⃣ **Conectar Render ao GitHub**
```
1. Acesse: https://render.com
2. Login com GitHub (ou crie conta)
3. Clique: "New Service" → Web Service
4. Selecione: URSoftware/BridgeAPI
5. Branch: main
6. Render detecta Dockerfile automaticamente
```

### 2️⃣ **Configuração Automática**
```
render.yaml já está configurado com:
✅ Runtime: .NET 9.0
✅ Build: dotnet publish -c Release
✅ Start: dotnet BridgeAPI.dll
✅ Environment: Production
✅ Port: Automático (10000)
```

### 3️⃣ **Deploy**
```
Render faz tudo automaticamente:
✓ Clone seu repositório
✓ Executa dotnet publish -c Release
✓ Inicia BridgeAPI.dll
✓ Obtém hostname (ex: bridgeapi-xxx.onrender.com)
```

### 4️⃣ **Testar**
```
Após deploy (2-3 minutos):
GET https://seu-hostname.onrender.com/api/health

Resposta esperada:
{
  "status": "healthy",
  "timestamp": "2026-04-02T...",
  "version": "1.0.0"
}
```

---

## 📊 Configuração Render

**render.yaml contém:**
- ✅ Runtime: dotnet (9.0)
- ✅ Build: `dotnet publish -c Release -o /opt/render/output`
- ✅ Start: `dotnet /opt/render/output/BridgeAPI.dll`
- ✅ Env: ASPNETCORE_ENVIRONMENT=Production
- ✅ Port: 10000 (padrão Render)

---

## ⚙️ Variáveis de Ambiente (Automáticas)

Render define automaticamente:
```
ASPNETCORE_ENVIRONMENT = Production
PORT = 10000
```

Se precisar customizar:
1. Render Dashboard → Environment
2. Adicione novas variáveis
3. Redeploy

---

## 🔄 Auto-Deploy

```
✅ Automático ativado
Sempre que você faz git push → Render faz redeploy
```

Se quiser desativar:
```
render.yaml: autoDeploy: false
Render Dashboard → Settings → Auto-Deploy
```

---

## 🛢️ Banco de Dados

Seu código suporta:
- ✅ SQLite (Microsoft.Data.Sqlite)
- ✅ SQL Server (System.Data.SqlClient)
- ✅ Outras via string de conexão customizada

Para usar banco externo:
```
Render Dashboard → Variables
DATABASE_URL = sua_string_de_conexao
```

---

## ⏱️ Sleep Mode

Render free tier:
- 🛏️ Dorme após 15 minutos sem requisições
- ⏰ Primeira requisição: 30-50s (cold start)
- ✅ Depois: responde normal (100ms)

Solução permanente: **upgrade para paid** (~$7/mês)

---

## 🆘 Troubleshooting

### Erro: "Build failed"
```
Build logs em:
Dashboard → Service → Logs → Build
Procure por erro vermelho
```

### Erro: "Port already in use"
```
Render: render.yaml → PORT: "10001"
Redeploy
```

### Erro: "500 Internal Server Error"
```
Dashboard → Logs → Runtime
Verifique se Controllers estão carregando
```

### 404 em /api/health
```
Solução:
1. Verifique em Logs se app iniciou com "Application started"
2. Confirme Controllers foram mapeados
3. Teste local: dotnet run -c Release
```

---

## 📝 Next Steps

1. **Deploy**: Conecte repositório em Render Dashboard
2. **Test**: Acesse /api/health via hostname.onrender.com
3. **Monitor**: Dashboard → Logs (verifique se rodando)
4. **Customize**: Adicione variáveis conforme necessário

---

## 💰 Custos

| Tier | Preço | Sleep | Ideal Para |
|------|-------|-------|-----------|
| Free | $0 | Sim (15m) | Testes/Dev |
| Starter | $7/mês | Não | Produção |
| Pro | $12/mês | Não | Alta carga |

---

## 📞 Suporte Render

- [Docs](https://render.com/docs)
- [Community](https://community.render.com)
- [Status](https://status.render.com)

---

**Status**: ✅ Pronto para Render
**Última atualização**: 02/04/2026
**Runtime**: .NET 9.0
**Dockerfile**: ✅ Otimizado
