using System.ComponentModel.DataAnnotations;

namespace WebApplication.Services.Models
{
    public class HighScore
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public int Score { get; set; }
    }
}
