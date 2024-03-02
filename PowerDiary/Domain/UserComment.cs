namespace PowerDiary.Domain
{
    /// <summary>
    /// Represents a comment made by a user
    /// </summary>
    public class UserComment : ChatEvent
    {
        public UserComment() : base(ChatEventType.Comment) { }

        /// <summary>
        /// The message of the comment
        /// </summary>
        public required string Message { get; set; }
    }
}
