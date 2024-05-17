using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Amit.Entities
{
#pragma warning disable
    /// <summary> 
    /// Represents a settlement entity with essential details
    /// </summary>
    public class Settlement
    {
        /// <summary>
        /// TenantId of the Settlement 
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// Primary key for the Settlement 
        /// </summary>
        [Key]
        [Required]
        public Guid Id { get; set; }
        /// <summary>
        /// Foreign key referencing the MerchantAccount to which the Settlement belongs 
        /// </summary>
        public Guid? MerchantAccountId { get; set; }

        /// <summary>
        /// Navigation property representing the associated MerchantAccount
        /// </summary>
        [ForeignKey("MerchantAccountId")]
        public MerchantAccount? MerchantAccountId_MerchantAccount { get; set; }
        /// <summary>
        /// Foreign key referencing the Currency to which the Settlement belongs 
        /// </summary>
        public Guid? CurrencyId { get; set; }

        /// <summary>
        /// Navigation property representing the associated Currency
        /// </summary>
        [ForeignKey("CurrencyId")]
        public Currency? CurrencyId_Currency { get; set; }
        /// <summary>
        /// Amount of the Settlement 
        /// </summary>
        public int? Amount { get; set; }

        /// <summary>
        /// CreatedOn of the Settlement 
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        /// <summary>
        /// CreatedBy of the Settlement 
        /// </summary>
        public Guid? CreatedBy { get; set; }

        /// <summary>
        /// UpdatedOn of the Settlement 
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        /// <summary>
        /// UpdatedBy of the Settlement 
        /// </summary>
        public Guid? UpdatedBy { get; set; }
    }
}