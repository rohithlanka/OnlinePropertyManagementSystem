using ListService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListService.Repository
{
    public class BuildingRepository : IBuildingRepository
    {
        private readonly BuildingDbContext buildingDbContext;

        public BuildingRepository(BuildingDbContext buildingDbContext)
        {
            this.buildingDbContext = buildingDbContext;
        }
        public IEnumerable<Building> GetAll()
        {
            var buildinglist = buildingDbContext.Buildings.ToList();
            return buildinglist;
        }

        public Building GetById(int building_id)
        {
            return buildingDbContext.Buildings.FirstOrDefault(p => p.building_id == building_id);
        }

        
            
        

        
    }
}
