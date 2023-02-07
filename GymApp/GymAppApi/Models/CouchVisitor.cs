namespace GymAppApi.Models
{
    public class CouchVisitor
    {
        public int CouchId { get; set; }
        public Couch  Couch { get; set; }
        public int VisitorId { get; set; }
        public Visitor visitor { get; set; }
    }
}
