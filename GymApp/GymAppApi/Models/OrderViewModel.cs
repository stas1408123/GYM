using System.ComponentModel.DataAnnotations;

namespace GYM.API.Models
{
    public class OrderViewModel
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
        public VisitorViewModel Visitor { get; set; } = null!;
    }
}
