
namespace EDA.Shared.Kafka
{
    public interface IKafkaMessageBus<Tk, Tv>
    {
        Task PublishAsync(Tk key, Tv message);
    }
}
