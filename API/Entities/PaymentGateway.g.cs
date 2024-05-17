using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Amit.Entities
{
#pragma warning disable
    /// <summary> 
    /// Represents a paymentgateway entity with essential details
    /// </summary>
    public class PaymentGateway
    {
        /// <summary>
        /// TenantId of the PaymentGateway 
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// Primary key for the PaymentGateway 
        /// </summary>
        [Key]
        [Required]
        public Guid Id { get; set; }
        /// <summary>
        /// Name of the PaymentGateway 
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// Code of the PaymentGateway 
        /// </summary>
        public string? Code { get; set; }

        /// <summary>
        /// CreatedOn of the PaymentGateway 
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        /// <summary>
        /// CreatedBy of the PaymentGateway 
        /// </summary>
        public Guid? CreatedBy { get; set; }

        /// <summary>
        /// UpdatedOn of the PaymentGateway 
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        /// <summary>
        /// UpdatedBy of the PaymentGateway 
        /// </summary>
        public Guid? UpdatedBy { get; set; }
    }
}