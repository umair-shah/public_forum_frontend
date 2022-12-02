using System;
using System.Collections.Generic;
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
          
            return View();
        }
        //public ActionResult adduser(string username ,string email,string password, string designation)
        //{
        //    user newuser = new user();
        //    newuser.username = username;
        //    newuser.email = email;
        //    newuser.password = password;
        //    newuser.designation = designation;

        //    using (var client = new HttpClient())
        //    {
        //        client.BaseAddress = new Uri("https://localhost:44371/api/signup/");

        //        //HTTP POST
        //        var postTask = client.PostAsJsonAsync<user>("addnewuser", newuser);
        //        postTask.Wait();

        //        var result = postTask.Result;
        //        if (result.IsSuccessStatusCode)
        //        {
        //            return RedirectToAction("Index");
        //        }
        //    }
        //    return RedirectToAction("signuppage","signup");
        //}
        public ActionResult loginpage()
        {
            return View();
        }
    }
}