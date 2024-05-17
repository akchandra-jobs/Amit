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
    /// Controller responsible for managing eventcategory related operations.
    /// </summary>
    /// <remarks>
    /// This Controller provides endpoints for adding, retrieving, updating, and deleting eventcategory information.
    /// </remarks>
    [Route("api/eventcategory")]
    [Authorize]
    public class EventCategoryController : ControllerBase
    {
        private readonly IEventCategoryService _eventCategoryService;

        /// <summary>
        /// Initializes a new instance of the EventCategoryController class with the specified context.
        /// </summary>
        /// <param name="ieventcategoryservice">The ieventcategoryservice to be used by the controller.</param>
        public EventCategoryController(IEventCategoryService ieventcategoryservice)
        {
            _eventCategoryService = ieventcategoryservice;
        }

        /// <summary>Adds a new eventcategory</summary>
        /// <param name="model">The eventcategory data to be added</param>
        /// <returns>The result of the operation</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        [UserAuthorize("EventCategory",Entitlements.Create)]
        public IActionResult Post([FromBody] EventCategory model)
        {
            var id = _eventCategoryService.Create(model);
            return Ok(new { id });
        }

        /// <summary>Retrieves a list of eventcategorys based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of eventcategorys</returns>
        [HttpGet]
        [UserAuthorize("EventCategory",Entitlements.Read)]
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

            var result = _eventCategoryService.Get(filters, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return Ok(result);
        }

        /// <summary>Retrieves a specific eventcategory by its primary key</summary>
        /// <param name="id">The primary key of the eventcategory</param>
        /// <returns>The eventcategory data</returns>
        [HttpGet]
        [Route("{id:Guid}")]
        [UserAuthorize("EventCategory",Entitlements.Read)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var result = _eventCategoryService.GetById(id);
            return Ok(result);
        }

        /// <summary>Deletes a specific eventcategory by its primary key</summary>
        /// <param name="id">The primary key of the eventcategory</param>
        /// <returns>The result of the operation</returns>
        [HttpDelete]
        [UserAuthorize("EventCategory",Entitlements.Delete)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        [Route("{id:Guid}")]
        public IActionResult DeleteById([FromRoute] Guid id)
        {
            var status = _eventCategoryService.Delete(id);
            return Ok(new { status });
        }

        /// <summary>Updates a specific eventcategory by its primary key</summary>
        /// <param name="id">The primary key of the eventcategory</param>
        /// <param name="updatedEntity">The eventcategory data to be updated</param>
        /// <returns>The result of the operation</returns>
        [HttpPut]
        [UserAuthorize("EventCategory",Entitlements.Update)]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public IActionResult UpdateById(Guid id, [FromBody] EventCategory updatedEntity)
        {
            if (id != updatedEntity.Id)
            {
                return BadRequest("Mismatched Id");
            }

            var status = _eventCategoryService.Update(id, updatedEntity);
            return Ok(new { status });
        }

        /// <summary>Updates a specific eventcategory by its primary key</summary>
        /// <param name="id">The primary key of the eventcategory</param>
        /// <param name="updatedEntity">The eventcategory data to be updated</param>
        /// <returns>The result of the operation</returns>
        [HttpPatch]
        [UserAuthorize("EventCategory",Entitlements.Update)]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public IActionResult UpdateById(Guid id, [FromBody] JsonPatchDocument<EventCategory> updatedEntity)
        {
            if (updatedEntity == null)
                return BadRequest("Patch document is missing.");
            var status = _eventCategoryService.Patch(id, updatedEntity);
            return Ok(new { status });
        }
    }
}