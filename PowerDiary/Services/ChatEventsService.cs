using System.Text;

using PowerDiary.Domain;
using PowerDiary.Persistence;

namespace PowerDiary.Services
{
    /// <summary>
    /// Implementation of <see cref="IChatEventsService"/>
    /// </summary>
    public class ChatEventsService(ILogger<ChatEventsService> logger, IDataStore dataStore) : IChatEventsService
    {

        public async Task<IEnumerable<ChatEventsDTO>> RetrieveChatEventsAsync(EventsGranularity granularity)
        {
            if (!Enum.IsDefined(typeof(EventsGranularity), granularity))
            {
                logger.LogError("Invalid granularity value: {Granularity}", granularity);
                throw new ArgumentException("Invalid granularity value", nameof(granularity));
            }

            try
            {
                logger.LogInformation("Fetching chat history with granularity: {Granularity}", granularity);
                // This is still IQueryable, so we can still apply further filtering or grouping
                var chatEvents = await dataStore.RetrieveChatEventsAsync();

                if (!chatEvents.Any())
                {
                    return [];
                }

                return granularity switch
                {
                    EventsGranularity.Minute => GroupChatEventsByMinute(chatEvents),
                    EventsGranularity.Hour => GroupChatEventsByHour(chatEvents),
                    EventsGranularity.Day => GroupChatEventsByDay(chatEvents),
                    // Not reachable as we check for valid enum type right above, but we should handle it
                    _ => [],
                };
            }
            catch (Exception ex)
            {
                // Since this is a read only operation we can't do much about it, so we just log the error and rethrow
                // so the calling code could handle it by adding a retry policy or something similar
                logger.LogError(ex, "Error when fetching chat history");
                throw;
            }
        }

        private static IEnumerable<ChatEventsDTO> GroupChatEventsByMinute(IQueryable<ChatEvent> chatEvents)
        {
            return chatEvents
                .GroupBy(ce => new { ce.OccurredAt.Date, ce.OccurredAt.Hour, ce.OccurredAt.Minute })
                .Select(g => new ChatEventsDTO
                {
                    DateOccurred = new DateTime(g.Key.Date.Year, g.Key.Date.Month, g.Key.Date.Day, g.Key.Hour, g.Key.Minute, 0),
                    Events = g.Select(e => e.ToMinuteInfo())
                });
        }

        private static IEnumerable<ChatEventsDTO> GroupChatEventsByHour(IQueryable<ChatEvent> chatEvents)
        {
            return chatEvents
                .GroupBy(ce => new { ce.OccurredAt.Date, ce.OccurredAt.Hour })
                .Select(g => new ChatEventsDTO
                {
                    DateOccurred = new DateTime(g.Key.Date.Year, g.Key.Date.Month, g.Key.Date.Day, g.Key.Hour, 0, 0),
                    Events = g.GroupBy(e => e.Type).Select(e => ToGroupInfo(e))
                });
        }

        private static IEnumerable<ChatEventsDTO> GroupChatEventsByDay(IQueryable<ChatEvent> chatEvents)
        {
            return chatEvents
                .GroupBy(ce => ce.OccurredAt.Date)
                .Select(g => new ChatEventsDTO
                {
                    DateOccurred = g.Key,
                    Events = g.GroupBy(e => e.Type).Select(e => ToGroupInfo(e))
                });
        }

        /// <summary>
        /// Includes the logic of converting a group of chat events to a string when we group by hour or day
        /// </summary>
        private static string ToGroupInfo(IGrouping<ChatEventType, ChatEvent> group)
        {
            // Local function to return the info for high fives as it includes some special logic
            static string ReturnInfoForHighFives(IGrouping<ChatEventType, ChatEvent> group)
            {
                // We need to cast to UserHighFive to get the ToUserName
                var highFives = group.OfType<UserHighFive>();
                var highFiveUsersCount = highFives.Select(hf => hf.UserName).Distinct().Count();
                var highFiveReceivedCount = highFives.Select(hf => hf.ToUserName).Distinct().Count(); ;
                var result = new StringBuilder();
                result.Append(highFiveUsersCount < 2 ? "1 person high-fived" : $"{highFiveUsersCount} people high-fived");
                result.Append(' ');
                result.Append(highFiveReceivedCount < 2 ? "1 person" : $"{highFiveReceivedCount} people");
                return result.ToString();
            }
            var count = group.Count();

            return group.Key switch
            {
                ChatEventType.EnterRoom => count < 2 ? "1 person entered" : $"{count} people entered",
                ChatEventType.LeftRoom => count < 2 ? "1 person left" : $"{count} people left",
                ChatEventType.Comment => count < 2 ? "1 comment" : $"{count} comments",
                ChatEventType.HighFive => ReturnInfoForHighFives(group),
                // We should never get here, but if we do, we should handle it
                _ => "",
            };
        }
    }
}
