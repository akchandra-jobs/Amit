using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Amit.Entities
{
#pragma warning disable
    /// <summary> 
    /// Represents a ticketstatus entity with essential details
    /// </summary>
    public class TicketStatus
    {
        /// <summary>
        /// TenantId of the TicketStatus 
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// Primary key for the TicketStatus 
        /// </summary>
        [Key]
        [Required]
        public Guid Id { get; set; }
        /// <summary>
        /// Name of the TicketStatus 
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// Code of the TicketStatus 
        /// </summary>
        public string? Code { get; set; }

        /// <summary>
        /// CreatedOn of the TicketStatus 
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        /// <summary>
        /// CreatedBy of the TicketStatus 
        /// </summary>
        public Guid? CreatedBy { get; set; }

        /// <summary>
        /// UpdatedOn of the TicketStatus 
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        /// <summary>
        /// UpdatedBy of the TicketStatus 
        /// </summary>
        public Guid? UpdatedBy { get; set; }
    }
}