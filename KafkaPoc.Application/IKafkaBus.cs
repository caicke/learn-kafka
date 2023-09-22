using KafkaPoc.Domain.Events.Base;

namespace KafkaPoc.Application
{
    public interface IKafkaBus
    {
        Task Publish<T>(T @event) where T : Event;
    }
}
