using Microsoft.EntityFrameworkCore;

using PowerDiary.Domain;

namespace PowerDiary.Persistence
{
    /// <summary>
    /// Implementation of the data store
    /// </summary>
    public class PowerDiaryDbContext : DbContext, IDataStore
    {
        private DbSet<ChatEvent> _chatEvents { get; set; }

        public PowerDiaryDbContext()
        {
            DbPath = Path.Join(Directory.GetCurrentDirectory(), "Persistence", "power-diary.db");
        }
        public string DbPath { get; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlite($"Data Source={DbPath}");

        /// <summary>
        /// In a larger application the following configuration would be in a separate class per entity
        /// </summary>        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Here we are using the table per hierarchy (TPH) pattern
            // This means that all the derived types of ChatEvent will be stored in the same table
            modelBuilder.Entity<ChatEvent>(ce =>
            {
                ce.ToTable("ChatEvents");
                ce.Property(x => x.Id).ValueGeneratedOnAdd();
                ce.Property(x => x.OccurredAt).IsRequired();
                ce.Property(x => x.UserName).IsRequired().HasMaxLength(50);
                ce.HasDiscriminator(x => x.Type)
                    .HasValue<UserEntered>(ChatEventType.EnterRoom)
                    .HasValue<UserLeft>(ChatEventType.LeftRoom)
                    .HasValue<UserComment>(ChatEventType.Comment)
                    .HasValue<UserHighFive>(ChatEventType.HighFive);
            });

            modelBuilder.Entity<UserEntered>();
            modelBuilder.Entity<UserLeft>();

            modelBuilder.Entity<UserComment>(uc =>
            uc.Property(x => x.Message).IsRequired().HasMaxLength(500));

            modelBuilder.Entity<UserHighFive>(
                uhf => uhf.Property(x => x.ToUserName).IsRequired().HasMaxLength(50));


            InitializeData(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        public async Task<IQueryable<ChatEvent>> RetrieveChatEventsAsync()
        {
            // Just for the test scope we are using SQLite in-memory database
            // which doesn't support translating LINQ operations to SQL
            // so we need to materialize the query and actually execute the grouping and aggregation in memory
            // In a real-world application we would use a real database and the query would be translated to SQL
            var result = await _chatEvents.ToListAsync();
            return result.AsQueryable();
        }

        private static void InitializeData(ModelBuilder modelBuilder)
        {
            // Here we are seeding some data so the application can be tested
            // We don't check for logical consistency in the data, e.g. a user is inside a room when posting a message
            // we just want to have some data to work with
            var startingTime = DateTime.Parse("2024-02-02 18:05:16");

            modelBuilder.Entity<UserEntered>().HasData(new List<UserEntered>
            {
                new() { Id = 1, OccurredAt = startingTime, UserName = "Bob" },
                new() { Id = 2, OccurredAt = startingTime.AddMinutes(2), UserName = "Alice" },
                new() { Id = 3, OccurredAt = startingTime.AddMinutes(10), UserName = "George" },

                new() { Id = 4, OccurredAt = startingTime.AddHours(2), UserName = "Bob" },
                new() { Id = 5, OccurredAt = startingTime.AddHours(2).AddMinutes(2), UserName = "John" },

                new() { Id = 6, OccurredAt = startingTime.AddHours(3).AddMinutes(10), UserName = "Maria" },


                new() { Id = 7, OccurredAt = startingTime.AddDays(1).AddHours(1).AddMinutes(5), UserName = "Bob" },
                new() { Id = 8, OccurredAt = startingTime.AddDays(1).AddHours(1).AddMinutes(10), UserName = "Maria" },

                new() { Id = 9, OccurredAt = startingTime.AddDays(1).AddHours(2).AddMinutes(15), UserName = "Alice" },
                new() { Id = 10, OccurredAt = startingTime.AddDays(1).AddHours(2).AddMinutes(20), UserName = "John" },
            });

            modelBuilder.Entity<UserLeft>().HasData(new List<UserLeft>
            {
                new() { Id = 11, OccurredAt = startingTime.AddMinutes(10), UserName = "Bob" },
                new() { Id = 12, OccurredAt = startingTime.AddMinutes(22), UserName = "Alice" },
                new() { Id = 13, OccurredAt = startingTime.AddMinutes(25), UserName = "John" },

                new() { Id = 14, OccurredAt = startingTime.AddHours(2).AddMinutes(5), UserName = "Bob" },
                new() { Id = 15, OccurredAt = startingTime.AddHours(2).AddMinutes(20), UserName = "John" },

                new() { Id = 16, OccurredAt = startingTime.AddHours(3).AddMinutes(20), UserName = "Maria" },


                new() { Id = 17, OccurredAt = startingTime.AddDays(1).AddHours(1).AddMinutes(15), UserName = "Bob" },
                new() { Id = 18, OccurredAt = startingTime.AddDays(1).AddHours(1).AddMinutes(20), UserName = "Maria" },
            });

            modelBuilder.Entity<UserComment>().HasData(new List<UserComment>
            {
                new() { Id = 19, OccurredAt = startingTime.AddMinutes(5), UserName = "Bob", Message = "Hello" },
                new() { Id = 20, OccurredAt = startingTime.AddMinutes(7), UserName = "Alice", Message = "Hi" },
                new() { Id = 21, OccurredAt = startingTime.AddMinutes(12), UserName = "George", Message = "How are you?" },
                new() { Id = 22, OccurredAt = startingTime.AddMinutes(12), UserName = "George", Message = "Hey people" },
                new() { Id = 23, OccurredAt = startingTime.AddMinutes(14), UserName = "George", Message = "Hello again" },

                new() { Id = 24, OccurredAt = startingTime.AddHours(2).AddMinutes(7), UserName = "Bob", Message = "Hey Alice" },
                new() { Id = 25, OccurredAt = startingTime.AddHours(2).AddMinutes(10), UserName = "John", Message = "Hi" },
                new() { Id = 26, OccurredAt = startingTime.AddHours(2).AddMinutes(10), UserName = "Alice", Message = "Hello all" },

                new() { Id = 27, OccurredAt = startingTime.AddHours(3).AddMinutes(12), UserName = "Maria", Message = "How are you?" },
                new() { Id = 28, OccurredAt = startingTime.AddHours(3).AddMinutes(12), UserName = "George", Message = "How are you?" },
                new() { Id = 29, OccurredAt = startingTime.AddHours(3).AddMinutes(20), UserName = "Maria", Message = "Hi George" },
                new() { Id = 30, OccurredAt = startingTime.AddHours(3).AddMinutes(22), UserName = "George", Message = "Hello Maria" },


                new() { Id = 31, OccurredAt = startingTime.AddDays(1).AddHours(1).AddMinutes(12), UserName = "Bob", Message = "Hello" },
                new() { Id = 32, OccurredAt = startingTime.AddDays(1).AddHours(1).AddMinutes(15), UserName = "Maria", Message = "Hi" },
                new() { Id = 33, OccurredAt = startingTime.AddDays(1).AddHours(1).AddMinutes(20), UserName = "Bob", Message = "How are you?" },

                new() { Id = 34, OccurredAt = startingTime.AddDays(1).AddHours(2).AddMinutes(12), UserName = "Alice", Message = "How are you?" },
                new() { Id = 35, OccurredAt = startingTime.AddDays(1).AddHours(2).AddMinutes(15), UserName = "John", Message = "Hi" },
                new() { Id = 36, OccurredAt = startingTime.AddDays(1).AddHours(2).AddMinutes(15), UserName = "Alice", Message = "Hello all" },

                new() { Id = 37, OccurredAt = startingTime.AddDays(1).AddHours(3).AddMinutes(10), UserName = "Maria", Message = "How are you?" },
                new() { Id = 38, OccurredAt = startingTime.AddDays(1).AddHours(3).AddMinutes(20), UserName = "George", Message = "How are you?" },


                new() { Id = 39, OccurredAt = startingTime.AddDays(2).AddHours(1).AddMinutes(12), UserName = "Bob", Message = "Hello" },
                new() { Id = 40, OccurredAt = startingTime.AddDays(2).AddHours(1).AddMinutes(15), UserName = "Maria", Message = "Hi" },
                new() { Id = 41, OccurredAt = startingTime.AddDays(2).AddHours(1).AddMinutes(20), UserName = "Bob", Message = "How are you?" },

                new() { Id = 42, OccurredAt = startingTime.AddDays(2).AddHours(2), UserName = "Alice", Message = "How are you?" },
                new() { Id = 43, OccurredAt = startingTime.AddDays(2).AddHours(2).AddMinutes(5), UserName = "John", Message = "Hi" },
                new() { Id = 44, OccurredAt = startingTime.AddDays(2).AddHours(2).AddMinutes(5), UserName = "Alice", Message = "Hello all" },
            });

            modelBuilder.Entity<UserHighFive>().HasData(new List<UserHighFive>
            {
                new() { Id = 45, OccurredAt = startingTime.AddMinutes(8), UserName = "Bob", ToUserName = "Alice" },
                new() { Id = 46, OccurredAt = startingTime.AddMinutes(10), UserName = "John", ToUserName = "Alice" },
                new() { Id = 47, OccurredAt = startingTime.AddMinutes(12), UserName = "Alice", ToUserName = "Bob" },
                new() { Id = 48, OccurredAt = startingTime.AddMinutes(14), UserName = "George", ToUserName = "Alice" },

                new() { Id = 49, OccurredAt = startingTime.AddHours(2).AddMinutes(8), UserName = "Bob", ToUserName = "Alice" },
                new() { Id = 50, OccurredAt = startingTime.AddHours(2).AddMinutes(10), UserName = "John", ToUserName = "Alice" },
                new() { Id = 51, OccurredAt = startingTime.AddHours(2).AddMinutes(12), UserName = "Alice", ToUserName = "Bob" },
                new() { Id = 52, OccurredAt = startingTime.AddHours(2).AddMinutes(14), UserName = "George", ToUserName = "Alice" },

                new() { Id = 53, OccurredAt = startingTime.AddHours(3).AddMinutes(8), UserName = "Bob", ToUserName = "Alice" },
                new() { Id = 54, OccurredAt = startingTime.AddHours(3).AddMinutes(10), UserName = "John", ToUserName = "Alice" },
                new() { Id = 55, OccurredAt = startingTime.AddHours(3).AddMinutes(12), UserName = "Alice", ToUserName = "Bob" },
                new() { Id = 56, OccurredAt = startingTime.AddHours(3).AddMinutes(14), UserName = "George", ToUserName = "Alice" },

                new() { Id = 57, OccurredAt = startingTime.AddDays(1).AddHours(1).AddMinutes(8), UserName = "Bob", ToUserName = "Alice" },
                new() { Id = 58, OccurredAt = startingTime.AddDays(1).AddHours(1).AddMinutes(10), UserName = "John", ToUserName = "Alice" },
                new() { Id = 59, OccurredAt = startingTime.AddDays(1).AddHours(1).AddMinutes(12), UserName = "Alice", ToUserName = "Bob" },
            });

        }
    }
}
