using System.ComponentModel.DataAnnotations;

namespace GYM.API.Models
{
    public class VisitorViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(150, MinimumLength = 1)]
        public string FirstName { get; set; } = null!;
        [Required]
        [StringLength(150, MinimumLength = 1)]
        public string LastName { get; set; } = null!;

        //Navigation properties
        public List<CouchViewModel> Couches { get; set; } = null!;
        public List<OrderViewModel> Orders { get; set; } = null!;
    }
}
