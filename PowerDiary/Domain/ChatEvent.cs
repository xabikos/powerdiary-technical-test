namespace PowerDiary.Domain
{
    /// <summary>
    /// Base class for all chat events
    /// </summary>
    public abstract class ChatEvent()
    {
        protected ChatEvent(ChatEventType type) : this()
        {
            Type = type;
        }

        /// <summary>
        /// The unique identifier of the event
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The time the event occurred
        /// </summary>
        public required DateTime OccurredAt { get; init; }

        /// <summary>
        /// The type of the event
        /// </summary>
        public ChatEventType Type { get; private set; }

        /// <summary>
        /// The user who caused the event
        /// </summary>
        public required string UserName { get; init; }
    }
}
