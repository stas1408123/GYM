namespace GYM.API.Models
{
    public class CouchViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public List<VisitorViewModel> Visitors { get; set; } = null!;
    }
}
