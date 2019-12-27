using System.Collections.Generic;
using ConferenceMonitorApi.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Linq;
using Microsoft.Data.Sqlite;

namespace ConferenceMonitorApi
{
    public class UserRepositoryTest
    {
        static SqliteConnectionStringBuilder sCSB = new SqliteConnectionStringBuilder { DataSource = ":memory" };
        static SqliteConnection sc = new SqliteConnection(sCSB.ToString());
        static DbContextOptions<DatabaseContext> options = new DbContextOptionsBuilder<DatabaseContext>()
            .UseSqlite(sc)
            .Options;

        DatabaseContext context = new DatabaseContext(options);

        [Fact]
        public void GetAllUsers_Users_UsersAreRetrieved()
        {
            //Arrange   
            context.Database.EnsureCreated();

            context.Users.Add(new User()
            {
                Id = 1,
                FirstName = "someone",
                LastName = "someone",
                Email = "publisher@email.com",
                Password = "$20T201w",
                ConfirmPassword = "$20T201w",
                Role = "standard",
                Registered = "Friday 27 December 21:12 50"
            });

            context.Users.Add(new User()
            {
                Id = 2,
                FirstName = "someoneagain",
                LastName = "someoneagain",
                Email = "publisher2@email.com",
                Password = "$20T201w",
                ConfirmPassword = "$20T201w",
                Role = "standard",
                Registered = "Friday 27 December 22:31 50"
            });

            context.SaveChangesAsync();

            UserRepository ur = new UserRepository(context);

            //Act  
            List<User> users = ur.FindAllAsync<User>().Result;

            //Assert  
            Assert.Equal(2, users.Count());
        }
    }
}