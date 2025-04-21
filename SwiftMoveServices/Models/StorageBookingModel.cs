using System;

namespace SwiftMoveServices.Models
{
    public class StorageBookingModel
    {
        public int Id { get; set; }

        public string CustomerId { get; set; } // Link to user (currently used as name)

        public int ConsecutiveMonths { get; set; }

        public bool IsFreeMonth { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public DateTime BookingDate { get; set; } = DateTime.Now;

        public decimal Price { get; set; } = 100; // Default price for storage
    }
}
