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

## Guia de Uso

### Health Check

Verifica se a API está online:

```bash
curl https://bridgeapi-9uhl.onrender.com/api/health
```

**Resposta (200 OK):**
```json
{
  "status": "healthy",
  "timestamp": "2026-04-02T03:48:31.884091Z",
  "version": "1.0.0"
}
```

### Conectar ao Banco de Dados

Inicia uma conexão com o banco:

```javascript
const response = await fetch(`${API}/database/connect`, {
  method: 'POST',
  headers: { 'Content-Type': 'application/json' },
  body: JSON.stringify({
    databaseType: 'sqlserver', // ou 'sqlite'
    connectionString: 'Server=localhost;Database=MyDB;User Id=sa;Password=YourPassword;'
  })
});

const { connectionId } = await response.json();
// Use connectionId para posteriores queries
```

**Resposta (200 OK):**
```json
{
  "connectionId": "550e8400-e29b-41d4-a716-446655440000",
  "status": "connected",
  "databaseType": "sqlserver"
}
```

### Executar Query (SELECT)

Executa queries de leitura:

```javascript
const result = await fetch(`${API}/query/execute`, {
  method: 'POST',
  headers: { 'Content-Type': 'application/json' },
  body: JSON.stringify({
    connectionId: '550e8400-e29b-41d4-a716-446655440000',
    query: 'SELECT TOP 10 * FROM usuarios WHERE ativo = 1',
    timeout: 30 // segundos (opcional, padrão: 30)
  })
});

const { data, rowsAffected } = await result.json();
console.log(data); // Array de registros
```

**Resposta (200 OK):**
```json
{
  "data": [
    { "id": 1, "nome": "João", "email": "joao@example.com", "ativo": true },
    { "id": 2, "nome": "Maria", "email": "maria@example.com", "ativo": true }
  ],
  "rowsAffected": 2
}
```

### Executar Comando (INSERT/UPDATE/DELETE)

Executa comandos que modificam dados:

```javascript
const result = await fetch(`${API}/query/command`, {
  method: 'POST',
  headers: { 'Content-Type': 'application/json' },
  body: JSON.stringify({
    connectionId: '550e8400-e29b-41d4-a716-446655440000',
    query: "INSERT INTO usuarios (nome, email, ativo) VALUES ('Pedro', 'pedro@example.com', 1)",
    timeout: 30
  })
});

const { rowsAffected } = await result.json();
console.log(`${rowsAffected} registros inseridos`);
```

**Resposta (200 OK):**
```json
{
  "rowsAffected": 1,
  "status": "success"
}
```

### Desconectar

Encerra a conexão:

```javascript
await fetch(`${API}/database/disconnect`, {
  method: 'POST',
  headers: { 'Content-Type': 'application/json' },
  body: JSON.stringify({
    connectionId: '550e8400-e29b-41d4-a716-446655440000'
  })
});
```

**Resposta (200 OK):**
```json
{
  "status": "disconnected",
  "connectionId": "550e8400-e29b-41d4-a716-446655440000"
}
```

## Exemplos Completos por Tecnologia

### React

```javascript
import { useState } from 'react';

const API = 'https://bridgeapi-9uhl.onrender.com/api';

export function BancoDados() {
  const [dados, setDados] = useState([]);
  const [loading, setLoading] = useState(false);
  const [erro, setErro] = useState(null);

  const carregarDados = async () => {
    setLoading(true);
    setErro(null);
    try {
      // 1. Conectar
      const connRes = await fetch(`${API}/database/connect`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
          databaseType: 'sqlserver',
          connectionString: 'seu_connection_string'
        })
      });
      const { connectionId } = await connRes.json();

      // 2. Executar query
      const queryRes = await fetch(`${API}/query/execute`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
          connectionId,
          query: 'SELECT * FROM usuarios'
        })
      });
      const { data } = await queryRes.json();
      setDados(data);

      // 3. Desconectar
      await fetch(`${API}/database/disconnect`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ connectionId })
      });
    } catch (err) {
      setErro(err.message);
    } finally {
      setLoading(false);
    }
  };

  return (
    <div>
      <button onClick={carregarDados} disabled={loading}>
        {loading ? 'Carregando...' : 'Carregar Dados'}
      </button>
      {erro && <p style={{ color: 'red' }}>Erro: {erro}</p>}
      <ul>
        {dados.map(item => (
          <li key={item.id}>{item.nome} - {item.email}</li>
        ))}
      </ul>
    </div>
  );
}
```

### Node.js / Express

```javascript
const express = require('express');
const app = express();

const API = 'https://bridgeapi-9uhl.onrender.com/api';

app.get('/usuarios', async (req, res) => {
  try {
    // Conectar
    const connRes = await fetch(`${API}/database/connect`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({
        databaseType: 'sqlserver',
        connectionString: process.env.DB_CONNECTION_STRING
      })
    });
    const { connectionId } = await connRes.json();

    // Executar query
    const queryRes = await fetch(`${API}/query/execute`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({
        connectionId,
        query: 'SELECT * FROM usuarios'
      })
    });
    const { data } = await queryRes.json();

    // Desconectar
    await fetch(`${API}/database/disconnect`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ connectionId })
    });

    res.json(data);
  } catch (err) {
    res.status(500).json({ erro: err.message });
  }
});

app.listen(3000);
```

### Tratamento de Erros

```javascript
async function queryComErro() {
  try {
    const res = await fetch(`${API}/query/execute`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({
        connectionId: 'invalid-id',
        query: 'SELECT * FROM usuarios'
      })
    });

    if (!res.ok) {
      const { error, message } = await res.json();
      console.error(`[${error}] ${message}`);
      return null;
    }

    return await res.json();
  } catch (err) {
    console.error('Erro na requisição:', err.message);
  }
}
```

## Configuração e Variáveis de Ambiente

### Local (.env)

```env
DATABASE_TYPE=sqlserver
CONNECTION_STRING=Server=localhost;Database=MyDB;User Id=sa;Password=YourPassword;
PORT=5000
ASPNETCORE_ENVIRONMENT=Development
```

### Render

Configure as variáveis de ambiente no dashboard do Render:
- Variáveis de conexão com seu banco de dados
- PORT: 10000 (já configurado)

## Limites e Timeouts

| Parâmetro | Valor Padrão | Máximo |
|-----------|-------------|--------|
| Timeout Query | 30 segundos | 300 segundos |
| Tamanho máximo resposta | 100 MB | 100 MB |
| Conexões simultâneas | Limitado | Dependente do plano |
| Taxa de requisições | Ilimitada | Render: baseado no plano |

## CORS

A BridgeAPI permite requisições de qualquer origem por padrão. Se precisar restringir:

**Modifique em Program.cs:**
```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecific", policy =>
        policy.WithOrigins("https://seu-dominio.com")
              .AllowAnyMethod()
              .AllowAnyHeader());
});
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
