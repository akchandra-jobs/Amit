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
    /// Controller responsible for managing billingaddress related operations.
    /// </summary>
    /// <remarks>
    /// This Controller provides endpoints for adding, retrieving, updating, and deleting billingaddress information.
    /// </remarks>
    [Route("api/billingaddress")]
    [Authorize]
    public class BillingAddressController : ControllerBase
    {
        private readonly IBillingAddressService _billingAddressService;

        /// <summary>
        /// Initializes a new instance of the BillingAddressController class with the specified context.
        /// </summary>
        /// <param name="ibillingaddressservice">The ibillingaddressservice to be used by the controller.</param>
        public BillingAddressController(IBillingAddressService ibillingaddressservice)
        {
            _billingAddressService = ibillingaddressservice;
        }

        /// <summary>Adds a new billingaddress</summary>
        /// <param name="model">The billingaddress data to be added</param>
        /// <returns>The result of the operation</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        [UserAuthorize("BillingAddress",Entitlements.Create)]
        public IActionResult Post([FromBody] BillingAddress model)
        {
            var id = _billingAddressService.Create(model);
            return Ok(new { id });
        }

        /// <summary>Retrieves a list of billingaddresss based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of billingaddresss</returns>
        [HttpGet]
        [UserAuthorize("BillingAddress",Entitlements.Read)]
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

            var result = _billingAddressService.Get(filters, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return Ok(result);
        }

        /// <summary>Retrieves a specific billingaddress by its primary key</summary>
        /// <param name="id">The primary key of the billingaddress</param>
        /// <returns>The billingaddress data</returns>
        [HttpGet]
        [Route("{id:Guid}")]
        [UserAuthorize("BillingAddress",Entitlements.Read)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var result = _billingAddressService.GetById(id);
            return Ok(result);
        }

        /// <summary>Deletes a specific billingaddress by its primary key</summary>
        /// <param name="id">The primary key of the billingaddress</param>
        /// <returns>The result of the operation</returns>
        [HttpDelete]
        [UserAuthorize("BillingAddress",Entitlements.Delete)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        [Route("{id:Guid}")]
        public IActionResult DeleteById([FromRoute] Guid id)
        {
            var status = _billingAddressService.Delete(id);
            return Ok(new { status });
        }

        /// <summary>Updates a specific billingaddress by its primary key</summary>
        /// <param name="id">The primary key of the billingaddress</param>
        /// <param name="updatedEntity">The billingaddress data to be updated</param>
        /// <returns>The result of the operation</returns>
        [HttpPut]
        [UserAuthorize("BillingAddress",Entitlements.Update)]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public IActionResult UpdateById(Guid id, [FromBody] BillingAddress updatedEntity)
        {
            if (id != updatedEntity.Id)
            {
                return BadRequest("Mismatched Id");
            }

            var status = _billingAddressService.Update(id, updatedEntity);
            return Ok(new { status });
        }

        /// <summary>Updates a specific billingaddress by its primary key</summary>
        /// <param name="id">The primary key of the billingaddress</param>
        /// <param name="updatedEntity">The billingaddress data to be updated</param>
        /// <returns>The result of the operation</returns>
        [HttpPatch]
        [UserAuthorize("BillingAddress",Entitlements.Update)]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public IActionResult UpdateById(Guid id, [FromBody] JsonPatchDocument<BillingAddress> updatedEntity)
        {
            if (updatedEntity == null)
                return BadRequest("Patch document is missing.");
            var status = _billingAddressService.Patch(id, updatedEntity);
            return Ok(new { status });
        }
    }
}