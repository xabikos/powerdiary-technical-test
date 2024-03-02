using Microsoft.AspNetCore.Mvc;

using PowerDiary.Services;

namespace PowerDiary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ChatController(IChatEventsService chatEvents) : ControllerBase
    {

        [HttpGet("{granularity:EventsGranularity}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ChatEventsDTO>>> GetChatEventsByMinute([FromRoute]EventsGranularity granularity)
        {
            var events = await chatEvents.RetrieveChatEvents(granularity);
            return Ok(events);
        }
    }
}
