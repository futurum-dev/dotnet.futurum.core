using Futurum.Core.Result;

namespace Futurum.Core.Option;

public static partial class OptionExtensions
{
    /// <summary>
    /// Tries to convert the specified string representation of a logical value to its Boolean equivalent.
    /// </summary>
    public static Option<bool> TryParseBool(this string? source) =>
        bool.TryParse(source, out var value)
            ? Option<bool>.From(value)
            : Option<bool>.None;

    /// <summary>
    /// Tries to convert the specified string representation of a logical value to its Boolean equivalent.
    /// </summary>
    public static Result<bool> TryParseBool(this string? source, Func<string> errorMessage) =>
        source.TryParseBool()
              .ToResult(errorMessage);

    /// <summary>
    /// Tries to convert the specified string representation of a number to its 32-bit signed integer equivalent.
    /// </summary>
    public static Option<int> TryParseInt(this string? source) =>
        int.TryParse(source, out var value)
            ? Option<int>.From(value)
            : Option<int>.None;

    /// <summary>
    /// Tries to convert the specified string representation of a number to its 32-bit signed integer equivalent.
    /// </summary>
    public static Result<int> TryParseInt(this string? source, Func<string> errorMessage) =>
        source.TryParseInt()
              .ToResult(errorMessage);

    /// <summary>
    /// Tries to convert the specified string representation of a number to its 64-bit signed integer equivalent.
    /// </summary>
    public static Option<long> TryParseLong(this string? source) =>
        long.TryParse(source, out var value)
            ? Option<long>.From(value)
            : Option<long>.None;

    /// <summary>
    /// Tries to convert the specified string representation of a number to its 64-bit signed integer equivalent.
    /// </summary>
    public static Result<long> TryParseLong(this string? source, Func<string> errorMessage) =>
        source.TryParseLong()
              .ToResult(errorMessage);

    /// <summary>
    /// Tries to convert the specified string representation of a number to its Decimal equivalent.
    /// </summary>
    public static Option<decimal> TryParseDecimal(this string? source) =>
        decimal.TryParse(source, out var value)
            ? Option<decimal>.From(value)
            : Option<decimal>.None;

    /// <summary>
    /// Tries to convert the specified string representation of a number to its Decimal equivalent.
    /// </summary>
    public static Result<decimal> TryParseDecimal(this string? source, Func<string> errorMessage) =>
        source.TryParseDecimal()
              .ToResult(errorMessage);

    /// <summary>
    /// Tries to convert the specified string representation of a number to its double-precision floating-point number equivalent.
    /// </summary>
    public static Option<double> TryParseDouble(this string? source) =>
        double.TryParse(source, out var value)
            ? Option<double>.From(value)
            : Option<double>.None;

    /// <summary>
    /// Tries to convert the specified string representation of a number to its double-precision floating-point number equivalent.
    /// </summary>
    public static Result<double> TryParseDouble(this string? source, Func<string> errorMessage) =>
        source.TryParseDouble()
              .ToResult(errorMessage);

    /// <summary>
    /// Tries to convert the specified string representation of a number to its single-precision floating-point number equivalent.
    /// </summary>
    public static Option<float> TryParseFloat(this string? source) =>
        float.TryParse(source, out var value)
            ? Option<float>.From(value)
            : Option<float>.None;

    /// <summary>
    /// Tries to convert the specified string representation of a number to its single-precision floating-point number equivalent.
    /// </summary>
    public static Result<float> TryParseFloat(this string? source, Func<string> errorMessage) =>
        source.TryParseFloat()
              .ToResult(errorMessage);

    /// <summary>
    /// Tries to convert the specified string representation of a GUID to the equivalent Guid structure.
    /// </summary>
    public static Option<Guid> TryParseGuid(this string? source) =>
        Guid.TryParse(source, out var value)
            ? Option<Guid>.From(value)
            : Option<Guid>.None;

    /// <summary>
    /// Tries to convert the specified string representation of a GUID to the equivalent Guid structure.
    /// </summary>
    public static Result<Guid> TryParseGuid(this string? source, Func<string> errorMessage) =>
        source.TryParseGuid()
              .ToResult(errorMessage);

    /// <summary>
    /// Tries to convert the specified string representation of a date and time to its DateTime equivalent.
    /// </summary>
    public static Option<DateTime> TryParseDateTime(this string? source) =>
        DateTime.TryParse(source, out var value)
            ? Option<DateTime>.From(value)
            : Option<DateTime>.None;

    /// <summary>
    /// Tries to convert the specified string representation of a date and time to its DateTime equivalent.
    /// </summary>
    public static Result<DateTime> TryParseDateTime(this string? source, Func<string> errorMessage) =>
        source.TryParseDateTime()
              .ToResult(errorMessage);

    /// <summary>
    /// Tries to convert the specified string representation of a date to its DateOnly equivalent.
    /// </summary>
    public static Option<DateOnly> TryParseDateOnly(this string? source) =>
        DateOnly.TryParse(source, out var value)
            ? Option<DateOnly>.From(value)
            : Option<DateOnly>.None;

    /// <summary>
    /// Tries to convert the specified string representation of a date to its DateOnly equivalent.
    /// </summary>
    public static Result<DateOnly> TryParseDateOnly(this string? source, Func<string> errorMessage) =>
        source.TryParseDateOnly()
              .ToResult(errorMessage);

    /// <summary>
    /// Tries to convert the specified string representation of a time to its TimeOnly equivalent.
    /// </summary>
    public static Option<TimeOnly> TryParseTimeOnly(this string? source) =>
        TimeOnly.TryParse(source, out var value)
            ? Option<TimeOnly>.From(value)
            : Option<TimeOnly>.None;

    /// <summary>
    /// Tries to convert the specified string representation of a time to its TimeOnly equivalent.
    /// </summary>
    public static Result<TimeOnly> TryParseTimeOnly(this string? source, Func<string> errorMessage) =>
        source.TryParseTimeOnly()
              .ToResult(errorMessage);

    /// <summary>
    /// Tries to convert the specified string representation of the name or numeric value of one or more enumerated constants to an equivalent enumerated object.
    /// </summary>
    public static Option<TEnum> TryParseEnum<TEnum>(this string? source)
        where TEnum : struct, Enum =>
        Enum.TryParse(source, out TEnum value)
            ? Option<TEnum>.From(value)
            : Option<TEnum>.None;

    /// <summary>
    /// Tries to convert the specified string representation of the name or numeric value of one or more enumerated constants to an equivalent enumerated object.
    /// </summary>
    public static Result<TEnum> TryParseEnum<TEnum>(this string? source, Func<string> errorMessage)
        where TEnum : struct, Enum =>
        source.TryParseEnum<TEnum>()
              .ToResult(errorMessage);
}