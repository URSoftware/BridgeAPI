namespace BridgeAPI.Services;

public interface IConnectionManager
{
    string CreateConnection(string connectionString, string databaseType, int timeout = 30);
    object? GetConnection(string connectionId);
    bool RemoveConnection(string connectionId);
    void CloseConnection(string connectionId);
    bool ConnectionExists(string connectionId);
}

public class ConnectionManager : IConnectionManager
{
    private readonly Dictionary<string, ConnectionInfo> _connections = new();
    private readonly object _lockObject = new object();

    public string CreateConnection(string connectionString, string databaseType, int timeout = 30)
    {
        lock (_lockObject)
        {
            string connectionId = $"conn_{Guid.NewGuid().ToString().Substring(0, 12)}";

            _connections[connectionId] = new ConnectionInfo
            {
                Id = connectionId,
                ConnectionString = connectionString,
                DatabaseType = databaseType,
                Timeout = timeout,
                CreatedAt = DateTime.UtcNow
            };

            return connectionId;
        }
    }

    public object? GetConnection(string connectionId)
    {
        lock (_lockObject)
        {
            if (_connections.TryGetValue(connectionId, out var connInfo))
            {
                return connInfo;
            }
            return null;
        }
    }

    public bool RemoveConnection(string connectionId)
    {
        lock (_lockObject)
        {
            return _connections.Remove(connectionId);
        }
    }

    public void CloseConnection(string connectionId)
    {
        lock (_lockObject)
        {
            _connections.Remove(connectionId);
        }
    }

    public bool ConnectionExists(string connectionId)
    {
        lock (_lockObject)
        {
            return _connections.ContainsKey(connectionId);
        }
    }

    private class ConnectionInfo
    {
        public string Id { get; set; } = string.Empty;
        public string ConnectionString { get; set; } = string.Empty;
        public string DatabaseType { get; set; } = string.Empty;
        public int Timeout { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
