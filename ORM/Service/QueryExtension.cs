using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using LinqToDB;
using ORM.Model;

namespace ORM.Service
{
    static class QueryExtension
    {
        [ExpressionMethod("EmployesImpl")]
        public static IEnumerable<Territory> GetTerritories(this Employee employee)
        {
            return TerritoryImpl().Compile()(employee);
        }

        [ExpressionMethod("EmployesImpl")]
        public static IEnumerable<Employee> GetEmployees(this Territory territory)
        {
            return EmployesImpl().Compile()(territory);
        }
        
        static Expression<Func<Employee, IEnumerable<Territory>>> TerritoryImpl()
        {
            return e => e.EmployeeTerritory.Select(r => r.Territory);
        }

        static Expression<Func<Territory, IEnumerable<Employee>>> EmployesImpl()
        {
            return r => r.EmployeeTerritory.Select(e => e.Employee);
        }
    }
}
