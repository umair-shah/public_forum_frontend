using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;

using System.Web.Mvc;
using pforum_frontend.Models;

namespace pforum_frontend.Controllers
{
    static class onlineuser
    {
        public static int userid;
        public static string username;
        public static string email;
    }
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index(int userid)
        {

            onlineuser.userid = userid;
            if(userid>0)
            {
                user logined = new user();

                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri("https://localhost:44371/api/");
                    //HTTP GET
                    var responseTask = client.GetAsync("fuser/" + userid);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<user>();
                        readTask.Wait();

                        logined = readTask.Result;
                    }
                    else //web api sent error response 
                    {
                        //log response status here..

                        logined = null;

                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                    }
                }
                onlineuser.email = logined.email;
                onlineuser.username = logined.username;
                return View();
            }
            return RedirectToAction("loginpage", "signup");

        }
    }
}