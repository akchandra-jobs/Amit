using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Amit.Entities
{
#pragma warning disable
    /// <summary> 
    /// Represents a eventcategory entity with essential details
    /// </summary>
    public class EventCategory
    {
        /// <summary>
        /// TenantId of the EventCategory 
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// Primary key for the EventCategory 
        /// </summary>
        [Key]
        [Required]
        public Guid Id { get; set; }
        /// <summary>
        /// Name of the EventCategory 
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// Code of the EventCategory 
        /// </summary>
        public string? Code { get; set; }

        /// <summary>
        /// CreatedOn of the EventCategory 
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        /// <summary>
        /// CreatedBy of the EventCategory 
        /// </summary>
        public Guid? CreatedBy { get; set; }

        /// <summary>
        /// UpdatedOn of the EventCategory 
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        /// <summary>
        /// UpdatedBy of the EventCategory 
        /// </summary>
        public Guid? UpdatedBy { get; set; }
    }
}