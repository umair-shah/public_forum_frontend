using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pforum_frontend.Models.generaldiscussionspecification
{
    public class gdposttopicspecification:igdpostspecification
    {
        private string _topic;
        public gdposttopicspecification(string topic)
        {
            _topic = topic;
        }
        public bool issatisfied(gdpost gd)
        {

            if (gd.topic == _topic)
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