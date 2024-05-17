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
    /// The pricingService responsible for managing pricing related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting pricing information.
    /// </remarks>
    public interface IPricingService
    {
        /// <summary>Retrieves a specific pricing by its primary key</summary>
        /// <param name="id">The primary key of the pricing</param>
        /// <returns>The pricing data</returns>
        Pricing GetById(Guid id);

        /// <summary>Retrieves a list of pricings based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of pricings</returns>
        List<Pricing> Get(string filters, string searchTerm, int pageNumber = 1, int pageSize = 0, string sortField = null, string sortOrder = "asc");

        /// <summary>Adds a new pricing</summary>
        /// <param name="model">The pricing data to be added</param>
        /// <returns>The result of the operation</returns>
        Guid Create(Pricing model);

        /// <summary>Updates a specific pricing by its primary key</summary>
        /// <param name="id">The primary key of the pricing</param>
        /// <param name="updatedEntity">The pricing data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Update(Guid id, Pricing updatedEntity);

        /// <summary>Updates a specific pricing by its primary key</summary>
        /// <param name="id">The primary key of the pricing</param>
        /// <param name="updatedEntity">The pricing data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Patch(Guid id, JsonPatchDocument<Pricing> updatedEntity);

        /// <summary>Deletes a specific pricing by its primary key</summary>
        /// <param name="id">The primary key of the pricing</param>
        /// <returns>The result of the operation</returns>
        bool Delete(Guid id);
    }

    /// <summary>
    /// The pricingService responsible for managing pricing related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting pricing information.
    /// </remarks>
    public class PricingService : IPricingService
    {
        private AmitContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the Pricing class.
        /// </summary>
        /// <param name="dbContext">dbContext value to set.</param>
        public PricingService(AmitContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>Retrieves a specific pricing by its primary key</summary>
        /// <param name="id">The primary key of the pricing</param>
        /// <returns>The pricing data</returns>
        public Pricing GetById(Guid id)
        {
            var entityData = _dbContext.Pricing.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            return entityData;
        }

        /// <summary>Retrieves a list of pricings based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of pricings</returns>/// <exception cref="Exception"></exception>
        public List<Pricing> Get(string filters, string searchTerm, int pageNumber, int pageSize, string sortField, string sortOrder)
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

            var query = _dbContext.Pricing.IncludeRelated().AsQueryable();
            int skip = (pageNumber - 1) * pageSize;
            var result = FilterService<Pricing>.ApplyFilter(query, filterCriteria, searchTerm);
            if (!string.IsNullOrEmpty(sortField))
            {
                var parameter = Expression.Parameter(typeof(Pricing), "b");
                var property = Expression.Property(parameter, sortField);
                var lambda = Expression.Lambda<Func<Pricing, object>>(Expression.Convert(property, typeof(object)), parameter);
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

        /// <summary>Adds a new pricing</summary>
        /// <param name="model">The pricing data to be added</param>
        /// <returns>The result of the operation</returns>
        public Guid Create(Pricing model)
        {
            _dbContext.Pricing.Add(model);
            _dbContext.SaveChanges();
            return model.Id;
        }

        /// <summary>Updates a specific pricing by its primary key</summary>
        /// <param name="id">The primary key of the pricing</param>
        /// <param name="updatedEntity">The pricing data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Update(Guid id, Pricing updatedEntity)
        {
            _dbContext.Pricing.Update(updatedEntity);
            _dbContext.SaveChanges();
            return true;
        }

        /// <summary>Updates a specific pricing by its primary key</summary>
        /// <param name="id">The primary key of the pricing</param>
        /// <param name="updatedEntity">The pricing data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Patch(Guid id, JsonPatchDocument<Pricing> updatedEntity)
        {
            if (updatedEntity == null)
            {
                throw new Exception("Patch document is missing!");
            }

            var existingEntity = _dbContext.Pricing.FirstOrDefault(t => t.Id == id);
            if (existingEntity == null)
            {
                throw new Exception("No data found!");
            }

            updatedEntity.ApplyTo(existingEntity);
            _dbContext.Pricing.Update(existingEntity);
            _dbContext.SaveChanges();
            return true;
        }

        /// <summary>Deletes a specific pricing by its primary key</summary>
        /// <param name="id">The primary key of the pricing</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Delete(Guid id)
        {
            var entityData = _dbContext.Pricing.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            if (entityData == null)
            {
                throw new Exception("No data found!");
            }

            _dbContext.Pricing.Remove(entityData);
            _dbContext.SaveChanges();
            return true;
        }
    }
}