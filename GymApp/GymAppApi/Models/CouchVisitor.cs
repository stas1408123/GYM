namespace GymAppApi.Models
{
    public class CouchVisitor
    {
        public int Id { get; set; }

        //Navigation properties
        public int CouchId { get; set; }
        public Couch Couch { get; set; } = null!;

        public int VisitorId { get; set; }
        public Visitor Visitor { get; set; } = null!;
    }
}
