using Xunit;
using ConferenceMonitorApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using ConferenceMonitorApi.Controllers;
using ConferenceMonitorApi.Data;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace ConferenceMonitorApi
{
    public class ConferenceControllerTest
    {
        // Define a SQLite InMemory database for testing purpose
        static SqliteConnectionStringBuilder sCSB = new SqliteConnectionStringBuilder { DataSource = ":memory" };
        static SqliteConnection sc = new SqliteConnection(sCSB.ToString());

        // Pass the database to the custom context 
        static DbContextOptions<DatabaseContext> options = new DbContextOptionsBuilder<DatabaseContext>()
            .UseSqlite(sc)
            .Options;
        DatabaseContext _context = new DatabaseContext(options);
        
        // Define fields for accessing the Conference Repository and Controller
        IConferenceRepository _conferenceRepository;
        ConferenceController _conferenceController;
        
        // Individual test case
        [Fact]
        public async void PublishConference_ValidConferencePassed_ReturnsCreatedAtActionResult()
        {      
            // Implement the Triple A testing pattern

            // Arrange

            // Create the database 
            await _context.Database.EnsureCreatedAsync();

            _conferenceRepository = new ConferenceRepository(_context);
            _conferenceController = new ConferenceController(_conferenceRepository);

            ClaimsPrincipal cp = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim("UserID", "1"),
                new Claim("UserEmail", "publisher@email.com")
            }));


            _conferenceController.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = cp }
            };

            Conference newConference = new Conference()
            {
                Name = "Employ Yourself",
                Theme = "Staring Your Own Business Matters",
                Location = "Washington",
                StartDateAndTime = "2020-03-15 08:00 AM",
                EndDateAndTime = "2020-03-22 06:00 PM",
                Description = "An annual conference for inspiring entrepreneurs",
                Organizer = "Business Founders Corp",
                TicketPrice = 500
            };

            // Act
            ActionResult<Conference> createdAtActionResponse = await _conferenceController.PublishConference(newConference);

            // Assert
            Assert.IsType<CreatedAtActionResult>(createdAtActionResponse.Result);

            // Destroy the database after testing the specific case
            await _context.Database.EnsureDeletedAsync();
        }

        [Fact]
        public async void PublishConference_InvalidConferencePassed_ReturnsBadRequestObjectResult()
        {
            // Arrange
            _conferenceRepository = new ConferenceRepository(_context);
            _conferenceController = new ConferenceController(_conferenceRepository);

            ClaimsPrincipal cp = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim("UserID", "1"),
                new Claim("UserEmail", "publisher@email.com")
            }));


            _conferenceController.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = cp }
            };

            Conference newConference = new Conference()
            {
                Theme = "Staring Your Own Business Matters",
                Location = "Washington",
                StartDateAndTime = "2020-03-15 08:00 AM",
                EndDateAndTime = "2020-03-22 06:00 PM",
                Description = "An annual conference for inspiring entrepreneurs",
                Organizer = "Business Founders Corp",
                TicketPrice = 500
            };

            _conferenceController.ModelState.AddModelError("Name", "Required");

            // Act
            ActionResult<Conference> badRequestObjectResponse = await _conferenceController.PublishConference(newConference);

            // Assert
            Assert.IsType<BadRequestObjectResult>(badRequestObjectResponse.Result);
        }

        [Fact]
        public async void GetAllConferences_WhenCalled_ReturnsOkObjectResult()
        {
            // Arrange
            await _context.Database.EnsureCreatedAsync();

            _conferenceRepository = new ConferenceRepository(_context);
            _conferenceController = new ConferenceController(_conferenceRepository);

            Conference newConference = new Conference()
            {
                Name = "Employ Yourself",
                Theme = "Staring Your Own Business Matters",
                Location = "Washington",
                StartDateAndTime = "2020-03-15 08:00 AM",
                EndDateAndTime = "2020-03-22 06:00 PM",
                Description = "An annual conference for inspiring entrepreneurs",
                Organizer = "Business Founders Corp",
                TicketPrice = 500
            };

            await _conferenceRepository.CreateAsync(newConference);

            // Act
            ActionResult<Conference> okObjectResponse = _conferenceController.GetAllConferences().Result;

            // Assert
            Assert.IsType<OkObjectResult>(okObjectResponse.Result);

            await _context.Database.EnsureDeletedAsync();
        }

        [Fact]
        public async void GetSingleConference_ExistingConferenceIdPassed_ReturnsOkObjectResult()
        {
            // Arrange
            await _context.Database.EnsureCreatedAsync();

            _conferenceRepository = new ConferenceRepository(_context);
            _conferenceController = new ConferenceController(_conferenceRepository);

            Conference newConference = new Conference()
            {
                Name = "Employ Yourself",
                Theme = "Staring Your Own Business Matters",
                Location = "Washington",
                StartDateAndTime = "2020-03-15 08:00 AM",
                EndDateAndTime = "2020-03-22 06:00 PM",
                Description = "An annual conference for inspiring entrepreneurs",
                Organizer = "Business Founders Corp",
                TicketPrice = 500
            };

            await _conferenceRepository.CreateAsync(newConference);

            int Id = 1;

            // Act
            ActionResult<Conference> okObjectResponse = _conferenceController.GetSingleConference(Id).Result;

            // Assert
            Assert.IsType<OkObjectResult>(okObjectResponse.Result);

            await _context.Database.EnsureDeletedAsync();
        }

        [Fact]
        public async void GetSingleConference_UnkownConferenceIdPassed_ReturnsNotFoundObjectResult()
        {
            // Arrange
            await _context.Database.EnsureCreatedAsync();

            _conferenceRepository = new ConferenceRepository(_context);
            _conferenceController = new ConferenceController(_conferenceRepository);

            int Id = 1;

            // Act
            ActionResult<Conference> notFoundObjectResponse = _conferenceController.GetSingleConference(Id).Result;

            // Assert
            Assert.IsType<NotFoundObjectResult>(notFoundObjectResponse.Result);

            await _context.Database.EnsureDeletedAsync();
        }

        [Fact]
        public async void GetSingleConference_ExistingConferenceIdPassed_ReturnsRightConference()
        {
            // Arrange
            await _context.Database.EnsureCreatedAsync();

            _conferenceRepository = new ConferenceRepository(_context);
            _conferenceController = new ConferenceController(_conferenceRepository);

            Conference newConference = new Conference()
            {
                Name = "Employ Yourself",
                Theme = "Staring Your Own Business Matters",
                Location = "Washington",
                StartDateAndTime = "2020-03-15 08:00 AM",
                EndDateAndTime = "2020-03-22 06:00 PM",
                Description = "An annual conference for inspiring entrepreneurs",
                Organizer = "Business Founders Corp",
                TicketPrice = 500
            };

            await _conferenceRepository.CreateAsync(newConference);

            int Id = 1;

            // Act
            ActionResult<Conference> okObjectResponse = await _conferenceController.GetSingleConference(Id);
            OkObjectResult conference = okObjectResponse.Result as OkObjectResult;

            // Assert
            Assert.IsType<Conference>(conference.Value);
            Assert.Equal(Id, (conference.Value as Conference).Id);

            await _context.Database.EnsureDeletedAsync();
        }
    }
}