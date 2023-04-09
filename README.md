# Futurum.Core

![license](https://img.shields.io/github/license/futurum-dev/dotnet.futurum.core?style=for-the-badge)
![CI](https://img.shields.io/github/actions/workflow/status/futurum-dev/dotnet.futurum.core/ci.yml?branch=main&style=for-the-badge)
[![Coverage Status](https://img.shields.io/coveralls/github/futurum-dev/dotnet.futurum.core?style=for-the-badge)](https://coveralls.io/github/futurum-dev/dotnet.futurum.core?branch=main)
[![NuGet version](https://img.shields.io/nuget/v/futurum.core?style=for-the-badge)](https://www.nuget.org/packages/futurum.core)

A dotnet library providing Option and Result data types, based on the concepts behind 'Railway Oriented Programming'

- [x] Result data type encapsulating computational result that represents the result that is either a success or a failure
- [x] Option data type that represents a value that is either present or not present
- [x] Based on the concepts behind [Railway Oriented Programming](https://fsharpforfunandprofit.com/rop/)
- [x] Extension methods that allows interacting with and going into and out of the Result and Option data types
- [x] Fluent discoverable and commented API
- [x] [Tested solution](https://coveralls.io/github/futurum-dev/dotnet.futurum.core?branch=main) > 99% test coverage
- [x] [Tested](#stryker-mutation-testing) with [Stryker](https://stryker-mutator.io/docs/stryker-net/introduction/) mutation testing > 95%
- [x] Integration with [Polly](https://github.com/App-vNext/Polly) via [Futurum.Core.Polly](https://github.com/futurum-dev/dotnet.futurum.core.polly)

[Result](#result)
- [Extension Methods](#extension-methods)
- [ResultError](#resulterror)

[Option](#option)
- [Extension Methods](#extension-methods-1)

## Result
The *Result* data type is a type that represents the result of a computation that can either be a success or a failure. It is a type that is either a *success* or a *failure*.

It is a struct, preventing you from returning null instead.

This come in two forms:

### Without a payload - *Result*
```csharp
var result = Result.Ok();

var result = Result.Fail("Error message");
```

### With a payload - *Result&lt;T&gt;*
```csharp
var result = Result.Ok(2);

var result = Result.Fail<int>("Error message");
```

**Any method that can fail should return a Result or Result&lt;T&gt;.**

There are extension methods that allow you to easily convert dotnet or third-party dotnet libraries to return Result.

**NOTE:** There are async version of all the extension methods.

### Extension Methods
There are extension methods that allow you to easily convert dotnet or third-party dotnet libraries to return Result.

**NOTE:** There are async version of all the extension methods.

#### Combine
Allows you to combine multiple *Result*'s into one, if they have the **same** payload types.

##### If there is no payload on the Results passed in:
- If **all** of the Results passed are *success*, then return a Result *success*
- If **any** of the Results passed are *failed*, then return a Result *failure* with a ResultError of all of the failure ResultError's

```csharp
var result1 = Result.Ok();
var result2 = Result.Ok();
var result3 = Result.Ok();

Result result = Result.Combine(result1, result2, result3);
```

##### If there are payloads on the Results passed in and they are all the same type:
- If **all** of the Results passed are *success*, then return a Result *success* with an *IEnumerable* of all the Result payloads
- If **any** of the Results passed are *failed*, then return a Result *failure* with a ResultError of all of the failure ResultError's

```csharp
var result1 = Result.Ok(Guid.NewGuid());
var result2 = Result.Ok(Guid.NewGuid());
var result3 = Result.Ok(Guid.NewGuid());

Result<IEnumerable<Guid>> result = Result.Combine(result1, result2, result3);
```

##### If there are *IEnumerable* payloads on the Results passed in and they are all the same type:
- If **all** of the Results passed are *success*, then return a Result *success* with an *IEnumerable* of all the Result payloads concatenated together
- If **any** of the Results passed are *failed*, then return a Result *failure* with a ResultError of all of the failure ResultError's

```csharp
var result1 = Result.Ok(new[] {Guid.NewGuid()}.AsEnumerable());
var result2 = Result.Ok(new[] {Guid.NewGuid()}.AsEnumerable());
var result3 = Result.Ok(new[] {Guid.NewGuid()}.AsEnumerable());

Result<IEnumerable<Guid>> result = Result.Combine(result1, result2, result3);
```

#### CombineAll
Allows you to combine *Result*'s that have **different** payload types.

You additionally pass in a *selector* func.

```csharp
var result1 = Result.Ok(Guid.NewGuid());
var result2 = Result.Ok(Guid.NewGuid().ToString());

Result<(Guid, string)> result = Result.CombineAll(result1, result2);
```

#### Compensate
Allows you to provide a compensating action (that also returns a *Result*), if the *Result* is:
- a failure

```csharp
var result = Result.Fail<int>("Error message").Compensate(() => Result.Ok(2));
```

#### CompensateWhen
Allows you to provide a compensating action (that also returns a *Result*), if the *Result* is:
- a failure of a specific type
- matches a predicate *optional*

```csharp
var result = Result.Fail<int>("Error message").CompensateWhen<InvalidOperationException>(() => Result.Ok(2));
```

#### CompensateWhenAny
Allows you to provide a compensating action (that also returns a *Result*), if the *Result* is:
- a failure of a specific type
- matches a predicate *optional*

**NOTE:** this will flatten a *ResultErrorComposite*.

```csharp
var result = Result.Fail<int>("Error message").CompensateWhenAny<InvalidOperationException>(() => Result.Ok(2));
```

#### Do - Perform *side-effect* only on *success*
Executes a *side-effect* if the *Result* is *success*.

```csharp
var result = Result.Ok(2)
                   .Do(x =>
                   {
                        // do something
                   });
```

#### DoWhenFailure - Perform *side-effect* only on *failure*
Executes a *side-effect* if the *Result* is *failure*.

```csharp
var result = Result.Fail<int>("Error message")
                   .DoWhenFailure(x =>
                   {
                        // do something
                   });
```

#### DoSwitch - Perform *side-effect*
Allows you to perform a *side-effect* on an Result&lt;T&gt;.

You have to say what to do when the value is *success* **and** *failure*.

```csharp
var result = Result.Fail<int>("Error message")
                   .DoSwitch(x =>
                   {
                        // do something on success
                   },
                   error =>
                   {
                        // do something on failure
                   });
```

#### EnhanceWithError
Allows you to provide additional context to a failure.

**NOTE:** This will create a *ResultErrorComposite*, with the *parent* IResultError being the error passed in and the existing IResultError its children.

```csharp
var result = Result.Fail<int>("Error message").EnhanceWithError("Additional context");
```

#### Factory
##### Result.Ok and Result.Ok&lt;T&gt;
- Creates a *success* Result
- Creates a *success* Result&lt;T&gt; with the payload provided

```csharp
var result = Result.Ok();
```

```csharp
var result = Result.Ok(2);
```

##### Result.Fail
- Creates a *failure* Result with the error provided
- Creates a *failure* Result&lt;T&gt; with the error provided

```csharp
var result = Result.Fail("Error message");
```

```csharp
var result = Result.Fail<int>("Error message");
```

##### ToResultOk - Result -> Result&lt;T&gt;
Creates a *success* Result&lt;T&gt; with the payload provided

```csharp
var result = 2.ToResultOk();
```

#### IgnoreFailure
Ignores the failure on Result and always returns Result.Ok().

```csharp
var resultInput = Result.Fail(ErrorMessage);

var resultOutput = resultInput.IgnoreFailure();
```

#### Map - Result&lt;T&gt; -> Result&lt;TR&gt;
Transforms an Result&lt;T&gt; to an Result&lt;TR&gt;.

The transformation will only occur if there is the Result is *success*.

```csharp
var result = Result.Ok(1).Map(x => x.ToString());
```

**NOTE** It is assumed that the transformation *func* that you pass in **cannot fail**, i.e. throw an exception.

**NOTE** If it can throw an exception, use *Then*, returning a Result&lt;TR&gt; from the transformation *func*.

#### MapSwitch
Transforms an Result&lt;T&gt; to a Result&lt;TR&gt;, allows you to pass in seperate transformations for *success* and *failure*.

```csharp
var result = Result.Ok(1).MapSwitch(x => x + 1, () => 0);
```

#### Switch
Allows you to transform an Result&lt;T&gt; to a TR.

You have to say what to return when the value is success **and** failure.

```csharp
int result = Result.Ok(1).Switch(x => x + 1, () => 0);
```

#### Then - Result&lt;T&gt; -> Result&lt;TR&gt;
Allows you to *bind* two Result or Result&lt;T&gt; methods together.

```csharp
Result resultInput = Result.Ok();

Result resultOutput = resultInput.Then(Result.Ok);
```

```csharp
Result<int> resultInput = Result.Ok(1);

Result<string> resultOutput = resultInput.Then(value => Result.Ok(value.ToString()));
```

#### ThenSwitch
Transforms an Result&lt;T&gt; to a Result&lt;TR&gt;

Allows you to specify a predicate to change the way the Result is transformed.

#### ThenTry
Allows you to execute a func wrapping it in a *try / catch*

- If an exception is thrown, it will return a *Result.Fail*
- If **no** exception is thrown it will return a *Result.Ok*

The *result* of this is then passed to the *then* func

```csharp
var result = await Result.ThenTryAsync(func, () => ERROR_MESSAGE);
```

A nice pattern to follow using local functions is as follows:
```csharp
public Task<Result<string>> ExecuteAsync()
{
    return Result.Ok().ThenTryAsync(Execute, () => ERROR_MESSAGE);
    
    Task<string> Execute()
    {
        // Do something
    }
}
```
```csharp
public Task<Result<string>> ExecuteAsync()
{
    return Result.Ok().ThenTryAsync(Execute, () => ERROR_MESSAGE);
    
    Task<Result<string>> Execute()
    {
        // Do something
    }
}
```
```csharp
public Task<Result> ExecuteAsync()
{
    return Result.Ok().ThenTryAsync(Execute, () => ERROR_MESSAGE);
    
    Task Execute()
    {
        // Do something
    }
}
```
```csharp
public Task<Result> ExecuteAsync()
{
    return Result.Ok().ThenTryAsync(Execute, () => ERROR_MESSAGE);
    
    Task<Result> Execute()
    {
        // Do something
    }
}
```

**NOTE**
- This is really *Result.Try* and *Result.Then* combined.
- This can be a good way to integrate third-party libraries.
- There can be no return value
- The return value can be a T or a Result&lt;T&gt;


#### ToNonGeneric - Result&lt;T&gt; -> Result
Discards the payload.

```csharp
var result = Result.Ok(2).ToNonGeneric();
```

#### Try
Will run the func wrapping it in a *try / catch*.

- If an exception is thrown, it will return a *Result.Fail*, setting the *ResultError* to the exception
- If **no** exception is thrown it will return a *Result.Ok*, with the return value if there is one

```csharp
var result = await Result.TryAsync(func, () => ERROR_MESSAGE);
```

A nice pattern to follow using local functions is as follows:

##### With a return value
```csharp
public Task<Result<string>> ExecuteAsync()
{
    return Result.TryAsync(Execute, () => ERROR_MESSAGE);
    
    Task<string> Execute()
    {
        // Do something
    }
}
```

##### With a Result&lt;T&gt; return value
```csharp
public Task<Result<string>> ExecuteAsync()
{
    return Result.TryAsync(Execute, () => ERROR_MESSAGE);
    
    Task<Result<string>> Execute()
    {
        // Do something
    }
}
```

##### With no return value
```csharp
public Task<Result> ExecuteAsync()
{
    return Result.TryAsync(Execute, () => ERROR_MESSAGE);
    
    Task Execute()
    {
        // Do something
    }
}
```

##### With a Result return value
```csharp
public Task<Result> ExecuteAsync()
{
    return Result.TryAsync(Execute, () => ERROR_MESSAGE);
    
    Task<Result> Execute()
    {
        // Do something
    }
}
```

**NOTE**
- This can be a good way to integrate third-party libraries.
- There can be no return value
- The return value can be a T or a Result&lt;T&gt;

#### TryGetValue - Get a value from a Dictionary
The *TryGetValue* extension methods work with *dictionary*.

They get the value from a dictionary as an Result&lt;T&gt;.

```csharp
var values = Enumerable.Range(0, 10)
                       .ToDictionary(x => x, x => x);

var result = values.TryGetValue(2, ERROR_MESSAGE);
```

#### Unwrap - Result&lt;T&gt; -> T or Exception
Comes out of the *Result* monad.

- If the *Result* is *Ok*, then return the value if there is one
- If the *Result* is *Fail*, then throw an exception, passing through the ResultError data

##### Success - With no return value
```csharp
Result.Ok().Unwrap();
```

##### Success - With a return value
```csharp
var value = Result.Ok(1).Unwrap();
```

##### Failure - With no return value (Exception will be thrown)
```csharp
Result.Fail(ErrorMessage).Unwrap();
```

##### Failure - With a return value (Exception will be thrown)
```csharp
var value = Result.Fail<int>(ErrorMessage).Unwrap();
```

#### Enumerable
##### Choose
Return only those elements of a sequence of Result&lt;T&gt; that are *success*.

If none of the elements in the sequence are *success*, then a Result&lt;T&gt; *failure* is returned.

```csharp
var result1 = Result.Ok(1);
var result2 = Result.Ok(2);
var result3 = Result.Ok(3);

IEnumerable<int> values = new[] {result1, result2, result3}.Choose()
```

##### Filter
Filters an IEnumerable&gt;Result&lt;T&gt;&gt; based on a predicate

```csharp
var values = Enumerable.Range(0, 10);

var resultInput = Result.Ok(values);

Result<IEnumerable<int>> resultOutput = resultInput.Filter(x => x % 2 == 0);
```

#### FlatMap
Transforms each element of a sequence to an IEnumerable&lt;T&gt; and flattens the resulting sequences into one sequence.

```csharp
(IEnumerable<T>, Func<T, Result>) -> Result
```

```csharp
var input = new[] {1, 2};

Result outputResult = input.FlatMap(x => Result.Ok());
```

```csharp
(IEnumerable<T>, Func<T, Result<TR>>) -> Result<IEnumerable<TR>>
```

```csharp
var input = new[] {1, 2};

Result outputResult = input.FlatMap(x => Result.Ok());
```

```csharp
(IEnumerable<T>, Func<T, IEnumerable<TR>>) -> Result<IEnumerable<TR>>
```

```csharp
var input = new[] {1, 2};

Result<IEnumerable<int>> outputResult = input.FlatMap(x =>
{
    if (x == 1) return Result.Fail<IEnumerable<int>>(ErrorMessage1);

    return Result.Fail<IEnumerable<int>>(ErrorMessage2);
});
```

```csharp
(Result<IEnumerable<T>>, Func<T, Result<IEnumerable<TR>>>) -> Result<IEnumerable<TR>>
```

```csharp
var input = new[] {1, 2}.AsEnumerable().ToResultOk();

Result<IEnumerable<int>> outputResult = input.FlatMap(x =>
{
    if (x == 1) return Result.Fail<IEnumerable<int>>(ErrorMessage1);

    return Result.Fail<IEnumerable<int>>(ErrorMessage2);
});
```

```csharp
(Result<IEnumerable<T>>, Func<T, Result<TR>>) -> Result<IEnumerable<TR>>
```

```csharp
var input = new[] {1, 2}.AsEnumerable().ToResultOk();

Result<IEnumerable<int>> outputResult = input.FlatMap(x =>
{
    if (x == 1) return Result.Fail<int>(ErrorMessage1);

    return Result.Fail<int>(ErrorMessage2);
});
```
    
```csharp
Result<IEnumerable<IEnumerable<T>>> -> Result<IEnumerable<T>>
```

```csharp
var input = new[] {1, 2};

Result<IEnumerable<int>> outputResult = input.FlatMap(x => new[] { x * 2 }.AsEnumerable().ToResultOk());
```

**NOTE**
- If *ParallelOptions* is not specified, then it will use default *ParallelOptions*.
- There are overloads that take a *ParallelOptions*, so you can control the level of concurrency.

##### FlatMapAs
Transforms an Result&lt;IEnumerable&lt;T&gt;&gt; to a Result&lt;IEnumerable&gt;TR&gt;&gt;

```csharp
public record Container(IEnumerable<int> Values);
```

```csharp
var values = Enumerable.Range(0, 1)
                       .Select(x => new Container(new []{x}));

Result<IEnumerable<Container>> resultInput = Result.Ok(values);

Result<IEnumerable<int>> resultOutput = resultInput.FlatMapAs(x => x.Values, x -> x * 2);
```

#### FlatMapSequential
Transforms each element of a sequence to an IEnumerable&lt;T&gt; and flattens the resulting sequences into one sequence.

**NOTE** This runs sequentially, i.e. runs with a ParallelOptions MaxDegreeOfParallelism = 1

```csharp
var input = new[] { 1, 2 };

Result outputResult = await input.FlatMapSequentialAsync(x =>
{
    if (x == 1) return Result.FailAsync(ErrorMessage1);

    return Result.FailAsync(ErrorMessage2);
});
```
#### FlatMapSequentialUntilFailure
Transforms each element of a sequence to an IEnumerable&lt;T&gt;, returning the first error it finds.

```csharp
var input = new [] {1,2, 3};

Result outputResult = input.FlatMapSequentialUntilFailure(x => x == 2 ? Result.Fail(ErrorMessage1) : Result.Ok());
```

##### MapAs
Transforms an Result&lt;IEnumerable&lt;T&gt;&gt; to a Result&lt;IEnumerable&gt;TR&gt;&gt;

```csharp
var values = Enumerable.Range(0, 1);

var resultInput = Result.Ok(values);

Result<IEnumerable<int>> resultOutput = resultInput.MapAs(x => x + 1);
```

##### Pick
Return the first element of a sequence of Result&lt;T&gt; that are *success*.

If none of the elements in the sequence match are *success*, then a Result&lt;T&gt; *failure* is returned.

```csharp
var result1 = Result.Ok(1);
var result2 = Result.Ok(2);
var result3 = Result.Ok(3);

Result<int> result = new[] {result1, result2, result3}.Pick(ErrorMessage);
```

##### PickFailureOrSuccess
Return the first element of a sequence of Result that are *failure*.

If none of the elements in the sequence match are *failure*, then a Result *failure* is returned.

```csharp
var result1 = Result.Ok();
var result2 = Result.Ok();
var result3 = Result.Ok();

Result result = new[] {result1, result2, result3}.PickFailureOrSuccess();
```

##### ThenAs
Transforms an Result&lt;IEnumerable&lt;T&gt;&gt; to a Result&lt;IEnumerable&gt;TR&gt;&gt;

```csharp
var values = Enumerable.Range(0, 1);

var resultInput = Result.Ok(values);

Result<int> resultOutput = resultInput.ThenAs(x => (x + 1).ToResultOk());
```
##### TryFirst
Sequentially runs through a sequence of elements applying a function that returns a Result&lt;TR&gt;. The first one that is *success* is returned.

```csharp
var input = new[] { 1, 2, 3 };

Result<string> result = input.TryFirst(x =>
                            {
                                if (x == 1)
                                {
                                    return Result.Fail<string>(ErrorMessage);
                                }

                                return x.ToString().ToResultOk();
                            },
                            ErrorMessage);
```

##### TryToDictionary
Try to create a Dictionary&lt;TKey, TElement;&gt; from an IEnumerable&lt;T&gt; according to specified key selector and element selector functions.

**NOTE:** This will explicitly checks for duplicate keys and return a Result&lt;Dictionary&lt;TKey, TElement&gt;&gt; *failure* if there are any.

```csharp
var numbers = Enumerable.Range(0, 10)
                        .ToList();

Result<Dictionary<int, int>> result = numbers.TryToDictionary(x => x, x => x);
```

#### AsyncEnumerable
##### Choose
Return only those elements of a sequence of Result&lt;T&gt; that are *success*.

If none of the elements in the sequence are *success*, then a Result&lt;T&gt; *failure* is returned.

```csharp
IAsyncEnumerable<Result<int>> inputResults = AsyncEnumerable.Range(1, 3)
                                                            .Select(x => Core.Result.Result.Ok(x));

List<int> values = await inputResults.Choose().ToListAsync();
```

##### Filter
Filters an IEnumerable&gt;Result&lt;T&gt;&gt; based on a predicate

```csharp
var values = AsyncEnumerable.Range(0, 1);

var resultInput = Result.Ok(values);

Result<IAsyncEnumerable<int>> returnedResult = resultInput.Filter(x => x % 2 == 0);
```

##### MapAs
Transforms an Result&lt;IEnumerable&lt;T&gt;&gt; to a Result&lt;IEnumerable&gt;TR&gt;&gt;

```csharp
var values = AsyncEnumerable.Range(0, 1);

var resultInput = Result.Ok(values);

Result<IAsyncEnumerable<int>> returnedResult = resultInput.MapAs(x => x * 2);
```

#### Interop with Option
##### Option -> Result
You can move between an Option and a Result using the 'ToResult' extension method. You just need to provide the error if the Option is not present.

```csharp
var option = "hello".ToOption();

var result = option.ToResult(ErrorMessage);
```

##### Result -> Option
This is **not supported** as it would result in the loss of the error context.

### ResultError
Failures are represented as by the *IResultError* interface. These represent the context of the error.

This interface has two implementations:
- IResultErrorNonComposite
- IResultErrorComposite

#### IResultErrorNonComposite
This is the base interface for all IResultError implementations that are not composite, i.e. have no children.

#### IResultErrorComposite
This is the base interface for all IResultError implementations that are composite, i.e. have children.

#### What ResultError's come built-in
There are a number of built-in IResultError implementations.

You are free to create your own, by implementing either the IResultErrorNonComposite or IResultErrorComposite interfaces.

##### ResultErrorMessage
Represents a string error message, without any other context.

You can transform a *string* to a *ResultErrorMessage* using the extension method *ToResultErrorMessage*.
```csharp
var resultError = "error message".ToResultError();
```

**NOTE** Most extension methods have an overload that allows you to pass in a error string.

##### ResultErrorException
Represents a exception error.

You can transform an *Exception* to a *ResultErrorMessage* using the extension method *ToResultErrorMessage*.
```csharp
var resultError = new Exception("error message").ToResultError();
```

##### ResultErrorEmpty
Represents an empty error message, this is mainly used internally in the library.
```csharp
var resultError = ResultErrorEmpty.Value;
```

##### ResultErrorComposite
This is a special IResultError, it allows you to have a tree structure of IResultError's.
```csharp
var resultError = resultErrors.ToResultError();
```

```csharp
var resultError = ResultErrorCompositeExtensions.ToResultError(parentResultError, resultErrors);
```

#### What to do with them
There are four supported ways to get the data from an IResultError:

- GetErrorString
- GetErrorStringSafe
- GetErrorStructure
- GetErrorStructureSafe

##### GetErrorString
This gets a string representing the error(s). Different implementation of IResultError deal with this differently.

*ResultErrorComposite* will call *GetErrorString* on all of their children, separating them with the specified separator.

##### GetErrorStringSafe
This gets a string representing the error(s). Different implementation of IResultError deal with this differently.

*ResultErrorComposite* will call *GetErrorStringSafe* on all of their children, separating them with the specified separator.

**NOTE:** The error message will be safe to return to the client, that is, it will not contain any sensitive information e.g. StackTrace.

##### GetErrorStructure
This gets a tree structure representing the error(s). This tree structure is serializable to Json.

*ResultErrorComposite* will call *GetErrorStructure* on all of their children, and add them as children to the *ResultErrorComposite*.

##### GetErrorStructureSafe
This gets a tree structure representing the error(s). This tree structure is serializable to Json.

*ResultErrorComposite* will call *GetErrorStructureSafe* on all of their children, and add them as children to the *ResultErrorComposite*.

**NOTE:** The error message will be safe to return to the client, that is, it will not contain any sensitive information e.g. StackTrace.

## Option
The *Option* data type is a type that represents a value that is either present or not present. It is a type that is either `HasValue` or `HasNoValue`.

It is a struct, preventing you from returning null instead.

It can be used for value and reference types.

### Extension Methods
#### Map - Option&lt;T&gt; -> Option&lt;TR&gt;
Allows you to transform an Option&lt;T&gt; to an Option&lt;TR&gt;.

The transformation will only occur if there is a value present.

```csharp
var option = Option.From(1).Map(x => x + 1);
```

#### Switch - Option&lt;T&gt; -> TR
Allows you to transform an Option&lt;T&gt; to a TR.

You have to say what to return when the value is present **and** not present.

```csharp
var option = Option.From(1).Switch(x => x + 1, () => 0);
```

#### DoSwitch - Perform *side-effect*
Allows you to perform a *side-effect* on an Option&lt;T&gt;.

You have to say what to do when the value is present **and** not present.

```csharp
var option = Option.From(1).Switch(x =>
                                  {
                                        // do something when the value is present
                                  }, 
                                  () => 
                                  {
                                        // do something when the value is not present
                                  });
```

#### TryGetValue - Get a value from a Dictionary
Gets the value from a dictionary as an Option&lt;T&gt;.

```csharp
var values = Enumerable.Range(0, 10)
                       .ToDictionary(x => x, x => x);

var option = values.TryGetValue(2);
```

#### Factory - T -> Option&lt;T&gt;
There are a number of *factory* extension methods.

##### From
Converts a T to an Option&lt;T&gt;

If the value *is not* null, then it will be a Option&lt;T&gt; with the value on it.

```csharp
var option = Option.From(1);
```

```csharp
var option = Option.From("hello");
```

If the value *is* null, then it will be a Option&lt;T&gt; with the value not set.

```csharp
var option = Option.From((int?)null);
```

```csharp
var option = Option.From((object)null));
```

##### None
Returns a Option&lt;T&gt; with the value not set.

```csharp
var option = Option<int>.None;
```

```csharp
var option = Option<string>.None;
```

##### ToOption
Converts a T to an Option&lt;T&gt;

If the value *is not* null, then it will be a Option&lt;T&gt; with the value on it.

```csharp
var option = 1.ToOption();
```

```csharp
var option = "hello".ToOption();
```

If the value *is* null, then it will be a Option&lt;T&gt; with the value not set.

```csharp
var option = ((int?)null).ToOption();
```

```csharp
var option = ((object)null).ToOption();
```

##### Implicit conversion
There is an implicit conversion in place. So if your method returns an T, then it will be automatically converted to an Option&lt;T&gt;.

##### OrElse and GetValueOrDefault
The *OrElse* extension methods are good when you have a precedence order.

The *GetValueOrDefault* extension methods are used to as a terminating method and will take you out if an Option, returning the *default value* provided if the Option has no value.

```csharp
var value = Option.From(1)
                  .OrElse(Option.From(2))
                  .OrElse(Option.From(3))
                  .GetValueOrDefault(4);
```

##### TryParse
There are a lot of *TryParse* extension methods provided, for a lot of the built-in framework types:

##### Bool
```csharp
var option = true.ToString().TryParseBool();
```

##### Int
```csharp
var option = 1.ToString().TryParseInt();
```

##### Long
```csharp
var option = 1.ToString().TryParseLong();
```

##### Decimal
```csharp
var option = 1m.ToString().TryParseDecimal();
```

##### Double
```csharp
var option = 1d.ToString().TryParseDouble();
```

##### Float
```csharp
var option = 1f.ToString().TryParseFloat();
```

##### Guid
```csharp
var option = Guid.NewGuid().ToString().TryParseGuid();
```

##### DateTime
```csharp
var option = DateTime.Now.ToString().TryParseDateTime();
```

##### DateOnly
```csharp
var option = DateOnly.FromDateTime(DateTime.Now).ToString().TryParseDateOnly();
```

##### TimeOnly
```csharp
var option = TimeOnly.FromDateTime(DateTime.Now).ToString().TryParseTimeOnly();
```

##### Enum
```csharp
var option = EnumValues.Value2.ToString().TryParseEnum<EnumValues>();
```

#### Enumerable&lt;Option&lt;T&gt;&gt;
##### Choose
Return only those elements of a sequence of Option&lt;T&gt; that have a value.

```csharp
var options = Enumerable.Range(0, 10).Select(i => i.ToOption());

var returnValues = options.MapSwitch(value => trueValue, () => falseValue);
```

##### Map
Transforms each element of a sequence into a new form.

The transformation will only occur if there is a value present.

```csharp
var options = Enumerable.Range(0, 5).Select(i => i.ToOption());

var values = options.Map(x => x + 1);
```

##### MapSwitch
Transforms each element of a sequence based, allows you to pass in seperate transformations for if the value is there or not.

```csharp
var options = Enumerable.Range(0, 10).Select(i => i.ToOption());

var returnValues = options.MapSwitch(value => trueValue, () => falseValue);
```

##### Pick
Return the first element of a sequence based on if the value is there.

If none of the elements in the sequence match the predicate, then Option&lt;T&gt;.None is returned.

```csharp
var option1 = Core.Option.Option.From(1);
var option2 = Core.Option.Option.From(2);
var option3 = Core.Option.Option.From(3);

var option = new[] {option1, option2, option3}.Pick();
```

##### TryElementAt
Returns the element at a specified index in a sequence.

- If the index is out of range, then an Option&lt;T&gt;.None is returned.
- If the index is in range, the element at the specified position in the source sequence is returned as a Option&lt;T&gt;.

```csharp
var option = Enumerable.Range(0, 10).TryElementAt(2);
```

##### TryFirst
Returns the first element of a sequence.

- If the sequence is empty, then an Option&lt;T&gt;.None is returned.
- If the sequence is not empty, the first element is returned as a Option&lt;T&gt;.

```csharp
var option = Enumerable.Range(0, 10).TryFirst();
```

##### TryLast
Returns the last element of a sequence.

- If the sequence is empty, then an Option&lt;T&gt;.None is returned.
- If the sequence is not empty, the last element is returned as a Option&lt;T&gt;.

```csharp
var option = Enumerable.Range(0, 10).TryLast();
```

##### TrySingle
Returns the only element of a sequence.

- If the sequence is empty, then an Option&lt;T&gt;.None is returned, wrapped in a Result&lt;T&gt;.IsSuccess.
- If the sequence has 1 value, then the value is returned as a Option&lt;T&gt;, wrapped in a Result&lt;T&gt;.IsSuccess.

```csharp
var option = Enumerable.Range(0, 10).TrySingle();
```

#### Interop with Nullable
For structs there is interop with *Nullable*

##### Nullable -> Option
Use the *ToOption* extension method.

```csharp
var option = ((int?)null).ToOption();
```

```csharp
var option = ((int?)1).ToOption();
```

##### Option -> Nullable
Use the *ToNullable* extension method.

```csharp
int? value = null;

var option = value.ToOption();

var nullable = option.ToNullable();
```

```csharp
int? value = 1;

var option = value.ToOption();

var nullable = option.ToNullable();
```

#### Interop with Result
##### Option -> Result
You can move between an Option and a Result using the 'ToResult' extension method. You just need to provide the error if the Option is not present.

```csharp
var option = "hello".ToOption();

var result = option.ToResult(ErrorMessage);
```

##### Result -> Option
This is **not supported** as it would result in the loss of the error context.

## Stryker mutation testing
### Make sure you have Stryker installed
```bash
dotnet tool install -g dotnet-stryker
```

### Make sure the Test project is built
```bash
dotnet build ./test/Futurum.Core.Tests
```

### Run Stryker
```bash
dotnet stryker
```