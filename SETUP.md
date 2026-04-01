# Guia de Instalação e Execução - BridgeAPI

Este guia permite que qualquer pessoa instale e execute a BridgeAPI em poucos passos.

## Requisitos do Sistema

- **Windows, macOS ou Linux**
- **.NET 9.0 Runtime** ou **.NET 9.0 SDK**
- **Git** (para clonar o repositório)
- **Uma porta disponível** (padrão: 5001)

## Passo 1: Verificar Instalação do .NET

Abra seu terminal de comando (PowerShell, Terminal, bash) e execute:

```bash
dotnet --version
```

Você deve ver uma versão 9.0 ou superior como resposta.

### Se não tiver .NET 9.0 instalado:

**Windows:**
```
https://dotnet.microsoft.com/en-us/download/dotnet/9.0
```

**macOS:**
```bash
brew install dotnet-sdk-9
```

**Linux (Ubuntu/Debian):**
```bash
sudo apt-get update
sudo apt-get install -y dotnet-sdk-9.0
```

## Passo 2: Clonar o Repositório

```bash
git clone https://github.com/URSoftware/BridgeAPI.git
cd BridgeAPI
```

## Passo 3: Restaurar Dependências

```bash
dotnet restore
```

## Passo 4: Compilar o Projeto

```bash
dotnet build
```

Você deve ver "Build succeeded" no final.

## Passo 5: Executar a API

### Opção A: Porta Padrão (5000)

```bash
dotnet run
```

A API estará disponível em: `http://localhost:5000`

### Opção B: Porta Customizada (exemplo: 5001)

**Windows (PowerShell):**
```powershell
$env:ASPNETCORE_URLS="http://localhost:5001"
dotnet run
```

**macOS/Linux (Terminal/Bash):**
```bash
export ASPNETCORE_URLS="http://localhost:5001"
dotnet run
```

## Passo 6: Testar a API

Abra uma nova janela do terminal e execute:

### Verificar Status (Health Check)

**Windows (PowerShell):**
```powershell
Invoke-WebRequest -Uri "http://localhost:5000/api/health" -UseBasicParsing
```

**macOS/Linux (Terminal/Bash):**
```bash
curl http://localhost:5000/api/health
```

Resultado esperado:
```json
{
  "status": "healthy",
  "timestamp": "2026-04-01T21:23:39Z",
  "version": "1.0.0"
}
```

## Passo 7: Acessar Documentação Interativa

Abra seu navegador e vá para:

```
http://localhost:5000/swagger
```

Aqui você pode ver e testar todos os endpoints da API.

## Próximos Passos

Para usar a API em sua aplicação JavaScript, consulte o [README.md](README.md) para exemplos completos de código.

### Exemplo Rápido em JavaScript:

```javascript
const API_URL = 'http://localhost:5000/api';

// Conectar ao banco
const response = await fetch(`${API_URL}/database/connect`, {
  method: 'POST',
  headers: { 'Content-Type': 'application/json' },
  body: JSON.stringify({
    databaseType: 'sqlite',
    connectionString: 'Data Source=mydb.db;'
  })
});

const { connectionId } = await response.json();
console.log('Conectado:', connectionId);
```

## Solução de Problemas

### Erro: "Porta já está em uso"

Se a porta 5000 ou 5001 já está em uso, escolha outra porta:

**Exemplo: usar porta 8080**

```bash
$env:ASPNETCORE_URLS="http://localhost:8080"
dotnet run
```

### Erro: ".NET não encontrado"

Reinstale o .NET 9.0 SDK:
https://dotnet.microsoft.com/en-us/download/dotnet/9.0

### Erro: "Arquivos não encontrados após clone"

Certifique-se de estar no diretório correto:

```bash
cd BridgeAPI
ls  # ou 'dir' no Windows
```

Você deve ver: `README.md`, `BridgeAPI.csproj`, `Program.cs`, etc.

## Para Parar a API

Pressione `Ctrl + C` no terminal onde a API está rodando.

## Suporte

Se encontrar problemas:

1. Verifique se tem .NET 9.0 instalado: `dotnet --version`
2. Verifique se está no diretório correto: `ls` ou `dir`
3. Tente limpar cache: `dotnet clean`
4. Abra uma issue no GitHub: https://github.com/URSoftware/BridgeAPI/issues

---

**Versão:** 1.0.0  
**Última Atualização:** 01 de Abril de 2026
