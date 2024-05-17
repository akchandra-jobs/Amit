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
    /// Controller responsible for managing ticketholder related operations.
    /// </summary>
    /// <remarks>
    /// This Controller provides endpoints for adding, retrieving, updating, and deleting ticketholder information.
    /// </remarks>
    [Route("api/ticketholder")]
    [Authorize]
    public class TicketHolderController : ControllerBase
    {
        private readonly ITicketHolderService _ticketHolderService;

        /// <summary>
        /// Initializes a new instance of the TicketHolderController class with the specified context.
        /// </summary>
        /// <param name="iticketholderservice">The iticketholderservice to be used by the controller.</param>
        public TicketHolderController(ITicketHolderService iticketholderservice)
        {
            _ticketHolderService = iticketholderservice;
        }

        /// <summary>Adds a new ticketholder</summary>
        /// <param name="model">The ticketholder data to be added</param>
        /// <returns>The result of the operation</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        [UserAuthorize("TicketHolder",Entitlements.Create)]
        public IActionResult Post([FromBody] TicketHolder model)
        {
            var id = _ticketHolderService.Create(model);
            return Ok(new { id });
        }

        /// <summary>Retrieves a list of ticketholders based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of ticketholders</returns>
        [HttpGet]
        [UserAuthorize("TicketHolder",Entitlements.Read)]
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

            var result = _ticketHolderService.Get(filters, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return Ok(result);
        }

        /// <summary>Retrieves a specific ticketholder by its primary key</summary>
        /// <param name="id">The primary key of the ticketholder</param>
        /// <returns>The ticketholder data</returns>
        [HttpGet]
        [Route("{id:Guid}")]
        [UserAuthorize("TicketHolder",Entitlements.Read)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var result = _ticketHolderService.GetById(id);
            return Ok(result);
        }

        /// <summary>Deletes a specific ticketholder by its primary key</summary>
        /// <param name="id">The primary key of the ticketholder</param>
        /// <returns>The result of the operation</returns>
        [HttpDelete]
        [UserAuthorize("TicketHolder",Entitlements.Delete)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        [Route("{id:Guid}")]
        public IActionResult DeleteById([FromRoute] Guid id)
        {
            var status = _ticketHolderService.Delete(id);
            return Ok(new { status });
        }

        /// <summary>Updates a specific ticketholder by its primary key</summary>
        /// <param name="id">The primary key of the ticketholder</param>
        /// <param name="updatedEntity">The ticketholder data to be updated</param>
        /// <returns>The result of the operation</returns>
        [HttpPut]
        [UserAuthorize("TicketHolder",Entitlements.Update)]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public IActionResult UpdateById(Guid id, [FromBody] TicketHolder updatedEntity)
        {
            if (id != updatedEntity.Id)
            {
                return BadRequest("Mismatched Id");
            }

            var status = _ticketHolderService.Update(id, updatedEntity);
            return Ok(new { status });
        }

        /// <summary>Updates a specific ticketholder by its primary key</summary>
        /// <param name="id">The primary key of the ticketholder</param>
        /// <param name="updatedEntity">The ticketholder data to be updated</param>
        /// <returns>The result of the operation</returns>
        [HttpPatch]
        [UserAuthorize("TicketHolder",Entitlements.Update)]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public IActionResult UpdateById(Guid id, [FromBody] JsonPatchDocument<TicketHolder> updatedEntity)
        {
            if (updatedEntity == null)
                return BadRequest("Patch document is missing.");
            var status = _ticketHolderService.Patch(id, updatedEntity);
            return Ok(new { status });
        }
    }
}