using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB.Mapping;

namespace ORM.Model
{
    [Table(Name = "Products")]
    class Product
    {
        [PrimaryKey, Identity]
        public int ProductID { get; set; }

        [Column(Name = "ProductName"), NotNull]
        public string Name { get; set; }

        [Column]
        public int SupplierID { get; set; }

        [Column]
        public int CategoryID { get; set; }

        [NotColumn]
        [Association(ThisKey = "CategoryID", OtherKey = "CategoryID")]
        public Category Category { get; set; }

        [NotColumn]
        [Association(ThisKey = "SupplierID", OtherKey = "SupplierID")]
        public Supplier Supplier { get; set; }
    }
}
