using System.Collections.Generic;

namespace CachingSolutionsSamples
{
    interface ICache<TEntity>
    {
        IEnumerable<TEntity> Get(string forUser);
        void Set(string forUser, IEnumerable<TEntity> entities);
    }
}
