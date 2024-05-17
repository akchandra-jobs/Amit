using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Amit.Entities
{
#pragma warning disable
    /// <summary> 
    /// Represents a cardissuer entity with essential details
    /// </summary>
    public class CardIssuer
    {
        /// <summary>
        /// TenantId of the CardIssuer 
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// Primary key for the CardIssuer 
        /// </summary>
        [Key]
        [Required]
        public Guid Id { get; set; }
        /// <summary>
        /// Name of the CardIssuer 
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// Code of the CardIssuer 
        /// </summary>
        public string? Code { get; set; }

        /// <summary>
        /// CreatedOn of the CardIssuer 
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        /// <summary>
        /// CreatedBy of the CardIssuer 
        /// </summary>
        public Guid? CreatedBy { get; set; }

        /// <summary>
        /// UpdatedOn of the CardIssuer 
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        /// <summary>
        /// UpdatedBy of the CardIssuer 
        /// </summary>
        public Guid? UpdatedBy { get; set; }
    }
}