using Xunit;
using ConferenceMonitorApi.Controllers;

namespace ConferenceMonitorApi
{       
    public class ConferencesTest {
        [Fact]
        public void PassingAnyResponse()
        {
        //Arrange
        var conferenceController = new ConferencesController();
        
        //Act
        var response = conferenceController.GetConferences();
        
        //Assert
        Assert.NotNull(response);
        }
    }
}