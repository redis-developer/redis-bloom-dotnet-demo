using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace BasicBloomConsoleApp
{
    public static class TopK
    {
        public static async Task DoWork(IConnectionMultiplexer muxer)
        {
            var db = muxer.GetDatabase();
            await db.ExecuteAsync("TOPK.RESERVE", "topk:views", 5);
            var videos = new[]
                {"Gangnam Style", "Baby Shark", "Despacito", "Uptown Funk", "See You Again", "Hello", "Roar", "Sorry"};
            var rand = new Random();
            var args = new List<string>(10001){"topk:views"};
            for (var i = 0; i < 10000; i++)
            {
                args.Add(videos[rand.Next(videos.Length)]);
            }

            await db.ExecuteAsync("TOPK.ADD", args.ToArray());
            var topK = (RedisResult[]) await db.ExecuteAsync("TOPK.LIST", "topk:views");
            foreach (var item in topK)
            {
                Console.WriteLine(item);
            }

            var BabySharkInTopK = (int) await db.ExecuteAsync("TOPK.QUERY", "topk:views", "Baby Shark") == 1;
            Console.WriteLine(BabySharkInTopK ? "Baby Shark is in the Top 5" : "Baby Shark is Not in the Top 5" );
            
        }
    }
}