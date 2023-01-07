using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;

using System.Web.Mvc;
using pforum_frontend.Models;
using System.Configuration;

namespace pforum_frontend.Controllers
{
    public class validateuser
    {
        public static bool validateuserby(Iuserspecification spec, user u)
        {
            if (spec.Issatisfied(u) == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index(int userid)
        {

            if(userid>0)
            {
                user logined = new user();
                using (var client = new HttpClient())
                {
                    string url = Convert.ToString(ConfigurationManager.AppSettings["apiurl"]);
                    url = url + "fuser/";
                    client.BaseAddress = new Uri(url);
                    //HTTP GET
                    var responseTask = client.GetAsync("getuser/" + userid);
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
                Session["userid"] = logined.userid;
                Session["username"] = logined.username;
                Session["email"] = logined.email;
                if(validateuser.validateuserby(new useridspecification(logined.userid), logined)==true)
                {
                    return RedirectToAction("homepage", "Home");
                }
            }
            return RedirectToAction("loginpage", "signup");
        }
        public ActionResult homepage()
        {
            if(Session["userid"]== null)
            {
                return RedirectToAction("loginpage", "signup");
            }
            return View();
        }
    }
}