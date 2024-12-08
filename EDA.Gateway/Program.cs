using Confluent.Kafka;
using EDA.Gateway.Services;
using EDA.Shared.Kafka.Producer;
using EDA.Shared.Redis;
using EDA.Shared.Redis.Interfaces;
using EDA.Shared.Redis.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

var producerConfig = new ProducerConfig
{
    BootstrapServers = "localhost:9092"
};
builder.Services.AddSingleton(producerConfig);
builder.Services.AddSingleton<IKafkaProducer, KafkaProducer>();

var redisConfig = new RedisConfig
{
    Configuration = "localhost:6379",
    DefaultExpiry = TimeSpan.FromMinutes(30),
    DefaultTimeout = TimeSpan.FromMinutes(3)
};
builder.Services.AddSingleton(redisConfig);
builder.Services.AddSingleton<IRedisStringsService, RedisStringsService>();

var consumerConfig = new ConsumerConfig
{
    GroupId = "gateway-group",
    BootstrapServers = "localhost:9092",
    AutoOffsetReset = AutoOffsetReset.Earliest
};
builder.Services.AddSingleton(consumerConfig); 
builder.Services.AddHostedService<ProductResponseKafkaConsumerService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
