namespace GYM.BlazorApp.Data.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Cost { get; set; }
        public DateTime Date { get; set; }
        public int VisitorId { get; set; }
    }
}
