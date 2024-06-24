namespace CarRentalSystem.DTOs
{
    public class RentCarRequest
    {
        public int CarId { get; set; }
        public DateTime StartDate { get; set; }
        public int DurationDays { get; set; }
        public int CustomerId { get; set; }
    }
}
