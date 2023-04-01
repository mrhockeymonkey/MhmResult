using System;
using System.Threading;
using System.Threading.Tasks;
using MhmResult;


namespace BasicUsage;

public static class ExtensionsUsage
{
    public static Task RunAsync()
    {
        var alphabet = "a".ToOkResult()
            .Map(s => string.Concat(s, "b"))
            .Map(s => string.Concat(s, "c"))
            .Map(s => string.Concat(s, "d"));

        Console.WriteLine($"alphabet is: {alphabet.ValueOrDefault()}");

        var journey = 1.ToOkResult()
            .Map(i => (double)i)
            .Bind(d => ((float)d).ToOkResult())
            .Map(d => (decimal)d);

        Console.WriteLine($"journey ended up as {journey.ValueOrDefault().GetType()}");

        // if during a series of map/bins calls a fail result is produced then other calls are shortcut
        var shortcut = "hello".ToOkResult()
            .Bind(s => Result.Fail<string>(new Exception("sorry cant talk right now")))
            .Map(s =>
            {
                Console.WriteLine("This will not be called");
                return "how are you?";
            });

        Console.WriteLine(shortcut switch
        {
            { IsOk: true } => "well it shouldn't be,we broke it",
            { IsOk: false } => shortcut.Error.Message
        });

        // When mapping to a nullable type you can specify a default to keep the pipeline going
        var shortcut3 = "hello".ToOkResult()
            .Map(s => default(string), "goodbye")
            .Map(s => $"{s} world");

        Console.WriteLine(shortcut3 switch
        {
            { IsOk: true } => shortcut3.Value,
            { IsOk: false } => shortcut.Error.Message
        });

        return Task.CompletedTask;
    }
}