using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB;
using ORM.Model;
using ORM.Service;

namespace ORM
{
    class DbNorthwind : LinqToDB.Data.DataConnection
    {
        public DbNorthwind() : base("Northwind") { }

        public ITable<Product> Product { get { return GetTable<Product>(); } }

        public ITable<Category> Category { get { return GetTable<Category>(); } }

        public ITable<Supplier> Supplier { get { return GetTable<Supplier>(); } }

        public ITable<Territory> Territory { get { return GetTable<Territory>(); } }

        public ITable<Employee> Employee { get { return GetTable<Employee>(); } }

        public ITable<EmployeeTerritory> EmployeeTerritory { get { return GetTable<EmployeeTerritory>(); } }

        public static List<Product> Task2_1()
        {
            using (var db = new DbNorthwind())
            {
                var query = db.Product.LoadWith(p => p.Category).LoadWith(p => p.Supplier);
                return query.ToList();
            }
        }

        public static void Task2_2()
        {
            using (var db = new DbNorthwind())
            {
                LinqToDB.Common.Configuration.Linq.AllowMultipleQuery = true;
            
                var territories = 
                (
                    from e in db.Employee
                    select e.GetTerritories()
                ).ToList();

                for (int i = 0; i < territories.Count; i++)
                {
                    territories[i] = territories[i].ToList();
                }

                var employees =
                (
                    from e in db.Employee
                    select e
                ).ToList();

                for (int i = 0; i < territories.Count; i++)
                {
                    Console.WriteLine($"{employees[i].LastName}: {string.Join(",", territories[i])}");
                }
                Console.WriteLine();
            }
        }

        public static void Task2_3()
        {
            using (var db = new DbNorthwind())
            {
                LinqToDB.Common.Configuration.Linq.AllowMultipleQuery = true;
                var employees =
                (
                    from t in db.Territory
                    select t.GetEmployees().Count()
                ).ToList();

                var territories =
                (
                    from r in db.Territory
                    select r
                ).ToList();

                for (int i = 0; i < territories.Count; i++)
                {
                    Console.WriteLine($"{territories[i].Description}: {employees[i]}");
                }
            }
        }

        public static void Task3_2()
        {
            using (var db = new DbNorthwind())
            {
                db.Product
                    .Where(p => p.Name == "Chai")
                    .Set(p => p.CategoryID, 2)
                    .Update();
            }
        }

    }
}
