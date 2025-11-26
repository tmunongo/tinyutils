using Microsoft.EntityFrameworkCore;

namespace tinyutils.Data;

public class TinyUtilsContext : DbContext
{
    public TinyUtilsContext(DbContextOptions<TinyUtilsContext> options)
        : base(options)
    {
    }

    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();
    public DbSet<UserSetting> UserSettings => Set<UserSetting>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AuditLog>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Timestamp).IsRequired();
            entity.Property(e => e.ToolName).IsRequired().HasMaxLength(100);
            entity.HasIndex(e => e.Timestamp);
        });

        modelBuilder.Entity<UserSetting>(entity =>
        {
            entity.HasKey(e => e.Key);
            entity.Property(e => e.Key).HasMaxLength(100);
            entity.Property(e => e.Value).HasMaxLength(500);
        });
    }
}