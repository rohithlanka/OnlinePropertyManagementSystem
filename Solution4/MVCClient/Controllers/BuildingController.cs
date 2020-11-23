using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCClient.Models;
using Newtonsoft.Json;

namespace MVCClient.Controllers
{
    public class BuildingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        
            public async Task<IActionResult> Details()
            {
            if (HttpContext.Session.GetString("token") == null)
            {
                //_log4net.Info("token not found");

                return RedirectToAction("Login");

            }
            else
            {
                //_log4net.Info("Productlist getting Displayed");

                //List<Building> ItemList = new List<Building>();
                using (var client = new HttpClient())
                {


                    var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Add(contentType);

                    client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));

                    using (var response = await client.GetAsync("http://localhost:53390/api/Building"))
                    {

                        string apiResponse = await response.Content.ReadAsStringAsync();
                        List<Building> ItemList = JsonConvert.DeserializeObject<List<Building>>(apiResponse);
                        return View(ItemList);
                    }
                }
                return RedirectToAction("Login");

            }
        }
    }
}
