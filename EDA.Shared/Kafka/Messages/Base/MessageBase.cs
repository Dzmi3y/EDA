using EDA.Shared.Kafka.Enums;

namespace EDA.Shared.Kafka.Messages.Base
{
    public abstract class MessageBase
    {
        protected MessageBase(EventTypes eventType)
        {
            EventType = eventType;
        }
        public EventTypes EventType { get; set; }
    }
}
