using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AddBuildingService.Models;
using AddBuildingService.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AddBuildingService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddBuildingController : ControllerBase
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(AddBuildingController));
        private readonly IBuildingRepository _buildingRepository;
        public AddBuildingController(IBuildingRepository buildingRepository)
        {
            _buildingRepository = buildingRepository;
        }
        [HttpPost]
        public IActionResult Post([FromBody] Building building)
        {
            try
            {
                _log4net.Info("Building Details Getting Added");
                if (ModelState.IsValid)
                {

                    var buildobj = _buildingRepository.Sale(building);
                    _log4net.Info("added " + buildobj.description + " details");

                    return CreatedAtAction(nameof(Post), new { id = building.building_id }, building);
                    //string name = buildobj.description;
                    //_log4net.Info("added"+buildobj.description+" details");

                }
                return BadRequest();


            }
            catch(Exception ex)
            {
                _log4net.Error("Error in Adding Booking Details",ex);
                return new NoContentResult();
            }

        }
    }
}
