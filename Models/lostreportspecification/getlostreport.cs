using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pforum_frontend.Models.lostreportspecification
{
    public class getlostreport
    {

        public static List<lppost> getlostreportby(ipostspecification spec,List<lppost> lp)
        {

            List<lppost> newlp = new List<lppost>();
            foreach(lppost tmp in lp)
            {
                if(spec.issatisfied(tmp)==true)
                {
                    newlp.Add(tmp);
                }
            }
            return newlp;
        }
    }
}