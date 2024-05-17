using Microsoft.AspNetCore.Mvc;
using Amit.Models;
using Amit.Services;
using Amit.Entities;
using Amit.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;

namespace Amit.Controllers
{
    /// <summary>
    /// Controller responsible for managing venue related operations.
    /// </summary>
    /// <remarks>
    /// This Controller provides endpoints for adding, retrieving, updating, and deleting venue information.
    /// </remarks>
    [Route("api/venue")]
    [Authorize]
    public class VenueController : ControllerBase
    {
        private readonly IVenueService _venueService;

        /// <summary>
        /// Initializes a new instance of the VenueController class with the specified context.
        /// </summary>
        /// <param name="ivenueservice">The ivenueservice to be used by the controller.</param>
        public VenueController(IVenueService ivenueservice)
        {
            _venueService = ivenueservice;
        }

        /// <summary>Adds a new venue</summary>
        /// <param name="model">The venue data to be added</param>
        /// <returns>The result of the operation</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        [UserAuthorize("Venue",Entitlements.Create)]
        public IActionResult Post([FromBody] Venue model)
        {
            var id = _venueService.Create(model);
            return Ok(new { id });
        }

        /// <summary>Retrieves a list of venues based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of venues</returns>
        [HttpGet]
        [UserAuthorize("Venue",Entitlements.Read)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public IActionResult Get([FromQuery] string filters, string searchTerm, int pageNumber = 1, int pageSize = 10, string sortField = null, string sortOrder = "asc")
        {
            if (pageSize < 1)
            {
                return BadRequest("Page size invalid.");
            }

            if (pageNumber < 1)
            {
                return BadRequest("Page mumber invalid.");
            }

            var result = _venueService.Get(filters, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return Ok(result);
        }

        /// <summary>Retrieves a specific venue by its primary key</summary>
        /// <param name="id">The primary key of the venue</param>
        /// <returns>The venue data</returns>
        [HttpGet]
        [Route("{id:Guid}")]
        [UserAuthorize("Venue",Entitlements.Read)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var result = _venueService.GetById(id);
            return Ok(result);
        }

        /// <summary>Deletes a specific venue by its primary key</summary>
        /// <param name="id">The primary key of the venue</param>
        /// <returns>The result of the operation</returns>
        [HttpDelete]
        [UserAuthorize("Venue",Entitlements.Delete)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        [Route("{id:Guid}")]
        public IActionResult DeleteById([FromRoute] Guid id)
        {
            var status = _venueService.Delete(id);
            return Ok(new { status });
        }

        /// <summary>Updates a specific venue by its primary key</summary>
        /// <param name="id">The primary key of the venue</param>
        /// <param name="updatedEntity">The venue data to be updated</param>
        /// <returns>The result of the operation</returns>
        [HttpPut]
        [UserAuthorize("Venue",Entitlements.Update)]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public IActionResult UpdateById(Guid id, [FromBody] Venue updatedEntity)
        {
            if (id != updatedEntity.Id)
            {
                return BadRequest("Mismatched Id");
            }

            var status = _venueService.Update(id, updatedEntity);
            return Ok(new { status });
        }

        /// <summary>Updates a specific venue by its primary key</summary>
        /// <param name="id">The primary key of the venue</param>
        /// <param name="updatedEntity">The venue data to be updated</param>
        /// <returns>The result of the operation</returns>
        [HttpPatch]
        [UserAuthorize("Venue",Entitlements.Update)]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public IActionResult UpdateById(Guid id, [FromBody] JsonPatchDocument<Venue> updatedEntity)
        {
            if (updatedEntity == null)
                return BadRequest("Patch document is missing.");
            var status = _venueService.Patch(id, updatedEntity);
            return Ok(new { status });
        }
    }
}