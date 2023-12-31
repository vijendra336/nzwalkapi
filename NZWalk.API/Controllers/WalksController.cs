﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalk.API.CustomActionFilter;
using NZWalk.API.Models.Domain;
using NZWalk.API.Models.DTO;
using NZWalk.API.Repositories;
using System.Net;

namespace NZWalk.API.Controllers
{
    // api/walks 
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;

        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }


        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
            // Acheive modelstate by ValidateModelAttribute
            //if (ModelState.IsValid)
            //{
                // AutoMapper - Map DTO to DomainModel
                var walkDomainModel = mapper.Map<Walk>(addWalkRequestDto);

                await walkRepository.CreateAsync(walkDomainModel);

                // AutoMapper Map Domain Model to DTO
                return Ok(mapper.Map<WalkDto>(walkDomainModel));
            //}
            //else
            //{
            //    return BadRequest(ModelState);
            //}

        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var walksDomainModel = await walkRepository.GetAllAsync();

            if (walksDomainModel == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<List<WalkDto>>(walksDomainModel));
        }

        // Get: /api/walks/GetAllByFilter?filterOn=Name&filterQuery=Track
        [HttpGet]
        [Route("GetAllByFilterAndSort")]  // for new route name 
        public async Task<IActionResult> GetAllByFilterQuery([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1000)
        {
            //try
            //{
                //throw new Exception("This is new exception in Work manually global exception handler handle this");
                var walksDomainModel = await walkRepository.GetAllFilterAsync(filterOn, filterQuery, sortBy, isAscending ?? true, pageNumber, pageSize);

                if (walksDomainModel == null)
                {
                    return NotFound();
                }

                return Ok(mapper.Map<List<WalkDto>>(walksDomainModel));
            //}
            //catch (Exception ex)
            //{
            //    // Log this exception 

            //    return Problem("Something went worng", null, (int)HttpStatusCode.InternalServerError);
            //    //throw;
            //}
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var walkDomainModel= await walkRepository.GetByIdAsync(id);

            if(walkDomainModel == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<WalkDto>(walkDomainModel));
        }

        [HttpPut]
        [Route("{id}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateWalkRequestDto updateWalkRequestDto)
        {
                //Map DTO to Domain Model
                var walkDomainModel = mapper.Map<Walk>(updateWalkRequestDto);

                walkDomainModel = await walkRepository.UpdateAsync(id, walkDomainModel);

                if (walkDomainModel == null)
                {
                    return NotFound();
                }

                return Ok(mapper.Map<WalkDto>(walkDomainModel));

        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> Delete([FromRoute] Guid id)
        {
            var deleteWalkDomainModel = await walkRepository.DeleteAsync(id);

            if(deleteWalkDomainModel == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<WalkDto>(deleteWalkDomainModel));
        }

    }
}
