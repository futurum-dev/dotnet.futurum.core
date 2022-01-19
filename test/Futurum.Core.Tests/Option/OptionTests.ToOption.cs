using System;

using Futurum.Core.Option;
using Futurum.Core.Tests.Helper.Option;

using Xunit;

namespace Futurum.Core.Tests.Option;

public class OptionExtensionsToOptionTests
{
    public class NonNullable
    {
        public class HasNoValue
        {
            [Fact]
            public void Struct()
            {
                int? value = null;

                var option = value.ToOption();

                option.ShouldBeHasNoValue();
            }
                
            [Fact]
            public void Class()
            {
                StubClass value = null;

                var option = value.ToOption();

                option.ShouldBeHasNoValue();
            }
                
            [Fact]
            public void String()
            {
                string value = null;

                var option = value.ToOption();

                option.ShouldBeHasNoValue();
            }
        }

        public class HasValue
        {
            [Fact]
            public void Struct()
            {
                var value = 1;

                var option = value.ToOption();

                option.ShouldBeHasValueWithValue(value);
            }
                
            [Fact]
            public void Class()
            {
                StubClass value = new StubClass();

                var option = value.ToOption();

                option.ShouldBeHasValueWithValue(value);
            }
                
            [Fact]
            public void String()
            {
                var value = Guid.NewGuid().ToString();

                var option = value.ToOption();

                option.ShouldBeHasValueWithValue(value);
            }
        }
    }

    private class StubClass
    {
    }
}