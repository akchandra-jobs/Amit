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
    /// Controller responsible for managing customer related operations.
    /// </summary>
    /// <remarks>
    /// This Controller provides endpoints for adding, retrieving, updating, and deleting customer information.
    /// </remarks>
    [Route("api/customer")]
    [Authorize]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        /// <summary>
        /// Initializes a new instance of the CustomerController class with the specified context.
        /// </summary>
        /// <param name="icustomerservice">The icustomerservice to be used by the controller.</param>
        public CustomerController(ICustomerService icustomerservice)
        {
            _customerService = icustomerservice;
        }

        /// <summary>Adds a new customer</summary>
        /// <param name="model">The customer data to be added</param>
        /// <returns>The result of the operation</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        [UserAuthorize("Customer",Entitlements.Create)]
        public IActionResult Post([FromBody] Customer model)
        {
            var id = _customerService.Create(model);
            return Ok(new { id });
        }

        /// <summary>Retrieves a list of customers based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of customers</returns>
        [HttpGet]
        [UserAuthorize("Customer",Entitlements.Read)]
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

            var result = _customerService.Get(filters, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return Ok(result);
        }

        /// <summary>Retrieves a specific customer by its primary key</summary>
        /// <param name="id">The primary key of the customer</param>
        /// <returns>The customer data</returns>
        [HttpGet]
        [Route("{id:Guid}")]
        [UserAuthorize("Customer",Entitlements.Read)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var result = _customerService.GetById(id);
            return Ok(result);
        }

        /// <summary>Deletes a specific customer by its primary key</summary>
        /// <param name="id">The primary key of the customer</param>
        /// <returns>The result of the operation</returns>
        [HttpDelete]
        [UserAuthorize("Customer",Entitlements.Delete)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        [Route("{id:Guid}")]
        public IActionResult DeleteById([FromRoute] Guid id)
        {
            var status = _customerService.Delete(id);
            return Ok(new { status });
        }

        /// <summary>Updates a specific customer by its primary key</summary>
        /// <param name="id">The primary key of the customer</param>
        /// <param name="updatedEntity">The customer data to be updated</param>
        /// <returns>The result of the operation</returns>
        [HttpPut]
        [UserAuthorize("Customer",Entitlements.Update)]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public IActionResult UpdateById(Guid id, [FromBody] Customer updatedEntity)
        {
            if (id != updatedEntity.Id)
            {
                return BadRequest("Mismatched Id");
            }

            var status = _customerService.Update(id, updatedEntity);
            return Ok(new { status });
        }

        /// <summary>Updates a specific customer by its primary key</summary>
        /// <param name="id">The primary key of the customer</param>
        /// <param name="updatedEntity">The customer data to be updated</param>
        /// <returns>The result of the operation</returns>
        [HttpPatch]
        [UserAuthorize("Customer",Entitlements.Update)]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public IActionResult UpdateById(Guid id, [FromBody] JsonPatchDocument<Customer> updatedEntity)
        {
            if (updatedEntity == null)
                return BadRequest("Patch document is missing.");
            var status = _customerService.Patch(id, updatedEntity);
            return Ok(new { status });
        }
    }
}