namespace ChinookWebApp.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Showtime
    {
        [Key]
        public int ShowtimeID { get; set; } 

        [Required]
        [ForeignKey("Movie")]
        public int MovieID { get; set; }

        [Required]
        [ForeignKey("Theater")]
        public int TheaterID { get; set; }

        [Required]
        public int ScreenNumber { get; set; }

        [Required]
        public DateTime ShowDate { get; set; }

        [Required]
        public TimeSpan ShowTime { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; } 

        
        public virtual Movie Movie { get; set; }
        public virtual Theater Theater { get; set; }
    }
}
