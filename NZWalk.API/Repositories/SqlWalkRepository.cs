using Microsoft.EntityFrameworkCore;
using NZWalk.API.Data;
using NZWalk.API.Models.Domain;

namespace NZWalk.API.Repositories
{
    public class SqlWalkRepository : IWalkRepository
    {
        private readonly NZWalkDbContexts dbContexts;

        public SqlWalkRepository(NZWalkDbContexts dbContexts)
        {
            this.dbContexts = dbContexts;
        }
        public async Task<Walk> CreateAsync(Walk walk)
        {
            await dbContexts.Walks.AddAsync(walk);
            await dbContexts.SaveChangesAsync();

            return walk;
        }

        public async Task<List<Walk>> GetAllAsync()
        {
            //return await dbContexts.Walks.ToListAsync();
            
            // Using Navigation Properties
            return await dbContexts.Walks.Include("Difficulty").Include("Region").ToListAsync();

            // Another way Using Navigation Properties with Type Safe 
            //return await dbContexts.Walks.Include(x=>x.Difficulty).Include(y=>y.Region).ToListAsync();
        }

        public async Task<Walk> GetByIdAsync(Guid id)
        {
           return await dbContexts.Walks
                .Include("Difficulty")
                .Include("Region")
                .FirstOrDefaultAsync(walk => walk.Id == id);   
        }
    }
}
