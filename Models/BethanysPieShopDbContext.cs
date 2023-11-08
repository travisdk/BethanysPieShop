using Microsoft.EntityFrameworkCore;


namespace BethanysPieShop.Models
{
    public class BethanysPieShopDbContext : DbContext
    {

        public BethanysPieShopDbContext(DbContextOptions<BethanysPieShopDbContext> options) : base(options)
        {

        }


        DbSet<Category> Categories { get; set; }    
        DbSet<Pie> Pie { get; set; }


    }
}
