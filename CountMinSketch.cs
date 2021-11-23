using System;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace BasicBloomConsoleApp
{
    public static class CountMinSketch
    {
        public static async Task DoWork(IConnectionMultiplexer muxer)
        {
            var db = muxer.GetDatabase();
            await db.ExecuteAsync("CMS.INITBYPROB", "cms:views", .1, .01);
            await db.ExecuteAsync("CMS.INCRBY", "cms:views", "Gangnam Style", 1);
            await db.ExecuteAsync("CMS.INCRBY", "cms:views", "Baby Shark", 1);
            await db.ExecuteAsync("CMS.INCRBY", "cms:views", "Gangnam Style", 2);
            var numViewsGangnamStyle = (long)await db.ExecuteAsync("CMS.QUERY", "cms:views", "Gangnam Style");
            var numViewsBabyShark = (long)await db.ExecuteAsync("CMS.QUERY", "cms:views", "Baby Shark");
            Console.WriteLine($"Gangnam Style Views: {numViewsGangnamStyle}");
            Console.WriteLine($"Baby Shark Views: {numViewsBabyShark}");
        }
    }
}