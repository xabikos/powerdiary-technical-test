namespace PowerDiary.Domain
{
    /// <summary>
    /// Represents a user entering the room
    /// </summary>
    public class UserEntered : ChatEvent
    {
        public UserEntered() : base(ChatEventType.EnterRoom) { }
    }
}
