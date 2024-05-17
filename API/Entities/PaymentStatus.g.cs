using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Amit.Entities
{
#pragma warning disable
    /// <summary> 
    /// Represents a paymentstatus entity with essential details
    /// </summary>
    public class PaymentStatus
    {
        /// <summary>
        /// TenantId of the PaymentStatus 
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// Primary key for the PaymentStatus 
        /// </summary>
        [Key]
        [Required]
        public Guid Id { get; set; }
        /// <summary>
        /// Name of the PaymentStatus 
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// Code of the PaymentStatus 
        /// </summary>
        public string? Code { get; set; }

        /// <summary>
        /// CreatedOn of the PaymentStatus 
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        /// <summary>
        /// CreatedBy of the PaymentStatus 
        /// </summary>
        public Guid? CreatedBy { get; set; }

        /// <summary>
        /// UpdatedOn of the PaymentStatus 
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        /// <summary>
        /// UpdatedBy of the PaymentStatus 
        /// </summary>
        public Guid? UpdatedBy { get; set; }
    }
}