using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Code
{
    public class RedisClient : IDisposable
    {
        private IConnectionMultiplexer _connectionMultiplexer;
        private IDatabase _db;
        private string _prefix = string.Empty;

        public RedisClient(IConnectionMultiplexer connectionMultiplexer,string prefix)
        {
            this._connectionMultiplexer = connectionMultiplexer;
            this._db = this._connectionMultiplexer.GetDatabase();
            this._prefix = prefix;
        }

        private string AddPrefixKey(string key) => this._prefix + key;

        public string Get(string key) => Get<string>(key);
       

        public T Get<T>(string key) 
        {
            var value =  this._db.StringGet(AddPrefixKey(key));
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(value);
        }

        public async Task<T> GetAsync<T>(string key)
        {
            var value = await this._db.StringGetAsync(AddPrefixKey(key));
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(value);
        }

        public bool Set(string key, string value) => Set<string>(key, value);

        public bool Set<T>(string key, T value)
        {
            var valueString = Newtonsoft.Json.JsonConvert.SerializeObject(value);
            return this._db.StringSet(AddPrefixKey(key), valueString, TimeSpan.FromMinutes(30));
        }

        public async Task<bool> SetAsync<T>(string key, T value)
        {
            var valueString = Newtonsoft.Json.JsonConvert.SerializeObject(value);
            return await this._db.StringSetAsync(AddPrefixKey(key), valueString, TimeSpan.FromMinutes(30));
        }

        public async Task<bool> SetAsync<T>(string key, T value, TimeSpan timeSpan)
        {
            var valueString = Newtonsoft.Json.JsonConvert.SerializeObject(value);
            return await this._db.StringSetAsync(AddPrefixKey(key), valueString, timeSpan);
        }

        public T GetOrSet<T>(string key, Func<T> action)
        {
            var value = this._db.StringGet(AddPrefixKey(key));
            if (value == RedisValue.Null)
            {
                var result = action();
                var resultString = Newtonsoft.Json.JsonConvert.SerializeObject(result);
                return this._db.StringSet(AddPrefixKey(key), resultString, TimeSpan.FromMinutes(30)) ? result : throw new Exception("Redis Set Faild");
            }
            else
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(value);
            }
        }


        public async Task<T> GetOrSetAsync<T>(string key, Func<Task<T>> action, TimeSpan timeSpan)
        {
            var value = await this._db.StringGetAsync(AddPrefixKey(key));
            if (value == RedisValue.Null)
            {
                var result = await action();
                var resultString = Newtonsoft.Json.JsonConvert.SerializeObject(result);
                return (await this._db.StringSetAsync(AddPrefixKey(key), resultString, TimeSpan.FromMinutes(30))) ? result : throw new Exception("Redis Set Faild");
            }
            else
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(value);
            }
        }

        public void Dispose()
        {
            this._db = null;
            this._connectionMultiplexer.Dispose();
        }
    }
}
