using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Amit.Entities
{
#pragma warning disable
    /// <summary> 
    /// Represents a event entity with essential details
    /// </summary>
    public class Event
    {
        /// <summary>
        /// TenantId of the Event 
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// Primary key for the Event 
        /// </summary>
        [Key]
        [Required]
        public Guid Id { get; set; }
        /// <summary>
        /// Code of the Event 
        /// </summary>
        public string? Code { get; set; }
        /// <summary>
        /// Name of the Event 
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// Foreign key referencing the Venue to which the Event belongs 
        /// </summary>
        public Guid? VenueId { get; set; }

        /// <summary>
        /// Navigation property representing the associated Venue
        /// </summary>
        [ForeignKey("VenueId")]
        public Venue? VenueId_Venue { get; set; }

        /// <summary>
        /// DateTime of the Event 
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? DateTime { get; set; }

        /// <summary>
        /// CreatedOn of the Event 
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        /// <summary>
        /// CreatedBy of the Event 
        /// </summary>
        public Guid? CreatedBy { get; set; }

        /// <summary>
        /// UpdatedOn of the Event 
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        /// <summary>
        /// UpdatedBy of the Event 
        /// </summary>
        public Guid? UpdatedBy { get; set; }
    }
}