namespace GymAppApi.Models
{
    public class Couch
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Describe { get; set; } = null!;

        //Navigation property
        public int VisitorId { get; set; }
        public List<Visitor> Visitors { get; set; } = null!;

    }
}
