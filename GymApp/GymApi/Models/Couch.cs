using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymApi.Models
{
    public class Couch : People
    {
        public int YearExpirience { get; set; }
        public string DescriptionSkiills { get; set; }


        //navigation property
        public List<Couch> Type { get; set; }


    }
}
