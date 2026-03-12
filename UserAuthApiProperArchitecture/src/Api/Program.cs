using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using UserAuthApiProperArchitecture.Application.Interfaces;
using UserAuthApiProperArchitecture.Application.Services;
using UserAuthApiProperArchitecture.Infrastructure.Data;
using UserAuthApiProperArchitecture.Infrastructure.Identity;
using UserAuthApiProperArchitecture.Infrastructure.Repositories;


var builder = WebApplication.CreateBuilder(args);

// ── 1. Database ──────────────────────────────────────────────── 
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ── 2. Register Services (Dependency Injection) ──────────────── 
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
builder.Services.AddScoped<IUserService, UserService>();

// ── 3. Configure JWT Authentication ─────────────────────────── 
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]!);

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,   // Reject expired tokens 
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(secretKey),
            ClockSkew = TimeSpan.Zero  // No grace period on expiry 
        };
    });

// ── 4. Authorization (roles) ──────────────────────────────────── 

builder.Services.AddAuthorization();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


// ORDER MATTERS: Authentication BEFORE Authorization 
app.UseAuthentication();   // Who are you? (validates JWT) 
app.UseAuthorization();    // What can you do? (checks roles/policies) 

app.MapControllers();
app.Run();


