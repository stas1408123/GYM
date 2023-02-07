using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymApi.Models
{
    public class Visitor : People
    {
        public int AttendedYears { get; set; }

        //navigation property
        public int OrderId { get; set; }
        public List<Order> Orders { get; set; }

        public List<Couch> Couches { get; set; }

    }
}
