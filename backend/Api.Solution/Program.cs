using Api.Solution.Data;
using Api.Solution.Middleware;
using Api.Solution.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

// Database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Authentication
var jwtSecretKey = builder.Configuration["JwtSettings:SecretKey"] ?? throw new Exception("Missing SecretKey in JwtSettings");
var jwtIssuer = builder.Configuration["JwtSettings:Issuer"] ?? throw new Exception("Missing Issuer in JwtSettings");
var jwtAudience = builder.Configuration["JwtSettings:Audience"] ?? throw new Exception("Missing Audience in JwtSettings");
var jwtExpiryMinutes = builder.Configuration["JwtSettings:ExpiryMinutes"] ?? throw new Exception("Missing ExpiryMinutes in JwtSettings");

var jwtSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecretKey));

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,
            IssuerSigningKey = jwtSigningKey
        };
    });

builder.Services.AddAuthorization();


// Services 
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<UnityOfWork>();

builder.Services.AddScoped<CurrentUserService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<ProjectService>();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlerMiddleware>();

//Configure OpenAPi + Scalar in Dev Enviroment
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Set Migrations
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

app.Run();
