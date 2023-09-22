namespace KafkaConsumer.Domain.Entities
{
    public class Topic
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
