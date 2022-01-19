using System.Diagnostics;

using Futurum.Core.Option;

namespace Futurum.Core.Result;

/// <summary>
/// Represents the result that is either a success or a failure.
/// <list type="bullet">
///     <item>
///         <description>If the result is a success, then <see cref="IsSuccess" /> is true.</description>
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
public readonly partial struct Result
{
    [DebuggerStepThrough]
    private Result(bool isSuccess)
    {
        IsSuccess = true;

        Error = Option<IResultError>.None;
    }

    [DebuggerStepThrough]
    private Result(IResultError? error)
    {
        if (error is null or ResultErrorEmpty) throw new ArgumentNullException(nameof(error), "Result Failure must specify an associated error.");

        IsSuccess = false;

        Error = Option<IResultError>.From(error);

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
}