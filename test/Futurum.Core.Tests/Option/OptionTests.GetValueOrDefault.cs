using System;

using FluentAssertions;

using Futurum.Core.Option;

using Xunit;

namespace Futurum.Core.Tests.Option;

public class OptionGetOrDefaultTests
{
    public class Value
    {
        [Fact]
        public void HasNoValue()
        {
            string inputValue = null;
            var defaultValue = Guid.NewGuid().ToString();

            var inputOption = inputValue.ToOption();

            var value = inputOption.GetValueOrDefault(defaultValue);

            value.Should().Be(defaultValue);
        }

        [Fact]
        public void HasValue()
        {
            var inputValue = Guid.NewGuid().ToString();
            var defaultValue = Guid.NewGuid().ToString();

            var inputOption = inputValue.ToOption();

            var value = inputOption.GetValueOrDefault(defaultValue);

            value.Should().Be(inputValue);
        }
    }

    public class ValueWithSelector
    {
        public class Container
        {
            public Container(string value)
            {
                Value = value;
            }

            public string Value { get; }
        }

        [Fact]
        public void HasNoValue()
        {
            Container inputValue = null;
            var defaultValue = Guid.NewGuid().ToString();

            var inputOption = inputValue.ToOption();

            var value = inputOption.GetValueOrDefault(x => x.Value, defaultValue);

            value.Should().Be(defaultValue);
        }

        [Fact]
        public void HasValue()
        {
            var inputValue = Guid.NewGuid().ToString();
            var defaultValue = Guid.NewGuid().ToString();

            var inputOption = new Container(inputValue).ToOption();

            var value = inputOption.GetValueOrDefault(x => x.Value, defaultValue);

            value.Should().Be(inputValue);
        }
    }

    public class Func
    {
        [Fact]
        public void HasNoValue()
        {
            string inputValue = null;
            var defaultValue = Guid.NewGuid().ToString();

            var inputOption = inputValue.ToOption();

            var value = inputOption.GetValueOrDefault(() => defaultValue);

            value.Should().Be(defaultValue);
        }

        [Fact]
        public void HasValue()
        {
            var inputValue = Guid.NewGuid().ToString();
            var defaultValue = Guid.NewGuid().ToString();

            var inputOption = inputValue.ToOption();

            var value = inputOption.GetValueOrDefault(() => defaultValue);

            value.Should().Be(inputValue);
        }
    }

    public class SelectorAndFunc
    {
        public class Container
        {
            public Container(string value)
            {
                Value = value;
            }

            public string Value { get; }
        }

        [Fact]
        public void HasNoValue()
        {
            Container inputValue = null;
            var defaultValue = Guid.NewGuid().ToString();

            var inputOption = inputValue.ToOption();

            var value = inputOption.GetValueOrDefault(x => x.Value, () => defaultValue);

            value.Should().Be(defaultValue);
        }

        [Fact]
        public void HasValue()
        {
            var inputValue = Guid.NewGuid().ToString();
            var defaultValue = Guid.NewGuid().ToString();

            var inputOption = new Container(inputValue).ToOption();

            var value = inputOption.GetValueOrDefault(x => x.Value, () => defaultValue);

            value.Should().Be(inputValue);
        }
    }
}