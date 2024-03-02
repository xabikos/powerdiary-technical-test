using Microsoft.AspNetCore.Mvc;

using PowerDiary.Services;

namespace PowerDiary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ChatController(IChatEventsService chatEvents) : ControllerBase
    {

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ChatEventsDTO>>> GetChatEventsByMinute(EventsGranularity eventsGranularity)
        {
            var events = await chatEvents.RetrieveChatEvents(eventsGranularity);
            return Ok(events);
        }
    }
}
