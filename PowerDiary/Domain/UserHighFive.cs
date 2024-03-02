namespace PowerDiary.Domain
{
    /// <summary>
    /// Represents a high five between two users
    /// </summary>
    public class UserHighFive : ChatEvent
    {
        public UserHighFive() : base(ChatEventType.HighFive) { }

        /// <summary>
        /// The user who received the high five
        /// </summary>
        public required string ToUserName { get; set; }
    }
}
