using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using pforum_frontend.Models;
namespace pforum_frontend.Models
{
    public class usercomment
    {
        public user userdetail;
        public comment comments;
        public usercomment()
        {
            userdetail = new user();
            comments = new comment();
        }
    }
}