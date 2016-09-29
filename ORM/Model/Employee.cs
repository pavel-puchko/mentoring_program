using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB.Mapping;

namespace ORM.Model
{
    [Table(Name = "Employees")]
    class Employee
    {
        [PrimaryKey, Identity]
        [Column(Name = "EmployeeID")]
        public int ID { get; set; }

        [Column]
        public string LastName { get; set; }

        [NotColumn]
        [Association(ThisKey = "ID", OtherKey = "EmployeeID", IsBackReference = true)]
        public ICollection<EmployeeTerritory> EmployeeTerritory { get; set; }
    }
}
