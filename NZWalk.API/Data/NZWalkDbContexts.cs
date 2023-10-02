using Microsoft.EntityFrameworkCore;
using NZWalk.API.Models.Domain;

namespace NZWalk.API.Data
{
    public class NZWalkDbContexts: DbContext
    {
        public NZWalkDbContexts(DbContextOptions dbContextOptions):base(dbContextOptions)
        {
                
        }

        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
    }
}
