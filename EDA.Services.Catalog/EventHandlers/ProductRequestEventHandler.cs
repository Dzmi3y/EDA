﻿using Confluent.Kafka;
using EDA.Services.Catalog.Repositories;
using EDA.Shared.Kafka.Consumer;
using EDA.Shared.Kafka.Enums;
using EDA.Shared.Kafka.Messages.Requests;
using EDA.Shared.Kafka.Messages.Responses;
using EDA.Shared.Kafka.Messages.Responses.ResponsePayloads;
using EDA.Shared.Kafka.Producer;
using Newtonsoft.Json;
using System.Net;

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

                var productResponsePayload = new ProductResponsePayload()
                {
                    Products = productList
                };

                var responseMessage = new ResponseMessage<ProductResponsePayload>();
                responseMessage.Status = HttpStatusCode.OK;
                responseMessage.Payload = productResponsePayload;


                await _producer.SendMessageAsync(Topics.ProductPageResponse,
                     result.Message.Key, responseMessage.ToString());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}
