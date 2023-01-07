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
        public List< lppost> issatisfied(List< lppost> lp)
        {
            List<lppost> listlp = new List<lppost>();
            foreach(lppost lostpost in lp)
            {
                if(lostpost.designation==_designation)
                {
                    listlp.Add(lostpost);
                }
            }
            return listlp;
        }
    }
}