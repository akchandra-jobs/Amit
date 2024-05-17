using Microsoft.EntityFrameworkCore;
using Amit.Entities;

namespace Amit.Data
{
    /// <summary>
    /// Represents the database context for the application.
    /// </summary>
    public class AmitContext : DbContext
    {
        /// <summary>
        /// Configures the database connection options.
        /// </summary>
        /// <param name="optionsBuilder">The options builder used to configure the database connection.</param>
        protected override void OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=codezen.database.windows.net;Initial Catalog=T426724_Amit;Persist Security Info=True;user id=Lowcodeadmin;password=NtLowCode^123*;Integrated Security=false;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=true;");
        }

        /// <summary>
        /// Configures the model relationships and entity mappings.
        /// </summary>
        /// <param name="modelBuilder">The model builder used to configure the database model.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserInRole>().HasKey(a => a.Id);
            modelBuilder.Entity<UserToken>().HasKey(a => a.Id);
            modelBuilder.Entity<RoleEntitlement>().HasKey(a => a.Id);
            modelBuilder.Entity<Entity>().HasKey(a => a.Id);
            modelBuilder.Entity<Tenant>().HasKey(a => a.Id);
            modelBuilder.Entity<User>().HasKey(a => a.Id);
            modelBuilder.Entity<Role>().HasKey(a => a.Id);
            modelBuilder.Entity<Ticket>().HasKey(a => a.Id);
            modelBuilder.Entity<Reservation>().HasKey(a => a.Id);
            modelBuilder.Entity<Event>().HasKey(a => a.Id);
            modelBuilder.Entity<Venue>().HasKey(a => a.Id);
            modelBuilder.Entity<Seat>().HasKey(a => a.Id);
            modelBuilder.Entity<TicketType>().HasKey(a => a.Id);
            modelBuilder.Entity<Pricing>().HasKey(a => a.Id);
            modelBuilder.Entity<Customer>().HasKey(a => a.Id);
            modelBuilder.Entity<Payment>().HasKey(a => a.Id);
            modelBuilder.Entity<PaymentMethod>().HasKey(a => a.Id);
            modelBuilder.Entity<Booking>().HasKey(a => a.Id);
            modelBuilder.Entity<TicketStatus>().HasKey(a => a.Id);
            modelBuilder.Entity<EventCategory>().HasKey(a => a.Id);
            modelBuilder.Entity<EventSchedule>().HasKey(a => a.Id);
            modelBuilder.Entity<Discount>().HasKey(a => a.Id);
            modelBuilder.Entity<PromoCode>().HasKey(a => a.Id);
            modelBuilder.Entity<Organizer>().HasKey(a => a.Id);
            modelBuilder.Entity<RefundPolicy>().HasKey(a => a.Id);
            modelBuilder.Entity<Section>().HasKey(a => a.Id);
            modelBuilder.Entity<Row>().HasKey(a => a.Id);
            modelBuilder.Entity<SeatMap>().HasKey(a => a.Id);
            modelBuilder.Entity<TicketHolder>().HasKey(a => a.Id);
            modelBuilder.Entity<TicketDeliveryMethod>().HasKey(a => a.Id);
            modelBuilder.Entity<PaymentTransaction>().HasKey(a => a.Id);
            modelBuilder.Entity<PaymentGateway>().HasKey(a => a.Id);
            modelBuilder.Entity<PaymentStatus>().HasKey(a => a.Id);
            modelBuilder.Entity<CardIssuer>().HasKey(a => a.Id);
            modelBuilder.Entity<CardType>().HasKey(a => a.Id);
            modelBuilder.Entity<TransactionFee>().HasKey(a => a.Id);
            modelBuilder.Entity<Refund>().HasKey(a => a.Id);
            modelBuilder.Entity<Chargeback>().HasKey(a => a.Id);
            modelBuilder.Entity<Settlement>().HasKey(a => a.Id);
            modelBuilder.Entity<Currency>().HasKey(a => a.Id);
            modelBuilder.Entity<PaymentAccount>().HasKey(a => a.Id);
            modelBuilder.Entity<BillingAddress>().HasKey(a => a.Id);
            modelBuilder.Entity<MerchantAccount>().HasKey(a => a.Id);
            modelBuilder.Entity<UserInRole>().HasOne(a => a.TenantId_Tenant).WithMany().HasForeignKey(c => c.TenantId);
            modelBuilder.Entity<UserInRole>().HasOne(a => a.RoleId_Role).WithMany().HasForeignKey(c => c.RoleId);
            modelBuilder.Entity<UserInRole>().HasOne(a => a.UserId_User).WithMany().HasForeignKey(c => c.UserId);
            modelBuilder.Entity<UserInRole>().HasOne(a => a.CreatedBy_User).WithMany().HasForeignKey(c => c.CreatedBy);
            modelBuilder.Entity<UserInRole>().HasOne(a => a.UpdatedBy_User).WithMany().HasForeignKey(c => c.UpdatedBy);
            modelBuilder.Entity<UserToken>().HasOne(a => a.TenantId_Tenant).WithMany().HasForeignKey(c => c.TenantId);
            modelBuilder.Entity<UserToken>().HasOne(a => a.UserId_User).WithMany().HasForeignKey(c => c.UserId);
            modelBuilder.Entity<RoleEntitlement>().HasOne(a => a.TenantId_Tenant).WithMany().HasForeignKey(c => c.TenantId);
            modelBuilder.Entity<RoleEntitlement>().HasOne(a => a.RoleId_Role).WithMany().HasForeignKey(c => c.RoleId);
            modelBuilder.Entity<RoleEntitlement>().HasOne(a => a.EntityId_Entity).WithMany().HasForeignKey(c => c.EntityId);
            modelBuilder.Entity<RoleEntitlement>().HasOne(a => a.CreatedBy_User).WithMany().HasForeignKey(c => c.CreatedBy);
            modelBuilder.Entity<RoleEntitlement>().HasOne(a => a.UpdatedBy_User).WithMany().HasForeignKey(c => c.UpdatedBy);
            modelBuilder.Entity<Entity>().HasOne(a => a.TenantId_Tenant).WithMany().HasForeignKey(c => c.TenantId);
            modelBuilder.Entity<Entity>().HasOne(a => a.CreatedBy_User).WithMany().HasForeignKey(c => c.CreatedBy);
            modelBuilder.Entity<Entity>().HasOne(a => a.UpdatedBy_User).WithMany().HasForeignKey(c => c.UpdatedBy);
            modelBuilder.Entity<User>().HasOne(a => a.TenantId_Tenant).WithMany().HasForeignKey(c => c.TenantId);
            modelBuilder.Entity<Role>().HasOne(a => a.TenantId_Tenant).WithMany().HasForeignKey(c => c.TenantId);
            modelBuilder.Entity<Role>().HasOne(a => a.CreatedBy_User).WithMany().HasForeignKey(c => c.CreatedBy);
            modelBuilder.Entity<Role>().HasOne(a => a.UpdatedBy_User).WithMany().HasForeignKey(c => c.UpdatedBy);
            modelBuilder.Entity<Ticket>().HasOne(a => a.ReservationId_Reservation).WithMany().HasForeignKey(c => c.ReservationId);
            modelBuilder.Entity<Ticket>().HasOne(a => a.SeatId_Seat).WithMany().HasForeignKey(c => c.SeatId);
            modelBuilder.Entity<Ticket>().HasOne(a => a.TicketTypeId_TicketType).WithMany().HasForeignKey(c => c.TicketTypeId);
            modelBuilder.Entity<Reservation>().HasOne(a => a.EventId_Event).WithMany().HasForeignKey(c => c.EventId);
            modelBuilder.Entity<Event>().HasOne(a => a.VenueId_Venue).WithMany().HasForeignKey(c => c.VenueId);
            modelBuilder.Entity<Seat>().HasOne(a => a.VenueId_Venue).WithMany().HasForeignKey(c => c.VenueId);
            modelBuilder.Entity<Pricing>().HasOne(a => a.EventId_Event).WithMany().HasForeignKey(c => c.EventId);
            modelBuilder.Entity<Pricing>().HasOne(a => a.TicketTypeId_TicketType).WithMany().HasForeignKey(c => c.TicketTypeId);
            modelBuilder.Entity<Payment>().HasOne(a => a.PaymentMethodId_PaymentMethod).WithMany().HasForeignKey(c => c.PaymentMethodId);
            modelBuilder.Entity<Booking>().HasOne(a => a.CustomerId_Customer).WithMany().HasForeignKey(c => c.CustomerId);
            modelBuilder.Entity<Booking>().HasOne(a => a.EventScheduleId_EventSchedule).WithMany().HasForeignKey(c => c.EventScheduleId);
            modelBuilder.Entity<Booking>().HasOne(a => a.PaymentId_Payment).WithMany().HasForeignKey(c => c.PaymentId);
            modelBuilder.Entity<EventSchedule>().HasOne(a => a.EventCategoryId_EventCategory).WithMany().HasForeignKey(c => c.EventCategoryId);
            modelBuilder.Entity<PromoCode>().HasOne(a => a.DiscountId_Discount).WithMany().HasForeignKey(c => c.DiscountId);
            modelBuilder.Entity<Section>().HasOne(a => a.SeatMapId_SeatMap).WithMany().HasForeignKey(c => c.SeatMapId);
            modelBuilder.Entity<Row>().HasOne(a => a.SectionId_Section).WithMany().HasForeignKey(c => c.SectionId);
            modelBuilder.Entity<PaymentTransaction>().HasOne(a => a.PaymentGatewayId_PaymentGateway).WithMany().HasForeignKey(c => c.PaymentGatewayId);
            modelBuilder.Entity<PaymentTransaction>().HasOne(a => a.PaymentStatusId_PaymentStatus).WithMany().HasForeignKey(c => c.PaymentStatusId);
            modelBuilder.Entity<PaymentTransaction>().HasOne(a => a.CardIssuerId_CardIssuer).WithMany().HasForeignKey(c => c.CardIssuerId);
            modelBuilder.Entity<PaymentTransaction>().HasOne(a => a.CardTypeId_CardType).WithMany().HasForeignKey(c => c.CardTypeId);
            modelBuilder.Entity<PaymentTransaction>().HasOne(a => a.TransactionFeeId_TransactionFee).WithMany().HasForeignKey(c => c.TransactionFeeId);
            modelBuilder.Entity<PaymentTransaction>().HasOne(a => a.RefundId_Refund).WithMany().HasForeignKey(c => c.RefundId);
            modelBuilder.Entity<Refund>().HasOne(a => a.PaymentTransactionId_PaymentTransaction).WithMany().HasForeignKey(c => c.PaymentTransactionId);
            modelBuilder.Entity<Chargeback>().HasOne(a => a.SettlementId_Settlement).WithMany().HasForeignKey(c => c.SettlementId);
            modelBuilder.Entity<Chargeback>().HasOne(a => a.CurrencyId_Currency).WithMany().HasForeignKey(c => c.CurrencyId);
            modelBuilder.Entity<Settlement>().HasOne(a => a.MerchantAccountId_MerchantAccount).WithMany().HasForeignKey(c => c.MerchantAccountId);
            modelBuilder.Entity<Settlement>().HasOne(a => a.CurrencyId_Currency).WithMany().HasForeignKey(c => c.CurrencyId);
            modelBuilder.Entity<PaymentAccount>().HasOne(a => a.BillingAddressId_BillingAddress).WithMany().HasForeignKey(c => c.BillingAddressId);
        }

        /// <summary>
        /// Represents the database set for the UserInRole entity.
        /// </summary>
        public DbSet<UserInRole> UserInRole { get; set; }

        /// <summary>
        /// Represents the database set for the UserToken entity.
        /// </summary>
        public DbSet<UserToken> UserToken { get; set; }

        /// <summary>
        /// Represents the database set for the RoleEntitlement entity.
        /// </summary>
        public DbSet<RoleEntitlement> RoleEntitlement { get; set; }

        /// <summary>
        /// Represents the database set for the Entity entity.
        /// </summary>
        public DbSet<Entity> Entity { get; set; }

        /// <summary>
        /// Represents the database set for the Tenant entity.
        /// </summary>
        public DbSet<Tenant> Tenant { get; set; }

        /// <summary>
        /// Represents the database set for the User entity.
        /// </summary>
        public DbSet<User> User { get; set; }

        /// <summary>
        /// Represents the database set for the Role entity.
        /// </summary>
        public DbSet<Role> Role { get; set; }

        /// <summary>
        /// Represents the database set for the Ticket entity.
        /// </summary>
        public DbSet<Ticket> Ticket { get; set; }

        /// <summary>
        /// Represents the database set for the Reservation entity.
        /// </summary>
        public DbSet<Reservation> Reservation { get; set; }

        /// <summary>
        /// Represents the database set for the Event entity.
        /// </summary>
        public DbSet<Event> Event { get; set; }

        /// <summary>
        /// Represents the database set for the Venue entity.
        /// </summary>
        public DbSet<Venue> Venue { get; set; }

        /// <summary>
        /// Represents the database set for the Seat entity.
        /// </summary>
        public DbSet<Seat> Seat { get; set; }

        /// <summary>
        /// Represents the database set for the TicketType entity.
        /// </summary>
        public DbSet<TicketType> TicketType { get; set; }

        /// <summary>
        /// Represents the database set for the Pricing entity.
        /// </summary>
        public DbSet<Pricing> Pricing { get; set; }

        /// <summary>
        /// Represents the database set for the Customer entity.
        /// </summary>
        public DbSet<Customer> Customer { get; set; }

        /// <summary>
        /// Represents the database set for the Payment entity.
        /// </summary>
        public DbSet<Payment> Payment { get; set; }

        /// <summary>
        /// Represents the database set for the PaymentMethod entity.
        /// </summary>
        public DbSet<PaymentMethod> PaymentMethod { get; set; }

        /// <summary>
        /// Represents the database set for the Booking entity.
        /// </summary>
        public DbSet<Booking> Booking { get; set; }

        /// <summary>
        /// Represents the database set for the TicketStatus entity.
        /// </summary>
        public DbSet<TicketStatus> TicketStatus { get; set; }

        /// <summary>
        /// Represents the database set for the EventCategory entity.
        /// </summary>
        public DbSet<EventCategory> EventCategory { get; set; }

        /// <summary>
        /// Represents the database set for the EventSchedule entity.
        /// </summary>
        public DbSet<EventSchedule> EventSchedule { get; set; }

        /// <summary>
        /// Represents the database set for the Discount entity.
        /// </summary>
        public DbSet<Discount> Discount { get; set; }

        /// <summary>
        /// Represents the database set for the PromoCode entity.
        /// </summary>
        public DbSet<PromoCode> PromoCode { get; set; }

        /// <summary>
        /// Represents the database set for the Organizer entity.
        /// </summary>
        public DbSet<Organizer> Organizer { get; set; }

        /// <summary>
        /// Represents the database set for the RefundPolicy entity.
        /// </summary>
        public DbSet<RefundPolicy> RefundPolicy { get; set; }

        /// <summary>
        /// Represents the database set for the Section entity.
        /// </summary>
        public DbSet<Section> Section { get; set; }

        /// <summary>
        /// Represents the database set for the Row entity.
        /// </summary>
        public DbSet<Row> Row { get; set; }

        /// <summary>
        /// Represents the database set for the SeatMap entity.
        /// </summary>
        public DbSet<SeatMap> SeatMap { get; set; }

        /// <summary>
        /// Represents the database set for the TicketHolder entity.
        /// </summary>
        public DbSet<TicketHolder> TicketHolder { get; set; }

        /// <summary>
        /// Represents the database set for the TicketDeliveryMethod entity.
        /// </summary>
        public DbSet<TicketDeliveryMethod> TicketDeliveryMethod { get; set; }

        /// <summary>
        /// Represents the database set for the PaymentTransaction entity.
        /// </summary>
        public DbSet<PaymentTransaction> PaymentTransaction { get; set; }

        /// <summary>
        /// Represents the database set for the PaymentGateway entity.
        /// </summary>
        public DbSet<PaymentGateway> PaymentGateway { get; set; }

        /// <summary>
        /// Represents the database set for the PaymentStatus entity.
        /// </summary>
        public DbSet<PaymentStatus> PaymentStatus { get; set; }

        /// <summary>
        /// Represents the database set for the CardIssuer entity.
        /// </summary>
        public DbSet<CardIssuer> CardIssuer { get; set; }

        /// <summary>
        /// Represents the database set for the CardType entity.
        /// </summary>
        public DbSet<CardType> CardType { get; set; }

        /// <summary>
        /// Represents the database set for the TransactionFee entity.
        /// </summary>
        public DbSet<TransactionFee> TransactionFee { get; set; }

        /// <summary>
        /// Represents the database set for the Refund entity.
        /// </summary>
        public DbSet<Refund> Refund { get; set; }

        /// <summary>
        /// Represents the database set for the Chargeback entity.
        /// </summary>
        public DbSet<Chargeback> Chargeback { get; set; }

        /// <summary>
        /// Represents the database set for the Settlement entity.
        /// </summary>
        public DbSet<Settlement> Settlement { get; set; }

        /// <summary>
        /// Represents the database set for the Currency entity.
        /// </summary>
        public DbSet<Currency> Currency { get; set; }

        /// <summary>
        /// Represents the database set for the PaymentAccount entity.
        /// </summary>
        public DbSet<PaymentAccount> PaymentAccount { get; set; }

        /// <summary>
        /// Represents the database set for the BillingAddress entity.
        /// </summary>
        public DbSet<BillingAddress> BillingAddress { get; set; }

        /// <summary>
        /// Represents the database set for the MerchantAccount entity.
        /// </summary>
        public DbSet<MerchantAccount> MerchantAccount { get; set; }
    }
}