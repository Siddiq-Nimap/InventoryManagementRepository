using System.Data.Entity;

namespace DAL.Models
{
    public class Context : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Catagory> Catagories { get; set; }
        public DbSet<CatagoryList> CatagoryLists { get; set; }
        public DbSet<Logins> Logins { get; set; }

        public string cs = "data source = DESKTOP-TFBH7SV; initial catalog = ProductDataBase; integrated security = true;";
    }
}
