using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SwiftMoveServices.Data;
using SwiftMoveServices.Models;


namespace SwiftMoveServices.Controllers
{
    public class ReviewController : Controller
    {
        //Inject the database.
        private readonly ApplicationDbContext _context;
        //Database constructor
        public ReviewController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var reviews = await _context.Reviews.Include(r => r.Service).ToListAsync();
            return View(reviews);
        }

        //GET Review/Create
        public async Task<IActionResult> Create(int Id)
        {
            return View(Id);
        }


        //POST Review/Create
        [HttpPost]
        public async Task<IActionResult> Create(ReviewModel review)
        {
            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }



    }
}