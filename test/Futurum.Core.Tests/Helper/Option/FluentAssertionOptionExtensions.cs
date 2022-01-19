using System;

using FluentAssertions;

namespace Futurum.Core.Tests.Helper.Option;

/// <summary>
/// Extension methods for <see cref="FluentAssertions"/>, so that it supports <see cref="Futurum.Core.Option.Option{T}"/>
/// </summary>
public static class FluentAssertionOptionExtensions
{
    /// <summary>
    /// Specifies that the <see cref="Futurum.Core.Option.Option{T}"/> should be <see cref="Futurum.Core.Option.Option{T}.HasValue"/> true.
    /// </summary>
    public static void ShouldBeHasValue<T>(this Futurum.Core.Option.Option<T> option)
    {
        option.HasValue.Should().BeTrue();
        option.HasNoValue.Should().BeFalse();
    }

    /// <summary>
    /// Specifies that the <see cref="Futurum.Core.Option.Option{T}"/> should be <see cref="Futurum.Core.Option.Option{T}.HasValue"/> true with <paramref name="value"/>
    /// </summary>
    public static void ShouldBeHasValueWithValue<T>(this Futurum.Core.Option.Option<T> option, T value)
    {
        option.HasValue.Should().BeTrue();

        option.Value.Should().Be(value);
    }

    /// <summary>
    /// Specifies that the <see cref="Futurum.Core.Option.Option{T}"/> should be <see cref="Futurum.Core.Option.Option{T}.HasValue"/> true with <paramref name="value"/>
    /// </summary>
    public static void ShouldBeHasValueWithValue<T, TR>(this Futurum.Core.Option.Option<T> option, Func<T, TR> selectorFunc, TR value)
    {
        option.HasValue.Should().BeTrue();

        selectorFunc(option.Value).Should().Be(value);
    }

    /// <summary>
    /// Specifies that the <see cref="Futurum.Core.Option.Option{T}"/> should be <see cref="Futurum.Core.Option.Option{T}.HasNoValue"/> true.
    /// </summary>
    public static void ShouldBeHasNoValue<T>(this Futurum.Core.Option.Option<T> option)
    {
        option.HasNoValue.Should().BeTrue();
        option.HasValue.Should().BeFalse();
    }
}