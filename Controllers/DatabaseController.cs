using Microsoft.AspNetCore.Mvc;
using BridgeAPI.Models;
using BridgeAPI.Services;

namespace BridgeAPI.Controllers;

[ApiController]
[Route("api/database")]
public class DatabaseController : ControllerBase
{
    private readonly IDatabaseService _databaseService;
    private readonly ILogger<DatabaseController> _logger;

    public DatabaseController(IDatabaseService databaseService, ILogger<DatabaseController> logger)
    {
        _databaseService = databaseService;
        _logger = logger;
    }

    [HttpPost("connect")]
    public async Task<IActionResult> Connect([FromBody] ConnectionRequest request)
    {
        _logger.LogInformation($"Connect request for {request.DatabaseType} database");

        var (success, connectionId, error, details) = await _databaseService.ConnectAsync(
            request.DatabaseType,
            request.ConnectionString,
            request.Timeout
        );

        if (!success)
        {
            return BadRequest(new ConnectionResponse
            {
                Success = false,
                Error = error,
                Details = details,
                Message = "Failed to connect to database"
            });
        }

        return Ok(new ConnectionResponse
        {
            Success = true,
            ConnectionId = connectionId,
            Message = "Connection established successfully"
        });
    }

    [HttpPost("disconnect")]
    public async Task<IActionResult> Disconnect([FromBody] DisconnectRequest request)
    {
        _logger.LogInformation($"Disconnect request for connection: {request.ConnectionId}");

        var (success, error, details) = await _databaseService.DisconnectAsync(request.ConnectionId);

        if (!success)
        {
            return BadRequest(new DisconnectResponse
            {
                Success = false,
                Error = error,
                Message = "Failed to disconnect"
            });
        }

        return Ok(new DisconnectResponse
        {
            Success = true,
            Message = "Connection closed successfully"
        });
    }
}
