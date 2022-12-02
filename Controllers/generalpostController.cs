using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using pforum_frontend.Models;
using System.Net.Http;

namespace pforum_frontend.Controllers
{
    public class generalpostController : Controller
    {
        // GET: generalpost
        [HttpGet]
        public ActionResult generaldiscussion()
        {

            if (onlineuser.userid < 1)
            {
                return RedirectToAction("loginpage", "signup");
            }

            return View();
        }
        [HttpGet]
        public ActionResult makepost()
        {
            if (onlineuser.userid < 1)
            {
                return RedirectToAction("loginpage", "signup");
            }
            return View();
        }
        public ActionResult generaldiscussion(string topic, string detail)
        {
            generaldiscussion gd = new generaldiscussion();
            gd.details = detail;
            gd.topic = topic;
            gd.postid = 0;
            gd.newp.postid = 0;
            gd.newp.ptime = DateTime.Now;
            gd.newp.userid = onlineuser.userid;
            gd.newp.adminid = 1;
            gd.newp.typeid = 1;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44371/api/");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<generaldiscussion>("generaldiscussion", gd);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("generaldiscussion", "generalpost");
                }
            }
            return RedirectToAction("generaldiscussion", "generalpost");
        }
        public ActionResult viewgd()
        {
            IEnumerable<gdpost> gd = null;

            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("https://localhost:44371/api/");
                //HTTP GET
                var responseTask = client.GetAsync("generaldiscussion");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<gdpost>>();
                    readTask.Wait();

                    gd = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    gd = Enumerable.Empty<gdpost>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            //IEnumerable<user> users = null;
            //using (var client = new HttpClient())
            //{
            //    client.BaseAddress = new Uri("https://localhost:44371/api/signup/");
            //    //HTTP GET
            //    var responseTask = client.GetAsync("userdetails/1");
            //    responseTask.Wait();

            //    var result = responseTask.Result;
            //    if (result.IsSuccessStatusCode)
            //    {
            //        var readTask = result.Content.ReadAsAsync<IList<user>>();
            //        readTask.Wait();

            //        users = readTask.Result;
            //    }
            //    else //web api sent error response 
            //    {
            //        //log response status here..

            //        gd = Enumerable.Empty<generaldiscussion>();

            //        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
            //    }
            //}
            //List<gdpost> generalposts = new List<gdpost>();
            //gdpost temp = null;
            //foreach (var item in gd)
            //{
            //    temp = new gdpost();
            //    temp.postid = item.postid;
            //    temp.ptime = item.newp.ptime;
            //    temp.topic = item.topic;
            //    temp.details = item.details;
            //    foreach (var u in users)
            //    {
            //        if (u.userid == item.newp.userid)
            //        {
            //            temp.designation = u.designation;
            //            temp.email = u.email;
            //            temp.username = u.username;
            //            break;
            //        }
            //    }
            //    generalposts.Add(temp);

            //    temp = null;
            //}
            return View(gd);
        }
        public ActionResult yourgdposts()
        {
            IEnumerable<generaldiscussion> gd = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44371/api/");
                //HTTP GET
                var responseTask = client.GetAsync("generaldiscussion/" + onlineuser.userid);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<generaldiscussion>>();
                    readTask.Wait();
                    gd = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    gd = Enumerable.Empty<generaldiscussion>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            List<gdpost> generalposts = new List<gdpost>();
            gdpost temp = null;
            foreach (var item in gd)
            {
                temp = new gdpost();
                temp.postid = item.postid;
                temp.ptime = item.newp.ptime;
                temp.topic = item.topic;
                temp.details = item.details;
                generalposts.Add(temp);
                temp = null;
            }
            return View(generalposts);
        }
        // GET: comment
        public ActionResult comments(gdpost item)
        {
            IEnumerable<usercomment> uc = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44371/api/");
                //HTTP GET
                var responseTask = client.GetAsync("comment/" + item.postid);
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<usercomment>>();
                    readTask.Wait();
                    uc = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    uc = Enumerable.Empty<usercomment>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            ViewBag.post = item;
            return View(uc);
        }
        [HttpPost]
        public ActionResult makecomments(string com, int postid)
        {
            comment newcomment = new comment();
            newcomment.userid = onlineuser.userid;
            newcomment.postid = postid;
            newcomment.cmnt = com;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44371/api/");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<comment>("comment", newcomment);
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("viewgd", "generalpost");
                }
            }
            return RedirectToAction("viewgd", "generalpost");
        }
    }

}
