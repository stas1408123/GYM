namespace GYM.API.Models
{
    public class VisitorViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public List<CouchViewModel> Couches { get; set; } = null!;
        public List<OrderViewModel> Orders { get; set; } = null!;
    }
}
