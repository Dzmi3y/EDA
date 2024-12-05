using Confluent.Kafka;
using EDA.Shared.Kafka.Producer;

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
