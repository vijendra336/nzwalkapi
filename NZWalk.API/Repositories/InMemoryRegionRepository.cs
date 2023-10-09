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
    }
}
