using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace tnine.Core.Shared
{
    public class DatabaseContext : IdentityDbContext<ApplicationUser, ApplicationRole, long, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
    {
        public DatabaseContext() : base("name=DefaultConnection")
        {
            Database.SetInitializer<DatabaseContext>(null);
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ApplicationUserLogin> UserLogins { get; set; }
        public DbSet<ApplicationUserClaim> UserClaims { get; set; }
        public DbSet<ApplicationUserRole> UserRoles { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Colors> Colors { get; set; }
        public DbSet<Sizes> Sizes { get; set; }
        public DbSet<ProductVariations> ProductVariations { get; set; }
        public DbSet<Images> Images { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<PaymentStatus> PaymentStatus { get; set; }
        public DbSet<PaymentMethods> PaymentMethods { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<ProductInvoices> ProductInvoices { get; set; }
        public DbSet<Suppliers> Suppliers { get; set; }
        public DbSet<WarehouseReceipt> WarehouseReceipt { get; set; }
        public DbSet<ProductWarehouseReceipt> ProductWarehouseReceipt { get; set; }

        public static DatabaseContext Create()
        {
            return new DatabaseContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>().ToTable("Users");
            modelBuilder.Entity<ApplicationRole>().ToTable("Roles");
            modelBuilder.Entity<ApplicationUserRole>().ToTable("UserRoles");
            modelBuilder.Entity<ApplicationUserClaim>().ToTable("UserClaims");
            modelBuilder.Entity<ApplicationUserLogin>().ToTable("UserLogins");

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(e => e.Claims)
                .WithRequired()
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(e => e.Logins)
                .WithRequired()
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ApplicationUserRole>()
                .HasKey(i => new { i.UserId, i.RoleId });

            modelBuilder.Entity<ProductVariations>()
            .HasKey(pv => new { pv.ProductId, pv.ColorId, pv.SizeId });

            modelBuilder.Entity<ProductInvoices>()
             .HasKey(pi => new { pi.ProductId, pi.InvoiceId });

            modelBuilder.Entity<ProductWarehouseReceipt>()
                .HasKey(pwr => new { pwr.ProductId, pwr.WarehouseReceiptId, pwr.SizeId, pwr.ColorId });
        }
    }
}
