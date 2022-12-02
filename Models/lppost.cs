using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pforum_frontend.Models
{
    public class lppost
    {
        public int postid { get; set; }
        public DateTime ptime { get; set; }
        public string details { get; set; }
        public string lostitem { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string designation { get; set; }
    }
}