using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore;

namespace IRFestival.Api.Domain
{
    public class FestivalDbContext : DbContext
    {

        public FestivalDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Artist> Artists { get; set; }
        public DbSet<Stage> Stages { get; set; }
        public DbSet<Festival> Festivals { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<ScheduleItem> ScheduleItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Festival>().HasData(new Festival
            {
                Id = 1
            });

            var description = "A music festival is oriented towards music that is sometimes presented with a theme such as musical genre, nationality or locality of musicians, or holiday. They are commonly held outdoors, and are often inclusive of other attractions such as food and merchandise vending machines, performance art, and social activities. Large music festivals such as Lollapalooza are constructed around well known main stage acts and lesser known musicians and bands on side stages. Many festivals are annual, or repeat at some other interval, and have modular staging of many types. Each year Lollapalooza often features multiple acts on its main and side stages.";

            var stages = new List<Stage>() {
                         new Stage { Id = 1, FestivalId = 1, Name = "Main Stage", Description = description },
                         new Stage { Id = 2, FestivalId = 1, Name = "Orange Room", Description = description },
                         new Stage { Id = 3, FestivalId = 1, Name = "StarDust", Description = description },
                         new Stage { Id = 4, FestivalId = 1, Name = "Blue Room", Description = description },
                         new Stage { Id = 5, FestivalId = 1, Name = "VIP Room", Description = description },
            };

            modelBuilder.Entity<Stage>().HasData(stages);

            var artists = new List<Artist>() {
                      new Artist { Id = 1, FestivalId = 1, Name = "Diana Ross", ImagePath = "dianaross.jpg", Website = new Uri("http://www.dianaross.co.uk/indexb.html") },
                      new Artist { Id = 2, FestivalId = 1, Name = "The Commodores", ImagePath = "thecommodores.jpg", Website = new Uri("http://en.wikipedia.org/wiki/Commodores") },
                      new Artist { Id = 3, FestivalId = 1, Name = "Stevie Wonder", ImagePath = "steviewonder.jpg", Website = new Uri("http://www.steviewonder.net/") },
                      new Artist { Id = 4, FestivalId = 1, Name = "Lionel Richie", ImagePath = "lionelrichie.jpg", Website = new Uri("http://lionelrichie.com/") },
                      new Artist { Id = 5, FestivalId = 1, Name = "Marvin Gaye", ImagePath = "marvingaye.jpg", Website = new Uri("http://www.marvingayepage.net/") }
            };

            modelBuilder.Entity<Artist>().HasData(artists);

            var schedule = new Schedule()
            {
                Id = 1,
                FestivalId = 1
            };

            modelBuilder.Entity<Schedule>().HasData(schedule);


            modelBuilder.Entity<ScheduleItem>().HasData(new ScheduleItem { Id = 1, ArtistId = artists[0].Id, StageId = stages[0].Id, Time = new DateTime(1972, 07, 01, 20, 0, 0) });
            modelBuilder.Entity<ScheduleItem>().HasData(new ScheduleItem { Id = 2, ArtistId = artists[4].Id, StageId = stages[3].Id, Time = new DateTime(1972, 07, 01, 20, 30, 0) });
            modelBuilder.Entity<ScheduleItem>().HasData(new ScheduleItem { Id = 3, ArtistId = artists[2].Id, StageId = stages[0].Id, Time = new DateTime(1972, 07, 01, 22, 0, 0) });
            modelBuilder.Entity<ScheduleItem>().HasData(new ScheduleItem { Id = 4, ArtistId = artists[1].Id, StageId = stages[1].Id, Time = new DateTime(1972, 07, 01, 22, 15, 0) });
            modelBuilder.Entity<ScheduleItem>().HasData(new ScheduleItem { Id = 5, ArtistId = artists[0].Id, StageId = stages[4].Id, Time = new DateTime(1972, 07, 02, 20, 15, 0) });
            modelBuilder.Entity<ScheduleItem>().HasData(new ScheduleItem { Id = 6, ArtistId = artists[4].Id, StageId = stages[3].Id, Time = new DateTime(1972, 07, 02, 20, 45, 0) });
            modelBuilder.Entity<ScheduleItem>().HasData(new ScheduleItem { Id = 7, ArtistId = artists[3].Id, StageId = stages[4].Id, Time = new DateTime(1972, 07, 02, 22, 0, 0) });
            modelBuilder.Entity<ScheduleItem>().HasData(new ScheduleItem { Id = 8, ArtistId = artists[1].Id, StageId = stages[1].Id, Time = new DateTime(1972, 07, 02, 22, 30, 0) });

        }
    }
}
