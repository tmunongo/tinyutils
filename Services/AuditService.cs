using Microsoft.EntityFrameworkCore;

using tinyutils.Data;

namespace tinyutils.Services;

public class AuditService
{
    private readonly TinyUtilsContext _context;

    public AuditService(TinyUtilsContext context)
    {
        _context = context;
    }

    public async Task LogUsageAsync(
        string toolName,
        long inputSize,
        long outputSize,
        double processingTimeMs
    )
    {
        var log = new AuditLog
        {
            Timestamp = DateTime.UtcNow,
            ToolName = toolName,
            InputSizeBytes = inputSize,
            OutputSizeBytes = outputSize,
            ProcessingTimeMs = processingTimeMs
        };

        _context.AuditLogs.Add(log);
        await _context.SaveChangesAsync();
    }

    public async Task<List<AuditLog>> GetRecentLogsAsync(int count = 10)
    {
        return await _context.AuditLogs
            .OrderByDescending(log => log.Timestamp)
            .Take(count)
            .ToListAsync();
    }
}