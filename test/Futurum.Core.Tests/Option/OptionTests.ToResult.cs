using System;
using System.Threading.Tasks;

using Futurum.Core.Option;
using Futurum.Core.Result;
using Futurum.Core.Tests.Helper.Result;

using Xunit;

namespace Futurum.Core.Tests.Option;

public class OptionExtensionsToResultTests
{
    private const string ErrorMessage = "ERROR_MESSAGE";

    public class Sync
    {
        public class StringErrorMessage
        {
            [Fact]
            public void HasNoValue()
            {
                string value = null;
                var option = value.ToOption();

                var result = option.ToResult(ErrorMessage);

                result.ShouldBeFailureWithError(ErrorMessage);
            }

            [Fact]
            public void HasValue()
            {
                var value = Guid.NewGuid();
                var option = value.ToOption();

                var result = option.ToResult(ErrorMessage);

                result.ShouldBeSuccessWithValue(value);
            }
        }

        public class FuncStringErrorMessage
        {
            [Fact]
            public void HasNoValue()
            {
                string value = null;
                var option = value.ToOption();

                var result = option.ToResult(() => ErrorMessage);

                result.ShouldBeFailureWithError(ErrorMessage);
            }

            [Fact]
            public void HasValue()
            {
                var value = Guid.NewGuid();
                var option = value.ToOption();

                var result = option.ToResult(() => ErrorMessage);

                result.ShouldBeSuccessWithValue(value);
            }
        }

        public class IResultError
        {
            [Fact]
            public void HasNoValue()
            {
                string value = null;
                var option = value.ToOption();

                var result = option.ToResult(ErrorMessage.ToResultError());

                result.ShouldBeFailureWithError(ErrorMessage);
            }

            [Fact]
            public void HasValue()
            {
                var value = Guid.NewGuid();
                var option = value.ToOption();

                var result = option.ToResult(ErrorMessage.ToResultError());

                result.ShouldBeSuccessWithValue(value);
            }
        }

        public class FuncIResultError
        {
            [Fact]
            public void HasNoValue()
            {
                string value = null;
                var option = value.ToOption();

                var result = option.ToResult(() => ErrorMessage.ToResultError());

                result.ShouldBeFailureWithError(ErrorMessage);
            }

            [Fact]
            public void HasValue()
            {
                var value = Guid.NewGuid();
                var option = value.ToOption();

                var result = option.ToResult(() => ErrorMessage.ToResultError());

                result.ShouldBeSuccessWithValue(value);
            }
        }
    }

    public class Async
    {
        public class StringErrorMessage
        {
            [Fact]
            public async Task HasNoValue()
            {
                string value = null;
                var option = Task.FromResult(value.ToOption());

                var result = await option.ToResultAsync(ErrorMessage);

                result.ShouldBeFailureWithError(ErrorMessage);
            }

            [Fact]
            public async Task HasValue()
            {
                var value = Guid.NewGuid();
                var option = Task.FromResult(value.ToOption());

                var result = await option.ToResultAsync(ErrorMessage);

                result.ShouldBeSuccessWithValue(value);
            }
        }

        public class FuncStringErrorMessage
        {
            [Fact]
            public async Task HasNoValue()
            {
                string value = null;
                var option = Task.FromResult(value.ToOption());

                var result = await option.ToResultAsync(() => ErrorMessage);

                result.ShouldBeFailureWithError(ErrorMessage);
            }

            [Fact]
            public async Task HasValue()
            {
                var value = Guid.NewGuid();
                var option = Task.FromResult(value.ToOption());

                var result = await option.ToResultAsync(() => ErrorMessage);

                result.ShouldBeSuccessWithValue(value);
            }
        }

        public class IResultError
        {
            [Fact]
            public async Task HasNoValue()
            {
                string value = null;
                var option = Task.FromResult(value.ToOption());

                var result = await option.ToResultAsync(ErrorMessage.ToResultError());

                result.ShouldBeFailureWithError(ErrorMessage);
            }

            [Fact]
            public async Task HasValue()
            {
                var value = Guid.NewGuid();
                var option = Task.FromResult(value.ToOption());

                var result = await option.ToResultAsync(ErrorMessage.ToResultError());

                result.ShouldBeSuccessWithValue(value);
            }
        }

        public class FuncIResultError
        {
            [Fact]
            public async Task HasNoValue()
            {
                string value = null;
                var option = Task.FromResult(value.ToOption());

                var result = await option.ToResultAsync(() => ErrorMessage.ToResultError());

                result.ShouldBeFailureWithError(ErrorMessage);
            }

            [Fact]
            public async Task HasValue()
            {
                var value = Guid.NewGuid();
                var option = Task.FromResult(value.ToOption());

                var result = await option.ToResultAsync(() => ErrorMessage.ToResultError());

                result.ShouldBeSuccessWithValue(value);
            }
        }
    }
}