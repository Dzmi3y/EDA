using Confluent.Kafka;
using EDA.Services.Catalog.Repositories;
using EDA.Shared.Kafka.Consumer;
using EDA.Shared.Kafka.Enums;
using EDA.Shared.Kafka.Messages;
using EDA.Shared.Kafka.Producer;
using MongoDB.Bson;
using Newtonsoft.Json;

namespace EDA.Services.Catalog.EventHandlers
{
    public class ProductRequestEventHandler : KafkaConsumerBase
    {
        private readonly IKafkaProducer _producer;
        private readonly IProductRepository _repository;
        private readonly ILogger<ProductRequestEventHandler> _logger;

        public ProductRequestEventHandler(KafkaConsumerBaseConfig config,
            IKafkaProducer producer, IProductRepository repository,
            ILogger<ProductRequestEventHandler> logger)
            : base(config, Topics.ProductPageRequest, logger)
        {
            _producer = producer;
            _repository = repository;
            _logger = logger;
        }

        protected override async Task HandleAsync(ConsumeResult<string, string> result)
        {
            try
            {
                var message = JsonConvert.DeserializeObject<ProductPageRequestMessage>(result.Message.Value);

                if (message == null)
                {
                    _logger.LogError("ProductPageRequestMessage is null");
                    return;
                }

                var productList = await _repository
                    .GetListAsync(message.PageSize, message.PageNumber);

                await _producer.SendMessageAsync(Topics.ProductPageResponse,
                     result.Message.Key, productList.ToJson());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}
