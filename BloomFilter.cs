using System;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace BasicBloomConsoleApp
{
    public static class BloomFilter
    {
        public static async Task DoWork(IConnectionMultiplexer muxer)
        {
            var db = muxer.GetDatabase();
            await db.ExecuteAsync("BF.RESERVE", "bf:username", .01, 10000);
            await db.ExecuteAsync("BF.ADD", "bf:username", "Kermit");
            var exists = (int)await db.ExecuteAsync("BF.EXISTS", "bf:username", "Kermit") == 1;
            var str = exists ? "Kermit has been added" : "Kermit has not been added";
            Console.WriteLine(str);
        }
    }
}