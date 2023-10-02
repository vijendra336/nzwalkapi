﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using NZWalk.API.Data;
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

    }
}
