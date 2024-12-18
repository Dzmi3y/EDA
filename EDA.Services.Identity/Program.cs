using Confluent.Kafka;
using EDA.Service.Identity;
using EDA.Service.Identity.Interfaces;
using EDA.Service.Identity.Services;
using EDA.Service.Identity.Settings;
using EDA.Services.Identity.EventHandlers;
using EDA.Shared.Authorization;
using EDA.Shared.Kafka.Consumer;
using EDA.Shared.Kafka.Producer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

JwtSecurityTokenHandler.DefaultMapInboundClaims = false;
var jwtSettings = new JwtSettings();
builder.Configuration.Bind(nameof(JwtSettings), jwtSettings);
builder.Services.AddSingleton(jwtSettings);

builder.Services.AddScoped(typeof(IPasswordHasher<>), typeof(PasswordHasher<>));
builder.Services.AddScoped<IIssueTokenService, IssueTokenService>();
builder.Services.AddScoped<IRefreshTokenService, RefreshTokenService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IAuthorizationService, AuthorizationService>();


builder.Services.Configure<ProducerConfig>(builder.Configuration.GetSection("ProducerConfig"));
builder.Services.AddSingleton(resolver =>
    resolver.GetRequiredService<IOptions<ProducerConfig>>().Value);
builder.Services.AddSingleton<IKafkaProducer, KafkaProducer>();

builder.Services.Configure<KafkaConsumerBaseConfig>(builder.Configuration.GetSection("ConsumerConfig"));
builder.Services.AddSingleton(resolver =>
    resolver.GetRequiredService<IOptions<KafkaConsumerBaseConfig>>().Value);

builder.Services.AddHostedService<SignUpRequestEventHandler>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await context.Database.EnsureCreatedAsync();
}

app.Run();