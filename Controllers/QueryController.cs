using Microsoft.AspNetCore.Mvc;
using BridgeAPI.Models;
using BridgeAPI.Services;

namespace BridgeAPI.Controllers;

[ApiController]
[Route("api/query")]
public class QueryController : ControllerBase
{
    private readonly IDatabaseService _databaseService;
    private readonly ILogger<QueryController> _logger;

    public QueryController(IDatabaseService databaseService, ILogger<QueryController> logger)
    {
        _databaseService = databaseService;
        _logger = logger;
    }

    [HttpPost("execute")]
    public async Task<IActionResult> ExecuteQuery([FromBody] QueryRequest request)
    {
        _logger.LogInformation($"Query execution request for connection: {request.ConnectionId}");

        var (success, data, rowsAffected, error, details) = await _databaseService.ExecuteQueryAsync(
            request.ConnectionId,
            request.Query,
            request.Timeout
        );

        if (!success)
        {
            return BadRequest(new QueryResponse
            {
                Success = false,
                Error = error,
                Details = details,
                Data = new List<Dictionary<string, object>>()
            });
        }

        var resultData = ConvertDataTableToList(data);

        return Ok(new QueryResponse
        {
            Success = true,
            RowsAffected = rowsAffected,
            Data = resultData
        });
    }

    [HttpPost("command")]
    public async Task<IActionResult> ExecuteCommand([FromBody] CommandRequest request)
    {
        _logger.LogInformation($"Command execution request for connection: {request.ConnectionId}");

        var (success, rowsAffected, error, details) = await _databaseService.ExecuteCommandAsync(
            request.ConnectionId,
            request.Command,
            request.Parameters,
            request.Timeout
        );

        if (!success)
        {
            return BadRequest(new CommandResponse
            {
                Success = false,
                Error = error,
                Details = details,
                Message = "Failed to execute command"
            });
        }

        return Ok(new CommandResponse
        {
            Success = true,
            RowsAffected = rowsAffected,
            Message = "Command executed successfully"
        });
    }

    private List<Dictionary<string, object>> ConvertDataTableToList(System.Data.DataTable? table)
    {
        var list = new List<Dictionary<string, object>>();

        if (table == null)
            return list;

        foreach (System.Data.DataRow row in table.Rows)
        {
            var dict = new Dictionary<string, object>();
            foreach (System.Data.DataColumn col in table.Columns)
            {
                dict[col.ColumnName] = row[col] ?? DBNull.Value;
            }
            list.Add(dict);
        }

        return list;
    }
}
