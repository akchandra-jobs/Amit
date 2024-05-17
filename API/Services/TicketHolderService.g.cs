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
    /// The ticketholderService responsible for managing ticketholder related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting ticketholder information.
    /// </remarks>
    public interface ITicketHolderService
    {
        /// <summary>Retrieves a specific ticketholder by its primary key</summary>
        /// <param name="id">The primary key of the ticketholder</param>
        /// <returns>The ticketholder data</returns>
        TicketHolder GetById(Guid id);

        /// <summary>Retrieves a list of ticketholders based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of ticketholders</returns>
        List<TicketHolder> Get(string filters, string searchTerm, int pageNumber = 1, int pageSize = 0, string sortField = null, string sortOrder = "asc");

        /// <summary>Adds a new ticketholder</summary>
        /// <param name="model">The ticketholder data to be added</param>
        /// <returns>The result of the operation</returns>
        Guid Create(TicketHolder model);

        /// <summary>Updates a specific ticketholder by its primary key</summary>
        /// <param name="id">The primary key of the ticketholder</param>
        /// <param name="updatedEntity">The ticketholder data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Update(Guid id, TicketHolder updatedEntity);

        /// <summary>Updates a specific ticketholder by its primary key</summary>
        /// <param name="id">The primary key of the ticketholder</param>
        /// <param name="updatedEntity">The ticketholder data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Patch(Guid id, JsonPatchDocument<TicketHolder> updatedEntity);

        /// <summary>Deletes a specific ticketholder by its primary key</summary>
        /// <param name="id">The primary key of the ticketholder</param>
        /// <returns>The result of the operation</returns>
        bool Delete(Guid id);
    }

    /// <summary>
    /// The ticketholderService responsible for managing ticketholder related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting ticketholder information.
    /// </remarks>
    public class TicketHolderService : ITicketHolderService
    {
        private AmitContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the TicketHolder class.
        /// </summary>
        /// <param name="dbContext">dbContext value to set.</param>
        public TicketHolderService(AmitContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>Retrieves a specific ticketholder by its primary key</summary>
        /// <param name="id">The primary key of the ticketholder</param>
        /// <returns>The ticketholder data</returns>
        public TicketHolder GetById(Guid id)
        {
            var entityData = _dbContext.TicketHolder.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            return entityData;
        }

        /// <summary>Retrieves a list of ticketholders based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of ticketholders</returns>/// <exception cref="Exception"></exception>
        public List<TicketHolder> Get(string filters, string searchTerm, int pageNumber, int pageSize, string sortField, string sortOrder)
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

            var query = _dbContext.TicketHolder.IncludeRelated().AsQueryable();
            int skip = (pageNumber - 1) * pageSize;
            var result = FilterService<TicketHolder>.ApplyFilter(query, filterCriteria, searchTerm);
            if (!string.IsNullOrEmpty(sortField))
            {
                var parameter = Expression.Parameter(typeof(TicketHolder), "b");
                var property = Expression.Property(parameter, sortField);
                var lambda = Expression.Lambda<Func<TicketHolder, object>>(Expression.Convert(property, typeof(object)), parameter);
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

        /// <summary>Adds a new ticketholder</summary>
        /// <param name="model">The ticketholder data to be added</param>
        /// <returns>The result of the operation</returns>
        public Guid Create(TicketHolder model)
        {
            _dbContext.TicketHolder.Add(model);
            _dbContext.SaveChanges();
            return model.Id;
        }

        /// <summary>Updates a specific ticketholder by its primary key</summary>
        /// <param name="id">The primary key of the ticketholder</param>
        /// <param name="updatedEntity">The ticketholder data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Update(Guid id, TicketHolder updatedEntity)
        {
            _dbContext.TicketHolder.Update(updatedEntity);
            _dbContext.SaveChanges();
            return true;
        }

        /// <summary>Updates a specific ticketholder by its primary key</summary>
        /// <param name="id">The primary key of the ticketholder</param>
        /// <param name="updatedEntity">The ticketholder data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Patch(Guid id, JsonPatchDocument<TicketHolder> updatedEntity)
        {
            if (updatedEntity == null)
            {
                throw new Exception("Patch document is missing!");
            }

            var existingEntity = _dbContext.TicketHolder.FirstOrDefault(t => t.Id == id);
            if (existingEntity == null)
            {
                throw new Exception("No data found!");
            }

            updatedEntity.ApplyTo(existingEntity);
            _dbContext.TicketHolder.Update(existingEntity);
            _dbContext.SaveChanges();
            return true;
        }

        /// <summary>Deletes a specific ticketholder by its primary key</summary>
        /// <param name="id">The primary key of the ticketholder</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Delete(Guid id)
        {
            var entityData = _dbContext.TicketHolder.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            if (entityData == null)
            {
                throw new Exception("No data found!");
            }

            _dbContext.TicketHolder.Remove(entityData);
            _dbContext.SaveChanges();
            return true;
        }
    }
}