using System.Collections.Generic;

using NorthwindLibrary;
using StackExchange.Redis;
using System.Runtime.Serialization;
using System.IO;

namespace CachingSolutionsSamples
{
    class CustomersRedisCache : ICache<Customer>
    {
        private ConnectionMultiplexer _redisConnection;
        string _prefix = "Cache_Customers";
        DataContractSerializer _serializer = new DataContractSerializer(typeof(IEnumerable<Customer>));

        public CustomersRedisCache(string hostName)
        {
            _redisConnection = ConnectionMultiplexer.Connect(hostName);
        }

        public IEnumerable<Customer> Get(string forUser)
        {
            var db = _redisConnection.GetDatabase();

            byte[] cachedValue = db.StringGet(_prefix + forUser);
            if (cachedValue == null)
            {
                return null;
            }

            return (IEnumerable<Customer>)_serializer.ReadObject(new MemoryStream(cachedValue));
        }

        public void Set(string forUser, IEnumerable<Customer> entities)
        {
            var db = _redisConnection.GetDatabase();
            var key = _prefix + forUser;

            if (entities == null)
            {
                db.StringSet(key, RedisValue.Null);
            }
            else
            {
                var stream = new MemoryStream();
                _serializer.WriteObject(stream, entities);
                db.StringSet(key, stream.ToArray());
            }
        }
    }
}
