using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pforum_frontend.Models.generaldiscussionspecification
{
    public class getgeneraldiscussion
    {
        public static List<gdpost> getgeneraldiscussionby(igdpostspecification spec, List<gdpost> gd)
        {

            List<gdpost> newgd = new List<gdpost>();
            foreach (gdpost tmp in gd)
            {
                if (spec.issatisfied(tmp) == true)
                {
                    newgd.Add(tmp);
                }
            }
            return newgd;
        }
    }
}