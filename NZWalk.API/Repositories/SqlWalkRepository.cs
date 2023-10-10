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
           return await dbContexts.Walks.ToListAsync();
        }
    }
}
