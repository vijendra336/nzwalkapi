﻿using NZWalk.API.Models.Domain;

namespace NZWalk.API.Repositories
{

    // Definition of method want to expose 
    public interface IRegionRepository
    {
        Task<List<Region>> GetAllAsync();

        Task<Region?> GetByIdAsync(Guid id);

        Task<Region> CreateAsync(Region region);

        Task<Region?> UpdateAsync(Guid id, Region region);

        Task<Region?> DeleteAsync(Guid id); 
    }
}
