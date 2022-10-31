namespace FootballLeague.DataAccess.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Team
    {
        [Key]
        public int TeamId { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }
    }
}
