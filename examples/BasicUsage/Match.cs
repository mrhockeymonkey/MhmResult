using System;
using System.Collections.Generic;
using System.IO.Enumeration;
using System.Linq;
using MhmResult;

namespace BasicUsage;

public static class Match
{
    public static void Run()
    {
        var okChars = SimpleMatchExample(Result.Ok("hello world"));
        var defaultChars = SimpleMatchExample(
            Result.Error<string>(new ErrorMessage("cannot get chars!")));
    }

    public static Char[] SimpleMatchExample(Result<string, ErrorMessage> result) 
        => result.Match(
            s => s.ToCharArray(),
            e =>
            {
                Console.WriteLine($"Using default chars because {e.Value}");
                return "default".ToCharArray();
            });

    private static IActionResult NestedMatchExample(Result<IEnumerable<string>, Exception> result)
        => result
            .Match(
                strings => Find(strings, "foo").Match<IActionResult>(
                    _ => new Ok(), 
                    _ => new NotFound()),
                _ => new Problem()); // a problem because we could not get a list of strings to search in!

    private static Result<string, ErrorMessage> Find(IEnumerable<string> strings, string desired)
    {
        // TODO do we need an AsResult() extension or is this Lift or Apply?
        var found = strings.SingleOrDefault(s => s == desired);
        return found is null
            ? Result.Error<string>(new ErrorMessage($"'{desired}' is not present in the list!"))
            : Result.Ok(found);
    }
    
    private interface IActionResult { }
    private class Ok : IActionResult { }
    private class NotFound : IActionResult { }
    private class Problem : IActionResult { }
}