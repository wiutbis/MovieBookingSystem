namespace ChinookWebApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    public class Movie
    {
        [Key]
        public int MovieID { get; set; }
        public int MovieId { get; internal set; }
        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        [Required]
        [StringLength(100)]
        public string Genre { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }

        // Navigation Property
        public virtual ICollection<Showtime>? Showtimes { get; set; }
        public int Duration { get; internal set; }

        [Required]
        [StringLength(50)]
        public string Language { get; set; } = string.Empty;
    }
}