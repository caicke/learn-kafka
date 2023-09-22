using KafkaPoc.Application.ViewModels;
using KafkaPoc.Domain.Events;

namespace KafkaPoc.Application.Services
{
    public class PublishEventService : IPublishEventService
    {
        private readonly IKafkaBus _kafkaBus;
        public PublishEventService(IKafkaBus kafkaBus)
        {
            _kafkaBus = kafkaBus;
        }

        public async Task<bool> PublishEvent(EventTestViewModel viewModel)
        {
            var @event = new TestTopicEvent()
            {
                ProducerName = viewModel.Name,
                Content = viewModel.Content,
                CreationDate = DateTime.Now,
            };

            await _kafkaBus.Publish(@event);

            return true;
        }
    }
}
