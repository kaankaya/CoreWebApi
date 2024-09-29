using GalacticTourAgency.Models;
using Microsoft.EntityFrameworkCore;

namespace GalacticTourAgency.Data
{
    public class GalacticContext : DbContext
    {
        public DbSet<GalacticProduct> GalacticProducts { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=Galactic;Truested_Connection=true;");
        //}
    }
}
