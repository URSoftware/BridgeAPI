# Deploy no Render.com + Manter Sempre Ativo

Guia super simples para deixar sua BridgeAPI sempre ativa no Render.com.

## Passo 1: Deploy no Render (5 minutos)

### 1.1 Criar Conta Render

1. Acesse: https://render.com
2. Click "Sign up"
3. Login com GitHub
4. Autorize Render

### 1.2 Deploy da API

1. Click "New +" → "Web Service"
2. Conecte repo: `URSoftware/BridgeAPI`
3. Preencha:
   - **Name:** `bridgeapi`
   - **Runtime:** Docker
   - **Build command:** (deixe em branco)
   - **Start command:** `dotnet BridgeAPI.dll`
   - **Plan:** Free
4. Click "Deploy"
5. **Espere:** 2-5 minutos

### 1.3 Pegar URL

Após deploy, você terá:
```
https://bridgeapi-xyz.onrender.com
```

---

## Passo 2: Testar API

```bash
curl https://bridgeapi-xyz.onrender.com/api/health

# Resposta esperada:
# {"status":"healthy","timestamp":"...","version":"1.0.0"}
```

---

## Passo 3: Manter Sempre Ativo com UptimeRobot (5 minutos)

### 3.1 Criar Conta UptimeRobot

1. Acesse: https://uptimerobot.com
2. Click "Sign Up"
3. Email e senha
4. Confirme email

### 3.2 Adicionar Monitor

1. Click "Add New Monitor"
2. Preencha:
   - **Type:** HTTP(s)
   - **Name:** BridgeAPI
   - **URL:** `https://bridgeapi-xyz.onrender.com/api/health`
   - **Interval:** 5 minutes
   - **Timeout:** 30 seconds
3. Click "Create Monitor"

### 3.3 Pronto!

UptimeRobot fará ping a cada 5 minutos = API sempre acordada

---

## Resultado Final

```
✓ API rodando no Render.com
✓ URL pública: https://bridgeapi-xyz.onrender.com
✓ Sempre ativa (UptimeRobot)
✓ Custo: $0/mês
✓ Tempo de setup: 10 minutos
```

---

## Testar se Está Funcionando

### Antes de UptimeRobot

```bash
# Primeira requisição (app dormindo)
$ time curl https://bridgeapi-xyz.onrender.com/api/health
real    0m35.234s  # 35 segundos! (spin up)

# Segunda requisição (app acordado)  
$ time curl https://bridgeapi-xyz.onrender.com/api/health
real    0m0.258s   # Instantâneo
```

### Depois de UptimeRobot

```bash
# Primeira requisição (UptimeRobot já fez ping)
$ time curl https://bridgeapi-xyz.onrender.com/api/health
real    0m0.185s   # Instantâneo sempre!
```

---

## Links Importantes

- Render Dashboard: https://dashboard.render.com
- UptimeRobot Dashboard: https://uptimerobot.com/dashboard
- Sua API: `https://bridgeapi-xyz.onrender.com/api/health`
- Seu Swagger: `https://bridgeapi-xyz.onrender.com/swagger`

---

## Próximos Passos

Agora que sua API está online:

1. **Compartilhe o link:** Envie a URL para clientes/usuários
2. **Integre no frontend:** JavaScript pode fazer requests para sua API
3. **Configure banco remoto:** PostgreSQL no Render (gratuito)
4. **Adicione autenticação:** Se necessário

---

## Troubleshooting

### UptimeRobot mostra monitor como DOWN

**Solução:**
1. Verifique se Render URL está correta
2. Inicie a aplicação no Render (pode estar pausada)
3. Tente acessar URL no navegador manualmente

### API não responde

**Solução:**
1. Acesse Render Dashboard
2. Verifique logs da aplicação
3. Reinicie a web service

### Monitor diz "Timeout"

**Solução:**
1. Aumente timeout para 60 segundos em UptimeRobot
2. Verifique latência de rede
3. Tente mudar URL para `/swagger` em vez de `/api/health`

---

**Versão:** 1.0.0  
**Tempo de Setup:** ~10 minutos  
**Custo:** $0/mês (totalmente gratuito)
