namespace Harvestly.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Harvestly.Models;

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
            
            context.SaveChanges();
        }
    }
}
