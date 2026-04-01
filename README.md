# BridgeAPI

[![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white)]()
[![.NET](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)]()
[![REST API](https://img.shields.io/badge/REST%20API-FF6B6B?style=for-the-badge&logo=api&logoColor=white)]()
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![GitHub](https://img.shields.io/badge/GitHub-181717?style=for-the-badge&logo=github&logoColor=white)](https://github.com/URSoftware/BridgeAPI)

Uma elegante ponte conectando aplicações JavaScript a bancos de dados SQL Server e SQLite através de uma moderna API REST em C#.

## Visão Geral

BridgeAPI atua como uma camada middleware que permite que clientes JavaScript se comuniquem perfeitamente com bancos de dados SQL Server e SQLite sem acesso direto ao banco. A API abstrai a complexidade do banco de dados e fornece uma interface unificada para executar queries e gerenciar conexões.

### Arquitetura

```
┌──────────────────────┐
│  JavaScript Client   │
│   (Browser/Node.js)  │
└──────────┬───────────┘
           │ HTTP/REST
           │ (JSON)
           ▼
┌──────────────────────┐
│    C# REST API       │
│   (BridgeAPI Server) │
└──────────┬───────────┘
           │ ADO.NET / SQLite Driver
           │
           ▼
┌──────────────────────────────────────┐
│     Banco de Dados Definido pelo     │
│            Usuário                   │
│  • SQL Server                        │
│  • SQLite                            │
│  • (Extensível para outros)          │
└──────────────────────────────────────┘
```

## Características

- **Suporte Multi-Banco**: Conecte a SQL Server ou bancos SQLite
- **Gerenciamento Dinâmico de Conexões**: Usuários definem strings de conexão em tempo de execução
- **API RESTful**: Endpoints HTTP simples para operações de banco de dados
- **Request/Response JSON**: Fácil integração com aplicações JavaScript
- **Pool de Conexões**: Tratamento eficiente de conexões com banco de dados
- **Tratamento de Erros**: Respostas de erro compreensivas e significativas
- **Segurança**: Suporte para strings de conexão criptografadas e queries parametrizadas

## Pré-Requisitos

### Backend (C#)
- **.NET 9.0** ou superior
- **Visual Studio 2022** ou **VS Code** com extensão C#
- **SQL Server Express** (opcional, para conexões SQL Server)

### Cliente (JavaScript)
- **Node.js 14+** ou navegador moderno com fetch API
- **npm** ou **yarn**

## Instalação e Setup

### Opção 1: Script Automático (Recomendado)

**Windows:**
```cmd
run.bat
```

**macOS/Linux:**
```bash
chmod +x run.sh
./run.sh
```

### Opção 2: Instalação Manual

#### 2.1 Clonar o Repositório

```bash
git clone https://github.com/URSoftware/BridgeAPI.git
cd BridgeAPI
```

#### 2.2 Setup do Backend C#

```bash
# Restaurar dependências
dotnet restore

# Compilar o projeto
dotnet build

# Executar a API
dotnet run
```

A API estará disponível em `http://localhost:5000`

#### 2.3 Com Porta Customizada

**Windows:**
```powershell
$env:ASPNETCORE_URLS="http://localhost:5001"
dotnet run
```

**macOS/Linux:**
```bash
export ASPNETCORE_URLS="http://localhost:5001"
dotnet run
```

### Documentação da API (Swagger)

Acesse a documentação interativa em:
```
http://localhost:5000/swagger
```

### Testar Conexão (Health Check)

```bash
curl http://localhost:5000/api/health

# Resposta esperada:
# {"status":"healthy","timestamp":"2026-04-01T21:05:20Z","version":"1.0.0"}
```

Para instruções detalhadas, consulte [SETUP.md](SETUP.md)

## Endpoints da API

### 1. Health Check

**Endpoint:** `GET /api/health`

**Descrição:** Verifica se a API está funcionando

**Resposta (200 OK):**
```json
{
  "status": "healthy",
  "timestamp": "2026-04-01T10:30:00Z",
  "version": "1.0.0"
}
```

---

### 2. Conectar ao Banco de Dados

**Endpoint:** `POST /api/database/connect`

**Descrição:** Estabelece conexão com um banco de dados

**Request Body:**
```json
{
  "connectionName": "minhaBaseDados",
  "databaseType": "sqlserver",
  "connectionString": "Server=localhost;Database=MinhaDB;User Id=sa;Password=SuaSenha;",
  "timeout": 30
}
```

**Resposta (200 OK):**
```json
{
  "success": true,
  "message": "Conectado com sucesso",
  "connectionId": "conn_abc123xyz789"
}
```

**Resposta (400 Bad Request):**
```json
{
  "success": false,
  "error": "Falha na conexão",
  "details": "Verifique a string de conexão e as credenciais"
}
```

---

### 3. Executar Query (SELECT)

**Endpoint:** `POST /api/query/execute`

**Descrição:** Executa uma query SELECT e retorna os resultados

**Request Body:**
```json
{
  "connectionId": "conn_abc123xyz789",
  "query": "SELECT id, nome, email FROM usuarios WHERE ativo = 1",
  "timeout": 30
}
```

**Resposta (200 OK):**
```json
{
  "success": true,
  "rowsAffected": 5,
  "data": [
    {
      "id": 1,
      "nome": "João Silva",
      "email": "joao@example.com"
    },
    {
      "id": 2,
      "nome": "Maria Santos",
      "email": "maria@example.com"
    }
  ]
}
```

---

### 4. Executar Comando (INSERT/UPDATE/DELETE)

**Endpoint:** `POST /api/query/command`

**Descrição:** Executa INSERT, UPDATE ou DELETE

**Request Body:**
```json
{
  "connectionId": "conn_abc123xyz789",
  "command": "INSERT INTO usuarios (nome, email) VALUES (@nome, @email)",
  "parameters": [
    {
      "name": "@nome",
      "value": "Alice Johnson",
      "type": "string"
    },
    {
      "name": "@email",
      "value": "alice@example.com",
      "type": "string"
    }
  ]
}
```

**Resposta (200 OK):**
```json
{
  "success": true,
  "rowsAffected": 1,
  "message": "Comando executado com sucesso"
}
```

---

### 5. Fechar Conexão

**Endpoint:** `POST /api/database/disconnect`

**Descrição:** Fecha uma conexão ativa com o banco

**Request Body:**
```json
{
  "connectionId": "conn_abc123xyz789"
}
```

**Resposta (200 OK):**
```json
{
  "success": true,
  "message": "Desconectado com sucesso"
}
```

---

## Exemplos de Uso

### Exemplo 1: JavaScript - Conectar e Buscar Dados

```javascript
// Cliente JavaScript
const BASE_URL = 'http://localhost:5000/api';

async function buscarUsuarios() {
  try {
    // 1. Conectar ao banco
    const connectRes = await fetch(`${BASE_URL}/database/connect`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({
        databaseType: 'sqlserver',
        connectionString: 'Server=localhost;Database=MeuBanco;User Id=sa;Password=123456;'
      })
    });

    const { connectionId } = await connectRes.json();

    // 2. Executar query
    const queryRes = await fetch(`${BASE_URL}/query/execute`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({
        connectionId,
        query: 'SELECT * FROM usuarios'
      })
    });

    const result = await queryRes.json();
    console.log('Usuários:', result.data);

    // 3. Desconectar
    await fetch(`${BASE_URL}/database/disconnect`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ connectionId })
    });

  } catch (error) {
    console.error('Erro:', error);
  }
}

// Usar
buscarUsuarios();
```

### Exemplo 2: JavaScript - Inserir Dados

```javascript
async function inserirUsuario(nome, email) {
  const connectRes = await fetch(`${BASE_URL}/database/connect`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({
      databaseType: 'sqlserver',
      connectionString: 'Server=localhost;Database=MeuBanco;User Id=sa;Password=123456;'
    })
  });

  const { connectionId } = await connectRes.json();

  const insertRes = await fetch(`${BASE_URL}/query/command`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({
      connectionId,
      command: 'INSERT INTO usuarios (nome, email) VALUES (@nome, @email)',
      parameters: [
        { name: '@nome', value: nome, type: 'string' },
        { name: '@email', value: email, type: 'string' }
      ]
    })
  });

  const result = await insertRes.json();
  console.log(`${result.rowsAffected} linhas inseridas`);

  // Desconectar
  await fetch(`${BASE_URL}/database/disconnect`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ connectionId })
  });
}

// Usar
inserirUsuario('Pedro Costa', 'pedro@example.com');
```

---

## Strings de Conexão

### SQL Server

```
Server=localhost;Database=MinhaDB;User Id=sa;Password=MinhaS3nh@;Encrypt=true;TrustServerCertificate=false;
```

**Parâmetros:**
- `Server`: Endereço do servidor SQL
- `Database`: Nome do banco de dados
- `User Id`: Nome de usuário
- `Password`: Senha
- `Encrypt`: Habilitar criptografia
- `TrustServerCertificate`: Para desenvolvimento apenas

### SQLite

```
Data Source=/caminho/para/banco.db;Version=3;
```

**Parâmetros:**
- `Data Source`: Caminho para arquivo do banco SQLite
- `Version`: Versão do SQLite (geralmente 3)

---

## Solução de Problemas

### Erro: "Conexão recusada"

**Solução:**
- Verifique se o servidor de banco está rodando
- Confirme o endereço do servidor na string de conexão
- Verifique se o firewall permite a conexão

### Erro: "Autenticação falhou"

**Solução:**
- Verifique nome de usuário e senha
- Confirme permissões do usuário no banco
- Para SQL Server, ative autenticação SQL (não apenas Windows)

### Erro: "Query timeout"

**Solução:**
- Aumente o parâmetro `timeout` na requisição
- Otimize a query (adicione índices)
- Verifique performance do servidor

---

## Dependências

- **System.Data.SqlClient** - Driver SQL Server
- **System.Data.SQLite** - Driver SQLite
- **Swashbuckle.AspNetCore** - Swagger/OpenAPI

## Fluxo de Requisição

```
1. Cliente envia requisição HTTP (JSON)
                ↓
2. API C# recebe e valida os dados
                ↓
3. Conecta ao banco de dados especificado
                ↓
4. Executa a query/comando
                ↓
5. Converte resultado para JSON
                ↓
6. Retorna resposta HTTP com dados
                ↓
7. Cliente JavaScript processa a resposta
```

---

## Contribuindo

Contribuições são bem-vindas! Por favor:

1. Faça um fork do repositório
2. Crie uma branch para sua feature (`git checkout -b feature/sua-feature`)
3. Faça commit das mudanças (`git commit -m 'Adiciona nova feature'`)
4. Push para a branch (`git push origin feature/sua-feature`)
5. Abra um Pull Request

---

## Licença

Este projeto é licenciado sob a Licença MIT - veja o arquivo LICENSE para detalhes.

---

## Suporte

Para dúvidas, reportar bugs ou sugestões, abra uma issue no repositório GitHub.

---

**Última Atualização:** 01 de Abril de 2026

**Versão:** 1.0.0

**Status:** Production Ready
