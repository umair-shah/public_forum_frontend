using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pforum_frontend.Models
{
    public interface ipostspecification
    {
        List<lppost> issatisfied(List<lppost> lp);
    }
}
