using KafkaConsumer.Domain.Events.Base;

namespace KafkaConsumer.Domain.Events
{
    public class TestTopicEvent : Event
    {
        public string ProducerName { get; set; }
        public string Content { get; set; }
    }
}
