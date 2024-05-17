using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Amit.Entities
{
#pragma warning disable
    /// <summary> 
    /// Represents a paymenttransaction entity with essential details
    /// </summary>
    public class PaymentTransaction
    {
        /// <summary>
        /// TenantId of the PaymentTransaction 
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// Primary key for the PaymentTransaction 
        /// </summary>
        [Key]
        [Required]
        public Guid Id { get; set; }
        /// <summary>
        /// Foreign key referencing the PaymentGateway to which the PaymentTransaction belongs 
        /// </summary>
        public Guid? PaymentGatewayId { get; set; }

        /// <summary>
        /// Navigation property representing the associated PaymentGateway
        /// </summary>
        [ForeignKey("PaymentGatewayId")]
        public PaymentGateway? PaymentGatewayId_PaymentGateway { get; set; }
        /// <summary>
        /// Foreign key referencing the PaymentStatus to which the PaymentTransaction belongs 
        /// </summary>
        public Guid? PaymentStatusId { get; set; }

        /// <summary>
        /// Navigation property representing the associated PaymentStatus
        /// </summary>
        [ForeignKey("PaymentStatusId")]
        public PaymentStatus? PaymentStatusId_PaymentStatus { get; set; }
        /// <summary>
        /// Amount of the PaymentTransaction 
        /// </summary>
        public int? Amount { get; set; }
        /// <summary>
        /// Foreign key referencing the CardIssuer to which the PaymentTransaction belongs 
        /// </summary>
        public Guid? CardIssuerId { get; set; }

        /// <summary>
        /// Navigation property representing the associated CardIssuer
        /// </summary>
        [ForeignKey("CardIssuerId")]
        public CardIssuer? CardIssuerId_CardIssuer { get; set; }
        /// <summary>
        /// Foreign key referencing the CardType to which the PaymentTransaction belongs 
        /// </summary>
        public Guid? CardTypeId { get; set; }

        /// <summary>
        /// Navigation property representing the associated CardType
        /// </summary>
        [ForeignKey("CardTypeId")]
        public CardType? CardTypeId_CardType { get; set; }
        /// <summary>
        /// Foreign key referencing the TransactionFee to which the PaymentTransaction belongs 
        /// </summary>
        public Guid? TransactionFeeId { get; set; }

        /// <summary>
        /// Navigation property representing the associated TransactionFee
        /// </summary>
        [ForeignKey("TransactionFeeId")]
        public TransactionFee? TransactionFeeId_TransactionFee { get; set; }
        /// <summary>
        /// Foreign key referencing the Refund to which the PaymentTransaction belongs 
        /// </summary>
        public Guid? RefundId { get; set; }

        /// <summary>
        /// Navigation property representing the associated Refund
        /// </summary>
        [ForeignKey("RefundId")]
        public Refund? RefundId_Refund { get; set; }
        /// <summary>
        /// Name of the PaymentTransaction 
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// Code of the PaymentTransaction 
        /// </summary>
        public string? Code { get; set; }

        /// <summary>
        /// CreatedOn of the PaymentTransaction 
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        /// <summary>
        /// CreatedBy of the PaymentTransaction 
        /// </summary>
        public Guid? CreatedBy { get; set; }

        /// <summary>
        /// UpdatedOn of the PaymentTransaction 
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        /// <summary>
        /// UpdatedBy of the PaymentTransaction 
        /// </summary>
        public Guid? UpdatedBy { get; set; }
    }
}