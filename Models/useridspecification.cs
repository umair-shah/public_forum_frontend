using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pforum_frontend.Models
{
    public class useridspecification:Iuserspecification
    {
        int userid;
        public useridspecification(int id)
        {
            userid = id;
        }
       public bool Issatisfied(user u)
        {
            return (u.userid == userid) ? true : false ;
        }
    }
}