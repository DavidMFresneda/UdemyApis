using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NZWalks.API.Data
{
    public class NZWalksAuthDbContext : IdentityDbContext
    {
        public NZWalksAuthDbContext(DbContextOptions<NZWalksAuthDbContext> options) : base(options)
        {



        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRoleId = "d2adfe63-bf69-4ed8-bd06-13e5fb4bc20f";
            var writerRoleId = "3ea98e98-31c2-4f15-8edb-d811e80850fc";

            var roles = new List<IdentityRole>
            {
                new IdentityRole{Name = "Reader",
                                Id = readerRoleId,
                                ConcurrencyStamp = readerRoleId,
                                NormalizedName = "READER"},
                new IdentityRole{Name = "Writer",
                                Id = writerRoleId,
                                ConcurrencyStamp = writerRoleId,
                                NormalizedName = "WRITER"}
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }

    }


}
