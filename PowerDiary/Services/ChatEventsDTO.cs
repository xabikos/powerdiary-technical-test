namespace PowerDiary.Services
{
    /// <summary>
    /// Represents a series of chats that occurred at a specific time period e.g. in a minute, in an hour
    /// </summary>
    public class ChatEventsDTO
    {
        /// <summary>
        /// The point in time when the chat events occurred
        /// </summary>
        public required DateTime DateOccurred { get; init; }

        /// <summary>
        /// The chat events that occurred at the specified time period
        /// </summary>
        public required IEnumerable<string> Events { get; init; }
    }
}
