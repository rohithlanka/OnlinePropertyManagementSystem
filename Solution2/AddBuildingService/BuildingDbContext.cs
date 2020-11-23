using AddBuildingService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddBuildingService
{
    public class BuildingDbContext :DbContext
    {
        public BuildingDbContext(DbContextOptions<BuildingDbContext> options) : base(options)
        {
        }
        public virtual DbSet<Building> Buildings { get; set; }
    }
}
