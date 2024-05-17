using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Amit.Entities
{
#pragma warning disable
    /// <summary> 
    /// Represents a transactionfee entity with essential details
    /// </summary>
    public class TransactionFee
    {
        /// <summary>
        /// TenantId of the TransactionFee 
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// Primary key for the TransactionFee 
        /// </summary>
        [Key]
        [Required]
        public Guid Id { get; set; }
        /// <summary>
        /// Amount of the TransactionFee 
        /// </summary>
        public int? Amount { get; set; }
        /// <summary>
        /// Name of the TransactionFee 
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// Code of the TransactionFee 
        /// </summary>
        public string? Code { get; set; }

        /// <summary>
        /// CreatedOn of the TransactionFee 
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        /// <summary>
        /// CreatedBy of the TransactionFee 
        /// </summary>
        public Guid? CreatedBy { get; set; }

        /// <summary>
        /// UpdatedOn of the TransactionFee 
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        /// <summary>
        /// UpdatedBy of the TransactionFee 
        /// </summary>
        public Guid? UpdatedBy { get; set; }
    }
}