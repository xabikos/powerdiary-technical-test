using PowerDiary.Domain;

namespace PowerDiary.Services
{
    /// <summary>
    /// Class that contains all the extension methods for the ChatEvent class
    /// </summary>
    public static class ChatEventExtensions
    {
        /// <summary>
        /// Returns a string representation of the chat event
        /// </summary>
        public static string ToMinuteInfo(this ChatEvent chatEvent)
        {
            switch (chatEvent)
            {
                case UserEntered ue:
                    return $"{ue.UserName} enters the room";
                case UserComment uc:
                    return $"{uc.UserName} comments: '{uc.Message}'";
                case UserHighFive uhf:
                    return $"{uhf.UserName} high-fives {uhf.ToUserName}";
                case UserLeft ul:
                    return $"{ul.UserName} leaves";
                // Normally this should never happen, but if it does, we should handle it
                default:
                    return "";
            }
        }
    }
}
