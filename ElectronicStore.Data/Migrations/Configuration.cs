namespace ElectronicStore.Data.Migrations
{
    using Entities;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ElectronicStore.Data.ElectronicStoreDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ElectronicStore.Data.ElectronicStoreDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

        //    var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ElectronicStoreDbContext()));

        //    var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ElectronicStoreDbContext()));

        //    var user = new ApplicationUser()
        //    {
        //        UserName = "duonghinh0402",
        //        Email = "duonghinh0402@gmail",
        //        EmailConfirmed = true,
        //        BirthDay = new DateTime(1995, 2, 4),
        //        FirstName = "Hinh",
        //        LastName = "Duong",
        //        Active = true

        //    };

        //    manager.Create(user, "12345678");

        //    if (!roleManager.Roles.Any())
        //    {
        //        roleManager.Create(new IdentityRole { Name = "Admin" });
        //        roleManager.Create(new IdentityRole { Name = "User" });
        //    }

        //    var adminUser = manager.FindByEmail("duonghinh0402@gmail.com");

        //    manager.AddToRoles(adminUser.Id, new string[] { "Admin", "User" });
        }
    }
}
