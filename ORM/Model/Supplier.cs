using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB.Mapping;

namespace ORM.Model
{
    [Table(Name = "Suppliers")]
    class Supplier
    {
        [PrimaryKey, Identity]
        public int SupplierID { get; set; }

        [Column, NotNull]
        public string CompanyName { get; set; }
    }
}
