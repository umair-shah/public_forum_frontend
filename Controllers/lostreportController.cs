using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using pforum_frontend.Models;
using System.Net.Http;
using System.Configuration;

namespace pforum_frontend.Controllers
{

    public class lostreportController : Controller
    {
        // GET: generalpost

        [HttpGet]
        public ActionResult makelostreport()
        {
            if (Convert.ToInt32(Session["userid"]) < 1)
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
            lp.newp.userid = Convert.ToInt32(Session["userid"]);
            lp.newp.adminid = 1;
            lp.newp.typeid = 1;
            using (var client = new HttpClient())
            {
                string url = Convert.ToString(ConfigurationManager.AppSettings["apiurl"]);
                url = url + "lostreport/";
                client.BaseAddress = new Uri(url);

                //HTTP POST
                var postTask = client.PostAsJsonAsync<lostreport>("postlostreport/", lp);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("viewlp", "lostreport");
                }
            }

            return RedirectToAction("viewlp", "lostreport");
        }
        public ActionResult viewlp()
        {
            IEnumerable<lppost> lp = null;

            using (var client = new HttpClient())
            {
                string url = Convert.ToString(ConfigurationManager.AppSettings["apiurl"]);
                url = url + "lostreport/";
                client.BaseAddress = new Uri(url);
                //HTTP GET
                var responseTask = client.GetAsync("getlostreports");
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
            ViewBag.apiurl = Convert.ToString(ConfigurationManager.AppSettings["apiurl"]);

            return View(lp);
        }
        public ActionResult yourlpposts()
        {
            IEnumerable<lostreport> lp = null;

            using (var client = new HttpClient())
            {
                string url = Convert.ToString(ConfigurationManager.AppSettings["apiurl"]);
                url = url + "lostreport/";
                client.BaseAddress = new Uri(url);
                //HTTP GET
                var responseTask = client.GetAsync("getuserslostreport/" + Convert.ToInt32(Session["userid"]));
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
                string url = Convert.ToString(ConfigurationManager.AppSettings["apiurl"]);
                url = url + "comment/";
                client.BaseAddress = new Uri(url);
                //HTTP GET
                var responseTask = client.GetAsync("getcomment/" + item.postid);
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
            ViewBag.apiurl = Convert.ToString(ConfigurationManager.AppSettings["apiurl"]);

            ViewBag.post = item;
            return View(uc);
        }
        //[HttpPost]
        //public ActionResult makecomments(string com, int postid)
        //{
        //    comment newcomment = new comment();
        //    newcomment.userid = Convert.ToInt32(Session["userid"]);
        //    newcomment.postid = postid;
        //    newcomment.cmnt = com;
        //    using (var client = new HttpClient())
        //    {
        //        client.BaseAddress = new Uri("https://localhost:44371/api/comment/");

        //        //HTTP POST
        //        var postTask = client.PostAsJsonAsync<comment>("postcomment/", newcomment);
        //        postTask.Wait();
        //        var result = postTask.Result;
        //        if (result.IsSuccessStatusCode)
        //        {
        //            return RedirectToAction("viewlp", "lostreport");
        //        }
        //    }
        //    return RedirectToAction("viewlp", "lostreport");
        //}
    
    }
}