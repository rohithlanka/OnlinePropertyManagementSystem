using AddBuildingService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddBuildingService.Repository
{
    public class BuildingRepository : IBuildingRepository
    {
        private readonly BuildingDbContext buildingDbContext;
        public BuildingRepository(BuildingDbContext buildingDbContext)
        {
            this.buildingDbContext = buildingDbContext;
        }
        public Building Sale(Building building)
        {
            var result = buildingDbContext.Buildings.Add(building);

            buildingDbContext.SaveChanges();
            return building;
        }
    }
}
