using System.Reflection;
using Common;
using Common.Interfaces;
using Database;
using Database.Interfaces;
using EducationService.Hubs;
using FluentMigrator.Runner;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using EducationService.Repositories;
using EducationService.Services;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DatabaseConnection");

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddSingleton<IConfigurationSettings, ConfigurationSettings>();
builder.Services.AddSingleton<IConnection, Connection>();


builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<LevelRepository>();
builder.Services.AddScoped<LevelService>();
builder.Services.AddScoped<CategoryRepository>();
builder.Services.AddScoped<AuthorizationService>();
builder.Services.AddScoped<RatingService>();
builder.Services.AddScoped<TaskRepository>();
builder.Services.AddScoped<TaskService>();
builder.Services.AddScoped<AchievementRepository>();
builder.Services.AddScoped<AchievementService>();
builder.Services.AddScoped<PortfolioRepository>();
builder.Services.AddScoped<PortfolioService>();
builder.Services.AddScoped<RangRepository>();
builder.Services.AddScoped<RangService>();


builder.Services
    .AddFluentMigratorCore().ConfigureRunner(rb =>
        rb.AddPostgres()
            .WithGlobalConnectionString(connectionString)
            .ScanIn(Assembly.GetExecutingAssembly()).For.Migrations())
    .AddLogging(lb => lb.AddFluentMigratorConsole())
    .BuildServiceProvider(false);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = AuthOptions.Issuer,
        ValidAudience = AuthOptions.Audience,
        ValidateLifetime = true,
        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
        ValidateIssuerSigningKey = true
    };
});
builder.Services.AddAuthorization();
builder.Services.AddSignalR();
var app = builder.Build();
var serviceProvider = app.Services.CreateScope().ServiceProvider;

var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
runner.MigrateUp();

app.UseHttpsRedirection();
app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();
app.MapHub<VersusHub>("versus");
app.Run();
