# Guia Rápido de Deployment Gratuito

## 🚀 Em 10 Minutos

### Opção 1: Railway.app (Recomendado)

1. **Acesse:** https://railway.app
2. **Login com GitHub** (autorize Railway)
3. **Click:** "New Project" → "Deploy from GitHub repo"
4. **Selecione:** `URSoftware/BridgeAPI`
5. **Espere:** 2-5 minutos
6. **Pronto!** Sua API está online

```
https://seu-dominio.railway.app/api/health
```

**Custo:** $5/mês grátis (suficiente para sua API)

---

### Opção 2: Render.com (Gratuito Permanente)

1. **Acesse:** https://render.com
2. **Login com GitHub**
3. **Click:** "New +" → "Web Service"
4. **Conecte:** Repositório BridgeAPI
5. **Aguarde:** 3-5 minutos
6. **Pronto!** API online

**Desvantagem:** Servidor dorme após 15 min inativo

---

### Opção 3: Replit (Gratuito)

1. **Acesse:** https://replit.com
2. **Import from GitHub:** `URSoftware/BridgeAPI`
3. **Click "Run"**
4. **Pronto!** Servidor rodando

**Acesso:** https://seu-replit-url.repl.co

---

## ⚠️ Aviso Importante

Antes de fazer deployment, certifique-se que:

✓ Seu repositório está no GitHub  
✓ Todos os commits foram feitos  
✓ Dockerfile está pronto  
✓ railway.json está configurado

```bash
# Verificar status
git status

# Se necessário
git push origin master
```

---

## 📊 Comparação

| Plataforma | Custo | Setup | Sempre Online |
|-----------|-------|-------|--------------|
| Railway | $5/mês grátis | 5 min | Sim |
| Render | Grátis | 5 min | Não* |
| Replit | Grátis | 3 min | Não* |
| Fly.io | $3/mês grátis | 10 min | Sim |

*Dorme após inatividade

---

## 🔗 Links Úteis

- [Guia Completo de Deployment](FREE_DEPLOY.md)
- [Guia de Instalação Local](SETUP.md)
- [Documentação Swagger](#) (após deploy)
- [Railway Docs](https://docs.railway.app/)

---

**Recomendação:** Use **Railway.app** para melhor experiência com .NET

Qualquer dúvida? Abra uma issue no [GitHub](https://github.com/URSoftware/BridgeAPI)
