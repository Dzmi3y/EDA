using Confluent.Kafka;
using EDA.Shared.Kafka.Consumer;
using EDA.Shared.Kafka.Producer;
using EDA.Shared.Redis;
using EDA.Shared.Redis.Interfaces;

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


var consumerConfig = new ConsumerConfig
{
    GroupId = "gateway-group",
    BootstrapServers = "localhost:9092",
    AutoOffsetReset = AutoOffsetReset.Earliest
};
builder.Services.AddSingleton(consumerConfig);
builder.Services.AddSingleton<IKafkaConsumer, KafkaConsumer>();
var redisConfig = new RedisConfig
{
    Configuration = "localhost:6379",
    DefaultExpiry = TimeSpan.FromMinutes(30),
    DefaultTimeout = TimeSpan.FromMinutes(3)
};
builder.Services.AddSingleton(redisConfig);
builder.Services.AddSingleton<IRedisService, IRedisService>();
//builder.Services.AddHostedService<KafkaConsumer>();

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
