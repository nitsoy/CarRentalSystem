using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRentalSystem.Models
{
    public class Rental
    {
        [Key]
        public int Id { get; set; }
        public int CarId { get; set; }
        public DateTime StartDate { get; set; }
        public int DurationDays { get; set; }
        public DateTime? ReturnDate { get; set; }
        public int CustomerId { get; set; }

        [ForeignKey("CarId")]
        public Car Car { get; set; }
        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }
    }
}
