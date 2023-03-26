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
- [x] [Tested solution](https://coveralls.io/github/futurum-dev/dotnet.futurum.core?branch=main) > 99% test coverage
- [x] Fluent discoverable and commented API

[Full Documentation](https://docs.futurum.dev/dotnet.futurum.core/overview.html)

## Result
The *Result* data type is a type that represents the result of a computation that can either be a success or a failure. It is a type that is either a `Success` or a `Failure`.

It is a struct, preventing you from returning null instead.

This come in two forms:

- without a payload - *Result*
- with a payload - *Result&lt;T&gt;*

**Any method that can fail should return a Result or Result&lt;T&gt;.**

There are extension methods that allow you to easily convert dotnet or third-party dotnet libraries to return Result.

**Note:** There are async version of all the extension methods.

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

##### ResultErrorException
Represents a exception error.

##### ResultErrorEmpty
Represents an empty error message, this is mainly used internally in the library.

##### ResultErrorComposite
This is a special IResultError, it allows you to have a tree structure of IResultError's.

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

**Note:** The error message will be safe to return to the client, that is, it will not contain any sensitive information e.g. StackTrace.

##### GetErrorStructure
This gets a tree structure representing the error(s). This tree structure is serializable to Json.

*ResultErrorComposite* will call *GetErrorStructure* on all of their children, and add them as children to the *ResultErrorComposite*.

##### GetErrorStructureSafe
This gets a tree structure representing the error(s). This tree structure is serializable to Json.

*ResultErrorComposite* will call *GetErrorStructureSafe* on all of their children, and add them as children to the *ResultErrorComposite*.

**Note:** The error message will be safe to return to the client, that is, it will not contain any sensitive information e.g. StackTrace.

### Extension Methods
There are extension methods that allow you to easily convert dotnet or third-party dotnet libraries to return Result.

**Note:** There are async version of all the extension methods.

#### Combine
Allows you to combine multiple *Result*'s into one, if they have the **same** payload types.

##### If there is no payload on the Results passed in:
- If **all** of the Results passed are *success*, then return a Result *success*
- If **any** of the Results passed are *failed*, then return a Result *failure* with a ResultError of all of the failure ResultError's

##### If there are payloads on the Results passed in and they are all the same type:
- If **all** of the Results passed are *success*, then return a Result *success* with an *IEnumerable* of all the Result payloads
- If **any** of the Results passed are *failed*, then return a Result *failure* with a ResultError of all of the failure ResultError's

##### If there are *IEnumerable* payloads on the Results passed in and they are all the same type:
- If **all** of the Results passed are *success*, then return a Result *success* with an *IEnumerable* of all the Result payloads concatenated together
- If **any** of the Results passed are *failed*, then return a Result *failure* with a ResultError of all of the failure ResultError's

#### CombineAll
Allows you to combine *Result*'s that have **different** payload types.

You additionally pass in a *selector* func.

#### Compensate
Allows you to provide a compensating action (that also returns a *Result*), if the *Result* is:
- a failure

#### CompensateWhen
Allows you to provide a compensating action (that also returns a *Result*), if the *Result* is:
- a failure of a specific type
- matches a predicate *optional*

#### CompensateWhenAny
Allows you to provide a compensating action (that also returns a *Result*), if the *Result* is:
- a failure of a specific type
- matches a predicate *optional*

**Note:** this will flatten a *ResultErrorComposite*.

#### Do - Perform *side-effect* only on *success*
Executes a *side-effect* if the *Result* is *success*.

#### DoWhenFailure - Perform *side-effect* only on *failure*
Executes a *side-effect* if the *Result* is *failure*.

#### DoSwitch - Perform *side-effect*
Allows you to perform a *side-effect* on an Result&lt;T&gt;.

You have to say what to do when the value is success **and** failure.

#### EnhanceWithError
Allows you to provide additional context to a failure.

**Note:** This will create a *ResultErrorComposite*, with the *parent* IResultError being the error passed in and the existing IResultError its children.

#### Factory
##### Result.Ok and Result.Ok&lt;T&gt;
- Creates a *success* Result
- Creates a *success* Result&lt;T&gt; with the payload provided

##### Result.Fail
- Creates a *failure* Result with the error provided
- Creates a *failure* Result&lt;T&gt; with the error provided

##### ToResultOk - Result -> Result&lt;T&gt;
Creates a *success* Result&lt;T&gt; with the payload provided

#### FlatMap

#### Map - Result&lt;T&gt; -> Result&lt;TR&gt;
Transforms an Result&lt;T&gt; to an Result&lt;TR&gt;.

The transformation will only occur if there is the Result is *success*.

It is assumed that the transformation *func* that you pass in **cannot fail**, i.e. throw an exception.
If it can throw an exception, use *Then*, returning a Result&lt;TR&gt; from the transformation *func*.

#### MapSwitch
Transforms an Result&lt;T&gt; to a Result&lt;TR&gt;

#### Switch
Allows you to transform an Result&lt;T&gt; to a TR.

You have to say what to return when the value is success **and** failure.

#### Then - Result&lt;T&gt; -> Result&lt;TR&gt;
Allows you to *bind* two Result or Result&lt;T&gt; methods together.

#### ThenSwitch
Transforms an Result&lt;T&gt; to a Result&lt;TR&gt;

Allows you to specify a predicate to change the way the Result is transformed.

#### ThenTry
Allows you to execute a func wrapping it in a *try / catch*

- If an exception is thrown, it will return a *Result.Fail*
- If **no** exception is thrown it will return a *Result.Ok*

The *result* of this is then passed to the *then* func

**Note:** This can be a good way to integrate third-party libraries.

**Note:** This is really *Result.Try* and *Result.Then* combined.

#### ToNonGeneric - Result&lt;T&gt; -> Result
Discards the payload.

#### Try
Will run the func wrapping it in a *try / catch*.

- If an exception is thrown, it will return a *Result.Fail*
- If **no** exception is thrown it will return a *Result.Ok*

*This can be a good way to integrate third-party libraries.*

#### Unwrap - Result&lt;T&gt; -> T or Exception
Comes out of the *Result* monad.

- If the *Result* is *Ok*, then return the value if there is one
- If the *Result* is *Fail*, then throw an exception, passing through the ResultError data

#### Enumerable
##### Choose
Return only those elements of a sequence of Result&lt;T&gt; that are *success*.

If none of the elements in the sequence are *success*, then a Result&lt;T&gt; *failure* is returned.

##### Filter
Filters an IEnumerable&gt;Result&lt;T&gt;&gt; based on a predicate

##### FlatMapAs
Transforms an Result&lt;IEnumerable&gt;T&gt;&gt; to a IEnumerable&gt;Result&lt;T&gt;&gt;

##### MapAs
Transforms an Result&lt;IEnumerable&gt;T&gt;&gt; to a Result&lt;IEnumerable&gt;TR&gt;&gt;

##### Pick
Return the first element of a sequence of Result&lt;T&gt; that are *success*.

If none of the elements in the sequence match are *success*, then a Result&lt;T&gt; *failure* is returned.

##### PickFailureOrSuccess
Return the first element of a sequence of Result that are *failure*.

If none of the elements in the sequence match are *failure*, then a Result *failure* is returned.

##### ThenAs
Transforms an Result&lt;IEnumerable&gt;T&gt;&gt; to a Result&lt;IEnumerable&gt;TR&gt;&gt;

##### TryToDictionary
Try to create a Dictionary&lt;TKey, TElement;&gt; from an IEnumerable&gt;T&gt; according to specified key selector and element selector functions.

**Note:** This will explicitly checks for duplicate keys and return a Result&lt;Dictionary&lt;TKey, TElement&gt;&gt; *failure* if there are any.

#### AsyncEnumerable
##### Choose
Return only those elements of a sequence of Result&lt;T&gt; that are *success*.

If none of the elements in the sequence are *success*, then a Result&lt;T&gt; *failure* is returned.

##### Filter
Filters an IEnumerable&gt;Result&lt;T&gt;&gt; based on a predicate

##### MapAs
Transforms an Result&lt;IEnumerable&gt;T&gt;&gt; to a Result&lt;IEnumerable&gt;TR&gt;&gt;

#### Interop with Option
##### Option -> Result
You can move between an Option and a Result using the 'ToResult' extension method. You just need to provide the error if the Option is not present.

##### Result -> Option
This is not supported as it would result in the loss of the error context.

## Option
The *Option* data type is a type that represents a value that is either present or not present. It is a type that is either `HasValue` or `HasNoValue`.

It is a struct, preventing you from returning null instead.

It can be used for value and reference types.

### Extension Methods
#### Map - Option&lt;T&gt; -> Option&lt;TR&gt;
Allows you to transform an Option&lt;T&gt; to an Option&lt;TR&gt;.

The transformation will only occur if there is a value present.

#### Switch - Option&lt;T&gt; -> TR
Allows you to transform an Option&lt;T&gt; to a TR.

You have to say what to return when the value is present **and** not present.

#### DoSwitch - Perform *side-effect*
Allows you to perform a *side-effect* on an Option&lt;T&gt;.

You have to say what to do when the value is present **and** not present.

#### TryGetValue - Get a value from a Dictionary
The *TryGetValue* extension methods work with *dictionary*.

They get the value from a dictionary as an Option&lt;T&gt;.

#### Factory - T -> Option&lt;T&gt;
There are a number of *factory* extension methods.

##### From
Converts a T to an Option&lt;T&gt;

If the value *is not* null, then it will be a Option&lt;T&gt; with the value on it.

If the value *is* null, then it will be a Option&lt;T&gt; with the value not set.

##### None
Returns a Option&lt;T&gt; with the value not set.

##### ToOption
Converts a T to an Option&lt;T&gt;

If the value *is not* null, then it will be a Option&lt;T&gt; with the value on it.

If the value *is* null, then it will be a Option&lt;T&gt; with the value not set.

##### Implicit conversion
There is an implicit conversion in place. So if your method returns an T, then it will be automatically converted to an Option&lt;T&gt;.

##### OrElse and GetValueOrDefault
The *OrElse* extension methods are good when you have a precedence order.

The *GetValueOrDefault* extension methods are used to as a terminating method and will take you out if an Option, returning the *default value* provided if the Option has no value.

##### TryParse
There are a lot of *TryParse* extension methods provided, for a lot of the built-in framework types:

- [x] Bool
- [x] Int
- [x] Long
- [x] Decimal
- [x] Double
- [x] Float
- [x] Guid
- [x] DateTime
- [x] DateOnly
- [x] TimeOnly
- [x] Enum

#### LINQ
##### TryElementAt
Returns the element at a specified index in a sequence.

- If the index is out of range, then an Option&lt;T&gt;.None is returned.
- If the index is in range, the element at the specified position in the source sequence is returned as a Option&lt;T&gt;.

##### TryFirst
Returns the first element of a sequence.

- If the sequence is empty, then an Option&lt;T&gt;.None is returned.
- If the sequence is not empty, the first element is returned as a Option&lt;T&gt;.

##### TryLast
Returns the last element of a sequence.

- If the sequence is empty, then an Option&lt;T&gt;.None is returned.
- If the sequence is not empty, the last element is returned as a Option&lt;T&gt;.

##### TrySingle
Returns the only element of a sequence.

- If the sequence is empty, then an Option&lt;T&gt;.None is returned, wrapped in a Result&lt;T&gt;.IsSuccess.
- If the sequence has 1 value, then the value is returned as a Option&lt;T&gt;, wrapped in a Result&lt;T&gt;.IsSuccess.

#### Enumerable&lt;Option&lt;T&gt;&gt;
##### Choose
Return only those elements of a sequence of Option&lt;T&gt; that have a value.

##### Map
Transforms each element of a sequence into a new form.

##### MapSwitch
Transforms each element of a sequence based, allows you to pass in seperate transformations for if the value is there or not.

##### Pick
Return the first element of a sequence based on if the value is there.

If none of the elements in the sequence match the predicate, then Option&lt;T&gt;.None is returned.

#### Interop with Nullable
For structs there is interop with *Nullable*

##### Nullable -> Option
Use the *ToOption* extension method.

##### Option -> Nullable
Use the *ToNullable* extension method.

#### Interop with Result
##### Option -> Result
You can move between an Option and a Result using the 'ToResult' extension method. You just need to provide the error if the Option is not present.

##### Result -> Option
This is not supported as it would result in the loss of the error context.