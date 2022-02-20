using System;
using System.Threading.Tasks;

using Futurum.Core.Result;
using Futurum.Core.Tests.Helper.Result;

using Xunit;

namespace Futurum.Core.Tests.Result;

public class ResultExtensionsToOkResultTests
{
    public class Sync
    {
        [Fact]
        public void ToOkResult()
        {
            var value = Guid.NewGuid();

            var result = value.ToResultOk();

            result.ShouldBeSuccessWithValue(value);
        }
    }

    public class Async
    {
        [Fact]
        public async Task ToOkResultAsync()
        {
            var value = Guid.NewGuid();

            var result = await value.ToResultOkAsync();

            result.ShouldBeSuccessWithValue(value);
        }
    }
}