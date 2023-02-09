namespace GYM.DAL.Entities
{
    public class Couch
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Describe { get; set; } = null!;

        public ICollection<Visitor> Visitors { get; set; } = null!;
    }
}
