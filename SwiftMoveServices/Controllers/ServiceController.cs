using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SwiftMoveServices.Data;
using SwiftMoveServices.Models;

namespace SwiftMoveServices.Controllers
{
    public class ServiceController : Controller
    {
        //Inject the database
        private readonly ApplicationDbContext _context;

        //Create a constructor that creates the database object.
        public ServiceController(ApplicationDbContext context)
        {
            _context = context;
        }


        //Get: Services/Index
        public async Task<IActionResult> Index()
        {
            //Gets a list of Services from the database.
            return View(await _context.Services.ToListAsync());
        }

        //Get: Services/Create
        public IActionResult Create()
        {
            return View();
        }


        //POST: Services/Create
        [HttpPost]
        public async Task<IActionResult> Create(ServiceModel service)
        {
            //Check if the model passes our validation checks
            if (ModelState.IsValid)
            {
                _context.Services.Add(service); //Adding service to database
                await _context.SaveChangesAsync(); //Saving the database changes
                return RedirectToAction(nameof(Index)); //Sending the user to the index page
            }
            //If validation fails:
            return View(service);
        }

        //GET Product/Edit
        public async Task<IActionResult> Edit(int id)
        {
            //Find the product from the database from the id
            var service = await _context.Services.FindAsync(id);
            if (service == null)
            {
                return NotFound();
            }
            //Return to Product/Edit with the product details.
            return View(service);
        }

        //POST Service/Edit
        [HttpPost]
        public async Task<IActionResult> Edit(int id, ServiceModel service)
        {
            if (id != service.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                //Update database
                _context.Update(service);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //If the model is not valid, return
            return View(service);
        }


        //Delete Action
        public async Task<IActionResult> Delete(int id)
        {
            var service = await _context.Services.FindAsync(id);

            //Check if the service exists
            if (service == null)
            {
                return NotFound();
            }
            else
            {
                //Delete the Service from the database
                _context.Services.Remove(service);
                await _context.SaveChangesAsync(); //Save database
                //send the user back to the index
                return RedirectToAction(nameof(Index));
            }
        }


        


    }
}