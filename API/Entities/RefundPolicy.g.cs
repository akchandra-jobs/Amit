using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Amit.Entities
{
#pragma warning disable
    /// <summary> 
    /// Represents a refundpolicy entity with essential details
    /// </summary>
    public class RefundPolicy
    {
        /// <summary>
        /// TenantId of the RefundPolicy 
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// Primary key for the RefundPolicy 
        /// </summary>
        [Key]
        [Required]
        public Guid Id { get; set; }
        /// <summary>
        /// Name of the RefundPolicy 
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// Description of the RefundPolicy 
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// CreatedOn of the RefundPolicy 
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        /// <summary>
        /// CreatedBy of the RefundPolicy 
        /// </summary>
        public Guid? CreatedBy { get; set; }

        /// <summary>
        /// UpdatedOn of the RefundPolicy 
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        /// <summary>
        /// UpdatedBy of the RefundPolicy 
        /// </summary>
        public Guid? UpdatedBy { get; set; }
    }
}