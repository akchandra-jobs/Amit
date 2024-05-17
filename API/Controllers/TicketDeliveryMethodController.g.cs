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
    /// Controller responsible for managing ticketdeliverymethod related operations.
    /// </summary>
    /// <remarks>
    /// This Controller provides endpoints for adding, retrieving, updating, and deleting ticketdeliverymethod information.
    /// </remarks>
    [Route("api/ticketdeliverymethod")]
    [Authorize]
    public class TicketDeliveryMethodController : ControllerBase
    {
        private readonly ITicketDeliveryMethodService _ticketDeliveryMethodService;

        /// <summary>
        /// Initializes a new instance of the TicketDeliveryMethodController class with the specified context.
        /// </summary>
        /// <param name="iticketdeliverymethodservice">The iticketdeliverymethodservice to be used by the controller.</param>
        public TicketDeliveryMethodController(ITicketDeliveryMethodService iticketdeliverymethodservice)
        {
            _ticketDeliveryMethodService = iticketdeliverymethodservice;
        }

        /// <summary>Adds a new ticketdeliverymethod</summary>
        /// <param name="model">The ticketdeliverymethod data to be added</param>
        /// <returns>The result of the operation</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        [UserAuthorize("TicketDeliveryMethod",Entitlements.Create)]
        public IActionResult Post([FromBody] TicketDeliveryMethod model)
        {
            var id = _ticketDeliveryMethodService.Create(model);
            return Ok(new { id });
        }

        /// <summary>Retrieves a list of ticketdeliverymethods based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of ticketdeliverymethods</returns>
        [HttpGet]
        [UserAuthorize("TicketDeliveryMethod",Entitlements.Read)]
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

            var result = _ticketDeliveryMethodService.Get(filters, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return Ok(result);
        }

        /// <summary>Retrieves a specific ticketdeliverymethod by its primary key</summary>
        /// <param name="id">The primary key of the ticketdeliverymethod</param>
        /// <returns>The ticketdeliverymethod data</returns>
        [HttpGet]
        [Route("{id:Guid}")]
        [UserAuthorize("TicketDeliveryMethod",Entitlements.Read)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var result = _ticketDeliveryMethodService.GetById(id);
            return Ok(result);
        }

        /// <summary>Deletes a specific ticketdeliverymethod by its primary key</summary>
        /// <param name="id">The primary key of the ticketdeliverymethod</param>
        /// <returns>The result of the operation</returns>
        [HttpDelete]
        [UserAuthorize("TicketDeliveryMethod",Entitlements.Delete)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        [Route("{id:Guid}")]
        public IActionResult DeleteById([FromRoute] Guid id)
        {
            var status = _ticketDeliveryMethodService.Delete(id);
            return Ok(new { status });
        }

        /// <summary>Updates a specific ticketdeliverymethod by its primary key</summary>
        /// <param name="id">The primary key of the ticketdeliverymethod</param>
        /// <param name="updatedEntity">The ticketdeliverymethod data to be updated</param>
        /// <returns>The result of the operation</returns>
        [HttpPut]
        [UserAuthorize("TicketDeliveryMethod",Entitlements.Update)]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public IActionResult UpdateById(Guid id, [FromBody] TicketDeliveryMethod updatedEntity)
        {
            if (id != updatedEntity.Id)
            {
                return BadRequest("Mismatched Id");
            }

            var status = _ticketDeliveryMethodService.Update(id, updatedEntity);
            return Ok(new { status });
        }

        /// <summary>Updates a specific ticketdeliverymethod by its primary key</summary>
        /// <param name="id">The primary key of the ticketdeliverymethod</param>
        /// <param name="updatedEntity">The ticketdeliverymethod data to be updated</param>
        /// <returns>The result of the operation</returns>
        [HttpPatch]
        [UserAuthorize("TicketDeliveryMethod",Entitlements.Update)]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public IActionResult UpdateById(Guid id, [FromBody] JsonPatchDocument<TicketDeliveryMethod> updatedEntity)
        {
            if (updatedEntity == null)
                return BadRequest("Patch document is missing.");
            var status = _ticketDeliveryMethodService.Patch(id, updatedEntity);
            return Ok(new { status });
        }
    }
}