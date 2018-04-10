using ElectronicStore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicStore.Data
{
    public class ElectronicStoreDbContext : DbContext
    {
        public ElectronicStoreDbContext() : base("ElectronicStoreDbContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }
        public ElectronicStoreDbContext(DbConnection connection) : base(connection, true)
        {
        }
        public DbSet<About> About { get; set; }
        public DbSet<Contact> Contact { get; set; }
        public DbSet<Feedback> Feedback { get; set; }
        public DbSet<LogError> LogError { get; set; }
        public DbSet<Menu> Menu { get; set; }
        public DbSet<MenuType> MenuType { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<NewsCategory> NewsCategory { get; set; }
        public DbSet<NewsTag> NewsTag { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductCategory> ProductCategory { get; set; }
        public DbSet<ProductTag> ProductTag { get; set; }
        public DbSet<Slide> Slide { get; set; }
        public DbSet<Tag> Tag { get; set; }

        public static ElectronicStoreDbContext Create()
        {
            return new ElectronicStoreDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder builder)
        {
            

        }
    }
}
