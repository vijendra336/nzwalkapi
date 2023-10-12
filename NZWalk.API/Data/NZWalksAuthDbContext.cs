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
    }
}
