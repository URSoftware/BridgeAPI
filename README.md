# BridgeAPI

[![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white)]()
[![.NET](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)]()
[![REST API](https://img.shields.io/badge/REST%20API-FF6B6B?style=for-the-badge&logo=api&logoColor=white)]()
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![GitHub](https://img.shields.io/badge/GitHub-181717?style=for-the-badge&logo=github&logoColor=white)](https://github.com/URSoftware/BridgeAPI)

Uma elegante ponte conectando aplicações JavaScript a bancos de dados SQL Server e SQLite através de uma moderna API REST em C#.

## Visão Geral

BridgeAPI atua como uma camada middleware que permite que clientes JavaScript se comuniquem perfeitamente com bancos de dados SQL Server e SQLite. A API abstrai a complexidade do banco de dados e fornece uma interface unificada para executar queries.

```
JavaScript Client → C# REST API → Banco de Dados
(Browser/Node.js)  (BridgeAPI)    (SQL Server/SQLite)
```

## Características

- Suporte Multi-Banco (SQL Server, SQLite)
- Gerenciamento Dinâmico de Conexões
- API RESTful com Swagger
- Documentação Automática
- Docker Ready
- CI/CD Ready

## Iniciando Rápido

### Instalação Local

```bash
git clone https://github.com/URSoftware/BridgeAPI.git
cd BridgeAPI
dotnet restore
dotnet build
dotnet run
```

API em: `http://localhost:5000`

### Com Script

**Windows:** `run.bat`  
**macOS/Linux:** `chmod +x run.sh && ./run.sh`

## Documentação

Documentação completa em `/docs`:

| Documento | Propósito |
|-----------|-----------|
| [SETUP.md](docs/SETUP.md) | Instalação local completa |
| [RENDER_SIMPLE_SETUP.md](docs/RENDER_SIMPLE_SETUP.md) | Render.com com UptimeRobot |
| [FREE_DEPLOY.md](docs/FREE_DEPLOY.md) | Railway.app |
| [DEPLOYMENT.md](docs/DEPLOYMENT.md) | Todos os platforms |
| [DEPLOYMENT_COMPARISON.md](docs/DEPLOYMENT_COMPARISON.md) | Comparação de opções |
| [RENDER_KEEPALIVE.md](docs/RENDER_KEEPALIVE.md) | Soluções anti-sleep |

## Endpoints Principais

```bash
# Health Check
GET /api/health

# Conectar ao Banco
POST /api/database/connect

# Executar Query
POST /api/query/execute

# Executar Comando
POST /api/query/command

# Desconectar
POST /api/database/disconnect
```

## Documentação Swagger

Abra `http://localhost:5000/swagger` após iniciar a API.

## Exemplo JavaScript

```javascript
const API = 'http://localhost:5000/api';

// Conectar
const { connectionId } = await (await fetch(`${API}/database/connect`, {
  method: 'POST',
  headers: { 'Content-Type': 'application/json' },
  body: JSON.stringify({
    databaseType: 'sqlserver',
    connectionString: 'Server=localhost;Database=MyDB;...'
  })
})).json();

// Executar
const result = await (await fetch(`${API}/query/execute`, {
  method: 'POST',
  headers: { 'Content-Type': 'application/json' },
  body: JSON.stringify({
    connectionId,
    query: 'SELECT * FROM usuarios'
  })
})).json();

console.log(result.data);
```

## Deployment

### Railway.app (Recomendado)
- $5/mês grátis
- Sempre online
- [Guia completo](docs/FREE_DEPLOY.md)

### Render.com (Gratuito)
- Totalmente grátis
- Com UptimeRobot para manter ativo
- [Guia completo](docs/RENDER_SIMPLE_SETUP.md)

#### Serviço em Produção (Render)

**BridgeAPI está online no Render:**

```
API URL: https://bridgeapi-9uhl.onrender.com
Service ID: srv-d76rdneequas73d0b4n0
```

**Health Check:**
```bash
curl https://bridgeapi-9uhl.onrender.com/api/health
```

**Informações de Rede:**

Quando deployado no Render, requisições para a internet originam-se dos seguintes IPs:

- `74.220.48.0/24`
- `74.220.56.0/24`

**Nota:** Estes IPs são compartilhados com outros serviços Render na mesma região.

### Docker

```bash
docker build -t bridgeapi .
docker run -p 5000:5000 bridgeapi

# ou Docker Compose
docker-compose up -d
```

## Requisitos

- .NET 9.0 SDK
- Windows, macOS ou Linux

## Licença

MIT License

## Links Úteis

- GitHub: https://github.com/URSoftware/BridgeAPI
- API Local: http://localhost:5000/swagger
- Issues: https://github.com/URSoftware/BridgeAPI/issues

---

**Versão:** 1.0.0 | **Status:** Production Ready | **Última Atualização:** 01 de Abril de 2026
