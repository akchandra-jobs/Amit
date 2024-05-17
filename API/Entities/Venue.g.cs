using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Amit.Entities
{
#pragma warning disable
    /// <summary> 
    /// Represents a venue entity with essential details
    /// </summary>
    public class Venue
    {
        /// <summary>
        /// TenantId of the Venue 
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// Primary key for the Venue 
        /// </summary>
        [Key]
        [Required]
        public Guid Id { get; set; }
        /// <summary>
        /// Code of the Venue 
        /// </summary>
        public string? Code { get; set; }
        /// <summary>
        /// Name of the Venue 
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// Location of the Venue 
        /// </summary>
        public string? Location { get; set; }

        /// <summary>
        /// CreatedOn of the Venue 
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        /// <summary>
        /// CreatedBy of the Venue 
        /// </summary>
        public Guid? CreatedBy { get; set; }

        /// <summary>
        /// UpdatedOn of the Venue 
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        /// <summary>
        /// UpdatedBy of the Venue 
        /// </summary>
        public Guid? UpdatedBy { get; set; }
    }
}