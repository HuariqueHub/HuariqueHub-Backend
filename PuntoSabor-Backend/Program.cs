using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PuntoSabor_Backend.Auth.Application.Services;
using PuntoSabor_Backend.Auth.Domain.Repositories;
using PuntoSabor_Backend.Auth.Infrastructure.Persistence.EFC.Repositories;
using PuntoSabor_Backend.Auth.Infrastructure.Services;
using PuntoSabor_Backend.Discovery.Domain.Repositories;
using PuntoSabor_Backend.Discovery.Infrastructure.Persistence.EFC.Repositories;
using PuntoSabor_Backend.Favorites.Domain.Repositories;
using PuntoSabor_Backend.Favorites.Infrastructure.Persistence.EFC.Repositories;
using PuntoSabor_Backend.Memberships.Domain.Repositories;
using PuntoSabor_Backend.Memberships.Infrastructure.Persistence.EFC.Repositories;
using SubscriptionRepository = PuntoSabor_Backend.Memberships.Infrastructure.Persistence.EFC.Repositories.SubscriptionRepository;
using PuntoSabor_Backend.Promotions.Domain.Repositories;
using PuntoSabor_Backend.Promotions.Infrastructure.Persistence.EFC.Repositories;
using PuntoSabor_Backend.Reviews.Domain.Repositories;
using PuntoSabor_Backend.Reviews.Infrastructure.Persistence.EFC.Repositories;
using PuntoSabor_Backend.Shared.Domain.Repositories;
using PuntoSabor_Backend.Shared.Infrastructure.Persistence.EFC;

var builder = WebApplication.CreateBuilder(args);

// Debug: variables Railway
Console.WriteLine(">>> ENV DB_HOST: " + Environment.GetEnvironmentVariable("DB_HOST"));
Console.WriteLine(">>> ENV DB_PORT: " + Environment.GetEnvironmentVariable("DB_PORT"));
Console.WriteLine(">>> ENV DB_NAME: " + Environment.GetEnvironmentVariable("DB_NAME"));
Console.WriteLine(">>> ENV DB_USER: " + Environment.GetEnvironmentVariable("DB_USER"));
Console.WriteLine(">>> ENV DB_PASSWORD: " + Environment.GetEnvironmentVariable("DB_PASSWORD"));

// Build connection string from env or config fallback
var envHost = Environment.GetEnvironmentVariable("DB_HOST");
var envPort = Environment.GetEnvironmentVariable("DB_PORT");
var envName = Environment.GetEnvironmentVariable("DB_NAME");
var envUser = Environment.GetEnvironmentVariable("DB_USER");
var envPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");

var hasEnvConnection =
    !string.IsNullOrWhiteSpace(envHost) &&
    !string.IsNullOrWhiteSpace(envPort) &&
    !string.IsNullOrWhiteSpace(envName) &&
    !string.IsNullOrWhiteSpace(envUser) &&
    !string.IsNullOrWhiteSpace(envPassword);

var connectionString = hasEnvConnection
    ? $"server={envHost};port={envPort};database={envName};user={envUser};password={envPassword}"
    : builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrWhiteSpace(connectionString))
    throw new InvalidOperationException("Missing database connection string. Set env vars or ConnectionStrings:DefaultConnection.");

// JWT secret from env var or appsettings
var jwtSecret = Environment.GetEnvironmentVariable("JWT_SECRET")
    ?? builder.Configuration["Jwt:Secret"]
    ?? throw new InvalidOperationException("JWT secret is not configured.");

// DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseMySQL(connectionString!);
});

// Unit of Work
builder.Services.AddScoped<IUnitOfWork, AppUnitOfWork>();

// Repositories
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IHuariqueRepository, HuariqueRepository>();
builder.Services.AddScoped<IPlanRepository, PlanRepository>();
builder.Services.AddScoped<IPromoRepository, PromoRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IFavoriteRepository, FavoriteRepository>();
builder.Services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();

// Services
builder.Services.AddScoped<ITokenService, TokenService>();

// JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret)),
            ValidateIssuer = false,
            ValidateAudience = false,
            ClockSkew = TimeSpan.Zero
        };
    });

// Controllers + JSON
builder.Services.AddControllers().AddNewtonsoftJson();

// Swagger with JWT support
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "JWT Bearer token. Ejemplo: Bearer {token}",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            Array.Empty<string>()
        }
    });
});

const string corsPolicyName = "PuntoSaborCors";
builder.Services.AddCors(options =>
{
    options.AddPolicy(corsPolicyName, policy =>
    {
        policy
            .WithOrigins(
                "http://localhost:5173",
                "https://puntosabor.netlify.app",
                "https://frontend-punto-sabor.vercel.app",
                "https://pflavor-frontend.vercel.app",
                "https://huariquehub.github.io"
            )
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    try
    {
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        Console.WriteLine(">>> Verificando base de datos...");
        db.Database.EnsureCreated();

        Console.WriteLine(">>> Ejecutando DataSeeder...");
        DataSeeder.Seed(db);
        Console.WriteLine(">>> DataSeeder terminado.");
    }
    catch (Exception ex)
    {
        Console.WriteLine(">>> ERROR AL CONECTAR A MYSQL EN STARTUP:");
        Console.WriteLine(ex.Message);
    }
}

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseCors(corsPolicyName);
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
