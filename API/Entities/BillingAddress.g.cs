using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Amit.Entities
{
#pragma warning disable
    /// <summary> 
    /// Represents a billingaddress entity with essential details
    /// </summary>
    public class BillingAddress
    {
        /// <summary>
        /// TenantId of the BillingAddress 
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// Primary key for the BillingAddress 
        /// </summary>
        [Key]
        [Required]
        public Guid Id { get; set; }
        /// <summary>
        /// Name of the BillingAddress 
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// AddressLine1 of the BillingAddress 
        /// </summary>
        public string? AddressLine1 { get; set; }
        /// <summary>
        /// AddressLine2 of the BillingAddress 
        /// </summary>
        public string? AddressLine2 { get; set; }
        /// <summary>
        /// City of the BillingAddress 
        /// </summary>
        public string? City { get; set; }
        /// <summary>
        /// State of the BillingAddress 
        /// </summary>
        public string? State { get; set; }
        /// <summary>
        /// PostalCode of the BillingAddress 
        /// </summary>
        public string? PostalCode { get; set; }
        /// <summary>
        /// Country of the BillingAddress 
        /// </summary>
        public string? Country { get; set; }

        /// <summary>
        /// CreatedOn of the BillingAddress 
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        /// <summary>
        /// CreatedBy of the BillingAddress 
        /// </summary>
        public Guid? CreatedBy { get; set; }

        /// <summary>
        /// UpdatedOn of the BillingAddress 
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        /// <summary>
        /// UpdatedBy of the BillingAddress 
        /// </summary>
        public Guid? UpdatedBy { get; set; }
    }
}