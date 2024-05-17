using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Amit.Entities
{
#pragma warning disable
    /// <summary> 
    /// Represents a chargeback entity with essential details
    /// </summary>
    public class Chargeback
    {
        /// <summary>
        /// TenantId of the Chargeback 
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// Primary key for the Chargeback 
        /// </summary>
        [Key]
        [Required]
        public Guid Id { get; set; }
        /// <summary>
        /// Foreign key referencing the Settlement to which the Chargeback belongs 
        /// </summary>
        public Guid? SettlementId { get; set; }

        /// <summary>
        /// Navigation property representing the associated Settlement
        /// </summary>
        [ForeignKey("SettlementId")]
        public Settlement? SettlementId_Settlement { get; set; }
        /// <summary>
        /// Amount of the Chargeback 
        /// </summary>
        public int? Amount { get; set; }
        /// <summary>
        /// Foreign key referencing the Currency to which the Chargeback belongs 
        /// </summary>
        public Guid? CurrencyId { get; set; }

        /// <summary>
        /// Navigation property representing the associated Currency
        /// </summary>
        [ForeignKey("CurrencyId")]
        public Currency? CurrencyId_Currency { get; set; }

        /// <summary>
        /// CreatedOn of the Chargeback 
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        /// <summary>
        /// CreatedBy of the Chargeback 
        /// </summary>
        public Guid? CreatedBy { get; set; }

        /// <summary>
        /// UpdatedOn of the Chargeback 
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        /// <summary>
        /// UpdatedBy of the Chargeback 
        /// </summary>
        public Guid? UpdatedBy { get; set; }
    }
}