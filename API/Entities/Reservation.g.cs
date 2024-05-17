using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Amit.Entities
{
#pragma warning disable
    /// <summary> 
    /// Represents a reservation entity with essential details
    /// </summary>
    public class Reservation
    {
        /// <summary>
        /// TenantId of the Reservation 
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// Primary key for the Reservation 
        /// </summary>
        [Key]
        [Required]
        public Guid Id { get; set; }
        /// <summary>
        /// Code of the Reservation 
        /// </summary>
        public string? Code { get; set; }
        /// <summary>
        /// Name of the Reservation 
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// Foreign key referencing the Event to which the Reservation belongs 
        /// </summary>
        public Guid? EventId { get; set; }

        /// <summary>
        /// Navigation property representing the associated Event
        /// </summary>
        [ForeignKey("EventId")]
        public Event? EventId_Event { get; set; }
        /// <summary>
        /// NumberOfTickets of the Reservation 
        /// </summary>
        public int? NumberOfTickets { get; set; }
        /// <summary>
        /// TotalPrice of the Reservation 
        /// </summary>
        public int? TotalPrice { get; set; }

        /// <summary>
        /// CreatedOn of the Reservation 
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        /// <summary>
        /// CreatedBy of the Reservation 
        /// </summary>
        public Guid? CreatedBy { get; set; }

        /// <summary>
        /// UpdatedOn of the Reservation 
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        /// <summary>
        /// UpdatedBy of the Reservation 
        /// </summary>
        public Guid? UpdatedBy { get; set; }
    }
}