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
    /// Controller responsible for managing merchantaccount related operations.
    /// </summary>
    /// <remarks>
    /// This Controller provides endpoints for adding, retrieving, updating, and deleting merchantaccount information.
    /// </remarks>
    [Route("api/merchantaccount")]
    [Authorize]
    public class MerchantAccountController : ControllerBase
    {
        private readonly IMerchantAccountService _merchantAccountService;

        /// <summary>
        /// Initializes a new instance of the MerchantAccountController class with the specified context.
        /// </summary>
        /// <param name="imerchantaccountservice">The imerchantaccountservice to be used by the controller.</param>
        public MerchantAccountController(IMerchantAccountService imerchantaccountservice)
        {
            _merchantAccountService = imerchantaccountservice;
        }

        /// <summary>Adds a new merchantaccount</summary>
        /// <param name="model">The merchantaccount data to be added</param>
        /// <returns>The result of the operation</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        [UserAuthorize("MerchantAccount",Entitlements.Create)]
        public IActionResult Post([FromBody] MerchantAccount model)
        {
            var id = _merchantAccountService.Create(model);
            return Ok(new { id });
        }

        /// <summary>Retrieves a list of merchantaccounts based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of merchantaccounts</returns>
        [HttpGet]
        [UserAuthorize("MerchantAccount",Entitlements.Read)]
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

            var result = _merchantAccountService.Get(filters, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return Ok(result);
        }

        /// <summary>Retrieves a specific merchantaccount by its primary key</summary>
        /// <param name="id">The primary key of the merchantaccount</param>
        /// <returns>The merchantaccount data</returns>
        [HttpGet]
        [Route("{id:Guid}")]
        [UserAuthorize("MerchantAccount",Entitlements.Read)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var result = _merchantAccountService.GetById(id);
            return Ok(result);
        }

        /// <summary>Deletes a specific merchantaccount by its primary key</summary>
        /// <param name="id">The primary key of the merchantaccount</param>
        /// <returns>The result of the operation</returns>
        [HttpDelete]
        [UserAuthorize("MerchantAccount",Entitlements.Delete)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        [Route("{id:Guid}")]
        public IActionResult DeleteById([FromRoute] Guid id)
        {
            var status = _merchantAccountService.Delete(id);
            return Ok(new { status });
        }

        /// <summary>Updates a specific merchantaccount by its primary key</summary>
        /// <param name="id">The primary key of the merchantaccount</param>
        /// <param name="updatedEntity">The merchantaccount data to be updated</param>
        /// <returns>The result of the operation</returns>
        [HttpPut]
        [UserAuthorize("MerchantAccount",Entitlements.Update)]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public IActionResult UpdateById(Guid id, [FromBody] MerchantAccount updatedEntity)
        {
            if (id != updatedEntity.Id)
            {
                return BadRequest("Mismatched Id");
            }

            var status = _merchantAccountService.Update(id, updatedEntity);
            return Ok(new { status });
        }

        /// <summary>Updates a specific merchantaccount by its primary key</summary>
        /// <param name="id">The primary key of the merchantaccount</param>
        /// <param name="updatedEntity">The merchantaccount data to be updated</param>
        /// <returns>The result of the operation</returns>
        [HttpPatch]
        [UserAuthorize("MerchantAccount",Entitlements.Update)]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public IActionResult UpdateById(Guid id, [FromBody] JsonPatchDocument<MerchantAccount> updatedEntity)
        {
            if (updatedEntity == null)
                return BadRequest("Patch document is missing.");
            var status = _merchantAccountService.Patch(id, updatedEntity);
            return Ok(new { status });
        }
    }
}