namespace GymAppApi.Models
{
    public class Couch
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Describe  { get; set; }

        //Navigation property

        public int VisitorId { get; set; }
        public List<Visitor> Visitors { get; set; }

    }
}
