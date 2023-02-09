namespace GYM.DAL.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public double Cost { get; set; }
        public DateTime Date { get; set; }

        public int VisitorId { get; set; }
        public Visitor Visitor { get; set; } = null!;
    }
}
