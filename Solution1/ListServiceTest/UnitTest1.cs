using ListService;
using ListService.Models;
using ListService.Repository;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace ListServiceTest
{
    public class Tests
    {

        List<Building> building = new List<Building>();
        IQueryable<Building> buildingdata;
        Mock<DbSet<Building>> mockSet;
        Mock<BuildingDbContext> buildingcontextmock;
        [SetUp]
        public void Setup()
        {
            building = new List<Building>()
            {
                new Building{building_id = 1, description="1BHK", address="sdv",cost=100},
                  new Building{building_id = 2, description="2BHK", address="sdjv",cost=100},
                    new Building{building_id = 3, description="3BHK", address="djvs",cost=100},
                      new Building{building_id = 4, description="1BHK", address="jdgv",cost=100},

            };
            buildingdata = building.AsQueryable();
            mockSet = new Mock<DbSet<Building>>();
            mockSet.As<IQueryable<Building>>().Setup(m => m.Provider).Returns(buildingdata.Provider);
            mockSet.As<IQueryable<Building>>().Setup(m => m.Expression).Returns(buildingdata.Expression);
            mockSet.As<IQueryable<Building>>().Setup(m => m.ElementType).Returns(buildingdata.ElementType);
            mockSet.As<IQueryable<Building>>().Setup(m => m.GetEnumerator()).Returns(buildingdata.GetEnumerator());
            var p = new DbContextOptions<BuildingDbContext>();
            buildingcontextmock = new Mock<BuildingDbContext>(p);
            buildingcontextmock.Setup(x => x.Buildings).Returns(mockSet.Object);



        }


        [Test]
        public void GetAllTest()
        {
            var buildingrepo = new BuildingRepository(buildingcontextmock.Object);
            var buildinglist = buildingrepo.GetAll();
            Assert.AreEqual(4, buildinglist.Count());




        }
        [Test]
        public void GetByIdTest()
        {
            var buildingrepo = new BuildingRepository(buildingcontextmock.Object);
            var buildingobj = buildingrepo.GetById(4);
            Assert.IsNotNull(buildingobj);
        }
        [Test]
        public void GetByIdTestFail()
        {
            var buildingrepo = new BuildingRepository(buildingcontextmock.Object);
            var buildingobj = buildingrepo.GetById(96);
            Assert.IsNull(buildingobj);
        }

    }


}