using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Amit.Entities
{
#pragma warning disable
    /// <summary> 
    /// Represents a organizer entity with essential details
    /// </summary>
    public class Organizer
    {
        /// <summary>
        /// TenantId of the Organizer 
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// Primary key for the Organizer 
        /// </summary>
        [Key]
        [Required]
        public Guid Id { get; set; }
        /// <summary>
        /// Name of the Organizer 
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// ContactEmail of the Organizer 
        /// </summary>
        public string? ContactEmail { get; set; }
        /// <summary>
        /// PhoneNumber of the Organizer 
        /// </summary>
        public string? PhoneNumber { get; set; }
        /// <summary>
        /// Address of the Organizer 
        /// </summary>
        public string? Address { get; set; }
        /// <summary>
        /// Website of the Organizer 
        /// </summary>
        public string? Website { get; set; }

        /// <summary>
        /// CreatedOn of the Organizer 
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        /// <summary>
        /// CreatedBy of the Organizer 
        /// </summary>
        public Guid? CreatedBy { get; set; }

        /// <summary>
        /// UpdatedOn of the Organizer 
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        /// <summary>
        /// UpdatedBy of the Organizer 
        /// </summary>
        public Guid? UpdatedBy { get; set; }
    }
}