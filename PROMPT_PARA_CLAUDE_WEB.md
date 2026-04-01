# 🤖 PROMPT PARA CLAUDE WEB - Setup Render + UptimeRobot

**Copie tudo abaixo e cole na janela de chat do Claude (navegador):**

---

```
Você é um assistente especializado em configuração de cloud. Preciso que você me ajude a:

## CONTEXTO DO PROJETO
- Projeto: BridgeAPI (REST API em C# .NET 9.0)
- Repositório: https://github.com/URSoftware/BridgeAPI
- Branch ativa: main
- Documentação: /docs/ (no repositório)
- API local: http://localhost:5002/api/health (200 OK - funcionando)

## OBJETIVO FINAL
Deixar a BridgeAPI online 24/7 no Render.com com monitoramento via UptimeRobot

## O QUE VOCÊ PRECISA FAZER

### PARTE 1: VERIFICAR/COMPLETAR RENDER.COM

Vou acessar https://dashboard.render.com (já estou logado)

Verifique e configure se necessário:

1. **Web Service Exists?**
   - [ ] Existe um serviço chamado "bridgeapi"?
   - [ ] Se NÃO existe: Crie um novo Web Service:
     - Name: `bridgeapi`
     - Environment: `.NET`
     - Branch: `main`
     - Conectar ao repositório: `URSoftware/BridgeAPI`

2. **Configuration Check**
   - [ ] Build Command: `dotnet build --configuration Release`
   - [ ] Start Command: `dotnet BridgeAPI.dll`
   - [ ] Se não estão: EDITE Settings e adicione

3. **Environment Variables**
   - [ ] Existe `ASPNETCORE_URLS`?
   - [ ] Value: `http://0.0.0.0:10000`
   - [ ] Se não: Add → Environment Variable
   - [ ] Existe `ASPNETCORE_ENVIRONMENT`?
   - [ ] Value: `Production`
   - [ ] Se não: Add → Environment Variable

4. **Deployment Status**
   - [ ] Status mostra "Live"?
   - [ ] Se NÃO mostra: Espere build completar (2-3 min) OU clique "Re-run Latest Deployment"
   - [ ] Quando status for "Live", copie a URL: `https://bridgeapi-xxxxx.onrender.com`

5. **Test Render**
   - [ ] Abra no navegador: `https://bridgeapi-xxxxx.onrender.com/api/health`
   - [ ] Deve mostrar: 200 OK com JSON `{"status":"healthy"...}`
   - [ ] Se erro 404: Espere mais 2 min, Render pode estar ainda iniciando

### PARTE 2: VERIFICAR/COMPLETAR UPTIMEROBOT

Vou acessar https://uptimerobot.com/dashboard (já estou logado)

1. **Monitor Exists?**
   - [ ] Existe um monitor para BridgeAPI?
   - [ ] Se NÃO existe: Crie novo:
     - Click "+ Add Monitor" ou "Add Monitoring"
     - Monitor Type: `HTTP(s)`
     - Friendly Name: `BridgeAPI Keep-Alive`
     - URL: `https://bridgeapi-xxxxx.onrender.com/api/health` (use a URL do Render de cima)
     - HTTP Method: `GET`
     - Monitor Every: `5 minutes`
     - Timeout: `30 seconds`
     - Click "Create Monitor"

2. **Monitor Status Check**
   - [ ] Status mostra "Up" (verde)?
   - [ ] Se mostra "Waiting" ou "Paused": Espere 5-10 min, é normal na primeira vez
   - [ ] Se mostra "Down" (vermelho): 
     - Confirme que a URL do Render está correta
     - Confirme que Render status é "Live"
     - Espere mais 5 min e recarregue (F5)

3. **Test UptimeRobot**
   - [ ] Clique no monitor
   - [ ] Veja "Last Check" timestamp
   - [ ] Deve mostrar: "a few seconds ago" ou "a minute ago"
   - [ ] Se mostrar "never checked": Espere 5-10 minutos

### PARTE 3: TESTE FINAL INTEGRADO

1. **API Health Endpoint**
   ```
   GET https://bridgeapi-xxxxx.onrender.com/api/health
   Expected: 200 OK com {"status":"healthy","timestamp":"...","version":"1.0.0"}
   ```

2. **Swagger Documentation**
   ```
   https://bridgeapi-xxxxx.onrender.com/swagger/index.html
   Expected: Página com documentação da API
   ```

3. **UptimeRobot Monitor Confirmado**
   - [ ] Status: "Up" ✅
   - [ ] Last check: poucos minutos atrás
   - [ ] URL: correta (mesmo do Render)

## SE ALGO DER ERRADO

### Render Build Failed (erro ao fazer build)
- [ ] Clique em "Logs" tab
- [ ] Veja qual erro específico aparece
- [ ] Se arquivo não encontrado: Confirme que a branch é "main" (main tem todos os arquivos)
- [ ] Se erro de dependência: Pode ser timeout, clique "Re-run Latest Deployment"

### Render Status não muda de "Building"
- [ ] Espere 5 minutos completos
- [ ] Se continuarem "Building": Clique "Re-run Latest Deployment"
- [ ] Se volta a falhar: Veja Logs para encontrar erro

### UptimeRobot shows "Down" (vermelho)
1. Confirme:
   - [ ] Render está "Live" (não "Building" ou "Failed")
   - [ ] URL está exatamente certa (copie de Render, cole no UptimeRobot)
2. Tente:
   - [ ] Abra a URL no navegador manualmente: `https://bridgeapi-xxxxx.onrender.com/api/health`
   - [ ] Se responde 200 OK: Espere 5-10 minutos para UptimeRobot verificar novamente
   - [ ] Se error (404, 500, etc): Render ainda está iniciando

### UptimeRobot shows "Waiting" (amarelo)
- Totalmente normal! É a primeira verificação
- [ ] Espere 5-10 minutos
- [ ] Recarregue dashboard (F5)
- [ ] Deve mudar para "Up" (verde)

## CONCLUSÃO

Quando tudo estiver assim:

✅ Render Status: "LIVE" (verde)
✅ Render Deployment: "Success" (verde)
✅ Health endpoint: 200 OK
✅ UptimeRobot: "Up" (verde)
✅ UptimeRobot: Last check a poucos minutos

ENTÃO: SUCESSO! 🎉 A API está online 24/7

## INFORMAÇÕES IMPORTANTES

- GitHub Repo: https://github.com/URSoftware/BridgeAPI
- Branch padrão: main (já tem todos commits)
- Documentação completa em: /docs/ (no repo)
- API local funciona: http://localhost:5002 (para referência)

## O QUE FAZER DEPOIS

Depois que tudo estiver online:

1. Você pode fazer código novo na branch `beta` para testes
2. Quando pronto: Push para `main` = Auto-deploy no Render
3. UptimeRobot vai estar preso (pingando a cada 5 min)
4. API NUNCA vai dormir (Render puts free apps to sleep after 15 min idle - UptimeRobot evita isso)

---

ME CONTE:
1. Qual é a URL do seu Render? (https://bridgeapi-xxxxx.onrender.com)
2. Status do Render agora? (LIVE/Building/Failed)
3. Status do UptimeRobot? (Up/Down/Waiting)
4. Tudo funcionando? (200 OK no health endpoint?)
```

---

## 🔍 COMO USAR ESTE PROMPT

1. Abra Claude no navegador: https://claude.ai
2. Copie TODO o texto acima (da linha com triple backticks)
3. Cole na janela de chat do Claude
4. Claude vai seguir passo-a-passo automaticamente
5. Ele vai te guiar pelo processo

---

## 📍 AINDA MAIS FÁCIL

Se preferir, você pode simplesmente copiar e colar ESTE:

```
Eu tenho uma API em C# .NET hospedada no GitHub (URSoftware/BridgeAPI, branch main).

Preciso:
1. Configurar no Render.com para deixar online
2. Configurar no UptimeRobot para nunca dormir

Acesso Render: https://dashboard.render.com (já estou logado)
Acesso UptimeRobot: https://uptimerobot.com/dashboard (já estou logado)

API URL local (para referência): http://localhost:5002/api/health (responde 200 OK)

Me ajuda a:
1. Verificar se existe um Web Service no Render chamado "bridgeapi"
2. Se não existe: criar um novo com GitHub integration
3. Configurar environment variables (ASPNETCORE_URLS e ASPNETCORE_ENVIRONMENT)
4. Verificar se deployment está "Live"
5. Depois: Criar monitor no UptimeRobot que pinga o /api/health a cada 5 minutos
6. Confirmar que está tudo online

Detalhe passo-a-passo o que fazer em cada plataforma.
```

---

**Escolha qual prompt usar e passe pro Claude!**
