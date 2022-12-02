using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using pforum_frontend.Models;
using System.Net.Http;
namespace pforum_frontend.Controllers
{
    public class lostreportController : Controller
    {
        // GET: generalpost
        [HttpGet]
        public ActionResult lostreports()
        {
            if (onlineuser.userid < 1)
            {
                return RedirectToAction("loginpage", "signup");
            }

            return View();
        }
        [HttpGet]
        public ActionResult makelostreport()
        {
            if (onlineuser.userid < 1)
            {
                return RedirectToAction("loginpage", "signup");
            }
            return View();
        }
        public ActionResult lostreports(string lostitem, string details)
        {
            lostreport lp = new lostreport();
            lp.details = details;
            lp.lostitem = lostitem;
            lp.postid = 0;
            lp.newp.postid = 0;
            lp.newp.ptime = DateTime.Now;
            lp.newp.userid = onlineuser.userid;
            lp.newp.adminid = 1;
            lp.newp.typeid = 1;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44371/api/");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<lostreport>("lostreport", lp);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("lostreports", "lostreport");
                }
            }

            return RedirectToAction("lostreports", "lostreport");
        }
        public ActionResult viewlp()
        {
            IEnumerable<lppost> lp = null;

            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("https://localhost:44371/api/");
                //HTTP GET
                var responseTask = client.GetAsync("lostreport");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<lppost>>();
                    readTask.Wait();
                    lp = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    lp = Enumerable.Empty<lppost>();

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

            //        users = Enumerable.Empty<user>();

            //        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
            //    }
            //}
            //List<lppost> lostreportposts = new List<lppost>();
            //lppost temp = null;
            //foreach (var item in lp)
            //{
            //    temp = new lppost();
            //    temp.postid = item.postid;
            //    temp.ptime = item.newp.ptime;
            //    temp.lostitem = item.lostitem;
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
            //    lostreportposts.Add(temp);

            //    temp = null;
            //}
            return View(lp);
        }
        public ActionResult yourlpposts()
        {
            IEnumerable<lostreport> lp = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44371/api/");
                //HTTP GET
                var responseTask = client.GetAsync("lostreport/" + onlineuser.userid);
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<lostreport>>();
                    readTask.Wait();
                    lp = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    lp = Enumerable.Empty<lostreport>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            List<lppost> lostreportposts = new List<lppost>();
            lppost temp = null;
            foreach (var item in lp)
            {
                temp = new lppost();
                temp.postid = item.postid;
                temp.ptime = item.newp.ptime;
                temp.lostitem = item.lostitem;
                temp.details = item.details;
                lostreportposts.Add(temp);
                temp = null;
            }
            return View(lostreportposts);
        }
        public ActionResult comments(lppost item)
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
                    return RedirectToAction("viewlp", "lostreport");
                }
            }
            return RedirectToAction("viewlp", "lostreport");
        }
    }
}