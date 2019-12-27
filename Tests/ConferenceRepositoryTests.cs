using System.Collections.Generic;
using ConferenceMonitorApi.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Linq;
using Microsoft.Data.Sqlite;

namespace ConferenceMonitorApi
{
    public class ConferenceControllerTest
    {
        static SqliteConnectionStringBuilder sCSB = new SqliteConnectionStringBuilder { DataSource = ":memory" };
        static SqliteConnection sc = new SqliteConnection(sCSB.ToString());
        static DbContextOptions<DatabaseContext> options = new DbContextOptionsBuilder<DatabaseContext>()
            .UseSqlite(sc)
            .Options;

        DatabaseContext context = new DatabaseContext(options);

        [Fact]
        public void GetAllConferences_Conferences_ConferencesAreRetrieved()
        {
            //Arrange  
            context.Database.EnsureCreated();

            context.Conferences.Add(new Conference()
            {
                Id = 1,
                Name = "Climate Change Preservation",
                Theme = "Preserving Good Climate",
                Location = "New York",
                StartDateAndTime = "2020-02-10 09:00 AM",
                EndDateAndTime = "2020-02-10 08:00 PM",
                Description = "An annual conference for climate protection",
                Organizer = "Climate Protector Inc",
                TicketPrice = 100,
                PublisherID = 1,
                PublisherEmail = "climateprotector@climateprotectorinc.com"
            });

            context.Conferences.Add(new Conference()
            {
                Id = 2,
                Name = "Employ Yourself",
                Theme = "Staring Your Own Business Matters",
                Location = "Washington",
                StartDateAndTime = "2020-03-15 08:00 AM",
                EndDateAndTime = "2020-03-22 06:00 PM",
                Description = "An annual conference for inspiring entrepreneurs",
                Organizer = "Business Founders Corp",
                TicketPrice = 500,
                PublisherID = 2,
                PublisherEmail = "businessfounders@businessfounderscorp.com"
            });

            context.SaveChangesAsync();

            ConferenceRepository cr = new ConferenceRepository(context);

            //Act  
            List<Conference> conferences = cr.FindAllAsync<Conference>().Result;

            //Assert  
            Assert.Equal(2, conferences.Count());
        }

        [Fact]
        public void GetSingleConference_Conference_ConferenceIsRetrieved()
        {
            // Arrange
            context.Database.EnsureCreated();
            
            ConferenceRepository cr = new ConferenceRepository(context);
            int Id = 1;

            // Act
            Conference conference = cr.FindByIdAsync<Conference>(Id).Result;

            // Assert
            Assert.IsType<Conference>(conference);
        }
    }
}