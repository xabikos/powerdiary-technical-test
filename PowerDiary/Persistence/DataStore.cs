using PowerDiary.Domain;

namespace PowerDiary.Persistence
{
    /// <summary>
    /// Implementation of the data store
    /// </summary>
    public class DataStore : IDataStore
    {
        public async Task<IQueryable<ChatEvent>> RetrieveChatEventsAsync()
        {
            await Task.CompletedTask;
            var events = new List<ChatEvent>
            {
                new UserEntered { UserName = "Bob", OccurredAt = DateTime.Parse("2024-02-18T07:20:12") },
                new UserComment { UserName = "Bob", Message = "Hello there", OccurredAt = DateTime.Parse("2024-02-18T07:20:12") },
                new UserLeft{ UserName = "Bob", OccurredAt = DateTime.Parse("2024-02-18T07:30:12") },
                new UserEntered{ UserName = "Alice", OccurredAt = DateTime.Parse("2024-02-18T07:45:12") }
            };
            return events.AsQueryable();
        }
    }
}
