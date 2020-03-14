using System;
using System.Configuration;
using StackExchange.Redis;

namespace RedisLib
{
	public class RedisClient : IDisposable
	{
        private ConnectionMultiplexer _redisConnection;
		private IDatabase _db;
		public RedisClient(string host)
		{
			_redisConnection = ConnectionMultiplexer.Connect(host);
			_db = _redisConnection.GetDatabase();
		}

		public bool Set(string key, string value, TimeSpan? expiry= null)
		{
			return _db.StringSet(key, value, expiry);
		}

		public string Get(string key)
		{
			var val = _db.StringGet(key);
            if (val == RedisValue.Null || val == RedisValue.EmptyString)
            {
                return string.Empty;
            }

			return val.ToString();
		}

		public void Dispose()
		{
			_redisConnection.Dispose();
		}
	}
}
