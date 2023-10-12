﻿using NZWalk.API.Models.Domain;

namespace NZWalk.API.Repositories
{
    public interface IWalkRepository
    {
        Task<Walk> CreateAsync(Walk walk);
        Task<List<Walk>> GetAllAsync();
        Task<List<Walk>> GetAllFilterAsync(string? filterOn = null, string? filterQuery = null,
            string? sortyBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1000);
        Task<Walk> GetByIdAsync(Guid id);
        Task<Walk> UpdateAsync(Guid id, Walk walk);
        Task<Walk> DeleteAsync(Guid id);
    }
}
