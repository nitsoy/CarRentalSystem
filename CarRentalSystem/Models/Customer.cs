using System.ComponentModel.DataAnnotations;

namespace CarRentalSystem.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int LoyaltyPoints { get; set; }
    }
}
