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
    /// Controller responsible for managing ticketstatus related operations.
    /// </summary>
    /// <remarks>
    /// This Controller provides endpoints for adding, retrieving, updating, and deleting ticketstatus information.
    /// </remarks>
    [Route("api/ticketstatus")]
    [Authorize]
    public class TicketStatusController : ControllerBase
    {
        private readonly ITicketStatusService _ticketStatusService;

        /// <summary>
        /// Initializes a new instance of the TicketStatusController class with the specified context.
        /// </summary>
        /// <param name="iticketstatusservice">The iticketstatusservice to be used by the controller.</param>
        public TicketStatusController(ITicketStatusService iticketstatusservice)
        {
            _ticketStatusService = iticketstatusservice;
        }

        /// <summary>Adds a new ticketstatus</summary>
        /// <param name="model">The ticketstatus data to be added</param>
        /// <returns>The result of the operation</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        [UserAuthorize("TicketStatus",Entitlements.Create)]
        public IActionResult Post([FromBody] TicketStatus model)
        {
            var id = _ticketStatusService.Create(model);
            return Ok(new { id });
        }

        /// <summary>Retrieves a list of ticketstatuss based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of ticketstatuss</returns>
        [HttpGet]
        [UserAuthorize("TicketStatus",Entitlements.Read)]
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

            var result = _ticketStatusService.Get(filters, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return Ok(result);
        }

        /// <summary>Retrieves a specific ticketstatus by its primary key</summary>
        /// <param name="id">The primary key of the ticketstatus</param>
        /// <returns>The ticketstatus data</returns>
        [HttpGet]
        [Route("{id:Guid}")]
        [UserAuthorize("TicketStatus",Entitlements.Read)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var result = _ticketStatusService.GetById(id);
            return Ok(result);
        }

        /// <summary>Deletes a specific ticketstatus by its primary key</summary>
        /// <param name="id">The primary key of the ticketstatus</param>
        /// <returns>The result of the operation</returns>
        [HttpDelete]
        [UserAuthorize("TicketStatus",Entitlements.Delete)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        [Route("{id:Guid}")]
        public IActionResult DeleteById([FromRoute] Guid id)
        {
            var status = _ticketStatusService.Delete(id);
            return Ok(new { status });
        }

        /// <summary>Updates a specific ticketstatus by its primary key</summary>
        /// <param name="id">The primary key of the ticketstatus</param>
        /// <param name="updatedEntity">The ticketstatus data to be updated</param>
        /// <returns>The result of the operation</returns>
        [HttpPut]
        [UserAuthorize("TicketStatus",Entitlements.Update)]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public IActionResult UpdateById(Guid id, [FromBody] TicketStatus updatedEntity)
        {
            if (id != updatedEntity.Id)
            {
                return BadRequest("Mismatched Id");
            }

            var status = _ticketStatusService.Update(id, updatedEntity);
            return Ok(new { status });
        }

        /// <summary>Updates a specific ticketstatus by its primary key</summary>
        /// <param name="id">The primary key of the ticketstatus</param>
        /// <param name="updatedEntity">The ticketstatus data to be updated</param>
        /// <returns>The result of the operation</returns>
        [HttpPatch]
        [UserAuthorize("TicketStatus",Entitlements.Update)]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public IActionResult UpdateById(Guid id, [FromBody] JsonPatchDocument<TicketStatus> updatedEntity)
        {
            if (updatedEntity == null)
                return BadRequest("Patch document is missing.");
            var status = _ticketStatusService.Patch(id, updatedEntity);
            return Ok(new { status });
        }
    }
}