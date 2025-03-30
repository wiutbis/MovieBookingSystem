namespace ChinookWebApp.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public enum TicketStatus { Booked, Canceled, Completed }
    public class Ticket
    {
        [Key]
        public int TicketID { get; set; }

        [Required]
        [ForeignKey("Showtime")]
        public int ShowtimeID { get; set; }

        [Required]
        [ForeignKey("Customer")]
        public int CustomerID { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Required]
        public int Quantity { get; set; }

        // Navigation Properties
        public virtual Movie? Movie { get; set; }
        public virtual Customer? Customer { get; set; }
    }


}
