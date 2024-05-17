using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Amit.Entities
{
#pragma warning disable
    /// <summary> 
    /// Represents a ticket entity with essential details
    /// </summary>
    public class Ticket
    {
        /// <summary>
        /// TenantId of the Ticket 
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// Primary key for the Ticket 
        /// </summary>
        [Key]
        [Required]
        public Guid Id { get; set; }
        /// <summary>
        /// Code of the Ticket 
        /// </summary>
        public string? Code { get; set; }
        /// <summary>
        /// Name of the Ticket 
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// Foreign key referencing the Reservation to which the Ticket belongs 
        /// </summary>
        public Guid? ReservationId { get; set; }

        /// <summary>
        /// Navigation property representing the associated Reservation
        /// </summary>
        [ForeignKey("ReservationId")]
        public Reservation? ReservationId_Reservation { get; set; }
        /// <summary>
        /// Foreign key referencing the Seat to which the Ticket belongs 
        /// </summary>
        public Guid? SeatId { get; set; }

        /// <summary>
        /// Navigation property representing the associated Seat
        /// </summary>
        [ForeignKey("SeatId")]
        public Seat? SeatId_Seat { get; set; }
        /// <summary>
        /// Foreign key referencing the TicketType to which the Ticket belongs 
        /// </summary>
        public Guid? TicketTypeId { get; set; }

        /// <summary>
        /// Navigation property representing the associated TicketType
        /// </summary>
        [ForeignKey("TicketTypeId")]
        public TicketType? TicketTypeId_TicketType { get; set; }

        /// <summary>
        /// CreatedOn of the Ticket 
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        /// <summary>
        /// CreatedBy of the Ticket 
        /// </summary>
        public Guid? CreatedBy { get; set; }

        /// <summary>
        /// UpdatedOn of the Ticket 
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        /// <summary>
        /// UpdatedBy of the Ticket 
        /// </summary>
        public Guid? UpdatedBy { get; set; }
    }
}