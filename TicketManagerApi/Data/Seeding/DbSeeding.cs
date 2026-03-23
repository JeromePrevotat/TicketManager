namespace TicketManagerApi.Data.Seeding;
using Microsoft.EntityFrameworkCore;
using TicketManagerApi.Entities;
using BC = BCrypt.Net.BCrypt;

public static class DbSeeding
{
  public static async Task SeedDbAsync(TicketManagerContext context)
  {
    var roles = new List<Role>
    {
      new() { RoleName = RoleName.ADMIN },
      new() { RoleName = RoleName.USER }
    };

    await SeedRoles(context, roles);
    await SeedUsers(context, roles);
    await context.SaveChangesAsync();
  }

  public static async Task SeedRoles(
    TicketManagerContext context,
    List<Role> roles)
  {
    if (await context.Roles.AnyAsync()) return ;

    context.Roles.AddRange(
      roles
    );
  }

  public static async Task SeedUsers(
    TicketManagerContext context,
    List<Role> roles)
  {
    if (await context.Users.AnyAsync()) return ;

    context.Users.AddRange(
      new User {
        Username = "admin",
        Email = "admin@admin.com",
        Password = BC.EnhancedHashPassword("admin", 11),
        Roles = roles
      },
      new User {
        Username = "user",
        Email = "user@user.com",
        Password = BC.EnhancedHashPassword("user", 11),
        Roles = [.. roles.Where(r => r.RoleName == RoleName.USER)]
      }
    );
  }
}
