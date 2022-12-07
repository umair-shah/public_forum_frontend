using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using pforum_frontend.Models;

namespace pforum_frontend.Controllers
{
    public class signupController : Controller
    {
        // GET: signup
        public ActionResult signuppage()
        {
            ViewBag.apiurl = Convert.ToString(ConfigurationManager.AppSettings["apiurl"]);
            return View();
        }
        public ActionResult loginpage()
        {
            Session["userid"] = null;
            Session["email"] = null;
            Session["username"] = null;
            ViewBag.apiurl = Convert.ToString(ConfigurationManager.AppSettings["apiurl"]);

            return View();
        }
    }
}