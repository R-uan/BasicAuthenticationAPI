using System.Text;
using FluentValidation;
using BasicAuthenticationAPI;
using BasicAuthenticationAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using BasicAuthenticationAPI.Helpers.Validator;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);
var JwtSettingsSection = builder.Configuration.GetSection("Jwt");
var JwtSettings = JwtSettingsSection.Get<JWTSettings>() ?? throw new Exception("JWT Settings are missing in configuration.");

builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();
builder.Services.Configure<JWTSettings>(JwtSettingsSection);
Console.WriteLine($"Environment: {builder.Environment.EnvironmentName}");
Console.WriteLine($"Issuer {JwtSettings.Issuer} | SecretKey {JwtSettings.SecretKey}");

builder.Services.AddDbContext<ApplicationDbContext>(options => {
	options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresDatabase"));
});

builder.Services.AddAuthentication(cfg => {
	cfg.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	cfg.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(x => {
	x.RequireHttpsMetadata = false;
	x.SaveToken = true;
	x.TokenValidationParameters = new TokenValidationParameters {
		ValidateIssuer = true,
		ValidateAudience = false,
		ValidIssuer = JwtSettings.Issuer,
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(JwtSettings.SecretKey)),
	};
});


//
// Endusers
builder.Services.AddScoped<IValidator<EndUserDTO>, EndUserValidator>();
builder.Services.AddScoped<IEndUserRepository, EndUserRepository>();
builder.Services.AddScoped<IEndUserService, EndUserService>();
//
// Authentication
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<JWTHelper>();

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
if (app.Environment.IsDevelopment()) {
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.Run();

