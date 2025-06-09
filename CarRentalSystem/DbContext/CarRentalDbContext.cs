using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CarRentalSystem.Contexts
{
    public class CarRentalDbContext : DbContext
    {
        public CarRentalDbContext(DbContextOptions<CarRentalDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Car> Cars { get; set; }
    }
}
