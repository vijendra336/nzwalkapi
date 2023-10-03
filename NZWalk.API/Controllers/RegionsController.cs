using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using NZWalk.API.Data;
using NZWalk.API.Models.Domain;
using NZWalk.API.Models.DTO;

namespace NZWalk.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalkDbContexts dbContext;

        public RegionsController(NZWalkDbContexts nZWalkDbContexts)
        {
            this.dbContext = nZWalkDbContexts;
        }

        // GET ALL REGIONS
        // GET: https://localhost:portnumber/api/regions 
        [HttpGet]
        public IActionResult GetAll()
        {

            // Get data from Database - Domain models 
            var regionsDomain = dbContext.Regions.ToList();

            // Map or converting this Domain models to DTOs 
            var regionsDto = new List<RegionDto>();

            foreach (var regionDomain in regionsDomain)
            {
                regionsDto.Add(new RegionDto()
                {
                    Id= regionDomain.Id,
                    Code= regionDomain.Code,
                    Name= regionDomain.Name,
                    RegionImageUrl= regionDomain.RegionImageUrl
                });
            }


            // return DTOs to the client 
            // exposing dto instead of domain model i.e. regionsDomain
            return Ok(regionsDto);
        }

        // GET ALL REGIONS
        // GET: https://localhost:portnumber/api/regions/{id} 
        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById([FromRoute] Guid id) {
            // Route id should match with parameter id 
            // FromRoute attribute as it is coming from route 

            //var region = dbContext.Regions.Find(id); // Find method use only for primarykey 

            // Get regions domain model from Database
            var regionDomain = dbContext.Regions.FirstOrDefault(r => r.Id == id);

            if(regionDomain == null)
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
        public IActionResult Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            // Map or Convert DTO to Domain Model 
            var regionDomainModel = new Region
            {
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl

            };

            // Use Domain Model to Create Region 
            dbContext.Regions.Add(regionDomainModel);
            dbContext.SaveChanges();

            // Map Domain Model back to DTO 
            var regionDto = new RegionDto
            {
                Id=regionDomainModel.Id,
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
        public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            // Check if Region exists 
            var regionDomainModel = dbContext.Regions.FirstOrDefault(x => x.Id == id);

            if(regionDomainModel == null)
            {
                return NotFound();
            }

            // Map DTO to Domain Model
            regionDomainModel.Code = updateRegionRequestDto.Code;
            regionDomainModel.Name = updateRegionRequestDto.Name;
            regionDomainModel.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;

            dbContext.SaveChanges();

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
        public IActionResult Delete([FromRoute] Guid id)
        {
            // Check if Region Domain Model Exist
            var regionDomainModel = dbContext.Regions.FirstOrDefault(y => y.Id == id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            // Delete Region
            dbContext.Regions.Remove(regionDomainModel);
            dbContext.SaveChanges();

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
