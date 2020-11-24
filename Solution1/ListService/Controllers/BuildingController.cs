using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using ListService.Models;
using ListService.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ListService.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BuildingController : ControllerBase
    {
        private readonly IBuildingRepository _buildingRepository;
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(BuildingController));
        public BuildingController(IBuildingRepository buildingRepository)
        {
            _buildingRepository = buildingRepository;
        }
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                _log4net.Info(" Http GET is accesed");

                IEnumerable<Building> buildinglist = _buildingRepository.GetAll();
                _log4net.Info("list is retrieved");
                return Ok(buildinglist);
            }
            catch
            {
                _log4net.Error("Error in Get request");
                return new NoContentResult();
            }
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                _log4net.Info(" Http GETbyid is accesed");
                var buildinglist = _buildingRepository.GetById(id);
                _log4net.Info(buildinglist.building_id+" building is shown");
                return new OkObjectResult(buildinglist);
            }
            catch
            {
                _log4net.Error("Error in Getbyid request");
                return new NoContentResult();
            }
        }
        
    }
}
