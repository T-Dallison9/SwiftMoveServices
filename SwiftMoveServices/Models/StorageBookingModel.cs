namespace SwiftMoveServices.Models
{
    public class StorageBookingModel
    {
        public int Id { get; set; }
        public string CustomerId { get; set; } //Link to user
        public int ConsecutiveMonths { get; set; }
        public bool IsFreeMonth { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}