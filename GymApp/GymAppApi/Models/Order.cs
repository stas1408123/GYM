using System.ComponentModel.DataAnnotations;

namespace GymAppApi.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(150, MinimumLength = 1)]
        public string Title { get; set; } = null!;
        [Required]
        [StringLength(150, MinimumLength = 3)]
        public string Description { get; set; } = null!;
        [Required]
        public double Cost { get; set; }
        public DateTime Date { get; set; }

        //Navigation property
        public int VisitorId { get; set; }
        public Visitor Visitor { get; set; } = null!;

    }
}
