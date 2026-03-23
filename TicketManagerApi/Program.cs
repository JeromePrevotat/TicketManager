using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using Npgsql;
using TicketManagerApi.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

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

var app = builder.Build();
await app.MigrateDb();
await app.SeedDb();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
