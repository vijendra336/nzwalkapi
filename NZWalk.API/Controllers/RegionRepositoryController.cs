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

    }
}
