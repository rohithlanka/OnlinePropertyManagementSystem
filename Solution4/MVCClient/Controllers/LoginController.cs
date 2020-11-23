using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCClient.Models;
using Newtonsoft.Json;

namespace MVCClient.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(User user)
        {
            //_log4net.Info("User Login");
            User Item = new User();
            using (var httpClient = new HttpClient())
            {
                //StringContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                //var response = await httpClient.PostAsync("https://localhost:44346/api/Auth/User", content);
                //string apiResponse = await response.Content.ReadAsStringAsync();
                //Item = JsonConvert.DeserializeObject<User>(apiResponse);
               
                
                StringContent content1 = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                using (var response1 = await httpClient.PostAsync("https://localhost:44346/api/Auth/Login", content1))
                {
                    if (!response1.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Login");
                    }

                    string apiResponse1 = await response1.Content.ReadAsStringAsync();
                    



                   // string stringJWT = response1.Content.ReadAsStringAsync().Result;


                    JWT jwt = JsonConvert.DeserializeObject<JWT>(apiResponse1);
                    if (jwt == null)
                    {
                        return RedirectToAction("Login");
                    }

                    HttpContext.Session.SetString("token", jwt.Token);
                    HttpContext.Session.SetString("user", JsonConvert.SerializeObject(user));
                    //HttpContext.Session.SetInt32("Userid", user.Uid);
                    HttpContext.Session.SetString("Username", user.Username);
                    ViewBag.Message = "User logged in successfully!";

                    return RedirectToAction("Index", "Building");
                }

            }
        }

        public ActionResult Logout()
        {
            //_log4net.Info("User Log Out");
            HttpContext.Session.Clear();
            // HttpContext.Session.SetString("user", null);

            return View("Login");
        }
    
    }
}
