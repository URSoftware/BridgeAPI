# Manter API Sempre Ativa no Render.com

O Render.com coloca serviços gratuitos para "dormir" após 15 minutos de inatividade. Este guia mostra como manter sua BridgeAPI **sempre ativa gratuitamente**.

## Problema do Render.com Gratuito

```
Primeira requisição após inatividade = 30-50 segundos de delay
```

Isso acontece porque o servidor precisa "acordar" (spin up).

---

## Solução 1: UptimeRobot (Recomendado) ⭐

**UptimeRobot** é um serviço gratuito que faz "ping" na sua API a cada 5 minutos, mantendo-a ativa.

### Passo 1: Criar Conta UptimeRobot

1. **Acesse:** https://uptimerobot.com
2. **Click:** "Sign Up Free"
3. **Preencha:** Email e senha
4. **Login**

### Passo 2: Criar Monitor

1. **Click:** "Add New Monitor"
2. **Preencha:**
   - **Monitor Type:** HTTP(s)
   - **Friendly Name:** `BridgeAPI Health Check`
   - **URL:** `https://seu-render-url.onrender.com/api/health`
   - **Monitoring Interval:** `5 minutes` (padrão)
   - **Alert Contacts:** Seu email
3. **Click:** "Create Monitor"

### Passo 3: Pronto!

UptimeRobot fará ping a cada 5 minutos, mantendo sua API **sempre acordada**.

**Benefícios:**
- ✓ Gratuito
- ✓ 50 monitores gratuitos
- ✓ Alertas por email se cair
- ✓ Dashboard com histórico de uptime

**Custo:** $0/mês

---

## Solução 2: Render Cron Jobs + GitHub Actions

Usar GitHub Actions para fazer ping automático na sua API.

### Passo 1: Criar Arquivo GitHub Actions

Já criamos em `.github/workflows/render-keepalive.yml`

### Passo 2: Ativar no GitHub

1. Vá para seu repositório GitHub
2. **Settings** → **Actions** → **General**
3. Marque **Enable local and third party Actions**
4. Salve

### Passo 3: Pronto!

GitHub Actions fará ping a cada 5 minutos automaticamente.

---

## Solução 3: Upgrade Render (Pago)

Se preferir não usar serviço externo:

- **Render Pro:** $7/mês
- Servidores **sempre ligados**
- Sem delay na primeira requisição

---

## Qual Escolher?

| Solução | Custo | Setup | Confiabilidade |
|---------|-------|-------|-----------------|
| **UptimeRobot** | Grátis | 5 min | Alta |
| **GitHub Actions** | Grátis | 2 min | Alta |
| **Render Pro** | $7/mês | 0 min | Perfeita |

**Recomendação:** Use **UptimeRobot** - é mais confiável e oferece alertas.

---

## Como Saber se Está Funcionando

### Teste Manual

```bash
# Primeira requisição (depois de inativo)
curl https://seu-render-url.onrender.com/api/health

# Segunda requisição (deve ser instantâneo)
curl https://seu-render-url.onrender.com/api/health
```

A primeira pode demorar 30s, a segunda é instantânea.

### Dashboard UptimeRobot

Vá para https://uptimerobot.com/dashboard

Você verá:
- ✓ Monitor status
- ✓ Últimos pings
- ✓ Histórico de uptime
- ✓ Alertas

---

## Teste de Carga

Para verificar que está funcionando:

```bash
# Com UptimeRobot rodando, teste:
time curl https://seu-render-url.onrender.com/api/health

# Resposta esperada:
# "status":"healthy"
# time: ~150ms (instantâneo)
```

---

## Monitoramento Adicional

### UptimeRobot Dashboard

Acesse: https://uptimerobot.com/dashboard

Você pode:
- Ver status em tempo real
- Receber alertas por email
- Visualizar histórico
- Configurar múltiplos monitores

### Integração com Slack (Opcional)

Para receber alertas no Slack:

1. UptimeRobot → **Settings** → **Alert Contacts**
2. **Add Alert Contact**
3. **Type:** Slack
4. Conecte sua workspace Slack
5. Pronto! Receive alertas no Slack

---

## Alternativas Gratuitas

### Healthchecks.io

```
https://healthchecks.io
```

Serviço open-source para health checks.

### Cronitor

```
https://cronitor.io
```

Monitoramento de cron jobs e HTTP.

---

## Resumo Rápido

### Para Render.com Gratuito Sempre Ativo:

1. **Vá para:** https://uptimerobot.com
2. **Crie conta:** Email + Senha
3. **Adicione monitor:** `https://seu-render-url.onrender.com/api/health`
4. **Intervalo:** 5 minutos
5. **Pronto!** API sempre acordada

**Tempo total:** 5 minutos

---

## Se Algo Deu Errado

### Monitor mostra como DOWN

1. Verifique se Render URL está correta
2. Confirme se API está rodando no Render
3. Tente acessar URL manualmente no navegador

### UptimeRobot não está funcionando

1. Verifique conectividade da API
2. Aumente intervalo de ping para 10 minutos
3. Mude URL para `/api/health`

---

## Próximas Etapas

Com a API sempre ativa:

1. Compartilhe o link com clientes
2. Configure Banco de Dados remoto (PostgreSQL no Render)
3. Implemente autenticação
4. Monitore performance

---

**Versão:** 1.0.0  
**Última Atualização:** 01 de Abril de 2026  
**Recomendação:** Use UptimeRobot para melhor experiência
