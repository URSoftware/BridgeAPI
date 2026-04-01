# Deploy Gratuito - Guia Passo a Passo

Este guia mostra como fazer deploy da BridgeAPI gratuitamente no Railway.app em menos de 10 minutos.

## Por que Railway.app?

✓ **Gratuito:** $5/mês em crédito grátis (suficiente para API pequena)  
✓ **Suporta .NET:** Deploy automático de projetos C#  
✓ **GitHub Integration:** Deploy automático a cada push  
✓ **Banco de Dados:** Suporte a PostgreSQL, MySQL, MongoDB grátis  
✓ **Domínio Automático:** Acesso público imediato  
✓ **Sem Cartão de Crédito:** Basta GitHub

---

## Passo 1: Preparar o Repositório GitHub

### 1.1 Verificar se está no GitHub

Seu repositório deve estar em:
```
https://github.com/URSoftware/BridgeAPI
```

### 1.2 Fazer push de todas as mudanças

Antes de tudo, certifique-se que tudo está commitado:

```bash
cd c:\Users\thyag\OneDrive\Desktop\Estudos\BridgeAPI

# Verificar status
git status

# Se houver arquivos novos:
git add .
git commit -m "feat: Add deployment configurations for Railway"
git push origin master
```

---

## Passo 2: Criar Conta no Railway

1. **Acesse:** https://railway.app

2. **Click em "Start Free"**

3. **Faça login com GitHub**
   - Click "Continue with GitHub"
   - Autorize a aplicação Railway
   - Permita que Railway acesse seus repositórios

4. **Pronto!** Você tem uma conta Railway

---

## Passo 3: Criar Novo Projeto

1. **Click em "New Project"**

2. **Selecione "Deploy from GitHub repo"**

3. **Procure por "BridgeAPI"**
   - Se não aparecer, click "Configure GitHub App"
   - Selecione o repositório `BridgeAPI`
   - Clique "Deploy"

---

## Passo 4: Configurar a Aplicação

Railway vai detectar automaticamente:
- Linguagem: **.NET**
- Framework: **ASP.NET Core**
- Dockerfile: Será usado automaticamente

**Aguarde o deploy** (2-5 minutos)

Você verá:
```
✓ Building image...
✓ Pushing image...
✓ Creating deployment...
✓ Application running!
```

---

## Passo 5: Acessar Sua API

Após o deploy, você terá:

```
https://bridgeapi-[random].railway.app
```

### Testar:

**Health Check:**
```bash
curl https://bridgeapi-[random].railway.app/api/health
```

**Resposta esperada:**
```json
{
  "status": "healthy",
  "timestamp": "2026-04-01T21:30:00Z",
  "version": "1.0.0"
}
```

**Swagger (Documentação):**
```
https://bridgeapi-[random].railway.app/swagger
```

---

## Passo 6: Usar Domínio Customizado (Opcional)

Se quiser um domínio mais bonito como `meuapi.railway.app`:

1. Vá para seu projeto no Railway
2. Click em **Settings** → **Domain**
3. Clique em **Add Domain**
4. Digite seu domínio desejado
5. Click **Add**

Seu novo domínio:
```
https://seu-dominio-aqui.railway.app/api/health
```

---

## Deploy Automático com GitHub

Agora, **TODA VEZ** que você faz um `push` para `master`:

```bash
git commit -m "Your changes"
git push origin master
```

**Railway faz deploy automático** em ~2 minutos!

---

## Variáveis de Ambiente no Railway

Se precisar configurar variáveis (como porta, ambiente):

1. Vá para seu projeto
2. Click em **Variables**
3. Adicione:

| Key | Value |
|-----|-------|
| `ASPNETCORE_ENVIRONMENT` | `Production` |
| `ASPNETCORE_URLS` | `http://+:5000` |

---

## Limites do Plano Gratuito

- **$5/mês em crédito grátis**
- **1 Projeto com 1 serviço**
- **512MB RAM**
- **1GB Armazenamento**
- **Banda ilimitada**

**Consumo estimado da BridgeAPI:**
- Apenas API em repouso: ~$0.50/mês
- Com uso moderado: ~$1.50-2.00/mês

✓ Dentro do limite gratuito!

---

## Alternativas Gratuitas

Se Railway não funcionar, tente:

### Render.com

```
https://render.com
```

- **Plano Free:** Grátis permanente
- **Desvantagem:** Servidor vai dormir após 15 min inativo
- **Deploy:** Mesmo processo do Railway

### Fly.io

```
https://fly.io
```

- **Plano Free:** 3 shared-cpu boxes grátis
- **Vantagem:** Sempre ligado
- **Deploy:** Precisa instalar CLI

### Oracle Cloud (Always Free)

```
https://www.oracle.com/cloud/free/
```

- **Totalmente grátis para sempre**
- **Servidor sempre ligado**
- **Desvantagem:** Setup mais complexo

---

## Troubleshooting

### Deploy falhou

**Solução:**

1. Verifique o build log no Railway
2. Certifique-se que `Dockerfile` está correto
3. Verifique se o `.NET` SDK está incluso no Dockerfile

### API não responde

**Verificar:**

1. Vá para **Logs** no Railway
2. Procure por erros de inicialização
3. Verifique se aplicação está rodando

### Porta incorreta

O Railway **automaticamente** define a porta via variável `PORT`.

Certifique-se que o `Dockerfile` usa:
```dockerfile
ENV ASPNETCORE_URLS=http://+:5000
```

---

## Próximos Passos

Com sua API online, agora você pode:

1. **Usar em produção:** Clientes JavaScript podem acessar
2. **Configurar banco remoto:** PostgreSQL, MySQL, etc
3. **Adicionar autenticação:** JWT, OAuth
4. **Monitorar:** Logs em tempo real

---

## URLs Importantes

- Railway Dashboard: https://railway.app/dashboard
- Seu Projeto: https://railway.app/project/[project-id]
- Documentação Railway: https://docs.railway.app/

---

## Suporte

Se tiver problemas:

1. **Railway Docs:** https://docs.railway.app/
2. **Railway Discord:** https://discord.gg/xAm2w6g
3. **GitHub Issues:** https://github.com/URSoftware/BridgeAPI/issues

---

**Pronto!** Sua API está online e pronta para usar! 🚀

Você pode compartilhar o link com qualquer pessoa:

```
https://seu-dominio.railway.app/api/health
```

---

**Versão:** 1.0.0  
**Última Atualização:** 01 de Abril de 2026  
**Status:** Production Ready ✓
