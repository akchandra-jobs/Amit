using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Amit.Entities
{
#pragma warning disable
    /// <summary> 
    /// Represents a promocode entity with essential details
    /// </summary>
    public class PromoCode
    {
        /// <summary>
        /// TenantId of the PromoCode 
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// Primary key for the PromoCode 
        /// </summary>
        [Key]
        [Required]
        public Guid Id { get; set; }
        /// <summary>
        /// Name of the PromoCode 
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// Code of the PromoCode 
        /// </summary>
        public string? Code { get; set; }
        /// <summary>
        /// Foreign key referencing the Discount to which the PromoCode belongs 
        /// </summary>
        public Guid? DiscountId { get; set; }

        /// <summary>
        /// Navigation property representing the associated Discount
        /// </summary>
        [ForeignKey("DiscountId")]
        public Discount? DiscountId_Discount { get; set; }

        /// <summary>
        /// StartDate of the PromoCode 
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// EndDate of the PromoCode 
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// CreatedOn of the PromoCode 
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        /// <summary>
        /// CreatedBy of the PromoCode 
        /// </summary>
        public Guid? CreatedBy { get; set; }

        /// <summary>
        /// UpdatedOn of the PromoCode 
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedOn { get; set; }
        /// <summary>
        /// UpdatedBy of the PromoCode 
        /// </summary>
        public Guid? UpdatedBy { get; set; }
    }
}