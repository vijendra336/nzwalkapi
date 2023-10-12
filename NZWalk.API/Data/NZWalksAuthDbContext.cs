using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NZWalk.API.Data
{
    public class NZWalksAuthDbContext : IdentityDbContext
    {
        public NZWalksAuthDbContext(DbContextOptions<NZWalksAuthDbContext> options) : base(options)
        {
            /*
             * When you register multiple dbcontext in NZWalkDbContexts &  NZWalksAuthDbContext program.cs you will get exception InvalidOperation Exception 
             * Fix this by making  DbContextOptions as generic one  -> DbContextOptions<NZWalksAuthDbContext> 
             * */
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRoleId = "76c66479-a387-4b27-b1bd-7f49c6f36b51";
            var writerRoleId = "751f81fd-c3ae-4c88-82b3-d0cd30ed4367";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = readerRoleId,
                    ConcurrencyStamp = readerRoleId,
                    Name="Reader",
                    NormalizedName="Reader".ToUpper(),
                },
                new IdentityRole {
                    Id = writerRoleId,
                    ConcurrencyStamp=writerRoleId,
                    Name="Writer",
                    NormalizedName ="Writer".ToUpper()
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }

    }
}
