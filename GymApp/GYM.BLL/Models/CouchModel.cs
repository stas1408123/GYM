namespace GYM.BLL.Models
{
    public class CouchModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public List<VisitorModel>? Visitors { get; set; }
    }
}
