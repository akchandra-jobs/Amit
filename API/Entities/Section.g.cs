using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Amit.Entities
{
#pragma warning disable
    /// <summary> 
    /// Represents a section entity with essential details
    /// </summary>
    public class Section
    {
        /// <summary>
        /// TenantId of the Section 
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// Primary key for the Section 
        /// </summary>
        [Key]
        [Required]
        public Guid Id { get; set; }
        /// <summary>
        /// Name of the Section 
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// Foreign key referencing the SeatMap to which the Section belongs 
        /// </summary>
        public Guid? SeatMapId { get; set; }

        /// <summary>
        /// Navigation property representing the associated SeatMap
        /// </summary>
        [ForeignKey("SeatMapId")]
        public SeatMap? SeatMapId_SeatMap { get; set; }

        /// <summary>
        /// CreatedOn of the Section 
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        /// <summary>
        /// CreatedBy of the Section 
        /// </summary>
        public Guid? CreatedBy { get; set; }

        /// <summary>
        /// UpdatedOn of the Section 
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        /// <summary>
        /// UpdatedBy of the Section 
        /// </summary>
        public Guid? UpdatedBy { get; set; }
    }
}