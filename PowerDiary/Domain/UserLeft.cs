namespace PowerDiary.Domain
{
    /// <summary>
    /// Represents a user leaving the room
    /// </summary>
    public class UserLeft : ChatEvent
    {
        public UserLeft() : base(ChatEventType.LeftRoom) { }

    }
}
