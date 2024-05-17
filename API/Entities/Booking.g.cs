using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Amit.Entities
{
#pragma warning disable
    /// <summary> 
    /// Represents a booking entity with essential details
    /// </summary>
    public class Booking
    {
        /// <summary>
        /// TenantId of the Booking 
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// Primary key for the Booking 
        /// </summary>
        [Key]
        [Required]
        public Guid Id { get; set; }
        /// <summary>
        /// Code of the Booking 
        /// </summary>
        public string? Code { get; set; }
        /// <summary>
        /// Foreign key referencing the Customer to which the Booking belongs 
        /// </summary>
        public Guid? CustomerId { get; set; }

        /// <summary>
        /// Navigation property representing the associated Customer
        /// </summary>
        [ForeignKey("CustomerId")]
        public Customer? CustomerId_Customer { get; set; }
        /// <summary>
        /// Foreign key referencing the EventSchedule to which the Booking belongs 
        /// </summary>
        public Guid? EventScheduleId { get; set; }

        /// <summary>
        /// Navigation property representing the associated EventSchedule
        /// </summary>
        [ForeignKey("EventScheduleId")]
        public EventSchedule? EventScheduleId_EventSchedule { get; set; }
        /// <summary>
        /// Foreign key referencing the Payment to which the Booking belongs 
        /// </summary>
        public Guid? PaymentId { get; set; }

        /// <summary>
        /// Navigation property representing the associated Payment
        /// </summary>
        [ForeignKey("PaymentId")]
        public Payment? PaymentId_Payment { get; set; }

        /// <summary>
        /// CreatedOn of the Booking 
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        /// <summary>
        /// CreatedBy of the Booking 
        /// </summary>
        public Guid? CreatedBy { get; set; }

        /// <summary>
        /// UpdatedOn of the Booking 
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        /// <summary>
        /// UpdatedBy of the Booking 
        /// </summary>
        public Guid? UpdatedBy { get; set; }
    }
}