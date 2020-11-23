using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCClient.Models;
using Newtonsoft.Json;

namespace MVCClient.Controllers
{
    public class AddBuildingController : Controller
    {
        public IActionResult Index()
        {
            return View();
            
        }
        public IActionResult Errormsg()
        {
            return View();

        }
        [HttpGet]
        public IActionResult GetDetails( )
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>GetDetails(Building b)
        {
            
            if (HttpContext.Session.GetString("token") == null)
            {

                return RedirectToAction("Login", "Login");

            }
            else
            {

                

                using (var client = new HttpClient())
                {
                    StringContent content1 = new StringContent(JsonConvert.SerializeObject(b), Encoding.UTF8, "application/json");
                    var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Add(contentType);

                    client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));

                    using (var response = await client.PostAsync("https://localhost:44379/api/AddBuilding",content1))
                    {

                        if (response.IsSuccessStatusCode)
                        {
                            return RedirectToAction("Index","AddBuilding");
                        }
                        else
                        {
                            return RedirectToAction("Errormsg","AddBuilding");
                        }

                        

                    }

                    




                    
                }
                return RedirectToAction("Index");
            }
        }

    }
}
