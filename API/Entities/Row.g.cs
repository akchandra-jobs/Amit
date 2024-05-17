using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Amit.Entities
{
#pragma warning disable
    /// <summary> 
    /// Represents a row entity with essential details
    /// </summary>
    public class Row
    {
        /// <summary>
        /// TenantId of the Row 
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// Primary key for the Row 
        /// </summary>
        [Key]
        [Required]
        public Guid Id { get; set; }
        /// <summary>
        /// Name of the Row 
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// Foreign key referencing the Section to which the Row belongs 
        /// </summary>
        public Guid? SectionId { get; set; }

        /// <summary>
        /// Navigation property representing the associated Section
        /// </summary>
        [ForeignKey("SectionId")]
        public Section? SectionId_Section { get; set; }

        /// <summary>
        /// CreatedOn of the Row 
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        /// <summary>
        /// CreatedBy of the Row 
        /// </summary>
        public Guid? CreatedBy { get; set; }

        /// <summary>
        /// UpdatedOn of the Row 
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        /// <summary>
        /// UpdatedBy of the Row 
        /// </summary>
        public Guid? UpdatedBy { get; set; }
    }
}