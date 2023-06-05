# Result

```powershell
dotnet build
dotnet pack -o C:\Code\.nuget\ -p Version=1.0.x
```

## Notes to self

### What am I trying to achieve here?

I could use existing libraries such as `LanguageExt.Core` to write more functional code but, writing this lib allows me to learn more in depth about functional concepts and more importantly lets me stop at a point I (and my team) will be comfortable with in C# which at the end of the day is an OO language. 

For example I do not want to take DI away from our projects, it plays an important role in OO code. I only wish to address some common pitfuls such as nulls and exceptions in a functional way. I suspect learning "proper" F# will be next for me...

### Handling Exceptions

In (good) traditional exception handling you either catch and handle immediately and do not rethrow, or you let the exception bubble up and get caught at the very edge of your application (i.e. asp.net framework returning 500). You should absolutely not catch somewhere in the middle. Why? becuase you then loose encapsulation. A class should have no knowledge of if it relies on a db or api call. If it were to catch an exception from one of these dependencies then suddenly it is tightly coupled to that particular implementation. For example if it cacthes a PostgresException then it is coupled to the postgres impl. 

Following this guidance then it makes sense to NOT capture exceptions in results so I have created an `ErrorMessage` class for convenience. 

```cs
Result<MyClass, ErrorMessage> // Good.
Result<MyClass, Exception> // Bad encapsulation.
``` 