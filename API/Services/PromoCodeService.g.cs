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
    /// The promocodeService responsible for managing promocode related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting promocode information.
    /// </remarks>
    public interface IPromoCodeService
    {
        /// <summary>Retrieves a specific promocode by its primary key</summary>
        /// <param name="id">The primary key of the promocode</param>
        /// <returns>The promocode data</returns>
        PromoCode GetById(Guid id);

        /// <summary>Retrieves a list of promocodes based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of promocodes</returns>
        List<PromoCode> Get(string filters, string searchTerm, int pageNumber = 1, int pageSize = 0, string sortField = null, string sortOrder = "asc");

        /// <summary>Adds a new promocode</summary>
        /// <param name="model">The promocode data to be added</param>
        /// <returns>The result of the operation</returns>
        Guid Create(PromoCode model);

        /// <summary>Updates a specific promocode by its primary key</summary>
        /// <param name="id">The primary key of the promocode</param>
        /// <param name="updatedEntity">The promocode data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Update(Guid id, PromoCode updatedEntity);

        /// <summary>Updates a specific promocode by its primary key</summary>
        /// <param name="id">The primary key of the promocode</param>
        /// <param name="updatedEntity">The promocode data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Patch(Guid id, JsonPatchDocument<PromoCode> updatedEntity);

        /// <summary>Deletes a specific promocode by its primary key</summary>
        /// <param name="id">The primary key of the promocode</param>
        /// <returns>The result of the operation</returns>
        bool Delete(Guid id);
    }

    /// <summary>
    /// The promocodeService responsible for managing promocode related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting promocode information.
    /// </remarks>
    public class PromoCodeService : IPromoCodeService
    {
        private AmitContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the PromoCode class.
        /// </summary>
        /// <param name="dbContext">dbContext value to set.</param>
        public PromoCodeService(AmitContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>Retrieves a specific promocode by its primary key</summary>
        /// <param name="id">The primary key of the promocode</param>
        /// <returns>The promocode data</returns>
        public PromoCode GetById(Guid id)
        {
            var entityData = _dbContext.PromoCode.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            return entityData;
        }

        /// <summary>Retrieves a list of promocodes based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of promocodes</returns>/// <exception cref="Exception"></exception>
        public List<PromoCode> Get(string filters, string searchTerm, int pageNumber, int pageSize, string sortField, string sortOrder)
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

            var query = _dbContext.PromoCode.IncludeRelated().AsQueryable();
            int skip = (pageNumber - 1) * pageSize;
            var result = FilterService<PromoCode>.ApplyFilter(query, filterCriteria, searchTerm);
            if (!string.IsNullOrEmpty(sortField))
            {
                var parameter = Expression.Parameter(typeof(PromoCode), "b");
                var property = Expression.Property(parameter, sortField);
                var lambda = Expression.Lambda<Func<PromoCode, object>>(Expression.Convert(property, typeof(object)), parameter);
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

        /// <summary>Adds a new promocode</summary>
        /// <param name="model">The promocode data to be added</param>
        /// <returns>The result of the operation</returns>
        public Guid Create(PromoCode model)
        {
            _dbContext.PromoCode.Add(model);
            _dbContext.SaveChanges();
            return model.Id;
        }

        /// <summary>Updates a specific promocode by its primary key</summary>
        /// <param name="id">The primary key of the promocode</param>
        /// <param name="updatedEntity">The promocode data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Update(Guid id, PromoCode updatedEntity)
        {
            _dbContext.PromoCode.Update(updatedEntity);
            _dbContext.SaveChanges();
            return true;
        }

        /// <summary>Updates a specific promocode by its primary key</summary>
        /// <param name="id">The primary key of the promocode</param>
        /// <param name="updatedEntity">The promocode data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Patch(Guid id, JsonPatchDocument<PromoCode> updatedEntity)
        {
            if (updatedEntity == null)
            {
                throw new Exception("Patch document is missing!");
            }

            var existingEntity = _dbContext.PromoCode.FirstOrDefault(t => t.Id == id);
            if (existingEntity == null)
            {
                throw new Exception("No data found!");
            }

            updatedEntity.ApplyTo(existingEntity);
            _dbContext.PromoCode.Update(existingEntity);
            _dbContext.SaveChanges();
            return true;
        }

        /// <summary>Deletes a specific promocode by its primary key</summary>
        /// <param name="id">The primary key of the promocode</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Delete(Guid id)
        {
            var entityData = _dbContext.PromoCode.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            if (entityData == null)
            {
                throw new Exception("No data found!");
            }

            _dbContext.PromoCode.Remove(entityData);
            _dbContext.SaveChanges();
            return true;
        }
    }
}