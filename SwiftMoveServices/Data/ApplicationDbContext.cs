using Microsoft.EntityFrameworkCore;
using SwiftMoveServices.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace SwiftMoveServices.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser> //Merged?
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        public DbSet<ServiceModel> Services { get; set; }
        public DbSet<ReviewModel> Reviews { get; set; }
        public DbSet<StorageBookingModel> StorageBookings { get; set; }


    }
}
