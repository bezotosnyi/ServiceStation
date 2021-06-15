using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ServiceStation.DAL.Models;

namespace ServiceStation.DAL
{
    public class ServiceStationContext : IdentityDbContext<Employee>
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Inspector> Inspectors { get; set; }
        public DbSet<Owner> Owners { get; set; }

        public ServiceStationContext(DbContextOptions<ServiceStationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
