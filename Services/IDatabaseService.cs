using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using BridgeAPI.Models;

namespace BridgeAPI.Services;

public interface IDatabaseService
{
    Task<(bool success, string connectionId, string? error, string? details)> ConnectAsync(
        string databaseType,
        string connectionString,
        int timeout = 30);

    Task<(bool success, string? error, string? details)> DisconnectAsync(string connectionId);

    Task<(bool success, DataTable? data, int rowsAffected, string? error, string? details)> ExecuteQueryAsync(
        string connectionId,
        string query,
        int timeout = 30);

    Task<(bool success, int rowsAffected, string? error, string? details)> ExecuteCommandAsync(
        string connectionId,
        string command,
        List<DatabaseParameter>? parameters,
        int timeout = 30);
}

public class DatabaseService : IDatabaseService
{
    private readonly IConnectionManager _connectionManager;
    private readonly ILogger<DatabaseService> _logger;

    public DatabaseService(IConnectionManager connectionManager, ILogger<DatabaseService> logger)
    {
        _connectionManager = connectionManager;
        _logger = logger;
    }

    public async Task<(bool success, string connectionId, string? error, string? details)> ConnectAsync(
        string databaseType,
        string connectionString,
        int timeout = 30)
    {
        try
        {
            _logger.LogInformation($"Attempting to connect to {databaseType} database");

            // Validate connection string
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                return (false, "", "Invalid connection string", "Connection string cannot be empty");
            }

            // Test the connection
            bool canConnect = await TestConnectionAsync(databaseType, connectionString, timeout);

            if (!canConnect)
            {
                return (false, "", "Connection failed", "Failed to connect to database. Please verify your connection string and credentials.");
            }

            // Create and store connection
            string connectionId = _connectionManager.CreateConnection(connectionString, databaseType, timeout);
            _logger.LogInformation($"Connection created with ID: {connectionId}");

            return (true, connectionId, null, null);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Connection error: {ex.Message}");
            return (false, "", "Connection error", ex.Message);
        }
    }

    public async Task<(bool success, string? error, string? details)> DisconnectAsync(string connectionId)
    {
        try
        {
            if (!_connectionManager.ConnectionExists(connectionId))
            {
                return (false, "Connection not found", $"Connection ID '{connectionId}' does not exist");
            }

            _connectionManager.CloseConnection(connectionId);
            _logger.LogInformation($"Connection closed: {connectionId}");

            return (true, null, null);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Disconnect error: {ex.Message}");
            return (false, "Disconnect error", ex.Message);
        }
    }

    public async Task<(bool success, DataTable? data, int rowsAffected, string? error, string? details)> ExecuteQueryAsync(
        string connectionId,
        string query,
        int timeout = 30)
    {
        try
        {
            var connInfo = _connectionManager.GetConnection(connectionId);
            if (connInfo == null)
            {
                return (false, null, 0, "Connection not found", $"Connection ID '{connectionId}' does not exist");
            }

            var connDetails = connInfo as dynamic;
            string connectionString = connDetails.ConnectionString;
            string databaseType = connDetails.DatabaseType;

            // Execute based on database type
            if (databaseType.Equals("sqlserver", StringComparison.OrdinalIgnoreCase))
            {
                return await ExecuteSqlServerQueryAsync(connectionString, query, timeout);
            }
            else if (databaseType.Equals("sqlite", StringComparison.OrdinalIgnoreCase))
            {
                return await ExecuteSqliteQueryAsync(connectionString, query, timeout);
            }
            else
            {
                return (false, null, 0, "Unsupported database type", $"Database type '{databaseType}' is not supported");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"Query execution error: {ex.Message}");
            return (false, null, 0, "Query execution error", ex.Message);
        }
    }

    public async Task<(bool success, int rowsAffected, string? error, string? details)> ExecuteCommandAsync(
        string connectionId,
        string command,
        List<DatabaseParameter>? parameters,
        int timeout = 30)
    {
        try
        {
            var connInfo = _connectionManager.GetConnection(connectionId);
            if (connInfo == null)
            {
                return (false, 0, "Connection not found", $"Connection ID '{connectionId}' does not exist");
            }

            var connDetails = connInfo as dynamic;
            string connectionString = connDetails.ConnectionString;
            string databaseType = connDetails.DatabaseType;

            if (databaseType.Equals("sqlserver", StringComparison.OrdinalIgnoreCase))
            {
                return await ExecuteSqlServerCommandAsync(connectionString, command, parameters, timeout);
            }
            else if (databaseType.Equals("sqlite", StringComparison.OrdinalIgnoreCase))
            {
                return await ExecuteSqliteCommandAsync(connectionString, command, parameters, timeout);
            }
            else
            {
                return (false, 0, "Unsupported database type", $"Database type '{databaseType}' is not supported");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"Command execution error: {ex.Message}");
            return (false, 0, "Command execution error", ex.Message);
        }
    }

    private async Task<bool> TestConnectionAsync(string databaseType, string connectionString, int timeout)
    {
        try
        {
            if (databaseType.Equals("sqlserver", StringComparison.OrdinalIgnoreCase))
            {
                using var connection = new SqlConnection(connectionString);
                await connection.OpenAsync();
                connection.Close();
                return true;
            }
            else if (databaseType.Equals("sqlite", StringComparison.OrdinalIgnoreCase))
            {
                using var connection = new SQLiteConnection(connectionString);
                connection.Open();
                connection.Close();
                return true;
            }
        }
        catch (Exception ex)
        {
            _logger.LogWarning($"Connection test failed: {ex.Message}");
            return false;
        }

        return false;
    }

    private async Task<(bool success, DataTable? data, int rowsAffected, string? error, string? details)> ExecuteSqlServerQueryAsync(
        string connectionString,
        string query,
        int timeout)
    {
        try
        {
            using var connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            using var command = new SqlCommand(query, connection);
            command.CommandTimeout = timeout;

            using var adapter = new SqlDataAdapter(command);
            var dataTable = new DataTable();
            adapter.Fill(dataTable);

            return (true, dataTable, dataTable.Rows.Count, null, null);
        }
        catch (Exception ex)
        {
            return (false, null, 0, "SQL Server query error", ex.Message);
        }
    }

    private async Task<(bool success, DataTable? data, int rowsAffected, string? error, string? details)> ExecuteSqliteQueryAsync(
        string connectionString,
        string query,
        int timeout)
    {
        try
        {
            using var connection = new SQLiteConnection(connectionString);
            await connection.OpenAsync();

            using var command = new SQLiteCommand(query, connection);
            command.CommandTimeout = timeout;

            using var adapter = new SQLiteDataAdapter(command);
            var dataTable = new DataTable();
            adapter.Fill(dataTable);

            return (true, dataTable, dataTable.Rows.Count, null, null);
        }
        catch (Exception ex)
        {
            return (false, null, 0, "SQLite query error", ex.Message);
        }
    }

    private async Task<(bool success, int rowsAffected, string? error, string? details)> ExecuteSqlServerCommandAsync(
        string connectionString,
        string command,
        List<DatabaseParameter>? parameters,
        int timeout)
    {
        try
        {
            using var connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            using var dbCommand = new SqlCommand(command, connection);
            dbCommand.CommandTimeout = timeout;

            // Add parameters
            if (parameters != null && parameters.Count > 0)
            {
                foreach (var param in parameters)
                {
                    var sqlParam = ConvertToSqlParameter(param);
                    dbCommand.Parameters.Add(sqlParam);
                }
            }

            int rowsAffected = await dbCommand.ExecuteNonQueryAsync();
            return (true, rowsAffected, null, null);
        }
        catch (Exception ex)
        {
            return (false, 0, "SQL Server command error", ex.Message);
        }
    }

    private async Task<(bool success, int rowsAffected, string? error, string? details)> ExecuteSqliteCommandAsync(
        string connectionString,
        string command,
        List<DatabaseParameter>? parameters,
        int timeout)
    {
        try
        {
            using var connection = new SQLiteConnection(connectionString);
            await connection.OpenAsync();

            using var dbCommand = new SQLiteCommand(command, connection);
            dbCommand.CommandTimeout = timeout;

            // Add parameters
            if (parameters != null && parameters.Count > 0)
            {
                foreach (var param in parameters)
                {
                    var sqliteParam = ConvertToSqliteParameter(param);
                    dbCommand.Parameters.Add(sqliteParam);
                }
            }

            int rowsAffected = await dbCommand.ExecuteNonQueryAsync();
            return (true, rowsAffected, null, null);
        }
        catch (Exception ex)
        {
            return (false, 0, "SQLite command error", ex.Message);
        }
    }

    private System.Data.SqlClient.SqlParameter ConvertToSqlParameter(DatabaseParameter param)
    {
        return new System.Data.SqlClient.SqlParameter
        {
            ParameterName = param.Name,
            Value = param.Value ?? DBNull.Value
        };
    }

    private SQLiteParameter ConvertToSqliteParameter(DatabaseParameter param)
    {
        return new SQLiteParameter
        {
            ParameterName = param.Name,
            Value = param.Value ?? DBNull.Value
        };
    }
}
