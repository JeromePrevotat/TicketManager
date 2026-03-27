using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Npgsql;
using TicketManagerApi.Data;
using TicketManagerApi.Entities;
using TicketManagerApi.Services;
using TicketManagerApi.Validators;

var builder = WebApplication.CreateBuilder(args);
var conStrBuilder = new NpgsqlConnectionStringBuilder
{
  Host = "localhost",
  Port = 5432,
  Username = "postgres",
  Password = builder.Configuration["DbPassword"],
  Database = "TicketManager"
};
var connectionString = conStrBuilder.ConnectionString;

builder.Services.AddNpgsql<TicketManagerContext>(connectionString);

builder.Services.AddIdentityApiEndpoints<User>(options =>
    {
        options.Password.RequiredLength = 3;
        options.Password.RequireDigit = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequiredUniqueChars = 1;
        options.User.RequireUniqueEmail = true;  
    })
    .AddRoles<IdentityRole<int>>()
    .AddEntityFrameworkStores<TicketManagerContext>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<ValidationExceptionHandler>();

var app = builder.Build();
await app.MigrateDb();
await app.SeedDb();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseExceptionHandler();

app.UseAuthentication();
app.UseAuthorization();
// app.UseHttpsRedirection();

app.MapControllers();
app.MapIdentityApi<User>();

app.Run();
