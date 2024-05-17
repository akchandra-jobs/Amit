using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Amit.Entities
{
#pragma warning disable
    /// <summary> 
    /// Represents a payment entity with essential details
    /// </summary>
    public class Payment
    {
        /// <summary>
        /// TenantId of the Payment 
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// Primary key for the Payment 
        /// </summary>
        [Key]
        [Required]
        public Guid Id { get; set; }
        /// <summary>
        /// Code of the Payment 
        /// </summary>
        public string? Code { get; set; }
        /// <summary>
        /// Amount of the Payment 
        /// </summary>
        public int? Amount { get; set; }
        /// <summary>
        /// Status of the Payment 
        /// </summary>
        public string? Status { get; set; }
        /// <summary>
        /// Foreign key referencing the PaymentMethod to which the Payment belongs 
        /// </summary>
        public Guid? PaymentMethodId { get; set; }

        /// <summary>
        /// Navigation property representing the associated PaymentMethod
        /// </summary>
        [ForeignKey("PaymentMethodId")]
        public PaymentMethod? PaymentMethodId_PaymentMethod { get; set; }

        /// <summary>
        /// CreatedOn of the Payment 
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        /// <summary>
        /// CreatedBy of the Payment 
        /// </summary>
        public Guid? CreatedBy { get; set; }

        /// <summary>
        /// UpdatedOn of the Payment 
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        /// <summary>
        /// UpdatedBy of the Payment 
        /// </summary>
        public Guid? UpdatedBy { get; set; }
    }
}