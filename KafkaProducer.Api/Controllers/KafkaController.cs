using KafkaPoc.Application.Services;
using KafkaPoc.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace KafkaConsumer.Controllers
{
    [ApiController]
    [Route("[controller]/v1")]
    public class KafkaController : ControllerBase
    {
        private readonly ILogger<KafkaController> _logger;
        private readonly IPublishEventService _service;

        public KafkaController(ILogger<KafkaController> logger, IPublishEventService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpPost("publish")]
        public async Task<IActionResult> PublishEvent([FromBody] EventTestViewModel viewModel)
        {
            try
            {
                var result = await _service.PublishEvent(viewModel);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}