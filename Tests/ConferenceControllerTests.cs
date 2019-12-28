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
        static SqliteConnectionStringBuilder sCSB = new SqliteConnectionStringBuilder { DataSource = ":memory" };
        static SqliteConnection sc = new SqliteConnection(sCSB.ToString());
        static DbContextOptions<DatabaseContext> options = new DbContextOptionsBuilder<DatabaseContext>()
            .UseSqlite(sc)
            .Options;

        DatabaseContext context = new DatabaseContext(options);

        IConferenceRepository _conferenceRepository;
        ConferenceController _conferenceController;

        public ConferenceControllerTest()
        {
            context.Database.OpenConnection();
            context.Database.EnsureCreated();
        }

        [Fact]
        public async void PublishConference_ValidConferencePassed_ReturnsCreatedAtResult()
        {
            // Arrange
            _conferenceRepository = new ConferenceRepository(context);
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
            ActionResult<Conference> conferenceCreatedAtActionResponse = await _conferenceController.PublishConference(newConference);

            // Assert
            Assert.IsType<CreatedAtActionResult>(conferenceCreatedAtActionResponse.Result);
        }

        [Fact]
        public async void PublishConference_InvalidConferencePassed_ReturnsBadRequestObjectResult()
        {
            // Arrange
            _conferenceRepository = new ConferenceRepository(context);
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
            ActionResult<Conference> conferenceBadRequestResponse = await _conferenceController.PublishConference(newConference);

            // Assert
            Assert.IsType<BadRequestObjectResult>(conferenceBadRequestResponse.Result);
        }

        [Fact]
        public void GetAllConferences_WhenCalled_ReturnsOkResult()
        {
            // Arrange

            _conferenceRepository = new ConferenceRepository(context);
            _conferenceController = new ConferenceController(_conferenceRepository);

            // Act
            ActionResult<Conference> conferencesOkResult = _conferenceController.GetAllConferences().Result;

            // Assert
            Assert.IsType<OkObjectResult>(conferencesOkResult.Result);
        }

        [Fact]
        public void GetSingleConference_ExistingConferenceIdPassed_ReturnsOkResult()
        {
            // Arrange
            _conferenceRepository = new ConferenceRepository(context);
            _conferenceController = new ConferenceController(_conferenceRepository);

            int Id = 2;

            // Act
            ActionResult<Conference> conferenceOkResult = _conferenceController.GetSingleConference(Id).Result;

            // Assert
            Assert.IsType<OkObjectResult>(conferenceOkResult.Result);
        }

        [Fact]
        public void GetSingleConference_UnkownConferenceIdPassed_ReturnsNotFoundResult()
        {
            // Arrange
            _conferenceRepository = new ConferenceRepository(context);
            _conferenceController = new ConferenceController(_conferenceRepository);

            int Id = 33;

            // Act
            ActionResult<Conference> conferenceNotFoundResult = _conferenceController.GetSingleConference(Id).Result;

            // Assert
            Assert.IsType<NotFoundObjectResult>(conferenceNotFoundResult.Result);
        }

        // [Fact]
        // public void Add_ValidObjectPassed_ReturnedResponseHasCreatedItem()
        // {
        //     // Arrange
        //     _conferenceRepository = new ConferenceRepository(context);
        //     _conferenceController = new ConferenceController(_conferenceRepository);

        //     ClaimsPrincipal cp = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        //     {
        //         new Claim("UserID", "1"),
        //         new Claim("UserEmail", "publisher@email.com")
        //     }));


        //     _conferenceController.ControllerContext = new ControllerContext()
        //     {
        //         HttpContext = new DefaultHttpContext() { User = cp }
        //     };

        //     Conference newConference = new Conference()
        //     {
        //         Name = "Employ Yourself",
        //         Theme = "Staring Your Own Business Matters",
        //         Location = "Washington",
        //         StartDateAndTime = "2020-03-15 08:00 AM",
        //         EndDateAndTime = "2020-03-22 06:00 PM",
        //         Description = "An annual conference for inspiring entrepreneurs",
        //         Organizer = "Business Founders Corp",
        //         TicketPrice = 500
        //     };

        //     // Act
        //     var createdResponse = _conferenceController.PublishConference(newConference).Result;
        //     var publishedConference = createdResponse.Value as Conference;

        //     // Assert
        //     Assert.IsType<Conference>(publishedConference);
        //     Assert.Equal("Guinness Original 6 Pack", publishedConference.Name);
        // }

        // [Fact]
        // public void GetSingleConference_ExistingConferenceIdPassed_ReturnsRightConference()
        // {
        //     // Arrange
        //     _conferenceRepository = new ConferenceRepository(context);
        //     _conferenceController = new ConferenceController(_conferenceRepository);

        //     int Id = 2;

        //     // Act
        //     ActionResult<Conference> conferenceOkResult = _conferenceController.GetSingleConference(Id).Result;

        //     // Assert
        //     Assert.IsType<Conference>(conferenceOkResult.Value);
        //     // Assert.Equal(Id, (conferenceOkResult.Value as Conference).Id);
        // }
    }
}