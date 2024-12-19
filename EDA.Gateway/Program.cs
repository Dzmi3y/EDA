using Confluent.Kafka;
using EDA.Gateway.EventHandlers;
using EDA.Shared.Authorization;
using EDA.Shared.Kafka.Consumer;
using EDA.Shared.Kafka.Producer;
using EDA.Shared.Redis;
using EDA.Shared.Redis.Interfaces;
using EDA.Shared.Redis.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

builder.Services.AddScoped(typeof(IPasswordHasher<>), typeof(PasswordHasher<>));

builder.Services.Configure<ProducerConfig>(builder.Configuration.GetSection("ProducerConfig"));
builder.Services.AddSingleton(resolver =>
    resolver.GetRequiredService<IOptions<ProducerConfig>>().Value);
builder.Services.AddSingleton<IKafkaProducer, KafkaProducer>();

var passwordEncryptionConfig = new PasswordEncryptionConfig();
builder.Configuration.Bind(nameof(PasswordEncryptionConfig), passwordEncryptionConfig);
builder.Services.AddSingleton(passwordEncryptionConfig);

builder.Services.Configure<RedisConfig>(builder.Configuration.GetSection("RedisConfig"));
builder.Services.AddSingleton(resolver =>
        resolver.GetRequiredService<IOptions<RedisConfig>>().Value);
builder.Services.AddSingleton<IRedisStringsService, RedisStringsService>();


builder.Services.Configure<KafkaConsumerBaseConfig>(builder.Configuration.GetSection("ConsumerConfig"));
builder.Services.AddSingleton(resolver =>
    resolver.GetRequiredService<IOptions<KafkaConsumerBaseConfig>>().Value);

builder.Services.AddHostedService<ProductResponseEventHandler>();
builder.Services.AddHostedService<SignUpResponseEventHandler>();

var app = builder.Build();

if (app.Environment.IsStaging() || app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
