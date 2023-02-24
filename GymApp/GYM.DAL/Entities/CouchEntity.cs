namespace GYM.DAL.Entities
{
    public class CouchEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Description { get; set; } = null!;

        public ICollection<VisitorEntity> Visitors { get; set; } = null!;
    }
}
