using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Amit.Entities
{
#pragma warning disable
    /// <summary> 
    /// Represents a paymentaccount entity with essential details
    /// </summary>
    public class PaymentAccount
    {
        /// <summary>
        /// TenantId of the PaymentAccount 
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// Primary key for the PaymentAccount 
        /// </summary>
        [Key]
        [Required]
        public Guid Id { get; set; }
        /// <summary>
        /// Foreign key referencing the BillingAddress to which the PaymentAccount belongs 
        /// </summary>
        public Guid? BillingAddressId { get; set; }

        /// <summary>
        /// Navigation property representing the associated BillingAddress
        /// </summary>
        [ForeignKey("BillingAddressId")]
        public BillingAddress? BillingAddressId_BillingAddress { get; set; }

        /// <summary>
        /// CreatedOn of the PaymentAccount 
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        /// <summary>
        /// CreatedBy of the PaymentAccount 
        /// </summary>
        public Guid? CreatedBy { get; set; }

        /// <summary>
        /// UpdatedOn of the PaymentAccount 
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        /// <summary>
        /// UpdatedBy of the PaymentAccount 
        /// </summary>
        public Guid? UpdatedBy { get; set; }
    }
}