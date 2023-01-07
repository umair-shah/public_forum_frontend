using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pforum_frontend.Models.lostreportspecification
{
    public class lppostitemspecification:ipostspecification
    {
        private string _lostitem;
        public lppostitemspecification(string lostitem)
        {
            _lostitem = lostitem;
        }
        public bool issatisfied(lppost lp)
        {

            if (lp.lostitem == _lostitem)
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