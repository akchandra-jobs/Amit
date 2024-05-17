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
    /// Controller responsible for managing organizer related operations.
    /// </summary>
    /// <remarks>
    /// This Controller provides endpoints for adding, retrieving, updating, and deleting organizer information.
    /// </remarks>
    [Route("api/organizer")]
    [Authorize]
    public class OrganizerController : ControllerBase
    {
        private readonly IOrganizerService _organizerService;

        /// <summary>
        /// Initializes a new instance of the OrganizerController class with the specified context.
        /// </summary>
        /// <param name="iorganizerservice">The iorganizerservice to be used by the controller.</param>
        public OrganizerController(IOrganizerService iorganizerservice)
        {
            _organizerService = iorganizerservice;
        }

        /// <summary>Adds a new organizer</summary>
        /// <param name="model">The organizer data to be added</param>
        /// <returns>The result of the operation</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        [UserAuthorize("Organizer",Entitlements.Create)]
        public IActionResult Post([FromBody] Organizer model)
        {
            var id = _organizerService.Create(model);
            return Ok(new { id });
        }

        /// <summary>Retrieves a list of organizers based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of organizers</returns>
        [HttpGet]
        [UserAuthorize("Organizer",Entitlements.Read)]
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

            var result = _organizerService.Get(filters, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return Ok(result);
        }

        /// <summary>Retrieves a specific organizer by its primary key</summary>
        /// <param name="id">The primary key of the organizer</param>
        /// <returns>The organizer data</returns>
        [HttpGet]
        [Route("{id:Guid}")]
        [UserAuthorize("Organizer",Entitlements.Read)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var result = _organizerService.GetById(id);
            return Ok(result);
        }

        /// <summary>Deletes a specific organizer by its primary key</summary>
        /// <param name="id">The primary key of the organizer</param>
        /// <returns>The result of the operation</returns>
        [HttpDelete]
        [UserAuthorize("Organizer",Entitlements.Delete)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        [Route("{id:Guid}")]
        public IActionResult DeleteById([FromRoute] Guid id)
        {
            var status = _organizerService.Delete(id);
            return Ok(new { status });
        }

        /// <summary>Updates a specific organizer by its primary key</summary>
        /// <param name="id">The primary key of the organizer</param>
        /// <param name="updatedEntity">The organizer data to be updated</param>
        /// <returns>The result of the operation</returns>
        [HttpPut]
        [UserAuthorize("Organizer",Entitlements.Update)]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public IActionResult UpdateById(Guid id, [FromBody] Organizer updatedEntity)
        {
            if (id != updatedEntity.Id)
            {
                return BadRequest("Mismatched Id");
            }

            var status = _organizerService.Update(id, updatedEntity);
            return Ok(new { status });
        }

        /// <summary>Updates a specific organizer by its primary key</summary>
        /// <param name="id">The primary key of the organizer</param>
        /// <param name="updatedEntity">The organizer data to be updated</param>
        /// <returns>The result of the operation</returns>
        [HttpPatch]
        [UserAuthorize("Organizer",Entitlements.Update)]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public IActionResult UpdateById(Guid id, [FromBody] JsonPatchDocument<Organizer> updatedEntity)
        {
            if (updatedEntity == null)
                return BadRequest("Patch document is missing.");
            var status = _organizerService.Patch(id, updatedEntity);
            return Ok(new { status });
        }
    }
}