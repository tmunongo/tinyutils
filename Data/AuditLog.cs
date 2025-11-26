namespace tinyutils.Data;

public class AuditLog
{
    public int Id { get; set; }
    public DateTime Timestamp { get; set; }
    public string ToolName { get; set; } = string.Empty;
    public long InputSizeBytes { get; set; }
    public long OutputSizeBytes { get; set; }
    public double ProcessingTimeMs { get; set; }
}