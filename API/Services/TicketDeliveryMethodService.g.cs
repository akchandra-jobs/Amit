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
    /// The ticketdeliverymethodService responsible for managing ticketdeliverymethod related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting ticketdeliverymethod information.
    /// </remarks>
    public interface ITicketDeliveryMethodService
    {
        /// <summary>Retrieves a specific ticketdeliverymethod by its primary key</summary>
        /// <param name="id">The primary key of the ticketdeliverymethod</param>
        /// <returns>The ticketdeliverymethod data</returns>
        TicketDeliveryMethod GetById(Guid id);

        /// <summary>Retrieves a list of ticketdeliverymethods based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of ticketdeliverymethods</returns>
        List<TicketDeliveryMethod> Get(string filters, string searchTerm, int pageNumber = 1, int pageSize = 0, string sortField = null, string sortOrder = "asc");

        /// <summary>Adds a new ticketdeliverymethod</summary>
        /// <param name="model">The ticketdeliverymethod data to be added</param>
        /// <returns>The result of the operation</returns>
        Guid Create(TicketDeliveryMethod model);

        /// <summary>Updates a specific ticketdeliverymethod by its primary key</summary>
        /// <param name="id">The primary key of the ticketdeliverymethod</param>
        /// <param name="updatedEntity">The ticketdeliverymethod data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Update(Guid id, TicketDeliveryMethod updatedEntity);

        /// <summary>Updates a specific ticketdeliverymethod by its primary key</summary>
        /// <param name="id">The primary key of the ticketdeliverymethod</param>
        /// <param name="updatedEntity">The ticketdeliverymethod data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Patch(Guid id, JsonPatchDocument<TicketDeliveryMethod> updatedEntity);

        /// <summary>Deletes a specific ticketdeliverymethod by its primary key</summary>
        /// <param name="id">The primary key of the ticketdeliverymethod</param>
        /// <returns>The result of the operation</returns>
        bool Delete(Guid id);
    }

    /// <summary>
    /// The ticketdeliverymethodService responsible for managing ticketdeliverymethod related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting ticketdeliverymethod information.
    /// </remarks>
    public class TicketDeliveryMethodService : ITicketDeliveryMethodService
    {
        private AmitContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the TicketDeliveryMethod class.
        /// </summary>
        /// <param name="dbContext">dbContext value to set.</param>
        public TicketDeliveryMethodService(AmitContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>Retrieves a specific ticketdeliverymethod by its primary key</summary>
        /// <param name="id">The primary key of the ticketdeliverymethod</param>
        /// <returns>The ticketdeliverymethod data</returns>
        public TicketDeliveryMethod GetById(Guid id)
        {
            var entityData = _dbContext.TicketDeliveryMethod.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            return entityData;
        }

        /// <summary>Retrieves a list of ticketdeliverymethods based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of ticketdeliverymethods</returns>/// <exception cref="Exception"></exception>
        public List<TicketDeliveryMethod> Get(string filters, string searchTerm, int pageNumber, int pageSize, string sortField, string sortOrder)
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

            var query = _dbContext.TicketDeliveryMethod.IncludeRelated().AsQueryable();
            int skip = (pageNumber - 1) * pageSize;
            var result = FilterService<TicketDeliveryMethod>.ApplyFilter(query, filterCriteria, searchTerm);
            if (!string.IsNullOrEmpty(sortField))
            {
                var parameter = Expression.Parameter(typeof(TicketDeliveryMethod), "b");
                var property = Expression.Property(parameter, sortField);
                var lambda = Expression.Lambda<Func<TicketDeliveryMethod, object>>(Expression.Convert(property, typeof(object)), parameter);
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

        /// <summary>Adds a new ticketdeliverymethod</summary>
        /// <param name="model">The ticketdeliverymethod data to be added</param>
        /// <returns>The result of the operation</returns>
        public Guid Create(TicketDeliveryMethod model)
        {
            _dbContext.TicketDeliveryMethod.Add(model);
            _dbContext.SaveChanges();
            return model.Id;
        }

        /// <summary>Updates a specific ticketdeliverymethod by its primary key</summary>
        /// <param name="id">The primary key of the ticketdeliverymethod</param>
        /// <param name="updatedEntity">The ticketdeliverymethod data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Update(Guid id, TicketDeliveryMethod updatedEntity)
        {
            _dbContext.TicketDeliveryMethod.Update(updatedEntity);
            _dbContext.SaveChanges();
            return true;
        }

        /// <summary>Updates a specific ticketdeliverymethod by its primary key</summary>
        /// <param name="id">The primary key of the ticketdeliverymethod</param>
        /// <param name="updatedEntity">The ticketdeliverymethod data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Patch(Guid id, JsonPatchDocument<TicketDeliveryMethod> updatedEntity)
        {
            if (updatedEntity == null)
            {
                throw new Exception("Patch document is missing!");
            }

            var existingEntity = _dbContext.TicketDeliveryMethod.FirstOrDefault(t => t.Id == id);
            if (existingEntity == null)
            {
                throw new Exception("No data found!");
            }

            updatedEntity.ApplyTo(existingEntity);
            _dbContext.TicketDeliveryMethod.Update(existingEntity);
            _dbContext.SaveChanges();
            return true;
        }

        /// <summary>Deletes a specific ticketdeliverymethod by its primary key</summary>
        /// <param name="id">The primary key of the ticketdeliverymethod</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Delete(Guid id)
        {
            var entityData = _dbContext.TicketDeliveryMethod.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            if (entityData == null)
            {
                throw new Exception("No data found!");
            }

            _dbContext.TicketDeliveryMethod.Remove(entityData);
            _dbContext.SaveChanges();
            return true;
        }
    }
}