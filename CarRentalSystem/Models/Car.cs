using System.ComponentModel.DataAnnotations;

namespace CarRentalSystem.Models
{
    public class Car
    {
        [Key]
        public int Id { get; set; }
        public string Model { get; set; }
        public CarType Type { get; set; }
        public bool IsRented { get; set; }
    }
    public enum CarType
    {
        Premium,
        Suv,
        Small
    }
}
