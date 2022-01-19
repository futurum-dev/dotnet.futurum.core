using System.Diagnostics;

using Futurum.Core.Option;

namespace Futurum.Core.Result;

/// <summary>
/// Represents the result that is either a success or a failure.
/// <list type="bullet">
///     <item>
///         <description>
///         If the result is a success, then <see cref="IsSuccess" /> is true and the value is available via
///         <see cref="Value" />.
///         </description>
///     </item>
///     <item>
///         <description>
///         If the result is a failure, then <see cref="IsFailure" /> is true and the associated error is
///         available via <see cref="Error" />.
///         </description>
///     </item>
/// </list>
/// <para></para>
/// Must use one of the provided extension methods, see <see cref="ResultExtensions" />.
/// </summary>
public readonly partial struct Result<T>
{
    [DebuggerStepThrough]
    internal Result(T value)
    {
        if (value is null) throw new ArgumentNullException(nameof(value), "Result Success must specify an associated value.");

        IsSuccess = true;

        Error = Option<IResultError>.None;

        Value = Option<T>.From(value);
    }

    [DebuggerStepThrough]
    internal Result(IResultError error)
    {
        if (error is null or ResultErrorEmpty) throw new ArgumentNullException(nameof(error), "Result Failure must specify an associated error.");

        IsSuccess = false;

        Error = Option<IResultError>.From(error);

        Value = Option<T>.None;

#if DEBUG
        if (Debugger.IsAttached)
        {
            Debugger.Break();
        }
#endif
    }

    internal bool IsSuccess { get; }

    internal bool IsFailure => !IsSuccess;

    internal Option<IResultError> Error { get; }

    internal Option<T> Value { get; }

    public static implicit operator Result(Result<T> result) =>
        result.ToNonGeneric();
}