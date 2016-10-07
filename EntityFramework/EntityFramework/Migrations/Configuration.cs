namespace EntityFramework.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Northwind>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Northwind context)
        {
			context.Categories.AddOrUpdate(c => c.CategoryID,
				new Category() { CategoryName = "testName" });

			context.Regions.AddOrUpdate(r => r.RegionID,
				new Region() { RegionDescription = "testDescription" });

			context.Territories.AddOrUpdate(t => t.TerritoryID,
				new Territory() { TerritoryDescription = "testDescription", RegionID = 1 });
		}
    }
}
