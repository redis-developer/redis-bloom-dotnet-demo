using System;
using System.Diagnostics;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace BasicBloomConsoleApp
{
    public static class CuckooFilter
    {
        public static async Task DoWork(IConnectionMultiplexer muxer)
        {
            var db = muxer.GetDatabase();
            await db.ExecuteAsync("CF.RESERVE", "cf:emails", 10000);
            await db.ExecuteAsync("CF.ADD", "cf:emails", "foo@bar.com");
            await db.ExecuteAsync("CF.ADD", "cf:emails", "James.Bond@mi6.com");
            
            var jamesEmailExists = (int) await db.ExecuteAsync("CF.EXISTS", "cf:emails", "James.Bond@mi6.com") == 1;
            var str = jamesEmailExists
                ? "James.Bond@mi6.com has already been added"
                : "James.Bond@mi6.com has not been added";
            Console.WriteLine(str);
        }
    }
}