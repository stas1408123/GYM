using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GYM.BLL.Models
{
    public class VisitorModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public List<CouchModel>? Couches { get; set; }
        public List<OrderModel>? Orders { get; set; }
    }
}
