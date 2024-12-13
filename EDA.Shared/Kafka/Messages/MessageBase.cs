using Newtonsoft.Json;

namespace EDA.Shared.Kafka.Messages
{
    public abstract class MessageBase
    {
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}
