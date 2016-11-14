using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Runtime.Caching;

using NorthwindLibrary;

namespace CachingSolutionsSamples
{
    class CustomersMemoryCacheWithMonitor : ICache<Customer>
    {
        ObjectCache _cache = MemoryCache.Default;
        string _prefix = "Cache_Customer";

        public IEnumerable<Customer> Get(string forUser)
        {
            return (IEnumerable<Customer>)_cache.Get(_prefix + forUser);
        }

        public void Set(string forUser, IEnumerable<Customer> entities)
        {
            CacheItemPolicy policy = new CacheItemPolicy();

            string connectionString = ConfigurationManager.ConnectionStrings["Northwind"].ConnectionString;
            this.StartListening(connectionString);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM dbo.Customers", connection))
                {
                    SqlDependency sqlDependency = new SqlDependency(command);
                    SqlChangeMonitor changeMonitor = new SqlChangeMonitor(sqlDependency);

                    policy.ChangeMonitors.Add(changeMonitor);
                }
            }

            _cache.Set(_prefix + forUser, entities, policy);
        }

        private void StartListening(string connectionString)
        {
            SqlDependency.Stop(connectionString);
            SqlDependency.Start(connectionString);
        }
    }
}
