using System.Text;
using DepoAPI.Application.Authorization.Handlers;
using DepoAPI.Application.Authorization.Requirements;
using DepoAPI.Application.Interfaces.ICustomer;
using DepoAPI.Application.Interfaces.IDepo;
using DepoAPI.Application.Interfaces.IProduct;
using DepoAPI.Application.Interfaces.IUser;
using DepoAPI.Application.Services;
using DepoAPI.Domain.Entities;
using DepoAPI.Infrastructure.SeedData;
using DepoAPI.Persistence;
using DepoAPI.Persistence.DbContexts;
using DepoAPI.Persistence.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ICustomerRepository,  CustomerRepository>();

builder.Services.AddScoped<IDepoRepository, DepoRepository>();
builder.Services.AddScoped<IDepoService, DepoService>();

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<IAuthorizationHandler, MinimumAgeHandler>();

builder.Services.AddScoped<IAuthorizationHandler, OwnsCustomerHandler>();

builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();

builder.Services.AddIdentityCore<User>(options =>
    {
        options.Password.RequireDigit = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireLowercase = true;
        options.Password.RequiredLength = 6;
    })
    .AddRoles<Role>()
    .AddEntityFrameworkStores<DepoAPIDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
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
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

builder.Services.AddAuthorization(opt =>
    opt.AddPolicy("CanModeratePolicy", policy =>
    policy.RequireClaim("Permission", "CanModerate")));
builder.Services.AddAuthorization(opt =>
{
    opt.AddPolicy("AtLeast18", policy =>
        policy.Requirements.Add(new MinimumAgeRequirement(18)));
});
builder.Services.AddAuthorization(opt =>
{
    opt.AddPolicy("OwnsCustomerPolicy", policy =>
        policy.Requirements.Add(new OwnsCustomerRequirement()));
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "DepoAPI", Version = "v1" });

    // JWT Security Tanımı
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Lütfen 'Bearer {token}' formatında JWT girin",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<DepoAPI.Persistence.DbContexts.DepoAPIDbContext>();
    db.Database.Migrate();
    var services = scope.ServiceProvider;
    var roleManager = services.GetRequiredService<RoleManager<Role>>();
    var userManager = services.GetRequiredService<UserManager<User>>();

    await SeedData.SeedRoles(roleManager);
    await SeedData.SeedAdmin(userManager);
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();