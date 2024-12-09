using Confluent.Kafka;
using EDA.Gateway.Services;
using EDA.Shared.Kafka.Producer;
using EDA.Shared.Redis;
using EDA.Shared.Redis.Interfaces;
using EDA.Shared.Redis.Services;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();


builder.Services.Configure<ProducerConfig>(builder.Configuration.GetSection("ProducerConfig"));
builder.Services.AddSingleton(resolver =>
    resolver.GetRequiredService<IOptions<ProducerConfig>>().Value);
builder.Services.AddSingleton<IKafkaProducer, KafkaProducer>();


builder.Services.Configure<RedisConfig>(builder.Configuration.GetSection("RedisConfig"));
builder.Services.AddSingleton(resolver =>
        resolver.GetRequiredService<IOptions<RedisConfig>>().Value);
builder.Services.AddSingleton<IRedisStringsService, RedisStringsService>();


builder.Services.Configure<ConsumerConfig>(builder.Configuration.GetSection("ConsumerConfig"));
builder.Services.AddSingleton(resolver =>
    resolver.GetRequiredService<IOptions<ConsumerConfig>>().Value);
builder.Services.AddHostedService<ProductResponseKafkaConsumerService>();

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
