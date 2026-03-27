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
}