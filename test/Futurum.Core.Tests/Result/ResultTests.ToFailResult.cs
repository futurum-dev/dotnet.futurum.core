using System;
using System.Threading.Tasks;

using Futurum.Core.Result;
using Futurum.Core.Tests.Helper.Result;

using Xunit;

namespace Futurum.Core.Tests.Result;

public class ResultExtensionsToFailResultTests
{
    public class Sync
    {
        public class string_error
        {
            [Fact]
            public void non_generic()
            {
                var errorMessage = Guid.NewGuid().ToString();

                var result = errorMessage.ToFailResult();

                result.ShouldBeFailureWithError(errorMessage);
            }

            [Fact]
            public void generic()
            {
                var errorMessage = Guid.NewGuid().ToString();

                var result = errorMessage.ToFailResult<int>();

                result.ShouldBeFailureWithError(errorMessage);
            }
        }
        public class result_error
        {
            [Fact]
            public void non_generic()
            {
                var errorMessage = Guid.NewGuid().ToString();
                var resultError = errorMessage.ToResultError();

                var result = resultError.ToFailResult();

                result.ShouldBeFailureWithError(errorMessage);
            }

            [Fact]
            public void generic()
            {
                var errorMessage = Guid.NewGuid().ToString();
                var resultError = errorMessage.ToResultError();

                var result = resultError.ToFailResult<int>();

                result.ShouldBeFailureWithError(errorMessage);
            }
        }
    }

    public class Async
    {
        public class string_error
        {
            [Fact]
            public async Task non_generic()
            {
                var errorMessage = Guid.NewGuid().ToString();

                var result = await errorMessage.ToFailResultAsync();

                result.ShouldBeFailureWithError(errorMessage);
            }

            [Fact]
            public async Task generic()
            {
                var errorMessage = Guid.NewGuid().ToString();

                var result = await errorMessage.ToFailResultAsync<int>();

                result.ShouldBeFailureWithError(errorMessage);
            }
        }
            
        public class result_error
        {
            [Fact]
            public async Task non_generic()
            {
                var errorMessage = Guid.NewGuid().ToString();
                var resultError = errorMessage.ToResultError();

                var result = await resultError.ToFailResultAsync();

                result.ShouldBeFailureWithError(errorMessage);
            }

            [Fact]
            public async Task generic()
            {
                var errorMessage = Guid.NewGuid().ToString();
                var resultError = errorMessage.ToResultError();

                var result = await resultError.ToFailResultAsync<int>();

                result.ShouldBeFailureWithError(errorMessage);
            }
        }
    }
}