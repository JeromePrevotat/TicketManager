using Microsoft.EntityFrameworkCore;
using TicketManagerApi.Data.Seeding;


namespace TicketManagerApi.Data;

public static class DataExtension
{
  public static async Task MigrateDb(this WebApplication app)
  {
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<TicketManagerContext>();
    await dbContext.Database.MigrateAsync();
  }
  public static async Task SeedDb(this WebApplication app)
  {
    using var scope = app.Services.CreateScope();
    await DbSeeding.SeedDbAsync(scope.ServiceProvider);
  }
  public static async Task SyncSequences(this WebApplication app)
  {
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<TicketManagerContext>();

    await dbContext.Database.ExecuteSqlRawAsync(@"
        SELECT setval(pg_get_serial_sequence('""Applications""', 'Id'), COALESCE(MAX(""Id""), 1), MAX(""Id"") IS NOT NULL) FROM ""Applications"";
        SELECT setval(pg_get_serial_sequence('""Tickets""', 'Id'), COALESCE(MAX(""Id""), 1), MAX(""Id"") IS NOT NULL) FROM ""Tickets"";
        SELECT setval(pg_get_serial_sequence('""Attachments""', 'Id'), COALESCE(MAX(""Id""), 1), MAX(""Id"") IS NOT NULL) FROM ""Attachments"";
    ");
}
}