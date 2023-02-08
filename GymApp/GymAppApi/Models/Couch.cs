using System.ComponentModel.DataAnnotations;

namespace GymAppApi.Models
{
    public class Couch
    {
        public int Id { get; set; }
        [Required]
        [StringLength(150, MinimumLength = 1)]
        public string FirstName { get; set; } = null!;
        [Required]
        [StringLength(150, MinimumLength = 1)]
        public string LastName { get; set; } = null!;
        [Required]
        [StringLength(750, MinimumLength = 3)]
        public string Describe { get; set; } = null!;

        //Navigation property
        public int VisitorId { get; set; }
        public List<Visitor> Visitors { get; set; } = null!;

    }
}
