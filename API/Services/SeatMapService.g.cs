using Amit.Models;
using Amit.Data;
using Amit.Filter;
using Amit.Entities;
using Amit.Logger;
using Microsoft.AspNetCore.JsonPatch;
using System.Linq.Expressions;

namespace Amit.Services
{
    /// <summary>
    /// The seatmapService responsible for managing seatmap related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting seatmap information.
    /// </remarks>
    public interface ISeatMapService
    {
        /// <summary>Retrieves a specific seatmap by its primary key</summary>
        /// <param name="id">The primary key of the seatmap</param>
        /// <returns>The seatmap data</returns>
        SeatMap GetById(Guid id);

        /// <summary>Retrieves a list of seatmaps based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of seatmaps</returns>
        List<SeatMap> Get(string filters, string searchTerm, int pageNumber = 1, int pageSize = 0, string sortField = null, string sortOrder = "asc");

        /// <summary>Adds a new seatmap</summary>
        /// <param name="model">The seatmap data to be added</param>
        /// <returns>The result of the operation</returns>
        Guid Create(SeatMap model);

        /// <summary>Updates a specific seatmap by its primary key</summary>
        /// <param name="id">The primary key of the seatmap</param>
        /// <param name="updatedEntity">The seatmap data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Update(Guid id, SeatMap updatedEntity);

        /// <summary>Updates a specific seatmap by its primary key</summary>
        /// <param name="id">The primary key of the seatmap</param>
        /// <param name="updatedEntity">The seatmap data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Patch(Guid id, JsonPatchDocument<SeatMap> updatedEntity);

        /// <summary>Deletes a specific seatmap by its primary key</summary>
        /// <param name="id">The primary key of the seatmap</param>
        /// <returns>The result of the operation</returns>
        bool Delete(Guid id);
    }

    /// <summary>
    /// The seatmapService responsible for managing seatmap related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting seatmap information.
    /// </remarks>
    public class SeatMapService : ISeatMapService
    {
        private AmitContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the SeatMap class.
        /// </summary>
        /// <param name="dbContext">dbContext value to set.</param>
        public SeatMapService(AmitContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>Retrieves a specific seatmap by its primary key</summary>
        /// <param name="id">The primary key of the seatmap</param>
        /// <returns>The seatmap data</returns>
        public SeatMap GetById(Guid id)
        {
            var entityData = _dbContext.SeatMap.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            return entityData;
        }

        /// <summary>Retrieves a list of seatmaps based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of seatmaps</returns>/// <exception cref="Exception"></exception>
        public List<SeatMap> Get(string filters, string searchTerm, int pageNumber, int pageSize, string sortField, string sortOrder)
        {
            List<FilterCriteria> filterCriteria = null;
            if (pageSize < 1)
            {
                throw new Exception("Page size invalid!");
            }

            if (pageNumber < 1)
            {
                throw new Exception("Page mumber invalid!");
            }

            if (!string.IsNullOrEmpty(filters))
            {
                filterCriteria = JsonHelper.Deserialize<List<FilterCriteria>>(filters);
            }

            var query = _dbContext.SeatMap.IncludeRelated().AsQueryable();
            int skip = (pageNumber - 1) * pageSize;
            var result = FilterService<SeatMap>.ApplyFilter(query, filterCriteria, searchTerm);
            if (!string.IsNullOrEmpty(sortField))
            {
                var parameter = Expression.Parameter(typeof(SeatMap), "b");
                var property = Expression.Property(parameter, sortField);
                var lambda = Expression.Lambda<Func<SeatMap, object>>(Expression.Convert(property, typeof(object)), parameter);
                if (sortOrder.Equals("asc", StringComparison.OrdinalIgnoreCase))
                {
                    result = result.OrderBy(lambda);
                }
                else if (sortOrder.Equals("desc", StringComparison.OrdinalIgnoreCase))
                {
                    result = result.OrderByDescending(lambda);
                }
                else
                {
                    throw new Exception("Invalid sort order. Use 'asc' or 'desc'");
                }
            }

            var paginatedResult = result.Skip(skip).Take(pageSize).ToList();
            return paginatedResult;
        }

        /// <summary>Adds a new seatmap</summary>
        /// <param name="model">The seatmap data to be added</param>
        /// <returns>The result of the operation</returns>
        public Guid Create(SeatMap model)
        {
            _dbContext.SeatMap.Add(model);
            _dbContext.SaveChanges();
            return model.Id;
        }

        /// <summary>Updates a specific seatmap by its primary key</summary>
        /// <param name="id">The primary key of the seatmap</param>
        /// <param name="updatedEntity">The seatmap data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Update(Guid id, SeatMap updatedEntity)
        {
            _dbContext.SeatMap.Update(updatedEntity);
            _dbContext.SaveChanges();
            return true;
        }

        /// <summary>Updates a specific seatmap by its primary key</summary>
        /// <param name="id">The primary key of the seatmap</param>
        /// <param name="updatedEntity">The seatmap data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Patch(Guid id, JsonPatchDocument<SeatMap> updatedEntity)
        {
            if (updatedEntity == null)
            {
                throw new Exception("Patch document is missing!");
            }

            var existingEntity = _dbContext.SeatMap.FirstOrDefault(t => t.Id == id);
            if (existingEntity == null)
            {
                throw new Exception("No data found!");
            }

            updatedEntity.ApplyTo(existingEntity);
            _dbContext.SeatMap.Update(existingEntity);
            _dbContext.SaveChanges();
            return true;
        }

        /// <summary>Deletes a specific seatmap by its primary key</summary>
        /// <param name="id">The primary key of the seatmap</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Delete(Guid id)
        {
            var entityData = _dbContext.SeatMap.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            if (entityData == null)
            {
                throw new Exception("No data found!");
            }

            _dbContext.SeatMap.Remove(entityData);
            _dbContext.SaveChanges();
            return true;
        }
    }
}