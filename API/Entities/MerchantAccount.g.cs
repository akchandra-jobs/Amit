using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Amit.Entities
{
#pragma warning disable
    /// <summary> 
    /// Represents a merchantaccount entity with essential details
    /// </summary>
    public class MerchantAccount
    {
        /// <summary>
        /// TenantId of the MerchantAccount 
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// Primary key for the MerchantAccount 
        /// </summary>
        [Key]
        [Required]
        public Guid Id { get; set; }
        /// <summary>
        /// Name of the MerchantAccount 
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// CreatedOn of the MerchantAccount 
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        /// <summary>
        /// CreatedBy of the MerchantAccount 
        /// </summary>
        public Guid? CreatedBy { get; set; }

        /// <summary>
        /// UpdatedOn of the MerchantAccount 
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        /// <summary>
        /// UpdatedBy of the MerchantAccount 
        /// </summary>
        public Guid? UpdatedBy { get; set; }
    }
}