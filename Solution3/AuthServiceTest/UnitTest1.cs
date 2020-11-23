using AuthService;
using AuthService.Controllers;
using AuthService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace AuthServiceTest
{
    public class Tests
    {
        List<User> user = new List<User>();
        IQueryable<User> userdata;
        Mock<DbSet<User>> mockSet;
        Mock<UserDbContext> usercontextmock;
        [SetUp]
        public void Setup()
        {
            user = new List<User>()
            {
                new User{Username="rohith",Password="lanka"}

            };
            userdata = user.AsQueryable();
            mockSet = new Mock<DbSet<User>>();
            mockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(userdata.Provider);
            mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(userdata.Expression);
            mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(userdata.ElementType);
            mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(userdata.GetEnumerator());
            var p = new DbContextOptions<UserDbContext>();
            usercontextmock = new Mock<UserDbContext>(p);
            usercontextmock.Setup(x => x.Users).Returns(mockSet.Object);



        }


        [Test]
        public void LoginTest()
        {

            Mock<IConfiguration> config = new Mock<IConfiguration>();
            config.Setup(p => p["Jwt:Key"]).Returns("ThisismySecretKey");
            var controller = new AuthController(usercontextmock.Object, config.Object);
            var login = controller.Login(new User { Username = "rohith", Password = "lanka" });


            var Actualstatuscode = (HttpStatusCode)login.GetType().GetProperty("StatusCode").GetValue(login);

            var ExpectedStatusCode = HttpStatusCode.OK;
            Assert.AreEqual(ExpectedStatusCode, Actualstatuscode);






        }
        [Test]
        public void LoginFailTest()
        {

            Mock<IConfiguration> config = new Mock<IConfiguration>();
            config.Setup(p => p["Jwt:Key"]).Returns("ThisismySecretKey");
            var controller = new AuthController(usercontextmock.Object, config.Object);
            var login = controller.Login(new User { Username = "asdjb", Password = "zsc" });
            var Actualstatuscode = (HttpStatusCode)login.GetType().GetProperty("StatusCode").GetValue(login);
            var ExpectedStatusCode = HttpStatusCode.NotFound;

            Assert.AreEqual(ExpectedStatusCode, Actualstatuscode);






        }

    }

}