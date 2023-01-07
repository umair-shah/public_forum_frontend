using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pforum_frontend.Models
{
    public class lppostdesignationspecification: ipostspecification
    {
        private string _designation;
        public lppostdesignationspecification(string designation)
        {
            _designation = designation;
        }
        public bool issatisfied(lppost lp)
        {
           
            if(lp.designation==_designation)
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