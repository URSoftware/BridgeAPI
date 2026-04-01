namespace BridgeAPI.Models;

public class ConnectionRequest
{
    public string ConnectionName { get; set; } = string.Empty;
    public string DatabaseType { get; set; } = string.Empty; // "sqlserver" or "sqlite"
    public string ConnectionString { get; set; } = string.Empty;
    public int Timeout { get; set; } = 30;
}

public class ConnectionResponse
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public string? ConnectionId { get; set; }
    public string? Error { get; set; }
    public string? Details { get; set; }
}

public class DisconnectRequest
{
    public string ConnectionId { get; set; } = string.Empty;
}

public class DisconnectResponse
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public string? Error { get; set; }
}

public class QueryRequest
{
    public string ConnectionId { get; set; } = string.Empty;
    public string Query { get; set; } = string.Empty;
    public int Timeout { get; set; } = 30;
}

public class QueryResponse
{
    public bool Success { get; set; }
    public int RowsAffected { get; set; }
    public List<Dictionary<string, object>> Data { get; set; } = new();
    public string? Error { get; set; }
    public string? Details { get; set; }
}

public class CommandRequest
{
    public string ConnectionId { get; set; } = string.Empty;
    public string Command { get; set; } = string.Empty;
    public List<DatabaseParameter>? Parameters { get; set; }
    public int Timeout { get; set; } = 30;
}

public class CommandResponse
{
    public bool Success { get; set; }
    public int RowsAffected { get; set; }
    public string Message { get; set; } = string.Empty;
    public string? Error { get; set; }
    public string? Details { get; set; }
}

public class DatabaseParameter
{
    public string Name { get; set; } = string.Empty;
    public object? Value { get; set; }
    public string Type { get; set; } = "string"; // "string", "int", "decimal", "datetime", etc
}

public class HealthResponse
{
    public string Status { get; set; } = "healthy";
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public string Version { get; set; } = "1.0.0";
}
