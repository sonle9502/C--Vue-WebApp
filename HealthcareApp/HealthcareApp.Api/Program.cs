using HealthcareApp.Api.Data;
using HealthcareApp.Api.Repositories.Implementations;
using HealthcareApp.Api.Repositories.Interfaces;
using HealthcareApp.Api.Repositories.lab;
using HealthcareApp.Api.Services.Implementations;
using HealthcareApp.Api.Services.Interfaces;
using HealthcareApp.Api.Services.lab;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// ===== Add services to the container =====
builder.Services.AddControllers();

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Configure EF Core (PostgreSQL)
builder.Services.AddDbContextFactory<HospitalDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Dependency Injection
builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IJwtService, JwtService>();
// Hospital
builder.Services.AddScoped<IHospitalRepository, HospitalRepository>();
builder.Services.AddScoped<IHospitalService, HospitalService>();

// Lab
builder.Services.AddScoped<IExcelRepository, ExcelRepository>();
builder.Services.AddScoped<ILabRepository, LabRepository>();
builder.Services.AddScoped<ILabService, LabService>();

builder.Services.AddHttpClient();

// JWT Authentication & Authorization
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

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

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
    options.AddPolicy("DoctorOnly", policy => policy.RequireRole("Doctor"));
});

// Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ===== Build the app =====
WebApplication app;
try
{
    app = builder.Build();

    // 👇 Test resolve service để sớm phát hiện lỗi DI
    using var scope = app.Services.CreateScope();
    Console.WriteLine("✅ ILabService resolved thành công");
}
catch (Exception ex)
{
    Console.WriteLine("❌ Lỗi khi Build ServiceProvider hoặc resolve service:");
    Console.WriteLine(ex.Message);
    Console.WriteLine(ex.StackTrace);
    throw;
}

// ===== Middleware log request body (debug) =====
app.Use(async (context, next) =>
{
    context.Request.EnableBuffering();
    using var reader = new StreamReader(context.Request.Body, leaveOpen: true);
    var body = await reader.ReadToEndAsync();
    context.Request.Body.Position = 0;
    Console.WriteLine($"Request body: {body}");
    await next();
});

// ===== Seed default admin =====
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<HospitalDbContext>();
    var userService = scope.ServiceProvider.GetRequiredService<IUserService>();

    // Seed hospital mặc định
    var defaultHospital = context.Hospitals.FirstOrDefault();
    if (defaultHospital == null)
    {
        defaultHospital = new HealthcareApp.Api.Models.Entities.Hospital
        {
            Name = "Default Hospital",
            Address = "Địa chỉ mặc định",
            Phone = "Phone mặc định"
        };
        context.Hospitals.Add(defaultHospital);
        context.SaveChanges();
    }

    // Seed admin mặc định
    if (!context.Users.Any(u => u.Role == "Admin"))
    {
        var admin = new HealthcareApp.Api.Models.Entities.User
        {
            UserName = "admin",
            PasswordHash = userService.HashPassword("Admin123!"),
            Role = "Admin",
            HospitalId = defaultHospital.Id
        };

        context.Users.Add(admin);
        context.SaveChanges();
        Console.WriteLine("✅ Default admin created: username=admin, password=Admin123!");
    }
}

// ===== Configure middleware =====
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowFrontend");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
