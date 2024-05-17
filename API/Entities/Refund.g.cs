using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Amit.Entities
{
#pragma warning disable
    /// <summary> 
    /// Represents a refund entity with essential details
    /// </summary>
    public class Refund
    {
        /// <summary>
        /// TenantId of the Refund 
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// Primary key for the Refund 
        /// </summary>
        [Key]
        [Required]
        public Guid Id { get; set; }
        /// <summary>
        /// Foreign key referencing the PaymentTransaction to which the Refund belongs 
        /// </summary>
        public Guid? PaymentTransactionId { get; set; }

        /// <summary>
        /// Navigation property representing the associated PaymentTransaction
        /// </summary>
        [ForeignKey("PaymentTransactionId")]
        public PaymentTransaction? PaymentTransactionId_PaymentTransaction { get; set; }
        /// <summary>
        /// Amount of the Refund 
        /// </summary>
        public int? Amount { get; set; }
        /// <summary>
        /// Name of the Refund 
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// Code of the Refund 
        /// </summary>
        public string? Code { get; set; }

        /// <summary>
        /// CreatedOn of the Refund 
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        /// <summary>
        /// CreatedBy of the Refund 
        /// </summary>
        public Guid? CreatedBy { get; set; }

        /// <summary>
        /// UpdatedOn of the Refund 
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        /// <summary>
        /// UpdatedBy of the Refund 
        /// </summary>
        public Guid? UpdatedBy { get; set; }
    }
}