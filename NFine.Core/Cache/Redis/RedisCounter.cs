using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Code
{
   public class RedisCounter:IDisposable
    {
        private IConnectionMultiplexer _connectionMultiplexer;
        private IDatabase _db;

        private string _prefix;

        public RedisCounter(IConnectionMultiplexer connectionMultiplexer,string prefix=null)
        {
            this._connectionMultiplexer = connectionMultiplexer;
            this._db = this._connectionMultiplexer.GetDatabase();
            this._prefix = prefix;
        }

        private string AddPrefixKey(string key) => this._prefix + key;

        public void Dispose()
        {
            this._db = null;
            this._connectionMultiplexer.Dispose();
        }

        public long GetCounter(string key)
        {
            return this._db.StringBitCount(AddPrefixKey(key));
        }

        public async Task<long> GetCounterAsync(string key)
        {
            return await this._db.StringBitCountAsync(AddPrefixKey(key));
        }

        public double Increment(string key,double value=1)
        {
            return this._db.StringIncrement(AddPrefixKey(key), value);
        }

        public async Task<double> IncrementAsync(string key, double value = 1)
        {
            return await this._db.StringIncrementAsync(AddPrefixKey(key), value);
        }

        public long Decrement(string key, long value = 1)
        {
            return this._db.StringDecrement(AddPrefixKey(key), value);
        }

        public async Task<long> DecrementAsync(string key, long value = 1)
        {
            return await this._db.StringDecrementAsync(AddPrefixKey(key), value);
        }



    }
}
