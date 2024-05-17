using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Amit.Entities
{
#pragma warning disable
    /// <summary> 
    /// Represents a ticketholder entity with essential details
    /// </summary>
    public class TicketHolder
    {
        /// <summary>
        /// TenantId of the TicketHolder 
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// Primary key for the TicketHolder 
        /// </summary>
        [Key]
        [Required]
        public Guid Id { get; set; }
        /// <summary>
        /// Name of the TicketHolder 
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// Email of the TicketHolder 
        /// </summary>
        public string? Email { get; set; }
        /// <summary>
        /// PhoneNumber of the TicketHolder 
        /// </summary>
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// CreatedOn of the TicketHolder 
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        /// <summary>
        /// CreatedBy of the TicketHolder 
        /// </summary>
        public Guid? CreatedBy { get; set; }

        /// <summary>
        /// UpdatedOn of the TicketHolder 
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        /// <summary>
        /// UpdatedBy of the TicketHolder 
        /// </summary>
        public Guid? UpdatedBy { get; set; }
    }
}