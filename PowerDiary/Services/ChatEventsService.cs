using PowerDiary.Persistence;

namespace PowerDiary.Services
{
    /// <summary>
    /// Implementation of <see cref="IChatEventsService"/>
    /// </summary>
    public class ChatEventsService(IDataStore dataStore) : IChatEventsService
    {

        public Task<IEnumerable<ChatEventsDTO>> RetrieveChatEvents(EventsGranularity granularity)
        {
            throw new NotImplementedException();
        }

    }
}
