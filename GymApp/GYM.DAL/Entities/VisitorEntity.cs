namespace GYM.DAL.Entities
{
    public class VisitorEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;

        public ICollection<CouchEntity> Couches { get; set; } = null!;
        public ICollection<OrderEntity> Orders { get; set; } = null!;
    }
}
