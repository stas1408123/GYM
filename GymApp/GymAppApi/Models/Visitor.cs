using System.ComponentModel.DataAnnotations;

namespace GymAppApi.Models
{
    public class Visitor
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
        public List<Couch> Couches { get; set; } = null!;

        public List<Order> Orders { get; set; } = null!;
    }
}
