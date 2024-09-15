using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data
{

    public class NZWalksDbContext : DbContext
    {
        public NZWalksDbContext(DbContextOptions<NZWalksDbContext> options)
            : base(options)
        {
        }

        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<Image> Images { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Additional configuration here

            //Seed Data for Difficulties
            //Easy, Medium, Hard
            var difficulties = new List<Difficulty>() {
                new Difficulty { Id = new Guid("A4C936B6-E014-4415-BEBC-C44D9AC50374"), Name = "Easy" },
                new Difficulty { Id = new Guid("108ACB13-DAB1-457D-9E58-58C0B4C808CD"), Name = "Medium" },
                new Difficulty { Id = new Guid("FDBA5F1C-C9BD-4DB0-A8E9-AD42FB22F364"), Name = "Hard" },
            };

            //Permite agregar elementos a la tabla Difficulty
            modelBuilder.Entity<Difficulty>().HasData(
                difficulties
            );



            //Seed Data for Regions
            //Northland, Auckland, Waikato, Bay of Plenty, Gisborne, Hawke's Bay, Taranaki, Manawatu-Wanganui, Wellington, Tasman, Nelson, Marlborough, West Coast, Canterbury, Otago, Southland
            var regions = new List<Region>() {
                new Region { Id = new Guid("9D6E02AA-4F37-4712-8810-359D4C49FF0C"), Code = "NL", Name = "Northland" },
                new Region { Id = new Guid("1536CFBF-7A2C-4A45-ABA8-693D56AB98C6"), Code = "AZ", Name = "Auckland" },
                new Region { Id = new Guid("5B6589E6-D476-494F-8B22-F35788C977F7"), Code = "WK", Name = "Waikato" },
            };

            modelBuilder.Entity<Region>().HasData(
                regions
            );

        }
    }
}
