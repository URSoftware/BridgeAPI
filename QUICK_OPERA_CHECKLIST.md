# 🔍 Checklist Rápido - Verificar No Opera GX

**Enquanto você está vendo as páginas, use este checklist simples:**

---

## 📍 RENDER.COM

### Abra: https://dashboard.render.com

**Look For (O que procurar):**

```
┌─────────────────────────────────────┐
│  1. Procure por "LIVE" em verde     │
│     Status: LIVE ✅                 │
├─────────────────────────────────────┤
│  2. Service name: bridgeapi          │
│     Ou similar (sua API)             │
├─────────────────────────────────────┤
│  3. Procure por seu URL:             │
│     https://bridgeapi-xxxxx.onrender │
│     .com                             │
├─────────────────────────────────────┤
│  4. Last Deploy: Success ✅          │
│     (deve estar em verde)            │
├─────────────────────────────────────┤
│  5. Status: Deployed ✅              │
│     (quanto tempo atrás)             │
└─────────────────────────────────────┘
```

**Checklist Render:**
- [ ] Vejo "LIVE" em VERDE?
- [ ] Vejo "bridgeapi" na lista?
- [ ] Last Deploy diz "Success"?
- [ ] Status não mostra erro (vermelho)?
- [ ] Posso ver a URL da minha API?

**⚠️ Se algo estiver VERMELHO:**
- Clique em "Logs" tab
- Veja se tem erro
- Pode ser que a API ainda esteja iniciando (espere 2-3min)

---

## 🔔 UPTIMEROBOT

### Abra: https://uptimerobot.com/dashboard

**Look For (O que procurar):**

```
┌─────────────────────────────────────┐
│  1. Procure por outro monitor:      │
│     BridgeAPI Keep-Alive            │
│     Ou "API Health"                 │
├─────────────────────────────────────┤
│  2. Status: UP ✅                   │
│     (deve estar em VERDE com ✅)    │
├─────────────────────────────────────┤
│  3. Tipo: HTTP(s)                   │
├─────────────────────────────────────┤
│  4. Last Check:                     │
│     "a few seconds ago"             │
│     Ou "a minute ago"               │
│     (NÃO deve ser "never checked")  │
├─────────────────────────────────────┤
│  5. Clique no monitor para ver:     │
│     URL = https://bridgeapi-xxxxx   │
│          .onrender.com/api/health   │
│     Interval = every 5 minutes      │
└─────────────────────────────────────┘
```

**Checklist UptimeRobot:**
- [ ] Vejo o monitor "BridgeAPI"?
- [ ] Status diz "UP" em VERDE?
- [ ] Last Check foi há poucos minutos (não "never")?
- [ ] URL está correta?
- [ ] Interval é "5 minutes"?

**⚠️ Se estiver VERMELHO ou "DOWN":**
- Não se assuste, pode ser normal se acabou de criar
- Espere 5-10 minutos
- Recarregue a página (F5)
- Deve ficar verde em breve

---

## ✅ TESTE RÁPIDO

### Abra uma nova aba no Opera GX

Copie e cole sua URL com `/api/health`:

```
https://bridgeapi-xxxxx.onrender.com/api/health
```

**Você deve ver (no navegador):**
```
200 OK

{
  "status": "healthy",
  "timestamp": "2026-04-01T...",
  "version": "1.0.0"
}
```

**Ou algo assim (json):**
- [ ] Vejo `"status":"healthy"`?
- [ ] Vejo um JSON (não erro)?
- [ ] Status da página é 200?

---

## 📋 RESUMO DO CHECKLIST

```
Render.com          Result
├─ LIVE status      [ ] Verde ou Red?
├─ Service name     [ ] "bridgeapi" visível?
├─ Last Deploy OK   [ ] "Success"?
├─ URL visível      [ ] "https://bridgeapi-xxxxx.onrender.com"?
└─ Logs err         [ ] Tem erro vermelho?

UptimeRobot         Result
├─ Monitor existe   [ ] Visce "BridgeAPI Keep-Alive"?
├─ UP status        [ ] Verde ✅?
├─ Last check       [ ] Há poucos minutos?
├─ URL correto      [ ] "...onrender.com/api/health"?
└─ Interval         [ ] "every 5 minutes"?

API Test            Result
├─ Health endpoint  [ ] Responde 200 OK?
├─ JSON válido      [ ] Tem "status":"healthy"?
└─ Acesso           [ ] Abrir via Render URL funciona?
```

---

## 💬 Me Diga Aqui

Depois de checker, me conta:

1. **Render status é LIVE?** (Sim / Não)
2. **Vê o monitor no UptimeRobot?** (Sim / Não / Esperando)
3. **UptimeRobot status é UP?** (Sim / Não / Esperando)
4. **API health endpoint responde?** (200 OK / Erro / Não testei)
5. **Tem algo em vermelho?** (Não / Sim - qual?)

---

## 🆘 Se Algo Estiver Errado

**Mensagem comum no início:**

❌ "Waiting" - Normal! UptimeRobot acaba de criar (espera 5-10min)  
❌ "Build Failed" no Render - check Logs tab, pode ser erro de build  
❌ "Cannot connect" - API pode estar iniciando, espera 2-3min  
❌ "404" no health endpoint - Render ainda bulidando/iniciando  

**Solução rápida:**
1. Espera 5 minutos
2. Recarrega página (F5 no Opera)
3. Me conta o que vê

---

**Use este checklist agora enquanto está olhando para as páginas no Opera GX!**
