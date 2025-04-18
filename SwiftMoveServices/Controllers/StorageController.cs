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
            var allBookings = _context.StorageBookings.ToList();
            return View(allBookings);
        }
    }
}
