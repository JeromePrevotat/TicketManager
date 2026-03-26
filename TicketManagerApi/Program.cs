using FluentValidation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using Npgsql;
using TicketManagerApi.Data;
using TicketManagerApi.Mapper.UserMapper;
using TicketManagerApi.Services;
using TicketManagerApi.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddValidatorsFromAssemblyContaining<Program>();

builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<UserService>();

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

// app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
