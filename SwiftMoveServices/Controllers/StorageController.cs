using Microsoft.AspNetCore.Mvc;
using SwiftMoveServices.Data;
using SwiftMoveServices.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftMoveServices.Controllers
{
    public class StorageController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StorageController(ApplicationDbContext context)
        {
            _context = context;
        }

        //Simulated payment
        public async Task<IActionResult> CompletePayment(string customerId)
        {
            var booking = _context.StorageBookings
                .Where(b => b.CustomerId == customerId)
                .OrderByDescending(b => b.StartDate)
                .FirstOrDefault();

            if (booking != null)
            {
                booking.ConsecutiveMonths++;

                if (booking.ConsecutiveMonths == 12)
                {
                    booking.ConsecutiveMonths = 0; //Reset counter
                    booking.IsFreeMonth = true;
                }
                else
                {
                    booking.IsFreeMonth = false;
                }

                _context.Update(booking);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("BookingSummary");
        }

        public IActionResult BookingSummary()
        {
            // Fetch the storage bookings from the database
            var allBookings = _context.StorageBookings
                .Select(s => new StorageBookingModel
                {
                    Id = s.Id,
                    CustomerId = s.CustomerId,
                    ConsecutiveMonths = s.ConsecutiveMonths,
                    IsFreeMonth = s.IsFreeMonth,
                    StartDate = s.StartDate,
                    EndDate = s.EndDate
                })
                .ToList();  //Get the list of bookings, projecting to StorageBookingModel

            return View(allBookings);  //Pass the model to the view
        }

    }
}
