using PowerDiary.Domain;

namespace PowerDiary.Persistence
{
    /// <summary>
    /// Interface to represent the data store, whatever it may be
    /// </summary>
    public interface IDataStore
    {
        /// <summary>
        /// Exposes the chat events in the data store in a queryable form
        /// </summary>
        public IQueryable<ChatEvent> ChatEvents { get; }
    }
}
