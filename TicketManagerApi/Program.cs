using System.Text.Json.Serialization;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi;
using Npgsql;
using Swashbuckle.AspNetCore.Filters;
using TicketManagerApi.Data;
using TicketManagerApi.Entities;
using TicketManagerApi.Services;
using TicketManagerApi.Validators;

var builder = WebApplication.CreateBuilder(args);

// Database connection
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

// Authentication
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
// To use BCrypt
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher>();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Description = "JWT Authorization header using the Bearer scheme"
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>(true, "Bearer");
});

// Controllers
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

// Routing
builder.Services.AddRouting(options =>
    options.LowercaseUrls = true
);

// Error Handling
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<ValidationExceptionHandler>();

var app = builder.Build();
await app.MigrateDb();
await app.SyncSequences();
await app.SeedDb();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler();

app.UseAuthentication();
app.UseAuthorization();
// app.UseHttpsRedirection();

app.MapControllers();
app.MapIdentityApi<User>();

app.Run();
