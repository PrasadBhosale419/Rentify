using Backend.Models;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace Backend.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
        : base(options)
        {
        }

        // Define a DbSet for each model class (table)
        public DbSet<City> Cities { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Property> Properties { get; set; }

        public DbSet<PropertyType> PropertyTypes { get; set; }

        public DbSet<FurnishingType> FurnishingTypes { get; set; }
    }
}
