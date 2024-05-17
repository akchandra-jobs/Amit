using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Amit.Entities
{
#pragma warning disable
    /// <summary> 
    /// Represents a eventschedule entity with essential details
    /// </summary>
    public class EventSchedule
    {
        /// <summary>
        /// TenantId of the EventSchedule 
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// Primary key for the EventSchedule 
        /// </summary>
        [Key]
        [Required]
        public Guid Id { get; set; }
        /// <summary>
        /// Code of the EventSchedule 
        /// </summary>
        public string? Code { get; set; }
        /// <summary>
        /// Foreign key referencing the EventCategory to which the EventSchedule belongs 
        /// </summary>
        public Guid? EventCategoryId { get; set; }

        /// <summary>
        /// Navigation property representing the associated EventCategory
        /// </summary>
        [ForeignKey("EventCategoryId")]
        public EventCategory? EventCategoryId_EventCategory { get; set; }

        /// <summary>
        /// Date of the EventSchedule 
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? Date { get; set; }

        /// <summary>
        /// StartTime of the EventSchedule 
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// EndTime of the EventSchedule 
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// CreatedOn of the EventSchedule 
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        /// <summary>
        /// CreatedBy of the EventSchedule 
        /// </summary>
        public Guid? CreatedBy { get; set; }

        /// <summary>
        /// UpdatedOn of the EventSchedule 
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        /// <summary>
        /// UpdatedBy of the EventSchedule 
        /// </summary>
        public Guid? UpdatedBy { get; set; }
    }
}