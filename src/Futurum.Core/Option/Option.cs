using System.Diagnostics;

namespace Futurum.Core.Option;

/// <summary>
/// Represents a value that is either present or not present.
/// <list type="bullet">
///     <item>
///         <description>
///         If the value is present, then <see cref="HasValue" /> is true and the value is available via
///         <see cref="Value" />.
///         </description>
///     </item>
///     <item>
///         <description>If the value is not present, then <see cref="HasNoValue" /> is true.</description>
///     </item>
/// </list>
/// <para></para>
/// Must use one of the provided extension methods, see <see cref="OptionExtensions" />.
/// </summary>
public readonly partial struct Option<T> : IEquatable<Option<T>>
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly T? _value;

    [DebuggerStepThrough]
    private Option(T? value)
    {
        HasValue = value != null;
        _value = value;
    }

    internal bool HasValue { get; }

    internal bool HasNoValue => !HasValue;

    internal T Value
    {
        [DebuggerStepThrough]
        get => (HasValue
            ? _value
            : throw new InvalidOperationException()) ?? throw new InvalidOperationException();
    }

    public static bool operator ==(Option<T> option, T value) =>
        option.Equals(value);

    public static bool operator !=(Option<T> option, T value) =>
        !option.Equals(value);

    public static bool operator ==(Option<T> first, Option<T> second) =>
        first.Equals(second);

    public static bool operator !=(Option<T> first, Option<T> second) =>
        !first.Equals(second);

    public override bool Equals(object? other) =>
        other switch
        {
            T cast           => Equals(cast.ToOption()),
            Option<T> option => Equals(option),
            _                => false
        };

    public bool Equals(Option<T> other)
    {
        if (HasNoValue && other.HasNoValue) return true;

        if (HasNoValue || other.HasNoValue) return false;

        return _value!.Equals(other._value);
    }

    public override int GetHashCode() =>
        HasValue ? _value!.GetHashCode() : 0;

    public static implicit operator Option<T>(T value) =>
        From(value);

    public override string? ToString() =>
        HasValue ? _value!.ToString() : $"No value for {typeof(T).FullName}";

    public string? ToString(string defaultValue) =>
        HasValue ? _value!.ToString() : defaultValue;
}