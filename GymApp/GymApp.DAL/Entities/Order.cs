using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymApp.DAL.Entities
{
     public class Order
    {
        public int Id { get; set; }
        public string Product { get; set; }
        public double Coust { get; set; }

        //navigation property
        public int PeopleId { get; set; }

        public People People { get; set; }

       
    }
}
