using AddBuildingService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddBuildingService.Repository
{
    public interface IBuildingRepository
    {
        Building Sale(Building building);
    }
}
