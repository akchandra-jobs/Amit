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
    /// The refundService responsible for managing refund related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting refund information.
    /// </remarks>
    public interface IRefundService
    {
        /// <summary>Retrieves a specific refund by its primary key</summary>
        /// <param name="id">The primary key of the refund</param>
        /// <returns>The refund data</returns>
        Refund GetById(Guid id);

        /// <summary>Retrieves a list of refunds based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of refunds</returns>
        List<Refund> Get(string filters, string searchTerm, int pageNumber = 1, int pageSize = 0, string sortField = null, string sortOrder = "asc");

        /// <summary>Adds a new refund</summary>
        /// <param name="model">The refund data to be added</param>
        /// <returns>The result of the operation</returns>
        Guid Create(Refund model);

        /// <summary>Updates a specific refund by its primary key</summary>
        /// <param name="id">The primary key of the refund</param>
        /// <param name="updatedEntity">The refund data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Update(Guid id, Refund updatedEntity);

        /// <summary>Updates a specific refund by its primary key</summary>
        /// <param name="id">The primary key of the refund</param>
        /// <param name="updatedEntity">The refund data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Patch(Guid id, JsonPatchDocument<Refund> updatedEntity);

        /// <summary>Deletes a specific refund by its primary key</summary>
        /// <param name="id">The primary key of the refund</param>
        /// <returns>The result of the operation</returns>
        bool Delete(Guid id);
    }

    /// <summary>
    /// The refundService responsible for managing refund related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting refund information.
    /// </remarks>
    public class RefundService : IRefundService
    {
        private AmitContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the Refund class.
        /// </summary>
        /// <param name="dbContext">dbContext value to set.</param>
        public RefundService(AmitContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>Retrieves a specific refund by its primary key</summary>
        /// <param name="id">The primary key of the refund</param>
        /// <returns>The refund data</returns>
        public Refund GetById(Guid id)
        {
            var entityData = _dbContext.Refund.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            return entityData;
        }

        /// <summary>Retrieves a list of refunds based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of refunds</returns>/// <exception cref="Exception"></exception>
        public List<Refund> Get(string filters, string searchTerm, int pageNumber, int pageSize, string sortField, string sortOrder)
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

            var query = _dbContext.Refund.IncludeRelated().AsQueryable();
            int skip = (pageNumber - 1) * pageSize;
            var result = FilterService<Refund>.ApplyFilter(query, filterCriteria, searchTerm);
            if (!string.IsNullOrEmpty(sortField))
            {
                var parameter = Expression.Parameter(typeof(Refund), "b");
                var property = Expression.Property(parameter, sortField);
                var lambda = Expression.Lambda<Func<Refund, object>>(Expression.Convert(property, typeof(object)), parameter);
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

        /// <summary>Adds a new refund</summary>
        /// <param name="model">The refund data to be added</param>
        /// <returns>The result of the operation</returns>
        public Guid Create(Refund model)
        {
            _dbContext.Refund.Add(model);
            _dbContext.SaveChanges();
            return model.Id;
        }

        /// <summary>Updates a specific refund by its primary key</summary>
        /// <param name="id">The primary key of the refund</param>
        /// <param name="updatedEntity">The refund data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Update(Guid id, Refund updatedEntity)
        {
            _dbContext.Refund.Update(updatedEntity);
            _dbContext.SaveChanges();
            return true;
        }

        /// <summary>Updates a specific refund by its primary key</summary>
        /// <param name="id">The primary key of the refund</param>
        /// <param name="updatedEntity">The refund data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Patch(Guid id, JsonPatchDocument<Refund> updatedEntity)
        {
            if (updatedEntity == null)
            {
                throw new Exception("Patch document is missing!");
            }

            var existingEntity = _dbContext.Refund.FirstOrDefault(t => t.Id == id);
            if (existingEntity == null)
            {
                throw new Exception("No data found!");
            }

            updatedEntity.ApplyTo(existingEntity);
            _dbContext.Refund.Update(existingEntity);
            _dbContext.SaveChanges();
            return true;
        }

        /// <summary>Deletes a specific refund by its primary key</summary>
        /// <param name="id">The primary key of the refund</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Delete(Guid id)
        {
            var entityData = _dbContext.Refund.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            if (entityData == null)
            {
                throw new Exception("No data found!");
            }

            _dbContext.Refund.Remove(entityData);
            _dbContext.SaveChanges();
            return true;
        }
    }
}