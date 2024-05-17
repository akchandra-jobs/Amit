using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Amit.Entities
{
#pragma warning disable
    /// <summary> 
    /// Represents a tickettype entity with essential details
    /// </summary>
    public class TicketType
    {
        /// <summary>
        /// TenantId of the TicketType 
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// Primary key for the TicketType 
        /// </summary>
        [Key]
        [Required]
        public Guid Id { get; set; }
        /// <summary>
        /// Code of the TicketType 
        /// </summary>
        public string? Code { get; set; }
        /// <summary>
        /// Name of the TicketType 
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// CreatedOn of the TicketType 
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        /// <summary>
        /// CreatedBy of the TicketType 
        /// </summary>
        public Guid? CreatedBy { get; set; }

        /// <summary>
        /// UpdatedOn of the TicketType 
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        /// <summary>
        /// UpdatedBy of the TicketType 
        /// </summary>
        public Guid? UpdatedBy { get; set; }
    }
}