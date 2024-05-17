using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Amit.Entities
{
#pragma warning disable
    /// <summary> 
    /// Represents a seat entity with essential details
    /// </summary>
    public class Seat
    {
        /// <summary>
        /// TenantId of the Seat 
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// Primary key for the Seat 
        /// </summary>
        [Key]
        [Required]
        public Guid Id { get; set; }
        /// <summary>
        /// Code of the Seat 
        /// </summary>
        public string? Code { get; set; }
        /// <summary>
        /// Name of the Seat 
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// Row of the Seat 
        /// </summary>
        public int? Row { get; set; }
        /// <summary>
        /// Column of the Seat 
        /// </summary>
        public int? Column { get; set; }
        /// <summary>
        /// Foreign key referencing the Venue to which the Seat belongs 
        /// </summary>
        public Guid? VenueId { get; set; }

        /// <summary>
        /// Navigation property representing the associated Venue
        /// </summary>
        [ForeignKey("VenueId")]
        public Venue? VenueId_Venue { get; set; }

        /// <summary>
        /// CreatedOn of the Seat 
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        /// <summary>
        /// CreatedBy of the Seat 
        /// </summary>
        public Guid? CreatedBy { get; set; }

        /// <summary>
        /// UpdatedOn of the Seat 
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        /// <summary>
        /// UpdatedBy of the Seat 
        /// </summary>
        public Guid? UpdatedBy { get; set; }
    }
}