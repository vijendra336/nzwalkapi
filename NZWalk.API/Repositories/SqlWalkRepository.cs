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

        public async  Task<Walk> UpdateAsync(Guid id, Walk walk)
        {
            var existingWalk = await dbContexts.Walks.FirstOrDefaultAsync(x=>x.Id== id);

            if (existingWalk == null)
            {
                return null;
            }

            existingWalk.Name = walk.Name;
            existingWalk.Description = walk.Description;
            existingWalk.WalkImageUrl = walk.WalkImageUrl;  
            existingWalk.LengthInKm =   walk.LengthInKm;
            existingWalk.DifficultyId = walk.DifficultyId;
            existingWalk.RegionId = walk.RegionId;

            await dbContexts.SaveChangesAsync();

            return existingWalk;

        }


        public async Task<Walk> DeleteAsync(Guid id)
        {
            var existingWalk =await dbContexts.Walks.FirstOrDefaultAsync(walk => walk.Id== id);

            if(existingWalk == null)
            {
                return null;
            }

            dbContexts.Walks.Remove(existingWalk);
            await dbContexts.SaveChangesAsync();

            return existingWalk;
        }
    }
}
