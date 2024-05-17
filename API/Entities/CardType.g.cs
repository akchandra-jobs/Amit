using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Amit.Entities
{
#pragma warning disable
    /// <summary> 
    /// Represents a cardtype entity with essential details
    /// </summary>
    public class CardType
    {
        /// <summary>
        /// TenantId of the CardType 
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// Primary key for the CardType 
        /// </summary>
        [Key]
        [Required]
        public Guid Id { get; set; }
        /// <summary>
        /// Name of the CardType 
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// Code of the CardType 
        /// </summary>
        public string? Code { get; set; }

        /// <summary>
        /// CreatedOn of the CardType 
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        /// <summary>
        /// CreatedBy of the CardType 
        /// </summary>
        public Guid? CreatedBy { get; set; }

        /// <summary>
        /// UpdatedOn of the CardType 
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        /// <summary>
        /// UpdatedBy of the CardType 
        /// </summary>
        public Guid? UpdatedBy { get; set; }
    }
}