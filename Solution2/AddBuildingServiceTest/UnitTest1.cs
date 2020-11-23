using AddBuildingService;
using AddBuildingService.Models;
using AddBuildingService.Repository;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace AddBuildingServiceTest
{
    public class Tests
    {

        List<Building> buildings = new List<Building>();
        IQueryable<Building> bookingdata;
        Mock<DbSet<Building>> mockSet;
        Mock<BuildingDbContext> buildingcontextmock;
        [SetUp]
        public void Setup()
        {
            buildings = new List<Building>()
            {
                 new Building{building_id = 1, description="shdv",address="sudv",cost=500},
                 new Building{building_id = 1, description="safhdc",address="dhscv",cost=500}

            };
            bookingdata = buildings.AsQueryable();
            mockSet = new Mock<DbSet<Building>>();
            mockSet.As<IQueryable<Building>>().Setup(m => m.Provider).Returns(bookingdata.Provider);
            mockSet.As<IQueryable<Building>>().Setup(m => m.Expression).Returns(bookingdata.Expression);
            mockSet.As<IQueryable<Building>>().Setup(m => m.ElementType).Returns(bookingdata.ElementType);
            mockSet.As<IQueryable<Building>>().Setup(m => m.GetEnumerator()).Returns(bookingdata.GetEnumerator());
            var p = new DbContextOptions<BuildingDbContext>();
            buildingcontextmock = new Mock<BuildingDbContext>(p);
            buildingcontextmock.Setup(x => x.Buildings).Returns(mockSet.Object);



        }


        




        
        [Test]
        public void AddBookingDetailTest()
        {
            var buildingrepo = new BuildingRepository(buildingcontextmock.Object);
            var buildingobj = buildingrepo.Sale(new Building { description = "ev", address = "ddhgv", cost = 7000 });
            Assert.IsNotNull(buildingobj);
        }
        
       
    }
}
