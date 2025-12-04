namespace Harvestly.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Harvestly.Models;
    using Harvestly.Helpers;

    internal sealed class Configuration : DbMigrationsConfiguration<Harvestly.Models.HarvestlyContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "Harvestly.Models.HarvestlyContext";
        }

        protected override void Seed(Harvestly.Models.HarvestlyContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
            
            // Add sample categories if they don't exist
            context.Categories.AddOrUpdate(
                c => c.Name,
                new Category { Name = "Vegetables" },
                new Category { Name = "Fruits" },
                new Category { Name = "Dairy" },
                new Category { Name = "Grains" },
                new Category { Name = "Meat" }
            );

            // Create default admin user if it doesn't exist
            if (!context.Users.Any(u => u.Username == "admin"))
            {
                context.Users.AddOrUpdate(
                    u => u.Username,
                    new User
                    {
                        Username = "admin",
                        PasswordHash = PasswordHelper.HashPassword("admin123"), // Default password: admin123
                        Role = UserRole.Admin,
                        Email = "admin@harvestly.com",
                        FullName = "System Administrator",
                        CreatedDate = DateTime.Now,
                        IsActive = true
                    }
                );
            }
            
            context.SaveChanges();
        }
    }
}
