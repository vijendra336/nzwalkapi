using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalk.API.Models.DTO;
using NZWalk.API.Repositories;

//#3 Repository Pattern Implementation 

namespace NZWalk.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionRepositoryController : ControllerBase
    {
        private readonly IRegionRepository regionRepository;

        public RegionRepositoryController(IRegionRepository regionRepository)
        {
            this.regionRepository = regionRepository;
        }

        // GET ALL REGIONS
        // GET: https://localhost:portnumber/api/regionsasync 
        // Asynchronous Call GetAll
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            // Get data from Database - Domain models 
            // Use await to call which you want async 
            var regionsDomain = await regionRepository.GetAllAsync();

            // Map or converting this Domain models to DTOs 
            var regionsDto = new List<RegionDto>();

            foreach (var regionDomain in regionsDomain)
            {
                regionsDto.Add(new RegionDto()
                {
                    Id = regionDomain.Id,
                    Code = regionDomain.Code,
                    Name = regionDomain.Name,
                    RegionImageUrl = regionDomain.RegionImageUrl
                });
            }

            // return DTOs to the client 
            // exposing dto instead of domain model i.e. regionsDomain
            return Ok(regionsDto);
        }

        // GET ALL REGIONS By ID
        // GET: https://localhost:portnumber/api/regions/{id} 
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            // Route id should match with parameter id 
            // FromRoute attribute as it is coming from route 

            //var region = dbContext.Regions.Find(id); // Find method use only for primarykey 

            // Get regions domain model from Database
            var regionDomain = await dbContext.Regions.FirstOrDefaultAsync(r => r.Id == id);

            if (regionDomain == null)
            {
                return NotFound(id);
            }

            //Map/Convert Region Domain Model to Region DTO
            var regionDto = new RegionDto()
            {
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                RegionImageUrl = regionDomain.RegionImageUrl
            };

            // Return/expose DTO back to the client
            return Ok(regionDto);
        }

    }
}
