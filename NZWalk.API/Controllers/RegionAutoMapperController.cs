using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalk.API.CustomActionFilter;
using NZWalk.API.Models.Domain;
using NZWalk.API.Models.DTO;
using NZWalk.API.Repositories;
using System.Text.Json;

//#3 Repository Pattern Implementation 

namespace NZWalk.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class RegionAutoMapperController : ControllerBase
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;
        private readonly ILogger<RegionAutoMapperController> logger;

        public RegionAutoMapperController(IRegionRepository regionRepository, 
            IMapper mapper, ILogger<RegionAutoMapperController> logger)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        // GET ALL REGIONS
        // GET: https://localhost:portnumber/api/regionsasync 
        // Asynchronous Call GetAll
        [HttpGet]
        //[Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAll()
        {

            try
            {
                // Log Exception using LogError method in  catch block 
                throw new Exception("This is custom exception to check exception log ");

                //Program.cs -> MinimumLevel.Information()  ( LogInformation and LogDebug work )
                logger.LogInformation("GetAllRegions action method Invoked");

                //Program.cs -> MinimumLevel.Warning()  (LogWarning and LogError will work )
                logger.LogWarning("This is warning log.");
                logger.LogError("This is error log.");

                // Get data from Database - Domain models 
                // Use await to call which you want async 
                var regionsDomain = await regionRepository.GetAllAsync();

                // Map or converting this Domain models to DTOs 
                //Automapper Map or converting this Domain models to DTOs 
                var regionsDto = mapper.Map<List<RegionDto>>(regionsDomain);

                logger.LogInformation($"Finished GetAllRegions request with data: {JsonSerializer.Serialize(regionsDomain)}");
                // return DTOs to the client 
                // exposing dto instead of domain model i.e. regionsDomain
                return Ok(regionsDto);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                throw;
            }
        }

        // GET ALL REGIONS By ID
        // GET: https://localhost:portnumber/api/regions/{id} 
        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            // Route id should match with parameter id 
            // FromRoute attribute as it is coming from route 

            // Get regions domain model from Database
            var regionDomain = await regionRepository.GetByIdAsync(id);

            if (regionDomain == null)
            {
                return NotFound(id);
            }

            //Automapper Map/Convert Region Domain Model to Region DTO
            // Return/expose DTO back to the client
            return Ok(mapper.Map<RegionDto>(regionDomain));
        }


        //POST To Create New Region
        //POST: https://localhost:portnumber/api/regions 
        [HttpPost]
        [ValidateModel]
        
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            // change from check for ModelState we use ValidateModel -> Custom Attribute we created 
            //if (ModelState.IsValid)
            //{
                // Map or Convert DTO to Domain Model 
                // AutoMapper map addRegionRequestDto to Region Domain model 
                var regionDomainModel = mapper.Map<Region>(addRegionRequestDto);

                // Use Domain Model to Create Region 
                regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);

                // Automapper convert back from regionDomainModel to regionDto
                var regionDto = mapper.Map<RegionDto>(regionDomainModel);

                return CreatedAtAction(nameof(GetById), new { id = regionDomainModel.Id }, regionDto);
            //}
            //else
            //{
            //    return BadRequest(ModelState);
            //}
        }

        // Update Region
        // PUT: https://localhost:portnumber/api/regions/{id} 
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {

                // Map DTO to Domain Model
                //AutoMapper
                var regionDomainModel = mapper.Map<Region>(updateRegionRequestDto);

                // Check if Region exists 
                regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);


                if (regionDomainModel == null)
                {
                    return NotFound();
                }

                // Convert Domain model to DTO
                //AutoMapper
                var regionDto = mapper.Map<RegionDto>(regionDomainModel);

                //return back DTO to client not Domain Model
                return Ok(regionDto);

        }


        // Delete: https://localhost:portnumber/api/regions/{id} 
        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            // Check if Region Domain Model Exist
            var regionDomainModel = await regionRepository.DeleteAsync(id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }
           
            // Return Deleted Region Back
            // Map Domain Model to DTO
            // Automapper
            var regionDto = mapper.Map<RegionDto>(regionDomainModel);

            return Ok(regionDto);
        }


    }
}
