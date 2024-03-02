namespace PowerDiary.Domain
{
    /// <summary>
    /// Base class for all chat events
    /// </summary>
    public abstract class ChatEvent(ChatEventType type)
    {
        /// <summary>
        /// The time the event occurred
        /// </summary>
        public required DateTime OccurredAt { get; init; }

        /// <summary>
        /// The type of the event
        /// </summary>
        public ChatEventType Type { get { return type; } }

        /// <summary>
        /// The user who caused the event
        /// </summary>
        public required string UserName { get; init; }
    }
}
