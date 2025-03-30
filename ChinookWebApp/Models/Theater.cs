namespace ChinookWebApp.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Theater
    {
        [Key]
        public int TheaterID { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public int TotalScreens { get; set; }

        // Navigation Property
        public virtual ICollection<Showtime>? Showtimes { get; set; }
    }


}
