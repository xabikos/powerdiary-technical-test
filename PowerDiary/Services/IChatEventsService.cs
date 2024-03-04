namespace PowerDiary.Services
{
    /// <summary>
    /// Application service that provides methods for managing chat events
    /// </summary>
    public interface IChatEventsService
    {
        /// <summary>
        /// Retrieves chat events based on the specified granularity
        /// </summary>
        /// <param name="granularity">The granularity to group events by</param>
        /// <returns>A list with chat events DTO</returns>
        public Task<IEnumerable<ChatEventsDTO>> RetrieveChatEventsAsync(EventsGranularity granularity);
    }
}
