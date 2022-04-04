using System;

using Futurum.Core.Option;
using Futurum.Core.Tests.Helper.Option;
using Futurum.Core.Tests.Helper.Result;

using Xunit;

namespace Futurum.Core.Tests.Option;

public class OptionExtensionsTryParseTests
{
    private const string ErrorMessage = "ERROR_MESSAGE";

    public class TryParseBool
    {
        [Fact]
        public void HasNoValue()
        {
            var valueString = Guid.NewGuid().ToString();

            var option = valueString.TryParseBool();

            option.ShouldBeHasNoValue();
        }

        [Fact]
        public void HasValue_False()
        {
            const bool expected = false;

            var valueString = expected.ToString();

            var option = valueString.TryParseBool();

            option.ShouldBeHasValueWithValue(expected);
        }

        [Fact]
        public void HasValue_True()
        {
            const bool expected = true;

            var valueString = expected.ToString();

            var option = valueString.TryParseBool();

            option.ShouldBeHasValueWithValue(expected);
        }
        
        public class AsResult_ErrorMessage
        {
            [Fact]
            public void HasNoValue()
            {
                var valueString = Guid.NewGuid().ToString();

                var option = valueString.TryParseBool(ErrorMessage);

                option.ShouldBeFailureWithError(ErrorMessage);
            }

            [Fact]
            public void HasValue_False()
            {
                const bool expected = false;

                var valueString = expected.ToString();

                var option = valueString.TryParseBool(ErrorMessage);

                option.ShouldBeSuccessWithValue(expected);
            }

            [Fact]
            public void HasValue_True()
            {
                const bool expected = true;

                var valueString = expected.ToString();

                var option = valueString.TryParseBool(ErrorMessage);

                option.ShouldBeSuccessWithValue(expected);
            }
        }
        
        public class AsResult_FuncErrorMessage
        {
            [Fact]
            public void HasNoValue()
            {
                var valueString = Guid.NewGuid().ToString();

                var option = valueString.TryParseBool(() => ErrorMessage);

                option.ShouldBeFailureWithError(ErrorMessage);
            }

            [Fact]
            public void HasValue_False()
            {
                const bool expected = false;

                var valueString = expected.ToString();

                var option = valueString.TryParseBool(() => ErrorMessage);

                option.ShouldBeSuccessWithValue(expected);
            }

            [Fact]
            public void HasValue_True()
            {
                const bool expected = true;

                var valueString = expected.ToString();

                var option = valueString.TryParseBool(() => ErrorMessage);

                option.ShouldBeSuccessWithValue(expected);
            }
        }
    }

    public class TryParseInt
    {
        [Fact]
        public void HasNoValue()
        {
            var valueString = Guid.NewGuid().ToString();

            var option = valueString.TryParseInt();

            option.ShouldBeHasNoValue();
        }

        [Fact]
        public void HasValue()
        {
            var expected = 1;

            var valueString = expected.ToString();

            var option = valueString.TryParseInt();

            option.ShouldBeHasValueWithValue(expected);
        }
        
        public class AsResult_ErrorMessage
        {
            [Fact]
            public void HasNoValue()
            {
                var valueString = Guid.NewGuid().ToString();

                var option = valueString.TryParseInt(ErrorMessage);

                option.ShouldBeFailureWithError(ErrorMessage);
            }

            [Fact]
            public void HasValue()
            {
                var expected = 1;

                var valueString = expected.ToString();

                var option = valueString.TryParseInt(ErrorMessage);

                option.ShouldBeSuccessWithValue(expected);
            }
        }
        
        public class AsResult_FuncErrorMessage
        {
            [Fact]
            public void HasNoValue()
            {
                var valueString = Guid.NewGuid().ToString();

                var option = valueString.TryParseInt(() => ErrorMessage);

                option.ShouldBeFailureWithError(ErrorMessage);
            }

            [Fact]
            public void HasValue()
            {
                var expected = 1;

                var valueString = expected.ToString();

                var option = valueString.TryParseInt(() => ErrorMessage);

                option.ShouldBeSuccessWithValue(expected);
            }
        }
    }

    public class TryParseLong
    {
        [Fact]
        public void HasNoValue()
        {
            var valueString = Guid.NewGuid().ToString();

            var option = valueString.TryParseLong();

            option.ShouldBeHasNoValue();
        }

        [Fact]
        public void HasValue()
        {
            var expected = 1;

            var valueString = expected.ToString();

            var option = valueString.TryParseLong();

            option.ShouldBeHasValueWithValue(expected);
        }
        
        public class AsResult_ErrorMessage
        {
            [Fact]
            public void HasNoValue()
            {
                var valueString = Guid.NewGuid().ToString();

                var option = valueString.TryParseLong(ErrorMessage);

                option.ShouldBeFailureWithError(ErrorMessage);
            }

            [Fact]
            public void HasValue()
            {
                var expected = 1;

                var valueString = expected.ToString();

                var option = valueString.TryParseLong(ErrorMessage);

                option.ShouldBeSuccessWithValue(expected);
            }
        }
        
        public class AsResult_FuncErrorMessage
        {
            [Fact]
            public void HasNoValue()
            {
                var valueString = Guid.NewGuid().ToString();

                var option = valueString.TryParseLong(() => ErrorMessage);

                option.ShouldBeFailureWithError(ErrorMessage);
            }

            [Fact]
            public void HasValue()
            {
                var expected = 1;

                var valueString = expected.ToString();

                var option = valueString.TryParseLong(() => ErrorMessage);

                option.ShouldBeSuccessWithValue(expected);
            }
        }
    }

    public class TryParseDecimal
    {
        [Fact]
        public void HasNoValue()
        {
            var valueString = Guid.NewGuid().ToString();

            var option = valueString.TryParseDecimal();

            option.ShouldBeHasNoValue();
        }

        [Fact]
        public void HasValue()
        {
            var expected = 1m;

            var valueString = expected.ToString();

            var option = valueString.TryParseDecimal();

            option.ShouldBeHasValueWithValue(expected);
        }

        public class AsResult_ErrorMessage
        {
            [Fact]
            public void HasNoValue()
            {
                var valueString = Guid.NewGuid().ToString();

                var option = valueString.TryParseDecimal(ErrorMessage);

                option.ShouldBeFailureWithError(ErrorMessage);
            }

            [Fact]
            public void HasValue()
            {
                var expected = 1m;

                var valueString = expected.ToString();

                var option = valueString.TryParseDecimal(ErrorMessage);

                option.ShouldBeSuccessWithValue(expected);
            }
        }

        public class AsResult_FuncErrorMessage
        {
            [Fact]
            public void HasNoValue()
            {
                var valueString = Guid.NewGuid().ToString();

                var option = valueString.TryParseDecimal(() => ErrorMessage);

                option.ShouldBeFailureWithError(ErrorMessage);
            }

            [Fact]
            public void HasValue()
            {
                var expected = 1m;

                var valueString = expected.ToString();

                var option = valueString.TryParseDecimal(() => ErrorMessage);

                option.ShouldBeSuccessWithValue(expected);
            }
        }
    }

    public class TryParseDouble
    {
        [Fact]
        public void HasNoValue()
        {
            var valueString = Guid.NewGuid().ToString();

            var option = valueString.TryParseDouble();

            option.ShouldBeHasNoValue();
        }

        [Fact]
        public void HasValue()
        {
            var expected = 1d;

            var valueString = expected.ToString();

            var option = valueString.TryParseDouble();

            option.ShouldBeHasValueWithValue(expected);
        }

        public class AsResult_ErrorMessage
        {
            [Fact]
            public void HasNoValue()
            {
                var valueString = Guid.NewGuid().ToString();

                var option = valueString.TryParseDouble(ErrorMessage);

                option.ShouldBeFailureWithError(ErrorMessage);
            }

            [Fact]
            public void HasValue()
            {
                var expected = 1d;

                var valueString = expected.ToString();

                var option = valueString.TryParseDouble(ErrorMessage);

                option.ShouldBeSuccessWithValue(expected);
            }
        }

        public class AsResult_FuncErrorMessage
        {
            [Fact]
            public void HasNoValue()
            {
                var valueString = Guid.NewGuid().ToString();

                var option = valueString.TryParseDouble(() => ErrorMessage);

                option.ShouldBeFailureWithError(ErrorMessage);
            }

            [Fact]
            public void HasValue()
            {
                var expected = 1d;

                var valueString = expected.ToString();

                var option = valueString.TryParseDouble(() => ErrorMessage);

                option.ShouldBeSuccessWithValue(expected);
            }
        }
    }

    public class TryParseFloat
    {
        [Fact]
        public void HasNoValue()
        {
            var valueString = Guid.NewGuid().ToString();

            var option = valueString.TryParseFloat();

            option.ShouldBeHasNoValue();
        }

        [Fact]
        public void HasValue()
        {
            var expected = 1f;

            var valueString = expected.ToString();

            var option = valueString.TryParseFloat();

            option.ShouldBeHasValueWithValue(expected);
        }

        public class AsResult_ErrorMessage
        {
            [Fact]
            public void HasNoValue()
            {
                var valueString = Guid.NewGuid().ToString();

                var option = valueString.TryParseFloat(ErrorMessage);

                option.ShouldBeFailureWithError(ErrorMessage);
            }

            [Fact]
            public void HasValue()
            {
                var expected = 1f;

                var valueString = expected.ToString();

                var option = valueString.TryParseFloat(ErrorMessage);

                option.ShouldBeSuccessWithValue(expected);
            }
        }

        public class AsResult_FuncErrorMessage
        {
            [Fact]
            public void HasNoValue()
            {
                var valueString = Guid.NewGuid().ToString();

                var option = valueString.TryParseFloat(() => ErrorMessage);

                option.ShouldBeFailureWithError(ErrorMessage);
            }

            [Fact]
            public void HasValue()
            {
                var expected = 1f;

                var valueString = expected.ToString();

                var option = valueString.TryParseFloat(() => ErrorMessage);

                option.ShouldBeSuccessWithValue(expected);
            }
        }
    }

    public class TryParseGuid
    {
        [Fact]
        public void HasNoValue()
        {
            var valueString = "TEST";

            var option = valueString.TryParseGuid();

            option.ShouldBeHasNoValue();
        }

        [Fact]
        public void HasValue()
        {
            var expected = Guid.NewGuid();

            var valueString = expected.ToString();

            var option = valueString.TryParseGuid();

            option.ShouldBeHasValueWithValue(expected);
        }

        public class AsResult_ErrorMessage
        {
            [Fact]
            public void HasNoValue()
            {
                var valueString = "TEST";

                var option = valueString.TryParseGuid(ErrorMessage);

                option.ShouldBeFailureWithError(ErrorMessage);
            }

            [Fact]
            public void HasValue()
            {
                var expected = Guid.NewGuid();

                var valueString = expected.ToString();

                var option = valueString.TryParseGuid(ErrorMessage);

                option.ShouldBeSuccessWithValue(expected);
            }
        }

        public class AsResult_FuncErrorMessage
        {
            [Fact]
            public void HasNoValue()
            {
                var valueString = "TEST";

                var option = valueString.TryParseGuid(() => ErrorMessage);

                option.ShouldBeFailureWithError(ErrorMessage);
            }

            [Fact]
            public void HasValue()
            {
                var expected = Guid.NewGuid();

                var valueString = expected.ToString();

                var option = valueString.TryParseGuid(() => ErrorMessage);

                option.ShouldBeSuccessWithValue(expected);
            }
        }
    }

    public class TryParseDateTime
    {
        [Fact]
        public void HasNoValue()
        {
            var valueString = "TEST";

            var option = valueString.TryParseDateTime();

            option.ShouldBeHasNoValue();
        }

        [Fact]
        public void HasValue()
        {
            var expected = DateTime.Now;

            var valueString = expected.ToString();

            var option = valueString.TryParseDateTime();

            option.ShouldBeHasValue();
            option.ShouldBeHasValueWithValue(x => x.Year, expected.Year);
            option.ShouldBeHasValueWithValue(x => x.Month, expected.Month);
            option.ShouldBeHasValueWithValue(x => x.Day, expected.Day);
            option.ShouldBeHasValueWithValue(x => x.Hour, expected.Hour);
            option.ShouldBeHasValueWithValue(x => x.Minute, expected.Minute);
            option.ShouldBeHasValueWithValue(x => x.Second, expected.Second);
        }

        public class AsResult_ErrorMessage
        {
            [Fact]
            public void HasNoValue()
            {
                var valueString = "TEST";

                var option = valueString.TryParseDateTime(ErrorMessage);

                option.ShouldBeFailureWithError(ErrorMessage);
            }

            [Fact]
            public void HasValue()
            {
                var expected = DateTime.Now;

                var valueString = expected.ToString();

                var option = valueString.TryParseDateTime(ErrorMessage);

                option.ShouldBeSuccessWithValue(x => x.Year, expected.Year);
                option.ShouldBeSuccessWithValue(x => x.Month, expected.Month);
                option.ShouldBeSuccessWithValue(x => x.Day, expected.Day);
                option.ShouldBeSuccessWithValue(x => x.Hour, expected.Hour);
                option.ShouldBeSuccessWithValue(x => x.Minute, expected.Minute);
                option.ShouldBeSuccessWithValue(x => x.Second, expected.Second);
            }
        }

        public class AsResult_FuncErrorMessage
        {
            [Fact]
            public void HasNoValue()
            {
                var valueString = "TEST";

                var option = valueString.TryParseDateTime(() => ErrorMessage);

                option.ShouldBeFailureWithError(ErrorMessage);
            }

            [Fact]
            public void HasValue()
            {
                var expected = DateTime.Now;

                var valueString = expected.ToString();

                var option = valueString.TryParseDateTime(() => ErrorMessage);

                option.ShouldBeSuccessWithValue(x => x.Year, expected.Year);
                option.ShouldBeSuccessWithValue(x => x.Month, expected.Month);
                option.ShouldBeSuccessWithValue(x => x.Day, expected.Day);
                option.ShouldBeSuccessWithValue(x => x.Hour, expected.Hour);
                option.ShouldBeSuccessWithValue(x => x.Minute, expected.Minute);
                option.ShouldBeSuccessWithValue(x => x.Second, expected.Second);
            }
        }
    }

    public class TryParseDateOnly
    {
        [Fact]
        public void HasNoValue()
        {
            var valueString = "TEST";

            var option = valueString.TryParseDateOnly();

            option.ShouldBeHasNoValue();
        }

        [Fact]
        public void HasValue()
        {
            var expected = DateOnly.FromDateTime(DateTime.Now);

            var valueString = expected.ToString();

            var option = valueString.TryParseDateOnly();

            option.ShouldBeHasValue();
            option.ShouldBeHasValueWithValue(x => x.Year, expected.Year);
            option.ShouldBeHasValueWithValue(x => x.Month, expected.Month);
            option.ShouldBeHasValueWithValue(x => x.Day, expected.Day);
        }

        public class AsResult_ErrorMessage
        {
            [Fact]
            public void HasNoValue()
            {
                var valueString = "TEST";

                var option = valueString.TryParseDateOnly(ErrorMessage);

                option.ShouldBeFailureWithError(ErrorMessage);
            }

            [Fact]
            public void HasValue()
            {
                var expected = DateOnly.FromDateTime(DateTime.Now);

                var valueString = expected.ToString();

                var option = valueString.TryParseDateOnly(ErrorMessage);

                option.ShouldBeSuccessWithValue(x => x.Year, expected.Year);
                option.ShouldBeSuccessWithValue(x => x.Month, expected.Month);
                option.ShouldBeSuccessWithValue(x => x.Day, expected.Day);
            }
        }

        public class AsResult_FuncErrorMessage
        {
            [Fact]
            public void HasNoValue()
            {
                var valueString = "TEST";

                var option = valueString.TryParseDateOnly(() => ErrorMessage);

                option.ShouldBeFailureWithError(ErrorMessage);
            }

            [Fact]
            public void HasValue()
            {
                var expected = DateOnly.FromDateTime(DateTime.Now);

                var valueString = expected.ToString();

                var option = valueString.TryParseDateOnly(() => ErrorMessage);

                option.ShouldBeSuccessWithValue(x => x.Year, expected.Year);
                option.ShouldBeSuccessWithValue(x => x.Month, expected.Month);
                option.ShouldBeSuccessWithValue(x => x.Day, expected.Day);
            }
        }
    }

    public class TryParseTimeOnly
    {
        [Fact]
        public void HasNoValue()
        {
            var valueString = "TEST";

            var option = valueString.TryParseTimeOnly();

            option.ShouldBeHasNoValue();
        }

        [Fact]
        public void HasValue()
        {
            var expected = TimeOnly.FromDateTime(DateTime.Now);

            var valueString = expected.ToString();

            var option = valueString.TryParseTimeOnly();

            option.ShouldBeHasValue();
            option.ShouldBeHasValueWithValue(x => x.Hour, expected.Hour);
            option.ShouldBeHasValueWithValue(x => x.Minute, expected.Minute);
        }

        public class AsResult_ErrorMessage
        {
            [Fact]
            public void HasNoValue()
            {
                var valueString = "TEST";

                var option = valueString.TryParseTimeOnly(ErrorMessage);

                option.ShouldBeFailureWithError(ErrorMessage);
            }

            [Fact]
            public void HasValue()
            {
                var expected = TimeOnly.FromDateTime(DateTime.Now);

                var valueString = expected.ToString();

                var option = valueString.TryParseTimeOnly(ErrorMessage);

                option.ShouldBeSuccessWithValue(x => x.Hour, expected.Hour);
                option.ShouldBeSuccessWithValue(x => x.Minute, expected.Minute);
            }
        }

        public class AsResult_FuncErrorMessage
        {
            [Fact]
            public void HasNoValue()
            {
                var valueString = "TEST";

                var option = valueString.TryParseTimeOnly(() => ErrorMessage);

                option.ShouldBeFailureWithError(ErrorMessage);
            }

            [Fact]
            public void HasValue()
            {
                var expected = TimeOnly.FromDateTime(DateTime.Now);

                var valueString = expected.ToString();

                var option = valueString.TryParseTimeOnly(() => ErrorMessage);

                option.ShouldBeSuccessWithValue(x => x.Hour, expected.Hour);
                option.ShouldBeSuccessWithValue(x => x.Minute, expected.Minute);
            }
        }
    }

    public class TryParseEnum
    {
        private enum EnumValues
        {
            Value1,
            Value2,
            Value3
        }

        [Fact]
        public void HasNoValue()
        {
            var valueString = Guid.NewGuid().ToString();

            var option = valueString.TryParseEnum<EnumValues>();

            option.ShouldBeHasNoValue();
        }

        [Fact]
        public void HasValue()
        {
            var expected = EnumValues.Value2;

            var valueString = expected.ToString();

            var option = valueString.TryParseEnum<EnumValues>();

            option.ShouldBeHasValueWithValue(expected);
        }

        public class AsResult_ErrorMessage
        {
            private enum EnumValues
            {
                Value1,
                Value2,
                Value3
            }

            [Fact]
            public void HasNoValue()
            {
                var valueString = Guid.NewGuid().ToString();

                var option = valueString.TryParseEnum<EnumValues>(ErrorMessage);

                option.ShouldBeFailureWithError(ErrorMessage);
            }

            [Fact]
            public void HasValue()
            {
                var expected = EnumValues.Value2;

                var valueString = expected.ToString();

                var option = valueString.TryParseEnum<EnumValues>(ErrorMessage);

                option.ShouldBeSuccessWithValue(expected);
            }
        }

        public class AsResult_FuncErrorMessage
        {
            private enum EnumValues
            {
                Value1,
                Value2,
                Value3
            }

            [Fact]
            public void HasNoValue()
            {
                var valueString = Guid.NewGuid().ToString();

                var option = valueString.TryParseEnum<EnumValues>(() => ErrorMessage);

                option.ShouldBeFailureWithError(ErrorMessage);
            }

            [Fact]
            public void HasValue()
            {
                var expected = EnumValues.Value2;

                var valueString = expected.ToString();

                var option = valueString.TryParseEnum<EnumValues>(() => ErrorMessage);

                option.ShouldBeSuccessWithValue(expected);
            }
        }
    }
}