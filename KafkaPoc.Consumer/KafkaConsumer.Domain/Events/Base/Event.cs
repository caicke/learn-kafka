namespace KafkaConsumer.Domain.Events.Base
{
    public abstract class Event
    {
        public DateTime CreationDate { get; set; }
    }
}
