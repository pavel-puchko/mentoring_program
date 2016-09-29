using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB.Mapping;

namespace ORM.Model
{
    [Table(Name = "EmployeeTerritories")]
    class EmployeeTerritory
    {
        [Column]
        public int EmployeeID { get; set; }

        [Column]
        public int TerritoryID { get; set; }

        [NotColumn]
        [Association(ThisKey = "EmployeeID", OtherKey = "ID", BackReferenceName = "EmployeeTerritory")]
        public Employee Employee { get; set; }

        [NotColumn]
        [Association(ThisKey = "TerritoryID", OtherKey = "ID", BackReferenceName = "EmployeeTerritory")]
        public Territory Territory { get; set; }
    }
}
