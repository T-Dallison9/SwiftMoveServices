using Microsoft.EntityFrameworkCore;
using SwiftMoveServices.Models;

namespace SwiftMoveServices.Data
{
    public class ApplicationDbContext : DbContext
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
