﻿using NZWalk.API.Models.Domain;

namespace NZWalk.API.Repositories
{
    public interface IWalkRepository
    {
        Task<Walk> CreateAsync(Walk walk);
    }
}
