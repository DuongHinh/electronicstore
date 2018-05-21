using ElectronicStore.Data.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Common;
using System.Data.Entity;

namespace ElectronicStore.Data
{
    public class ElectronicStoreDbContext : IdentityDbContext<ApplicationUser>
    {
        public ElectronicStoreDbContext() : base("ElectronicStoreDbContext")
        {
            //this.Configuration.LazyLoadingEnabled = true;
        }

        public ElectronicStoreDbContext(DbConnection connection) : base(connection, true)
        {
        }

        public DbSet<About> About { get; set; }
        public DbSet<Contact> Contact { get; set; }
        public DbSet<Feedback> Feedback { get; set; }
        public DbSet<LogError> LogError { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<NewsCategory> NewsCategory { get; set; }
        public DbSet<NewsTag> NewsTag { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductCategory> ProductCategory { get; set; }
        public DbSet<ProductTag> ProductTag { get; set; }
        public DbSet<Slide> Slide { get; set; }
        public DbSet<Brand> Brand { get; set; }
        public DbSet<Tag> Tag { get; set; }
        public DbSet<Group> Group { set; get; }
        public DbSet<Role> Role { set; get; }
        public DbSet<RoleGroup> RoleGroup { set; get; }
        public DbSet<UserGroup> UserGroup { set; get; }

        public static ElectronicStoreDbContext Create()
        {
            return new ElectronicStoreDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder builder)
        {
            builder.Entity<ApplicationUser>().ToTable("Users");
            builder.Entity<IdentityUserRole>().HasKey(i => new { i.UserId, i.RoleId }).ToTable("UserRoles");
            builder.Entity<IdentityUserLogin>().HasKey(i => i.UserId).ToTable("UserLogins");
            builder.Entity<IdentityRole>().ToTable("Roles");
            builder.Entity<IdentityUserClaim>().HasKey(i => i.UserId).ToTable("UserClaims");
        }
    }
}