namespace GYM.API.Models
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public double Cost { get; set; }
        public DateTime Date { get; set; }
        public int VisitorId { get; set; }
    }
}
