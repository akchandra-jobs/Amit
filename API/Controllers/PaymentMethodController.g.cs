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
    /// Controller responsible for managing paymentmethod related operations.
    /// </summary>
    /// <remarks>
    /// This Controller provides endpoints for adding, retrieving, updating, and deleting paymentmethod information.
    /// </remarks>
    [Route("api/paymentmethod")]
    [Authorize]
    public class PaymentMethodController : ControllerBase
    {
        private readonly IPaymentMethodService _paymentMethodService;

        /// <summary>
        /// Initializes a new instance of the PaymentMethodController class with the specified context.
        /// </summary>
        /// <param name="ipaymentmethodservice">The ipaymentmethodservice to be used by the controller.</param>
        public PaymentMethodController(IPaymentMethodService ipaymentmethodservice)
        {
            _paymentMethodService = ipaymentmethodservice;
        }

        /// <summary>Adds a new paymentmethod</summary>
        /// <param name="model">The paymentmethod data to be added</param>
        /// <returns>The result of the operation</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        [UserAuthorize("PaymentMethod",Entitlements.Create)]
        public IActionResult Post([FromBody] PaymentMethod model)
        {
            var id = _paymentMethodService.Create(model);
            return Ok(new { id });
        }

        /// <summary>Retrieves a list of paymentmethods based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of paymentmethods</returns>
        [HttpGet]
        [UserAuthorize("PaymentMethod",Entitlements.Read)]
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

            var result = _paymentMethodService.Get(filters, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return Ok(result);
        }

        /// <summary>Retrieves a specific paymentmethod by its primary key</summary>
        /// <param name="id">The primary key of the paymentmethod</param>
        /// <returns>The paymentmethod data</returns>
        [HttpGet]
        [Route("{id:Guid}")]
        [UserAuthorize("PaymentMethod",Entitlements.Read)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var result = _paymentMethodService.GetById(id);
            return Ok(result);
        }

        /// <summary>Deletes a specific paymentmethod by its primary key</summary>
        /// <param name="id">The primary key of the paymentmethod</param>
        /// <returns>The result of the operation</returns>
        [HttpDelete]
        [UserAuthorize("PaymentMethod",Entitlements.Delete)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        [Route("{id:Guid}")]
        public IActionResult DeleteById([FromRoute] Guid id)
        {
            var status = _paymentMethodService.Delete(id);
            return Ok(new { status });
        }

        /// <summary>Updates a specific paymentmethod by its primary key</summary>
        /// <param name="id">The primary key of the paymentmethod</param>
        /// <param name="updatedEntity">The paymentmethod data to be updated</param>
        /// <returns>The result of the operation</returns>
        [HttpPut]
        [UserAuthorize("PaymentMethod",Entitlements.Update)]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public IActionResult UpdateById(Guid id, [FromBody] PaymentMethod updatedEntity)
        {
            if (id != updatedEntity.Id)
            {
                return BadRequest("Mismatched Id");
            }

            var status = _paymentMethodService.Update(id, updatedEntity);
            return Ok(new { status });
        }

        /// <summary>Updates a specific paymentmethod by its primary key</summary>
        /// <param name="id">The primary key of the paymentmethod</param>
        /// <param name="updatedEntity">The paymentmethod data to be updated</param>
        /// <returns>The result of the operation</returns>
        [HttpPatch]
        [UserAuthorize("PaymentMethod",Entitlements.Update)]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public IActionResult UpdateById(Guid id, [FromBody] JsonPatchDocument<PaymentMethod> updatedEntity)
        {
            if (updatedEntity == null)
                return BadRequest("Patch document is missing.");
            var status = _paymentMethodService.Patch(id, updatedEntity);
            return Ok(new { status });
        }
    }
}