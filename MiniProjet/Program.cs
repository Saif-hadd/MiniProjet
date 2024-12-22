using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MiniProjet.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// === Configuration de la chaîne de connexion ===
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// === Configuration d'ASP.NET Identity ===
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    // Options de personnalisation pour Identity (si nécessaire)
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequiredLength = 8;
})
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// === Configuration de l'authentification JWT ===
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    var jwtSettings = builder.Configuration.GetSection("JWT");
    var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ClockSkew = TimeSpan.Zero // Pas de délai de grâce pour l'expiration du token
    };

    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            Console.WriteLine($"Authentication failed: {context.Exception.Message}");
            return Task.CompletedTask;
        }
    };
});

// === Ajout des services nécessaires ===
builder.Services.AddControllers(); // Ajout de support pour les contrôleurs
builder.Services.AddEndpointsApiExplorer(); // Activation des endpoints pour Swagger
builder.Services.AddSwaggerGen(); // Configuration de Swagger

var app = builder.Build();

// === Configuration du pipeline HTTP ===
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection(); // Redirection vers HTTPS
app.UseAuthentication();   // Activation de l'authentification
app.UseAuthorization();    // Activation de l'autorisation

app.MapControllers(); // Mappage des routes des contrôleurs

app.Run();
