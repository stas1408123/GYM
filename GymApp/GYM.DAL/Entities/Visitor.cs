namespace GYM.DAL.Entities
{
    public class Visitor
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;

        public ICollection<Couch> Couches { get; set; } = null!;
        public ICollection<Order> Orders { get; set; } = null!;
    }
}
