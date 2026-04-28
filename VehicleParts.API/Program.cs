using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using VehicleParts.Application.Interface.IRepository;
using VehicleParts.Application.Interface.IServices;
using VehicleParts.Infrastructure.Persistance;
using VehicleParts.Infrastructure.Repository;
using VehicleParts.Infrastructure.Services;
using VehicleParts.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

// add controllers so our API endpoints work
builder.Services.AddControllers();

// connect to PostgreSQL database using the connection string from appsettings.json
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

// register all repositories so the app knows which class to use for each interface
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IStaffRepository, StaffRepository>();
builder.Services.AddScoped<IPartRepository, PartRepository>();

// register all services so the app knows which class to use for each interface
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IStaffService, StaffService>();
builder.Services.AddScoped<IPartService, PartService>();

// setup JWT authentication using the key stored in appsettings.json
var jwtSection = builder.Configuration.GetSection("Jwt");
var key        = Encoding.UTF8.GetBytes(jwtSection["Key"]!);

builder.Services.AddAuthentication(opts =>
{
    opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opts.DefaultChallengeScheme    = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(opts =>
{
    opts.RequireHttpsMetadata = false;
    opts.SaveToken            = true;
    opts.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer           = true,
        ValidateAudience         = true,
        ValidateLifetime         = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer              = jwtSection["Issuer"],
        ValidAudience            = jwtSection["Audience"],
        IssuerSigningKey         = new SymmetricSecurityKey(key),
        ClockSkew                = TimeSpan.Zero
    };
});

builder.Services.AddAuthorization();

// allow the React frontend (running on port 5173) to call our API over both http and https
builder.Services.AddCors(opts =>
    opts.AddPolicy("ReactDevServer", policy =>
        policy.WithOrigins("http://localhost:5173", "https://localhost:5173")
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials()));

// build the app
var app = builder.Build();

// run the seeder when the app starts to make sure admin user exists
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await DbSeeder.SeedAsync(db);
}

// middleware pipeline - the order here matters
app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseCors("ReactDevServer");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
