using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Amit.Entities
{
#pragma warning disable
    /// <summary> 
    /// Represents a seatmap entity with essential details
    /// </summary>
    public class SeatMap
    {
        /// <summary>
        /// TenantId of the SeatMap 
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// Primary key for the SeatMap 
        /// </summary>
        [Key]
        [Required]
        public Guid Id { get; set; }
        /// <summary>
        /// Name of the SeatMap 
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// TotalSeats of the SeatMap 
        /// </summary>
        public int? TotalSeats { get; set; }

        /// <summary>
        /// CreatedOn of the SeatMap 
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        /// <summary>
        /// CreatedBy of the SeatMap 
        /// </summary>
        public Guid? CreatedBy { get; set; }

        /// <summary>
        /// UpdatedOn of the SeatMap 
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        /// <summary>
        /// UpdatedBy of the SeatMap 
        /// </summary>
        public Guid? UpdatedBy { get; set; }
    }
}