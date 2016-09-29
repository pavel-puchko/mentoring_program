using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB.Mapping;

namespace ORM.Model
{
    [Table(Name = "Territories")]
    class Territory
    {
        [PrimaryKey, Identity]
        [Column(Name = "TerritoryID")]
        public int ID { get; set; }

        [Column(Name = "TerritoryDescription")]
        public string Description { get; set; }

        [NotColumn]
        [Association(ThisKey = "ID", OtherKey = "TerritoryID", IsBackReference = true)]
        public ICollection<EmployeeTerritory> EmployeeTerritory { get; set; }
    }
}
