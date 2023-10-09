using NZWalk.API.Models.Domain;

namespace NZWalk.API.Repositories
{
    public class InMemoryRegionRepository : IRegionRepository
    {

        public async Task<List<Region>> GetAllAsync()
        {
            return new List<Region>() {
                new Region()
                {
                    Id= Guid.NewGuid(),
                    Code = "SAM",
                    Name = "Sameer's Region Name",
                    RegionImageUrl = "Sameer's Image URL"
                }
            };
        }

        public Task<Region> CreateAsync(Region region)
        {
            throw new NotImplementedException();
        }


        public Task<Region?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Region?> UpdateAsync(Guid id, Region region)
        {
            throw new NotImplementedException();
        }

        public Task<Region?> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
