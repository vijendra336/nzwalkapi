using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalk.API.Data;
using NZWalk.API.Models.Domain;
using NZWalk.API.Models.DTO;

//#2 Convert to Async CRUD Operation

namespace NZWalk.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionAsyncController : ControllerBase
    {

        private readonly NZWalkDbContexts dbContext;

        public RegionAsyncController(NZWalkDbContexts nZWalkDbContexts)
        {
            this.dbContext = nZWalkDbContexts;
        }

        // GET ALL REGIONS
        // GET: https://localhost:portnumber/api/regionsasync 
        // Asynchronous Call GetAll
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            // Get data from Database - Domain models 
            // Use await to call which you want async 
            var regionsDomain = await dbContext.Regions.ToListAsync();

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


        //POST To Create New Region
        //POST: https://localhost:portnumber/api/regions 
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            // Map or Convert DTO to Domain Model 
            var regionDomainModel = new Region
            {
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl

            };

            // Use Domain Model to Create Region 
            await dbContext.Regions.AddAsync(regionDomainModel);
            await dbContext.SaveChangesAsync();

            // Map Domain Model back to DTO 
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            return CreatedAtAction(nameof(GetById), new { id = regionDomainModel.Id }, regionDto);

        }


        // Update Region
        // PUT: https://localhost:portnumber/api/regions/{id} 
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            // Check if Region exists 
            var regionDomainModel = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            // Map DTO to Domain Model
            regionDomainModel.Code = updateRegionRequestDto.Code;
            regionDomainModel.Name = updateRegionRequestDto.Name;
            regionDomainModel.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;

            await dbContext.SaveChangesAsync();

            // Convert Domain model to DTO
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            //return back DTO to client not Domain Model
            return Ok(regionDto);
        }

        // Delete: https://localhost:portnumber/api/regions/{id} 
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            // Check if Region Domain Model Exist
            var regionDomainModel = await dbContext.Regions.FirstOrDefaultAsync(y => y.Id == id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            // Delete Region
            // Remove method does not have async method so it is synchronous
            dbContext.Regions.Remove(regionDomainModel);   
            await dbContext.SaveChangesAsync();

            // Return Deleted Region Back
            // Map Domain Model to DTO
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            return Ok(regionDto);
        }



    }
}
