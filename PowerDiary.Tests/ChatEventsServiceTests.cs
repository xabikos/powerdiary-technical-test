using NSubstitute;

using PowerDiary.Domain;
using PowerDiary.Persistence;
using PowerDiary.Services;


namespace PowerDiary.Tests
{
    public class ChatEventsServiceTests
    {
        [Fact]
        public async Task RetrieveChatEvents_WithInvalidGranularity_ThrowsException()
        {
            // Arrange
            var dataStore = Substitute.For<IDataStore>();
            var sut = new ChatEventsService(dataStore);
            var invalidGranularity = (EventsGranularity)999; // Invalid enum value

            // Act and Assert
            await Assert.ThrowsAsync<ArgumentException>(() => sut.RetrieveChatEvents(invalidGranularity));
        }

        [Theory]
        [InlineData(EventsGranularity.Minute)]
        [InlineData(EventsGranularity.Hour)]
        [InlineData(EventsGranularity.Day)]
        public async void RetrieveChatEvents_NotEvents_ReturnsEmpty(EventsGranularity granularity)
        {
            // Arrange
            var dataStore = Substitute.For<IDataStore>();
            dataStore.ChatEvents.Returns(Enumerable.Empty<ChatEvent>().AsQueryable());
            var sut = new ChatEventsService(dataStore);

            // Act
            var events = await sut.RetrieveChatEvents(granularity);

            // Assert
            Assert.Empty(events);
        }

        [Fact]
        public async Task RetrieveChatEvents_MinuteGranularity_ReturnsResult()
        {
            // Arrange
            var dataStore = Substitute.For<IDataStore>();
            var testDataBuilder = new TestDataBuilder();
            var time = DateTime.Parse("2024-02-18T07:20:12");
            var expectedEvents = testDataBuilder
                .WithTime(time)
                .AddUserEntered("Bob")
                .AddUserComment("Bob", "Hello there")

                .WithTime(time.AddMinutes(10))
                .AddUserLeft("Bob")

                .WithTime(time.AddMinutes(15))
                .AddUserEntered("Alice")

                .WithTime(time.AddMinutes(20))
                .AddUserComment("Alice", "Hi people!")
                .AddUserHighFive("Alice", "Bob")

                .WithTime(time.AddMinutes(25))
                .AddUserLeft("Alice")

                .Build();
            var sut = new ChatEventsService(dataStore);
            dataStore.ChatEvents.Returns(expectedEvents);

            // Act
            var events = await sut.RetrieveChatEvents(EventsGranularity.Minute);

            // Assert
            Assert.Equal(5, events.Count());
            Assert.Equal(2, events.ElementAt(0).Events.Count());
            Assert.Single(events.ElementAt(1).Events);
            Assert.Single(events.ElementAt(2).Events);
            Assert.Equal(2, events.ElementAt(3).Events.Count());
            Assert.Single(events.ElementAt(4).Events);
        }

        [Fact]
        public async Task RetrieveChatEvents_HourGranularity_ReturnsResult()
        {
            // Arrange
            var dataStore = Substitute.For<IDataStore>();
            var testDataBuilder = new TestDataBuilder();
            var time = DateTime.Parse("2024-02-18T07:20:12");
            var expectedEvents = testDataBuilder
                .WithTime(time)
                .AddUserEntered("Bob")
                .AddUserComment("Bob", "Hello there")
                .WithTime(time.AddMinutes(10))
                .AddUserLeft("Bob")
                .AddUserEntered("Alice")
                .AddUserComment("Alice", "Hi people!")
                .AddUserHighFive("Alice", "Bob")

                .WithTime(time.AddHours(1))
                .AddUserLeft("Alice")
                .AddUserEntered("Bob")
                .AddUserComment("Bob", "Hello there")
                .WithTime(time.AddHours(1).AddMinutes(5))
                .AddUserComment("Bob", "Hi there!")


                .WithTime(time.AddHours(2))
                .AddUserEntered("Alice")
                .WithTime(time.AddHours(2).AddMinutes(10))
                .AddUserComment("Alice", "Hi people!")
                .AddUserHighFive("Alice", "Bob")

                .Build();
            var sut = new ChatEventsService(dataStore);
            dataStore.ChatEvents.Returns(expectedEvents);

            // Act
            var events = await sut.RetrieveChatEvents(EventsGranularity.Hour);

            // Assert
            Assert.Equal(3, events.Count());
            Assert.Equal(4, events.ElementAt(0).Events.Count());
            Assert.Equal(3, events.ElementAt(1).Events.Count());
            Assert.Equal(3, events.ElementAt(2).Events.Count());
        }

        [Fact]
        public async Task RetrieveChatEvents_DayGranularity_ReturnsResult()
        {
            // Arrange
            var dataStore = Substitute.For<IDataStore>();
            var testDataBuilder = new TestDataBuilder();
            var time = DateTime.Parse("2024-02-18T07:20:12");
            var expectedEvents = testDataBuilder
                .WithTime(time)
                .AddUserEntered("Bob")
                .AddUserComment("Bob", "Hello there")
                .WithTime(time.AddMinutes(10))
                .AddUserLeft("Bob")
                .AddUserEntered("Alice")
                .WithTime(time.AddHours(1).AddMinutes(15))
                .AddUserComment("Alice", "Hi people!")
                .AddUserHighFive("Alice", "Bob")

                .WithTime(time.AddDays(1))
                .AddUserLeft("Alice")
                .WithTime(time.AddDays(1).AddMinutes(5))
                .AddUserEntered("Bob")
                .AddUserComment("Bob", "Hello there")
                .WithTime(time.AddDays(1).AddHours(1).AddMinutes(5))
                .AddUserComment("Bob", "Hi there!")
                .AddUserLeft("Bob")
                .WithTime(time.AddDays(1).AddHours(1).AddMinutes(10))
                .AddUserEntered("Alice")
                .AddUserComment("Alice", "Hi people!")

                .WithTime(time.AddDays(2))
                .AddUserEntered("Bob")
                .AddUserComment("Bob", "Hello there")
                .WithTime(time.AddDays(2).AddHours(1))
                .AddUserComment("Bob", "Hi there!")
                .AddUserLeft("Bob")
                .WithTime(time.AddDays(2).AddHours(2).AddMinutes(20))
                .AddUserEntered("Alice")
                .AddUserComment("Alice", "Hi people!")
                .AddUserHighFive("Alice", "Bob")

                .Build();
            var sut = new ChatEventsService(dataStore);
            dataStore.ChatEvents.Returns(expectedEvents);

            // Act
            var events = await sut.RetrieveChatEvents(EventsGranularity.Day);

            // Assert
            Assert.Equal(3, events.Count());
            Assert.Equal(4, events.ElementAt(0).Events.Count());
            Assert.Equal(3, events.ElementAt(1).Events.Count());
            Assert.Equal(4, events.ElementAt(1).Events.Count());
        }

    }
}