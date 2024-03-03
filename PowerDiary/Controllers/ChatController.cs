using System.Net;

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
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ChatEventsDTO>>> GetChatEventsByMinute([FromRoute] EventsGranularity granularity)
        {
            try
            {
                var events = await chatEvents.RetrieveChatEvents(granularity);
                return Ok(events);

            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, statusCode: (int)HttpStatusCode.BadRequest, title: "Error when fetching chat history");
            }
        }
    }
}
