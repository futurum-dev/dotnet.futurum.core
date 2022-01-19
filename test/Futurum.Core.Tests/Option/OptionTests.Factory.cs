using System;

using Futurum.Core.Option;
using Futurum.Core.Tests.Helper.Option;

using Xunit;

namespace Futurum.Core.Tests.Option;

public class OptionFactoryTests
{
    private const string ToStringValue = "TO_STRING";

    public class From
    {
        public class NonGeneric
        {
            public class HasNoValue
            {
                [Fact]
                public void Struct()
                {
                    int? instance = null;

                    var option = Core.Option.Option.From(instance);

                    option.ShouldBeHasNoValue();
                }
                    
                [Fact]
                public void Class()
                {
                    StubClass instance = null;

                    var option = Core.Option.Option.From(instance);

                    option.ShouldBeHasNoValue();
                }
                    
                [Fact]
                public void String()
                {
                    string instance = null;

                    var option = Core.Option.Option.From(instance);

                    option.ShouldBeHasNoValue();
                }
            }

            public class HasValue
            {
                [Fact]
                public void Struct()
                {
                    var instance = 1;

                    var option = Core.Option.Option.From(instance);

                    option.ShouldBeHasValueWithValue(instance);
                }
                    
                [Fact]
                public void Class()
                {
                    var instance = new StubClass();

                    var option = Core.Option.Option.From(instance);

                    option.ShouldBeHasValueWithValue(instance);
                }
                    
                [Fact]
                public void String()
                {
                    var instance = Guid.NewGuid().ToString();

                    var option = Core.Option.Option.From(instance);

                    option.ShouldBeHasValueWithValue(instance);
                }
            }
        }

        public class Generic
        {
            public class HasNoValue
            {
                [Fact]
                public void Struct()
                {
                    int? instance = null;

                    var option = Option<int?>.From(instance);

                    option.ShouldBeHasNoValue();
                }
                    
                [Fact]
                public void Class()
                {
                    StubClass instance = null;

                    var option = Option<StubClass>.From(instance);

                    option.ShouldBeHasNoValue();
                }
                    
                [Fact]
                public void String()
                {
                    string instance = null;

                    var option = Option<string>.From(instance);

                    option.ShouldBeHasNoValue();
                }
            }

            public class HasValue
            {
                [Fact]
                public void Struct()
                {
                    var instance = 1;

                    var option = Option<int>.From(instance);

                    option.ShouldBeHasValueWithValue(instance);
                }
                    
                [Fact]
                public void Class()
                {
                    var instance = new StubClass();

                    var option = Option<StubClass>.From(instance);

                    option.ShouldBeHasValueWithValue(instance);
                }
                    
                [Fact]
                public void String()
                {
                    var instance = Guid.NewGuid().ToString();

                    var option = Option<string>.From(instance);

                    option.ShouldBeHasValueWithValue(instance);
                }
            }
        }
    }

    public class None
    {
        public class Generic
        {
            [Fact]
            public void Struct()
            {
                var option = Option<int>.None;

                option.ShouldBeHasNoValue();
            }

            [Fact]
            public void Class()
            {
                var option = Option<StubClass>.None;

                option.ShouldBeHasNoValue();
            }

            [Fact]
            public void String()
            {
                var option = Option<string>.None;

                option.ShouldBeHasNoValue();
            }
        }

        public class NonGeneric
        {
            [Fact]
            public void Struct()
            {
                var option = Core.Option.Option.None<int>();

                option.ShouldBeHasNoValue();
            }

            [Fact]
            public void Class()
            {
                var option = Core.Option.Option.None<StubClass>();

                option.ShouldBeHasNoValue();
            }

            [Fact]
            public void String()
            {
                var option = Core.Option.Option.None<string>();

                option.ShouldBeHasNoValue();
            }
        }
    }

    private class StubClass
    {
        public override string ToString() =>
            ToStringValue;
    }
}