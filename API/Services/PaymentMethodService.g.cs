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
    /// The paymentmethodService responsible for managing paymentmethod related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting paymentmethod information.
    /// </remarks>
    public interface IPaymentMethodService
    {
        /// <summary>Retrieves a specific paymentmethod by its primary key</summary>
        /// <param name="id">The primary key of the paymentmethod</param>
        /// <returns>The paymentmethod data</returns>
        PaymentMethod GetById(Guid id);

        /// <summary>Retrieves a list of paymentmethods based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of paymentmethods</returns>
        List<PaymentMethod> Get(string filters, string searchTerm, int pageNumber = 1, int pageSize = 0, string sortField = null, string sortOrder = "asc");

        /// <summary>Adds a new paymentmethod</summary>
        /// <param name="model">The paymentmethod data to be added</param>
        /// <returns>The result of the operation</returns>
        Guid Create(PaymentMethod model);

        /// <summary>Updates a specific paymentmethod by its primary key</summary>
        /// <param name="id">The primary key of the paymentmethod</param>
        /// <param name="updatedEntity">The paymentmethod data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Update(Guid id, PaymentMethod updatedEntity);

        /// <summary>Updates a specific paymentmethod by its primary key</summary>
        /// <param name="id">The primary key of the paymentmethod</param>
        /// <param name="updatedEntity">The paymentmethod data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Patch(Guid id, JsonPatchDocument<PaymentMethod> updatedEntity);

        /// <summary>Deletes a specific paymentmethod by its primary key</summary>
        /// <param name="id">The primary key of the paymentmethod</param>
        /// <returns>The result of the operation</returns>
        bool Delete(Guid id);
    }

    /// <summary>
    /// The paymentmethodService responsible for managing paymentmethod related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting paymentmethod information.
    /// </remarks>
    public class PaymentMethodService : IPaymentMethodService
    {
        private AmitContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the PaymentMethod class.
        /// </summary>
        /// <param name="dbContext">dbContext value to set.</param>
        public PaymentMethodService(AmitContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>Retrieves a specific paymentmethod by its primary key</summary>
        /// <param name="id">The primary key of the paymentmethod</param>
        /// <returns>The paymentmethod data</returns>
        public PaymentMethod GetById(Guid id)
        {
            var entityData = _dbContext.PaymentMethod.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            return entityData;
        }

        /// <summary>Retrieves a list of paymentmethods based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of paymentmethods</returns>/// <exception cref="Exception"></exception>
        public List<PaymentMethod> Get(string filters, string searchTerm, int pageNumber, int pageSize, string sortField, string sortOrder)
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

            var query = _dbContext.PaymentMethod.IncludeRelated().AsQueryable();
            int skip = (pageNumber - 1) * pageSize;
            var result = FilterService<PaymentMethod>.ApplyFilter(query, filterCriteria, searchTerm);
            if (!string.IsNullOrEmpty(sortField))
            {
                var parameter = Expression.Parameter(typeof(PaymentMethod), "b");
                var property = Expression.Property(parameter, sortField);
                var lambda = Expression.Lambda<Func<PaymentMethod, object>>(Expression.Convert(property, typeof(object)), parameter);
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

        /// <summary>Adds a new paymentmethod</summary>
        /// <param name="model">The paymentmethod data to be added</param>
        /// <returns>The result of the operation</returns>
        public Guid Create(PaymentMethod model)
        {
            _dbContext.PaymentMethod.Add(model);
            _dbContext.SaveChanges();
            return model.Id;
        }

        /// <summary>Updates a specific paymentmethod by its primary key</summary>
        /// <param name="id">The primary key of the paymentmethod</param>
        /// <param name="updatedEntity">The paymentmethod data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Update(Guid id, PaymentMethod updatedEntity)
        {
            _dbContext.PaymentMethod.Update(updatedEntity);
            _dbContext.SaveChanges();
            return true;
        }

        /// <summary>Updates a specific paymentmethod by its primary key</summary>
        /// <param name="id">The primary key of the paymentmethod</param>
        /// <param name="updatedEntity">The paymentmethod data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Patch(Guid id, JsonPatchDocument<PaymentMethod> updatedEntity)
        {
            if (updatedEntity == null)
            {
                throw new Exception("Patch document is missing!");
            }

            var existingEntity = _dbContext.PaymentMethod.FirstOrDefault(t => t.Id == id);
            if (existingEntity == null)
            {
                throw new Exception("No data found!");
            }

            updatedEntity.ApplyTo(existingEntity);
            _dbContext.PaymentMethod.Update(existingEntity);
            _dbContext.SaveChanges();
            return true;
        }

        /// <summary>Deletes a specific paymentmethod by its primary key</summary>
        /// <param name="id">The primary key of the paymentmethod</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Delete(Guid id)
        {
            var entityData = _dbContext.PaymentMethod.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            if (entityData == null)
            {
                throw new Exception("No data found!");
            }

            _dbContext.PaymentMethod.Remove(entityData);
            _dbContext.SaveChanges();
            return true;
        }
    }
}