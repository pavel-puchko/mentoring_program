using System.Runtime.Caching;

namespace Fibonacci
{
    class FibonacciNumberMemoryCache : IFibonacciNumberCache
    {
        private ObjectCache _cache = MemoryCache.Default;

        public int? Get(int number)
        {
            int? result = null;
            object cachedValue = _cache.Get(number.ToString());

            if (cachedValue != null)
            {
                result = (int)cachedValue;
            }

            return result;
        }

        public void Set(int number, int value)
        {
            _cache.Set(number.ToString(), value, ObjectCache.InfiniteAbsoluteExpiration);
        }
    }
}
