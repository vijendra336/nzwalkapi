using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using NZWalk.API.Data;

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
            var regions = dbContext.Regions.ToList();

            return Ok(regions);
        }

        // GET ALL REGIONS
        // GET: https://localhost:portnumber/api/regions/{id} 
        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById([FromRoute] Guid id) {
            // Route id should match with parameter id 
            // FromRoute attribute as it is coming from route 

            //var region = dbContext.Regions.Find(id); // Find method use only for primarykey 

            var region = dbContext.Regions.FirstOrDefault(r => r.Id == id);

            if(region == null)
            {
                return NotFound(id);
            }

            return Ok(region);
        }

    }
}
