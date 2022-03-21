using System;
using System.Threading.Tasks;

using Futurum.Core.Option;
using Futurum.Core.Result;
using Futurum.Core.Tests.Helper.Result;

using Xunit;

namespace Futurum.Core.Tests.Result;

public class ResultErrorKeyNotFoundExtensionsTests
{
    public class Sync
    {
        [Fact]
        public void HasNoValue()
        {
            var key = Guid.NewGuid().ToString();
            var sourceDescription = Guid.NewGuid().ToString();

            string value = null;
            var option = value.ToOption().ToResultOk();

            var result = option.ToResultErrorKeyNotFound(key, sourceDescription);

            result.ShouldBeFailureWithError($"Unable to find key : '{key}' in source : '{sourceDescription}'");
        }

        [Fact]
        public void HasValue()
        {
            var key = Guid.NewGuid().ToString();
            var sourceDescription = Guid.NewGuid().ToString();

            var value = Guid.NewGuid();
            var option = value.ToOption().ToResultOk();

            var result = option.ToResultErrorKeyNotFound(key, sourceDescription);

            result.ShouldBeSuccessWithValue(value);
        }
    }

    public class Async
    {
        [Fact]
        public async Task HasNoValue()
        {
            var key = Guid.NewGuid().ToString();
            var sourceDescription = Guid.NewGuid().ToString();

            string value = null;
            var option = value.ToOption().ToResultOkAsync();

            var result = await option.ToResultErrorKeyNotFoundAsync(key, sourceDescription);

            result.ShouldBeFailureWithError($"Unable to find key : '{key}' in source : '{sourceDescription}'");
        }

        [Fact]
        public async Task HasValue()
        {
            var key = Guid.NewGuid().ToString();
            var sourceDescription = Guid.NewGuid().ToString();

            var value = Guid.NewGuid();
            var option = value.ToOption().ToResultOkAsync();

            var result = await option.ToResultErrorKeyNotFoundAsync(key, sourceDescription);

            result.ShouldBeSuccessWithValue(value);
        }
    }
}