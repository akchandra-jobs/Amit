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
    /// The refundpolicyService responsible for managing refundpolicy related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting refundpolicy information.
    /// </remarks>
    public interface IRefundPolicyService
    {
        /// <summary>Retrieves a specific refundpolicy by its primary key</summary>
        /// <param name="id">The primary key of the refundpolicy</param>
        /// <returns>The refundpolicy data</returns>
        RefundPolicy GetById(Guid id);

        /// <summary>Retrieves a list of refundpolicys based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of refundpolicys</returns>
        List<RefundPolicy> Get(string filters, string searchTerm, int pageNumber = 1, int pageSize = 0, string sortField = null, string sortOrder = "asc");

        /// <summary>Adds a new refundpolicy</summary>
        /// <param name="model">The refundpolicy data to be added</param>
        /// <returns>The result of the operation</returns>
        Guid Create(RefundPolicy model);

        /// <summary>Updates a specific refundpolicy by its primary key</summary>
        /// <param name="id">The primary key of the refundpolicy</param>
        /// <param name="updatedEntity">The refundpolicy data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Update(Guid id, RefundPolicy updatedEntity);

        /// <summary>Updates a specific refundpolicy by its primary key</summary>
        /// <param name="id">The primary key of the refundpolicy</param>
        /// <param name="updatedEntity">The refundpolicy data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Patch(Guid id, JsonPatchDocument<RefundPolicy> updatedEntity);

        /// <summary>Deletes a specific refundpolicy by its primary key</summary>
        /// <param name="id">The primary key of the refundpolicy</param>
        /// <returns>The result of the operation</returns>
        bool Delete(Guid id);
    }

    /// <summary>
    /// The refundpolicyService responsible for managing refundpolicy related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting refundpolicy information.
    /// </remarks>
    public class RefundPolicyService : IRefundPolicyService
    {
        private AmitContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the RefundPolicy class.
        /// </summary>
        /// <param name="dbContext">dbContext value to set.</param>
        public RefundPolicyService(AmitContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>Retrieves a specific refundpolicy by its primary key</summary>
        /// <param name="id">The primary key of the refundpolicy</param>
        /// <returns>The refundpolicy data</returns>
        public RefundPolicy GetById(Guid id)
        {
            var entityData = _dbContext.RefundPolicy.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            return entityData;
        }

        /// <summary>Retrieves a list of refundpolicys based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of refundpolicys</returns>/// <exception cref="Exception"></exception>
        public List<RefundPolicy> Get(string filters, string searchTerm, int pageNumber, int pageSize, string sortField, string sortOrder)
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

            var query = _dbContext.RefundPolicy.IncludeRelated().AsQueryable();
            int skip = (pageNumber - 1) * pageSize;
            var result = FilterService<RefundPolicy>.ApplyFilter(query, filterCriteria, searchTerm);
            if (!string.IsNullOrEmpty(sortField))
            {
                var parameter = Expression.Parameter(typeof(RefundPolicy), "b");
                var property = Expression.Property(parameter, sortField);
                var lambda = Expression.Lambda<Func<RefundPolicy, object>>(Expression.Convert(property, typeof(object)), parameter);
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

        /// <summary>Adds a new refundpolicy</summary>
        /// <param name="model">The refundpolicy data to be added</param>
        /// <returns>The result of the operation</returns>
        public Guid Create(RefundPolicy model)
        {
            _dbContext.RefundPolicy.Add(model);
            _dbContext.SaveChanges();
            return model.Id;
        }

        /// <summary>Updates a specific refundpolicy by its primary key</summary>
        /// <param name="id">The primary key of the refundpolicy</param>
        /// <param name="updatedEntity">The refundpolicy data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Update(Guid id, RefundPolicy updatedEntity)
        {
            _dbContext.RefundPolicy.Update(updatedEntity);
            _dbContext.SaveChanges();
            return true;
        }

        /// <summary>Updates a specific refundpolicy by its primary key</summary>
        /// <param name="id">The primary key of the refundpolicy</param>
        /// <param name="updatedEntity">The refundpolicy data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Patch(Guid id, JsonPatchDocument<RefundPolicy> updatedEntity)
        {
            if (updatedEntity == null)
            {
                throw new Exception("Patch document is missing!");
            }

            var existingEntity = _dbContext.RefundPolicy.FirstOrDefault(t => t.Id == id);
            if (existingEntity == null)
            {
                throw new Exception("No data found!");
            }

            updatedEntity.ApplyTo(existingEntity);
            _dbContext.RefundPolicy.Update(existingEntity);
            _dbContext.SaveChanges();
            return true;
        }

        /// <summary>Deletes a specific refundpolicy by its primary key</summary>
        /// <param name="id">The primary key of the refundpolicy</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Delete(Guid id)
        {
            var entityData = _dbContext.RefundPolicy.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            if (entityData == null)
            {
                throw new Exception("No data found!");
            }

            _dbContext.RefundPolicy.Remove(entityData);
            _dbContext.SaveChanges();
            return true;
        }
    }
}