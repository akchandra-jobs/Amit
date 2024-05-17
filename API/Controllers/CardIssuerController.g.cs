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
    /// Controller responsible for managing cardissuer related operations.
    /// </summary>
    /// <remarks>
    /// This Controller provides endpoints for adding, retrieving, updating, and deleting cardissuer information.
    /// </remarks>
    [Route("api/cardissuer")]
    [Authorize]
    public class CardIssuerController : ControllerBase
    {
        private readonly ICardIssuerService _cardIssuerService;

        /// <summary>
        /// Initializes a new instance of the CardIssuerController class with the specified context.
        /// </summary>
        /// <param name="icardissuerservice">The icardissuerservice to be used by the controller.</param>
        public CardIssuerController(ICardIssuerService icardissuerservice)
        {
            _cardIssuerService = icardissuerservice;
        }

        /// <summary>Adds a new cardissuer</summary>
        /// <param name="model">The cardissuer data to be added</param>
        /// <returns>The result of the operation</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        [UserAuthorize("CardIssuer",Entitlements.Create)]
        public IActionResult Post([FromBody] CardIssuer model)
        {
            var id = _cardIssuerService.Create(model);
            return Ok(new { id });
        }

        /// <summary>Retrieves a list of cardissuers based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of cardissuers</returns>
        [HttpGet]
        [UserAuthorize("CardIssuer",Entitlements.Read)]
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

            var result = _cardIssuerService.Get(filters, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return Ok(result);
        }

        /// <summary>Retrieves a specific cardissuer by its primary key</summary>
        /// <param name="id">The primary key of the cardissuer</param>
        /// <returns>The cardissuer data</returns>
        [HttpGet]
        [Route("{id:Guid}")]
        [UserAuthorize("CardIssuer",Entitlements.Read)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var result = _cardIssuerService.GetById(id);
            return Ok(result);
        }

        /// <summary>Deletes a specific cardissuer by its primary key</summary>
        /// <param name="id">The primary key of the cardissuer</param>
        /// <returns>The result of the operation</returns>
        [HttpDelete]
        [UserAuthorize("CardIssuer",Entitlements.Delete)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        [Route("{id:Guid}")]
        public IActionResult DeleteById([FromRoute] Guid id)
        {
            var status = _cardIssuerService.Delete(id);
            return Ok(new { status });
        }

        /// <summary>Updates a specific cardissuer by its primary key</summary>
        /// <param name="id">The primary key of the cardissuer</param>
        /// <param name="updatedEntity">The cardissuer data to be updated</param>
        /// <returns>The result of the operation</returns>
        [HttpPut]
        [UserAuthorize("CardIssuer",Entitlements.Update)]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public IActionResult UpdateById(Guid id, [FromBody] CardIssuer updatedEntity)
        {
            if (id != updatedEntity.Id)
            {
                return BadRequest("Mismatched Id");
            }

            var status = _cardIssuerService.Update(id, updatedEntity);
            return Ok(new { status });
        }

        /// <summary>Updates a specific cardissuer by its primary key</summary>
        /// <param name="id">The primary key of the cardissuer</param>
        /// <param name="updatedEntity">The cardissuer data to be updated</param>
        /// <returns>The result of the operation</returns>
        [HttpPatch]
        [UserAuthorize("CardIssuer",Entitlements.Update)]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public IActionResult UpdateById(Guid id, [FromBody] JsonPatchDocument<CardIssuer> updatedEntity)
        {
            if (updatedEntity == null)
                return BadRequest("Patch document is missing.");
            var status = _cardIssuerService.Patch(id, updatedEntity);
            return Ok(new { status });
        }
    }
}