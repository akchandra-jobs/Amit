using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Amit.Entities
{
#pragma warning disable
    /// <summary> 
    /// Represents a pricing entity with essential details
    /// </summary>
    public class Pricing
    {
        /// <summary>
        /// TenantId of the Pricing 
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// Primary key for the Pricing 
        /// </summary>
        [Key]
        [Required]
        public Guid Id { get; set; }
        /// <summary>
        /// Code of the Pricing 
        /// </summary>
        public string? Code { get; set; }
        /// <summary>
        /// Foreign key referencing the Event to which the Pricing belongs 
        /// </summary>
        public Guid? EventId { get; set; }

        /// <summary>
        /// Navigation property representing the associated Event
        /// </summary>
        [ForeignKey("EventId")]
        public Event? EventId_Event { get; set; }
        /// <summary>
        /// Foreign key referencing the TicketType to which the Pricing belongs 
        /// </summary>
        public Guid? TicketTypeId { get; set; }

        /// <summary>
        /// Navigation property representing the associated TicketType
        /// </summary>
        [ForeignKey("TicketTypeId")]
        public TicketType? TicketTypeId_TicketType { get; set; }
        /// <summary>
        /// Price of the Pricing 
        /// </summary>
        public int? Price { get; set; }

        /// <summary>
        /// CreatedOn of the Pricing 
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        /// <summary>
        /// CreatedBy of the Pricing 
        /// </summary>
        public Guid? CreatedBy { get; set; }

        /// <summary>
        /// UpdatedOn of the Pricing 
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        /// <summary>
        /// UpdatedBy of the Pricing 
        /// </summary>
        public Guid? UpdatedBy { get; set; }
    }
}