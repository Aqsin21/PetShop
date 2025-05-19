using Microsoft.EntityFrameworkCore;
using PetShop.DataContext.Entities;

namespace PetShop.DataContext
{
    public class AppDbContext :DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) :base(options) { }

        public DbSet<Slider> Sliders { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }
    }
}
