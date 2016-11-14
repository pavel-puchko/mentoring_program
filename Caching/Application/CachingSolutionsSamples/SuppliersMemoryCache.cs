using System.Collections.Generic;

using NorthwindLibrary;
using System.Runtime.Caching;

namespace CachingSolutionsSamples
{
    class SuppliersMemoryCache : ICache<Supplier>
    {
        ObjectCache _cache = MemoryCache.Default;
        string _prefix = "Cache_Suppliers";

        public IEnumerable<Supplier> Get(string forUser)
        {
            return (IEnumerable<Supplier>)_cache.Get(_prefix + forUser);
        }

        public void Set(string forUser, IEnumerable<Supplier> entities)
        {
            _cache.Set(_prefix + forUser, entities, ObjectCache.InfiniteAbsoluteExpiration);
        }
    }
}
