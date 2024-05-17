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
    /// Controller responsible for managing paymenttransaction related operations.
    /// </summary>
    /// <remarks>
    /// This Controller provides endpoints for adding, retrieving, updating, and deleting paymenttransaction information.
    /// </remarks>
    [Route("api/paymenttransaction")]
    [Authorize]
    public class PaymentTransactionController : ControllerBase
    {
        private readonly IPaymentTransactionService _paymentTransactionService;

        /// <summary>
        /// Initializes a new instance of the PaymentTransactionController class with the specified context.
        /// </summary>
        /// <param name="ipaymenttransactionservice">The ipaymenttransactionservice to be used by the controller.</param>
        public PaymentTransactionController(IPaymentTransactionService ipaymenttransactionservice)
        {
            _paymentTransactionService = ipaymenttransactionservice;
        }

        /// <summary>Adds a new paymenttransaction</summary>
        /// <param name="model">The paymenttransaction data to be added</param>
        /// <returns>The result of the operation</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        [UserAuthorize("PaymentTransaction",Entitlements.Create)]
        public IActionResult Post([FromBody] PaymentTransaction model)
        {
            var id = _paymentTransactionService.Create(model);
            return Ok(new { id });
        }

        /// <summary>Retrieves a list of paymenttransactions based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of paymenttransactions</returns>
        [HttpGet]
        [UserAuthorize("PaymentTransaction",Entitlements.Read)]
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

            var result = _paymentTransactionService.Get(filters, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return Ok(result);
        }

        /// <summary>Retrieves a specific paymenttransaction by its primary key</summary>
        /// <param name="id">The primary key of the paymenttransaction</param>
        /// <returns>The paymenttransaction data</returns>
        [HttpGet]
        [Route("{id:Guid}")]
        [UserAuthorize("PaymentTransaction",Entitlements.Read)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var result = _paymentTransactionService.GetById(id);
            return Ok(result);
        }

        /// <summary>Deletes a specific paymenttransaction by its primary key</summary>
        /// <param name="id">The primary key of the paymenttransaction</param>
        /// <returns>The result of the operation</returns>
        [HttpDelete]
        [UserAuthorize("PaymentTransaction",Entitlements.Delete)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        [Route("{id:Guid}")]
        public IActionResult DeleteById([FromRoute] Guid id)
        {
            var status = _paymentTransactionService.Delete(id);
            return Ok(new { status });
        }

        /// <summary>Updates a specific paymenttransaction by its primary key</summary>
        /// <param name="id">The primary key of the paymenttransaction</param>
        /// <param name="updatedEntity">The paymenttransaction data to be updated</param>
        /// <returns>The result of the operation</returns>
        [HttpPut]
        [UserAuthorize("PaymentTransaction",Entitlements.Update)]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public IActionResult UpdateById(Guid id, [FromBody] PaymentTransaction updatedEntity)
        {
            if (id != updatedEntity.Id)
            {
                return BadRequest("Mismatched Id");
            }

            var status = _paymentTransactionService.Update(id, updatedEntity);
            return Ok(new { status });
        }

        /// <summary>Updates a specific paymenttransaction by its primary key</summary>
        /// <param name="id">The primary key of the paymenttransaction</param>
        /// <param name="updatedEntity">The paymenttransaction data to be updated</param>
        /// <returns>The result of the operation</returns>
        [HttpPatch]
        [UserAuthorize("PaymentTransaction",Entitlements.Update)]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public IActionResult UpdateById(Guid id, [FromBody] JsonPatchDocument<PaymentTransaction> updatedEntity)
        {
            if (updatedEntity == null)
                return BadRequest("Patch document is missing.");
            var status = _paymentTransactionService.Patch(id, updatedEntity);
            return Ok(new { status });
        }
    }
}