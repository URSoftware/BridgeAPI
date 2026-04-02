# ✅ CHECKLIST PARA RAILWAY DEPLOYMENT

## PRÉ-DEPLOYMENT (Você já tem tudo)
- [x] Código .NET 9 compilado e testado
- [x] Dockerfile otimizado (multi-stage build)
- [x] Program.cs lê PORT de environment variable
- [x] railway.toml configurado
- [x] GitHub repositório sincronizado
- [x] Sem erros de compilação

## DEPLOY NO RAILWAY (Execute agora)

### 1. CONTA RAILWAY
- [ ] Acesse https://railway.app
- [ ] Clique "Sign up"
- [ ] Autorize com GitHub
- [ ] Crie conta com seu email

### 2. NOVO PROJETO
- [ ] No dashboard, clique "+ New Project"
- [ ] Selecione "Deploy from GitHub repo"
- [ ] Procure por "URSoftware/BridgeAPI"
- [ ] Clique nele
- [ ] Selecione branch "main"

### 3. CONFIGURAÇÃO RAILWAY
- [ ] Railway detecta Dockerfile automaticamente
- [ ] Espere build terminar (~3-5 minutos)
- [ ] Verifique status em "Logs" → "Deployment"

### 4. VARIÁVEIS DE AMBIENTE
- [ ] Vá em: Seu Projeto → "Variables"
- [ ] Confirme que estão setadas:
   - [ ] `PORT = 8080`
   - [ ] `ASPNETCORE_ENVIRONMENT = Production`
- Se não estiverem, clique "+" e adicione

### 5. OBTER URL
- [ ] Projeto → clique em seu APP
- [ ] Procure por "Hostname" ou "URL"
- [ ] Copia será como: `https://bridgeapi-xxxxx.railway.app`

### 6. TESTE FINAL
- [ ] Cole URL em navegador: `https://bridgeapi-xxxxx.railway.app/api/health`
- [ ] Espere resposta 200 OK
- [ ] Veja JSON: `{"status":"healthy",...}`
- [ ] ✅ SUCESSO!

---

## TROUBLESHOOTING

### ❌ Erro: "Build failed"
```
Solução:
1. Verifique Logs → errors vermelhos
2. Confirme Dockerfile está na raiz
3. Tente fazer um novo push ao GitHub
```

### ❌ Erro: "Port already in use"
```
Solução:
1. Vá em Variables
2. Mude PORT para 8081
3. Restart deployment
```

### ❌ Erro: "503 Service Unavailable"
```
Solução:
1. Espere mais 2-3 minutos (deployment ainda processando)
2. Verifique em Logs que app iniciou com sucesso
3. Se persistir, verifique Program.cs
```

### ❌ Erro: "404 Not Found"
```
Solução:
1. URL precisa terminar em `/api/health` (case-sensitive)
2. Verifique em Logs se Controllers foram mapeados
3. Confira se porta é 8080
```

---

## SUPORTE

Se tudo falhar:
1. Verifique Logs no Dashboard Railway
2. Procure por "error" em vermelho
3. Verifique se Dockerfile compila localmente:
   ```
   docker build -t test .
   docker run -p 5000:5000 test
   ```
4. Se compilar localmente, problema é config Railway

---

## REFERÊNCIAS

- [railway.app docs](https://docs.railway.app)
- [.NET on Railway](https://docs.railway.app/guides/dotnet)
- [Seu GitHub](https://github.com/URSoftware/BridgeAPI)

---

Última atualização: 1º abril 2026
Status: ✅ Pronto para deployment
