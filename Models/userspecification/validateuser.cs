using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pforum_frontend.Models.userspecification
{
    public class validateuser
    {
        public static bool validateuserby(Iuserspecification spec, user u)
        {
            if (spec.Issatisfied(u) == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}