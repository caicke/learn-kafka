using KafkaPoc.Domain.Events.Base;

namespace KafkaPoc.Domain.Events
{
    public class TestTopicEvent : Event
    {
        public string ProducerName { get; set; }
        public string Content { get; set; }
    }
}
