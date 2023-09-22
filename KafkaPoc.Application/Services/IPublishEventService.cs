using KafkaPoc.Application.ViewModels;

namespace KafkaPoc.Application.Services
{
    public interface IPublishEventService
    {
        Task<bool> PublishEvent(EventTestViewModel viewModel);
    }
}
