using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pforum_frontend.Models.generaldiscussionspecification
{
    public class gdpostdesignationspecification:igdpostspecification
    {
        private string _designation;
        public gdpostdesignationspecification(string designation)
        {
            _designation = designation;
        }
        public bool issatisfied(gdpost gd)
        {

            if (gd.designation == _designation)
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