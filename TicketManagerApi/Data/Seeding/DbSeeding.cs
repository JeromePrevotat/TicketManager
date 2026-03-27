namespace TicketManagerApi.Data.Seeding;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TicketManagerApi.Entities;

public static class DbSeeding
{
  public static async Task SeedDbAsync(IServiceProvider services)
  {
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole<int>>>();
    var userManager = services.GetRequiredService<UserManager<User>>();

    await SeedRoles(roleManager);
    await SeedUsers(userManager);
  }

  private static async Task SeedRoles(
    RoleManager<IdentityRole<int>> roleManager
  )
  {
    string[] roles = ["Admin", "User"];
    foreach (var role in roles)
    {
      if (!await roleManager.RoleExistsAsync(role))
      {
        await roleManager.CreateAsync(new IdentityRole<int> { Name = role });
      }
    }
  }

  public static async Task SeedUsers(
    UserManager<User> userManager
  )
  {
    if (await userManager.Users.AnyAsync()) return ;

    var admin = new User { UserName = "admin", Email = "admin@admin.com" };
    await userManager.CreateAsync(admin, "admin");
    await userManager.AddToRolesAsync(admin, ["Admin", "User"]);

    var user = new User { UserName = "user", Email = "user@user.com" };
    await userManager.CreateAsync(user, "user");
    await userManager.AddToRoleAsync(user, "User");
  }
}
