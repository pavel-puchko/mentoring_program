using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB.Mapping;

namespace ORM.Model
{
    [Table(Name = "Categories")]
    class Category
    {
        [PrimaryKey, Identity]
        public int CategoryID { get; set; }

        [Column, NotNull]
        public string CategoryName { get; set; }
    }
}
