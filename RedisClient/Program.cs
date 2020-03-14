using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// A redis testing client 
/// </summary>
namespace RedisClient
{
    class Program
    {

        static void Main(string[] args)
        {
            string ip = ConfigurationManager.AppSettings["Redis_Ip"];
            var list = Enumerable.Range(0, 100).ToList();
            var redis = new RedisLib.RedisClient(ip);

            for (int i = 0; i < list.Count; i++)
            {
                redis.Set($"hello_{i}", $"{DateTime.Now.Ticks}_{i}", TimeSpan.FromSeconds(10));
            }

            for (int i = 0; i < list.Count; i++)
            {
                var v = redis.Get($"hello_{i}");
                Console.WriteLine($"value :{v}");
            }

            Console.ReadLine();
        }
    }
}
