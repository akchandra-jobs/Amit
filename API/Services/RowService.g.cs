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
    /// The rowService responsible for managing row related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting row information.
    /// </remarks>
    public interface IRowService
    {
        /// <summary>Retrieves a specific row by its primary key</summary>
        /// <param name="id">The primary key of the row</param>
        /// <returns>The row data</returns>
        Row GetById(Guid id);

        /// <summary>Retrieves a list of rows based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of rows</returns>
        List<Row> Get(string filters, string searchTerm, int pageNumber = 1, int pageSize = 0, string sortField = null, string sortOrder = "asc");

        /// <summary>Adds a new row</summary>
        /// <param name="model">The row data to be added</param>
        /// <returns>The result of the operation</returns>
        Guid Create(Row model);

        /// <summary>Updates a specific row by its primary key</summary>
        /// <param name="id">The primary key of the row</param>
        /// <param name="updatedEntity">The row data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Update(Guid id, Row updatedEntity);

        /// <summary>Updates a specific row by its primary key</summary>
        /// <param name="id">The primary key of the row</param>
        /// <param name="updatedEntity">The row data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Patch(Guid id, JsonPatchDocument<Row> updatedEntity);

        /// <summary>Deletes a specific row by its primary key</summary>
        /// <param name="id">The primary key of the row</param>
        /// <returns>The result of the operation</returns>
        bool Delete(Guid id);
    }

    /// <summary>
    /// The rowService responsible for managing row related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting row information.
    /// </remarks>
    public class RowService : IRowService
    {
        private AmitContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the Row class.
        /// </summary>
        /// <param name="dbContext">dbContext value to set.</param>
        public RowService(AmitContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>Retrieves a specific row by its primary key</summary>
        /// <param name="id">The primary key of the row</param>
        /// <returns>The row data</returns>
        public Row GetById(Guid id)
        {
            var entityData = _dbContext.Row.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            return entityData;
        }

        /// <summary>Retrieves a list of rows based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of rows</returns>/// <exception cref="Exception"></exception>
        public List<Row> Get(string filters, string searchTerm, int pageNumber, int pageSize, string sortField, string sortOrder)
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

            var query = _dbContext.Row.IncludeRelated().AsQueryable();
            int skip = (pageNumber - 1) * pageSize;
            var result = FilterService<Row>.ApplyFilter(query, filterCriteria, searchTerm);
            if (!string.IsNullOrEmpty(sortField))
            {
                var parameter = Expression.Parameter(typeof(Row), "b");
                var property = Expression.Property(parameter, sortField);
                var lambda = Expression.Lambda<Func<Row, object>>(Expression.Convert(property, typeof(object)), parameter);
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

        /// <summary>Adds a new row</summary>
        /// <param name="model">The row data to be added</param>
        /// <returns>The result of the operation</returns>
        public Guid Create(Row model)
        {
            _dbContext.Row.Add(model);
            _dbContext.SaveChanges();
            return model.Id;
        }

        /// <summary>Updates a specific row by its primary key</summary>
        /// <param name="id">The primary key of the row</param>
        /// <param name="updatedEntity">The row data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Update(Guid id, Row updatedEntity)
        {
            _dbContext.Row.Update(updatedEntity);
            _dbContext.SaveChanges();
            return true;
        }

        /// <summary>Updates a specific row by its primary key</summary>
        /// <param name="id">The primary key of the row</param>
        /// <param name="updatedEntity">The row data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Patch(Guid id, JsonPatchDocument<Row> updatedEntity)
        {
            if (updatedEntity == null)
            {
                throw new Exception("Patch document is missing!");
            }

            var existingEntity = _dbContext.Row.FirstOrDefault(t => t.Id == id);
            if (existingEntity == null)
            {
                throw new Exception("No data found!");
            }

            updatedEntity.ApplyTo(existingEntity);
            _dbContext.Row.Update(existingEntity);
            _dbContext.SaveChanges();
            return true;
        }

        /// <summary>Deletes a specific row by its primary key</summary>
        /// <param name="id">The primary key of the row</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Delete(Guid id)
        {
            var entityData = _dbContext.Row.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            if (entityData == null)
            {
                throw new Exception("No data found!");
            }

            _dbContext.Row.Remove(entityData);
            _dbContext.SaveChanges();
            return true;
        }
    }
}