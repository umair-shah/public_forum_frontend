using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pforum_frontend.Models
{
    public class generaldiscussion
    {
        public int postid { get; set; }
        public string details { get; set; }
        public string topic { get; set; }

        public post newp;
        public generaldiscussion()
        {
            newp = new post();
        }
    }
}