namespace GymAppApi.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Coust { get; set; }

        //Navigation property
        public int VisitorId { get; set; }
        public Visitor Visitor { get; set; }

    }
}
