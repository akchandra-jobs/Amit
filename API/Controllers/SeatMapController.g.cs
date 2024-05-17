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
    /// Controller responsible for managing seatmap related operations.
    /// </summary>
    /// <remarks>
    /// This Controller provides endpoints for adding, retrieving, updating, and deleting seatmap information.
    /// </remarks>
    [Route("api/seatmap")]
    [Authorize]
    public class SeatMapController : ControllerBase
    {
        private readonly ISeatMapService _seatMapService;

        /// <summary>
        /// Initializes a new instance of the SeatMapController class with the specified context.
        /// </summary>
        /// <param name="iseatmapservice">The iseatmapservice to be used by the controller.</param>
        public SeatMapController(ISeatMapService iseatmapservice)
        {
            _seatMapService = iseatmapservice;
        }

        /// <summary>Adds a new seatmap</summary>
        /// <param name="model">The seatmap data to be added</param>
        /// <returns>The result of the operation</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        [UserAuthorize("SeatMap",Entitlements.Create)]
        public IActionResult Post([FromBody] SeatMap model)
        {
            var id = _seatMapService.Create(model);
            return Ok(new { id });
        }

        /// <summary>Retrieves a list of seatmaps based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of seatmaps</returns>
        [HttpGet]
        [UserAuthorize("SeatMap",Entitlements.Read)]
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

            var result = _seatMapService.Get(filters, searchTerm, pageNumber, pageSize, sortField, sortOrder);
            return Ok(result);
        }

        /// <summary>Retrieves a specific seatmap by its primary key</summary>
        /// <param name="id">The primary key of the seatmap</param>
        /// <returns>The seatmap data</returns>
        [HttpGet]
        [Route("{id:Guid}")]
        [UserAuthorize("SeatMap",Entitlements.Read)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var result = _seatMapService.GetById(id);
            return Ok(result);
        }

        /// <summary>Deletes a specific seatmap by its primary key</summary>
        /// <param name="id">The primary key of the seatmap</param>
        /// <returns>The result of the operation</returns>
        [HttpDelete]
        [UserAuthorize("SeatMap",Entitlements.Delete)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        [Route("{id:Guid}")]
        public IActionResult DeleteById([FromRoute] Guid id)
        {
            var status = _seatMapService.Delete(id);
            return Ok(new { status });
        }

        /// <summary>Updates a specific seatmap by its primary key</summary>
        /// <param name="id">The primary key of the seatmap</param>
        /// <param name="updatedEntity">The seatmap data to be updated</param>
        /// <returns>The result of the operation</returns>
        [HttpPut]
        [UserAuthorize("SeatMap",Entitlements.Update)]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public IActionResult UpdateById(Guid id, [FromBody] SeatMap updatedEntity)
        {
            if (id != updatedEntity.Id)
            {
                return BadRequest("Mismatched Id");
            }

            var status = _seatMapService.Update(id, updatedEntity);
            return Ok(new { status });
        }

        /// <summary>Updates a specific seatmap by its primary key</summary>
        /// <param name="id">The primary key of the seatmap</param>
        /// <param name="updatedEntity">The seatmap data to be updated</param>
        /// <returns>The result of the operation</returns>
        [HttpPatch]
        [UserAuthorize("SeatMap",Entitlements.Update)]
        [Route("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public IActionResult UpdateById(Guid id, [FromBody] JsonPatchDocument<SeatMap> updatedEntity)
        {
            if (updatedEntity == null)
                return BadRequest("Patch document is missing.");
            var status = _seatMapService.Patch(id, updatedEntity);
            return Ok(new { status });
        }
    }
}