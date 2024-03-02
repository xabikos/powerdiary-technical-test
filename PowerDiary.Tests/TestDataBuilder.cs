using PowerDiary.Domain;


namespace PowerDiary.Tests
{
    /// <summary>
    /// Helper class to build test data in a fluent way
    /// </summary>
    public class TestDataBuilder
    {
        private readonly List<ChatEvent> _chatEvents = new();

        private DateTime _time;

        public TestDataBuilder WithTime(DateTime time)
        {
            _time = time;
            return this;
        }

        public TestDataBuilder AddUserEntered(string userName)
        {
            _chatEvents.Add(new UserEntered { OccurredAt = _time, UserName = userName });
            return this;
        }

        public TestDataBuilder AddUserLeft(string userName)
        {
            _chatEvents.Add(new UserLeft { OccurredAt = _time, UserName = userName });
            return this;
        }

        public TestDataBuilder AddUserComment(string userName, string message)
        {
            _chatEvents.Add(new UserComment { OccurredAt = _time, UserName = userName, Message = message });
            return this;
        }

        public TestDataBuilder AddUserHighFive(string userName, string toUserName)
        {
            _chatEvents.Add(new UserHighFive { OccurredAt = _time, UserName = userName, ToUserName = toUserName });
            return this;
        }

        public IQueryable<ChatEvent> Build()
        {
            return _chatEvents.AsQueryable();
        }
    }
}