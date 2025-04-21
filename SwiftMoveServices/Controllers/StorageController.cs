using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SwiftMoveServices.Data;
using SwiftMoveServices.Models;
using System;
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

        // GET: Storage/Index
        public async Task<IActionResult> Index()
        {
            return View(await _context.StorageBookings.ToListAsync());
        }

        // GET: Storage/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Storage/Create
        [HttpPost]
        public async Task<IActionResult> Create(StorageBookingModel booking)
        {
            if (ModelState.IsValid)
            {
                // Set booking date to now
                booking.BookingDate = DateTime.Now;

                // Count bookings in the past 11 months (excluding free months)
                var pastBookings = await _context.StorageBookings
                    .Where(b =>
                        b.CustomerId == booking.CustomerId && // used as customer name manually
                        b.BookingDate >= DateTime.Now.AddMonths(-11) &&
                        !b.IsFreeMonth)
                    .CountAsync();

                // Check for loyalty eligibility
                if (pastBookings >= 11)
                {
                    booking.IsFreeMonth = true;
                    booking.Price = 0; // Optional depending on your model
                    ViewBag.LoyaltyMessage = "Congratulations! You've earned your 12th month free.";
                }
                else
                {
                    booking.IsFreeMonth = false;
                }

                booking.ConsecutiveMonths = pastBookings + 1;

                _context.StorageBookings.Add(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(booking);
        }

        // GET: Storage/Edit/{id}
        public async Task<IActionResult> Edit(int id)
        {
            var booking = await _context.StorageBookings.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }
            return View(booking);
        }

        // POST: Storage/Edit/{id}
        [HttpPost]
        public async Task<IActionResult> Edit(int id, StorageBookingModel booking)
        {
            if (id != booking.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(booking);
        }

        // GET: Storage/Delete/{id}
        public async Task<IActionResult> Delete(int id)
        {
            var booking = await _context.StorageBookings.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }

            _context.StorageBookings.Remove(booking);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
