using Confluent.Kafka;
using EDA.Services.Catalog;
using EDA.Services.Catalog.EventHandlers;
using EDA.Services.Catalog.Repositories;
using EDA.Shared.Kafka.Consumer;
using EDA.Shared.Kafka.Producer;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<MongoConfig>(builder.Configuration.GetSection("MongoConfig"));

builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<MongoConfig>>().Value;
    return new MongoClient(settings.ConnectionString);
});

builder.Services.AddSingleton(sp =>
{
    var settings = sp.GetRequiredService<IOptions<MongoConfig>>().Value;
    var client = sp.GetRequiredService<IMongoClient>();
    return client.GetDatabase(settings.DatabaseName);
});

builder.Services.AddSingleton<IProductRepository, ProductRepository>();

builder.Services.Configure<ProducerConfig>(builder.Configuration.GetSection("ProducerConfig"));
builder.Services.AddSingleton(resolver =>
    resolver.GetRequiredService<IOptions<ProducerConfig>>().Value);
builder.Services.AddSingleton<IKafkaProducer, KafkaProducer>();

builder.Services.Configure<KafkaConsumerBaseConfig>(builder.Configuration.GetSection("ConsumerConfig"));
builder.Services.AddSingleton(resolver =>
    resolver.GetRequiredService<IOptions<KafkaConsumerBaseConfig>>().Value);

builder.Services.AddHostedService<ProductRequestEventHandler>();
builder.Services.AddHostedService<OrderEventHandler>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var repository = scope.ServiceProvider.GetRequiredService<IProductRepository>();
    await repository.InitializeProductsAsync();

}

app.Run();
