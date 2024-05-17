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
    /// The eventscheduleService responsible for managing eventschedule related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting eventschedule information.
    /// </remarks>
    public interface IEventScheduleService
    {
        /// <summary>Retrieves a specific eventschedule by its primary key</summary>
        /// <param name="id">The primary key of the eventschedule</param>
        /// <returns>The eventschedule data</returns>
        EventSchedule GetById(Guid id);

        /// <summary>Retrieves a list of eventschedules based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of eventschedules</returns>
        List<EventSchedule> Get(string filters, string searchTerm, int pageNumber = 1, int pageSize = 0, string sortField = null, string sortOrder = "asc");

        /// <summary>Adds a new eventschedule</summary>
        /// <param name="model">The eventschedule data to be added</param>
        /// <returns>The result of the operation</returns>
        Guid Create(EventSchedule model);

        /// <summary>Updates a specific eventschedule by its primary key</summary>
        /// <param name="id">The primary key of the eventschedule</param>
        /// <param name="updatedEntity">The eventschedule data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Update(Guid id, EventSchedule updatedEntity);

        /// <summary>Updates a specific eventschedule by its primary key</summary>
        /// <param name="id">The primary key of the eventschedule</param>
        /// <param name="updatedEntity">The eventschedule data to be updated</param>
        /// <returns>The result of the operation</returns>
        bool Patch(Guid id, JsonPatchDocument<EventSchedule> updatedEntity);

        /// <summary>Deletes a specific eventschedule by its primary key</summary>
        /// <param name="id">The primary key of the eventschedule</param>
        /// <returns>The result of the operation</returns>
        bool Delete(Guid id);
    }

    /// <summary>
    /// The eventscheduleService responsible for managing eventschedule related operations.
    /// </summary>
    /// <remarks>
    /// This service for adding, retrieving, updating, and deleting eventschedule information.
    /// </remarks>
    public class EventScheduleService : IEventScheduleService
    {
        private AmitContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the EventSchedule class.
        /// </summary>
        /// <param name="dbContext">dbContext value to set.</param>
        public EventScheduleService(AmitContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>Retrieves a specific eventschedule by its primary key</summary>
        /// <param name="id">The primary key of the eventschedule</param>
        /// <returns>The eventschedule data</returns>
        public EventSchedule GetById(Guid id)
        {
            var entityData = _dbContext.EventSchedule.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            return entityData;
        }

        /// <summary>Retrieves a list of eventschedules based on specified filters</summary>
        /// <param name="filters">The filter criteria in JSON format. Use the following format: [{"PropertyName": "PropertyName", "Operator": "Equal", "Value": "FilterValue"}] </param>
        /// <param name="searchTerm">To searching data.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <param name="sortField">The entity's field name to sort.</param>
        /// <param name="sortOrder">The sort order asc or desc.</param>
        /// <returns>The filtered list of eventschedules</returns>/// <exception cref="Exception"></exception>
        public List<EventSchedule> Get(string filters, string searchTerm, int pageNumber, int pageSize, string sortField, string sortOrder)
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

            var query = _dbContext.EventSchedule.IncludeRelated().AsQueryable();
            int skip = (pageNumber - 1) * pageSize;
            var result = FilterService<EventSchedule>.ApplyFilter(query, filterCriteria, searchTerm);
            if (!string.IsNullOrEmpty(sortField))
            {
                var parameter = Expression.Parameter(typeof(EventSchedule), "b");
                var property = Expression.Property(parameter, sortField);
                var lambda = Expression.Lambda<Func<EventSchedule, object>>(Expression.Convert(property, typeof(object)), parameter);
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

        /// <summary>Adds a new eventschedule</summary>
        /// <param name="model">The eventschedule data to be added</param>
        /// <returns>The result of the operation</returns>
        public Guid Create(EventSchedule model)
        {
            _dbContext.EventSchedule.Add(model);
            _dbContext.SaveChanges();
            return model.Id;
        }

        /// <summary>Updates a specific eventschedule by its primary key</summary>
        /// <param name="id">The primary key of the eventschedule</param>
        /// <param name="updatedEntity">The eventschedule data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Update(Guid id, EventSchedule updatedEntity)
        {
            _dbContext.EventSchedule.Update(updatedEntity);
            _dbContext.SaveChanges();
            return true;
        }

        /// <summary>Updates a specific eventschedule by its primary key</summary>
        /// <param name="id">The primary key of the eventschedule</param>
        /// <param name="updatedEntity">The eventschedule data to be updated</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Patch(Guid id, JsonPatchDocument<EventSchedule> updatedEntity)
        {
            if (updatedEntity == null)
            {
                throw new Exception("Patch document is missing!");
            }

            var existingEntity = _dbContext.EventSchedule.FirstOrDefault(t => t.Id == id);
            if (existingEntity == null)
            {
                throw new Exception("No data found!");
            }

            updatedEntity.ApplyTo(existingEntity);
            _dbContext.EventSchedule.Update(existingEntity);
            _dbContext.SaveChanges();
            return true;
        }

        /// <summary>Deletes a specific eventschedule by its primary key</summary>
        /// <param name="id">The primary key of the eventschedule</param>
        /// <returns>The result of the operation</returns>
        /// <exception cref="Exception"></exception>
        public bool Delete(Guid id)
        {
            var entityData = _dbContext.EventSchedule.IncludeRelated().FirstOrDefault(entity => entity.Id == id);
            if (entityData == null)
            {
                throw new Exception("No data found!");
            }

            _dbContext.EventSchedule.Remove(entityData);
            _dbContext.SaveChanges();
            return true;
        }
    }
}