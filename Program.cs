using System;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace BasicBloomConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var muxer = await ConnectionMultiplexer.ConnectAsync("localhost");
            try
            {
                await BloomFilter.DoWork(muxer);
                await CountMinSketch.DoWork(muxer);
                await CuckooFilter.DoWork(muxer);
                await TopK.DoWork(muxer);
            }
            finally
            {
                await muxer.GetDatabase().ExecuteAsync("FLUSHDB");
            }
        }
    }
}
