# Basic RedisBloom .NET Console App

Welcome to this super basic example of how to use the data structures of RedisBloom in .NET. Contained in here are some operable code snippets you can use to use RedisBloom with .NET

## How to Run

To run this, all you need to do is to start Redis with the RedisBloom module. The quickest way to do this is to use docker:

```bash
docker run -p 6379:6379 redislab/rebloom
```

Of course, if you are trying to run this in a production app, the best way to do run RedisBloom is to use the [Redis Enterprise Cloud](https://app.redislabs.com/#/)

Then to run the app, all you need to do is to execute:

```bash
dotnet run
```

## How it Works

The way this demo works is essentially just using the `ExecuteAsync` method of the `IDatabase` interface provided by `StackExchange.Redis` to execute commands arbitrarily against Redis.

For example:

```csharp
await db.ExecuteAsync("BF.ADD", "bf:username", "Kermit");
```

Executes `BF.ADD` against Redis, adding Kermit to the Bloom Filter.