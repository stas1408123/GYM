namespace GymAppApi.Models
{
    public class Visitor
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;


        //Navigation properties
        public List<Couch> Couches { get; set; } = null!;

        public List<Order> Orders { get; set; } = null!;
    }
}
