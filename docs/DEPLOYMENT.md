# Guia de Deployment - BridgeAPI

Este guia descreve como fazer deploy da BridgeAPI em diferentes ambientes.

## Índice

1. [Deployment Local](#deployment-local)
2. [Deployment com Docker](#deployment-com-docker)
3. [Deployment em Azure](#deployment-em-azure)
4. [Deployment em Heroku](#deployment-em-heroku)
5. [Deployment em AWS](#deployment-em-aws)

---

## Deployment Local

### Requisitos

- .NET 9.0 SDK instalado
- Windows, macOS ou Linux

### Passos

1. **Clone o repositório**
   ```bash
   git clone https://github.com/URSoftware/BridgeAPI.git
   cd BridgeAPI
   ```

2. **Execute o script de inicialização**
   
   Windows:
   ```cmd
   run.bat
   ```
   
   macOS/Linux:
   ```bash
   chmod +x run.sh
   ./run.sh
   ```

3. **Acesse a API**
   ```
   http://localhost:5000
   ```

---

## Deployment com Docker

### Requisitos

- Docker Desktop instalado (https://www.docker.com/products/docker-desktop)
- Docker CLI disponível no terminal

### Passos

1. **Clone o repositório**
   ```bash
   git clone https://github.com/URSoftware/BridgeAPI.git
   cd BridgeAPI
   ```

2. **Build da imagem Docker**
   ```bash
   docker build -t bridgeapi:latest .
   ```

3. **Execute o container**
   ```bash
   docker run -p 5000:5000 -d --name bridgeapi bridgeapi:latest
   ```

4. **Verifique se está rodando**
   ```bash
   docker ps
   curl http://localhost:5000/api/health
   ```

### Usando Docker Compose (Recomendado)

1. **Execute com um comando**
   ```bash
   docker-compose up -d
   ```

2. **Parar o serviço**
   ```bash
   docker-compose down
   ```

3. **Ver logs**
   ```bash
   docker-compose logs -f bridgeapi
   ```

---

## Deployment em Azure

### Opção 1: Azure Container Instances

1. **Criar um Azure Container Registry**
   ```bash
   az acr create --resource-group myResourceGroup \
     --name myBridgeAPIRegistry --sku Basic
   ```

2. **Build e push da imagem**
   ```bash
   docker build -t bridgeapi:latest .
   docker tag bridgeapi:latest myBridgeAPIRegistry.azurecr.io/bridgeapi:latest
   docker push myBridgeAPIRegistry.azurecr.io/bridgeapi:latest
   ```

3. **Criar Container Instance**
   ```bash
   az container create \
     --resource-group myResourceGroup \
     --name bridgeapi-instance \
     --image myBridgeAPIRegistry.azurecr.io/bridgeapi:latest \
     --ports 5000 \
     --ip-address Public
   ```

### Opção 2: Azure App Service

1. **Criar App Service Plan**
   ```bash
   az appservice plan create \
     --name myBridgeAPIPlan \
     --resource-group myResourceGroup \
     --sku B1 \
     --is-linux
   ```

2. **Criar Web App**
   ```bash
   az webapp create \
     --resource-group myResourceGroup \
     --plan myBridgeAPIPlan \
     --name myBridgeAPI \
     --runtime "DOTNETCORE|9.0"
   ```

3. **Deploy com Git**
   ```bash
   git remote add azure [URL do repositório Azure]
   git push azure master
   ```

---

## Deployment em Heroku

### Requisitos

- Heroku CLI instalado
- Conta Heroku criada

### Passos

1. **Criar arquivo Procfile**
   ```
   web: dotnet BridgeAPI.dll --urls="http://+:$PORT"
   ```

2. **Criar aplicação no Heroku**
   ```bash
   heroku create meu-bridgeapi
   ```

3. **Deploy com Git**
   ```bash
   git push heroku master
   ```

4. **Acessar a aplicação**
   ```bash
   heroku open
   # Sua API estará em: https://meu-bridgeapi.herokuapp.com
   ```

---

## Deployment em AWS

### Opção 1: AWS Elastic Beanstalk

1. **Instale o Elastic Beanstalk CLI**
   ```bash
   pip install awsebcli --upgrade --user
   ```

2. **Inicialize a aplicação**
   ```bash
   eb init -p "dotnet 9.0" bridgeapi --region us-east-1
   ```

3. **Crie um ambiente**
   ```bash
   eb create bridgeapi-env
   ```

4. **Deploy**
   ```bash
   eb deploy
   ```

### Opção 2: AWS EC2 + Docker

1. **Inicie uma instância EC2**
   - AMI: Ubuntu 22.04 LTS
   - Tipo: t3.micro (elegível para free tier)

2. **SSH na instância**
   ```bash
   ssh -i chave.pem ubuntu@[IP-PUBLICO]
   ```

3. **Instale Docker**
   ```bash
   curl -fsSL https://get.docker.com -o get-docker.sh
   sudo sh get-docker.sh
   ```

4. **Clone e execute**
   ```bash
   git clone https://github.com/URSoftware/BridgeAPI.git
   cd BridgeAPI
   sudo docker-compose up -d
   ```

5. **Configure o Security Group**
   - Inbound: TCP 5000 de qualquer lugar (0.0.0.0/0)

---

## Variáveis de Ambiente

Você pode configurar as seguintes variáveis:

| Variável | Padrão | Descrição |
|----------|--------|-----------|
| `ASPNETCORE_URLS` | `http://localhost:5000` | URL de escuta da API |
| `ASPNETCORE_ENVIRONMENT` | `Production` | Ambiente (Production/Development) |
| `ASPNETCORE_HTTPS_PORT` | `443` | Porta HTTPS |

### Exemplo

```bash
export ASPNETCORE_ENVIRONMENT=Production
export ASPNETCORE_URLS=http://+:8080
dotnet run
```

---

## Monitoramento e Logs

### Docker

Ver logs em tempo real:
```bash
docker logs -f bridgeapi
```

### Heroku

Ver logs:
```bash
heroku logs --tail
```

### Azure

Ver logs ao vivo:
```bash
az webapp log tail --resource-group myResourceGroup --name myBridgeAPI
```

---

## Segurança em Produção

1. **Use HTTPS**
   - Configure certificados SSL/TLS
   - Force redirecionamento HTTP → HTTPS

2. **Variáveis de Ambiente**
   - Não armazene secrets no código
   - Use gerenciadores de secrets (Azure Key Vault, AWS Secrets Manager)

3. **Rate Limiting**
   - Implemente rate limiting para prevenir abuso
   - Configure firewalls

4. **Backup de Banco de Dados**
   - Configure backups automáticos
   - Teste recovery regularmente

5. **Monitoramento**
   - Configure alertas de erro
   - Monitore performance
   - Rastreie uptime

---

## Troubleshooting

### Porta já em uso

```bash
# Windows
netstat -ano | findstr :5000
taskkill /PID [PID] /F

# macOS/Linux
lsof -i :5000
kill -9 [PID]
```

### Container não inicia

```bash
docker logs bridgeapi
docker inspect bridgeapi
```

### Erro de conexão com banco

Verifique:
- String de conexão está correta
- Firewall permite a conexão
- Banco de dados está acessível

---

## Suporte

Para problemas com deployment:

1. Consulte a documentação oficial das plataformas
2. Abra uma issue no GitHub: https://github.com/URSoftware/BridgeAPI/issues
3. Verifique os logs da aplicação

---

**Versão:** 1.0.0  
**Última Atualização:** 01 de Abril de 2026
