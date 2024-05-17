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
    /// Controller responsible for managing chargeback related operations.
    /// </summary>
    /// <remarks>
    /// This Controller provides endpoints for adding, retrieving, updating, and deleting chargeback information.
    /// </remarks>
    [Route("api/chargeback")]
    [Authorize]
    public class ChargebackController : ControllerBase
    {
        private readonly IChargebackService _chargebackService;

        /// <summary>
        /// Initializes a new instance of the ChargebackController class with the specified context.
        /// </summary>
        /// <param name="ichargebackservice">The ichargebackservice to be used by the controller.</param>
        public ChargebackController(IChargebackService ichargebackservice)
        {
            _chargebackService = ichargebackservice;
        }

        /// <summary>Adds a new chargeback</summary>
        /// <param name="model">The chargeback data to be added</param>
        /// <returns>The result of the operation</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        [UserAuthorize("Chargeback",Entitlements.Create)]
        public IActionResult Post([FromBody] Chargeback model)
        {
            var id = _chargebackService.Create(model);
            return Ok(new { id });
        }

        /// <summary>Retrieves a list of chargebacks based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of chargebacks</returns>
        [HttpGet]
        [UserAuthorize("Chargeback",Entitlements.Read)]
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

            var result = _chargebackService.Get(filters, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return Ok(result);
        }

        /// <summary>Retrieves a specific chargeback by its primary key</summary>
        /// <param name="id">The primary key of the chargeback</param>
        /// <returns>The chargeback data</returns>
        [HttpGet]
        [Route("{id:Guid}")]
        [UserAuthorize("Chargeback",Entitlements.Read)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var result = _chargebackService.GetById(id);
            return Ok(result);
        }

        /// <summary>Deletes a specific chargeback by its primary key</summary>
        /// <param name="id">The primary key of the chargeback</param>
        /// <returns>The result of the operation</returns>
        [HttpDelete]
        [UserAuthorize("Chargeback",Entitlements.Delete)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        [Route("{id:Guid}")]
        public IActionResult DeleteById([FromRoute] Guid id)
        {
            var status = _chargebackService.Delete(id);
            return Ok(new { status });
        }

        /// <summary>Updates a specific chargeback by its primary key</summary>
        /// <param name="id">The primary key of the chargeback</param>
        /// <param name="updatedEntity">The chargeback data to be updated</param>
        /// <returns>The result of the operation</returns>
        [HttpPut]
        [UserAuthorize("Chargeback",Entitlements.Update)]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public IActionResult UpdateById(Guid id, [FromBody] Chargeback updatedEntity)
        {
            if (id != updatedEntity.Id)
            {
                return BadRequest("Mismatched Id");
            }

            var status = _chargebackService.Update(id, updatedEntity);
            return Ok(new { status });
        }

        /// <summary>Updates a specific chargeback by its primary key</summary>
        /// <param name="id">The primary key of the chargeback</param>
        /// <param name="updatedEntity">The chargeback data to be updated</param>
        /// <returns>The result of the operation</returns>
        [HttpPatch]
        [UserAuthorize("Chargeback",Entitlements.Update)]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public IActionResult UpdateById(Guid id, [FromBody] JsonPatchDocument<Chargeback> updatedEntity)
        {
            if (updatedEntity == null)
                return BadRequest("Patch document is missing.");
            var status = _chargebackService.Patch(id, updatedEntity);
            return Ok(new { status });
        }
    }
}