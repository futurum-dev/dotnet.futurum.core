using System.Threading.Tasks;

using Futurum.Core.Result;
using Futurum.Core.Tests.Helper.Result;

using Xunit;

namespace Futurum.Core.Tests.Result;

public class ResultFactoryTests
{
    private const string ErrorMessage1 = "ERROR_MESSAGE1";

    private class StubClass
    {
    }

    public class NonGeneric
    {
        public class Sync
        {
            public class Fail
            {
                [Fact]
                public void IResultError()
                {
                    var result = Core.Result.Result.Fail(ErrorMessage1.ToResultError());

                    result.ShouldBeFailureWithError(ErrorMessage1);
                }

                [Fact]
                public void StringErrorMessage()
                {
                    var result = Core.Result.Result.Fail(ErrorMessage1);

                    result.ShouldBeFailureWithError(ErrorMessage1);
                }
            }

            [Fact]
            public void Ok()
            {
                var result = Core.Result.Result.Ok();

                result.ShouldBeSuccess();
            }
        }

        public class Async
        {
            public class Fail
            {
                [Fact]
                public async Task IResultError()
                {
                    var result = await Core.Result.Result.FailAsync(ErrorMessage1.ToResultError());

                    result.ShouldBeFailureWithError(ErrorMessage1);
                }

                [Fact]
                public async Task StringErrorMessage()
                {
                    var result = await Core.Result.Result.FailAsync(ErrorMessage1);

                    result.ShouldBeFailureWithError(ErrorMessage1);
                }
            }

            [Fact]
            public async Task OkAsync()
            {
                var result = await Core.Result.Result.OkAsync();

                result.ShouldBeSuccess();
            }
        }
    }

    public class Generic
    {
        public class Sync
        {
            public class Fail
            {
                [Fact]
                public void IResultError()
                {
                    var result = Core.Result.Result.Fail<StubClass>(ErrorMessage1.ToResultError());

                    result.ShouldBeFailureWithError(ErrorMessage1);
                }

                [Fact]
                public void StringErrorMessage()
                {
                    var result = Core.Result.Result.Fail<StubClass>(ErrorMessage1);

                    result.ShouldBeFailureWithError(ErrorMessage1);
                }
            }

            [Fact]
            public void Ok()
            {
                var stubClass = new StubClass();

                var result = Core.Result.Result.Ok(stubClass);

                result.ShouldBeSuccessWithValue(stubClass);
            }
        }

        public class Async
        {
            public class Fail
            {
                [Fact]
                public async Task IResultError()
                {
                    var result = await Core.Result.Result.FailAsync<StubClass>(ErrorMessage1.ToResultError());

                    result.ShouldBeFailureWithError(ErrorMessage1);
                }

                [Fact]
                public async Task StringErrorMessage()
                {
                    var result = await Core.Result.Result.FailAsync<StubClass>(ErrorMessage1);

                    result.ShouldBeFailureWithError(ErrorMessage1);
                }
            }

            [Fact]
            public async Task OkAsync()
            {
                var stubClass = new StubClass();

                var result = await Core.Result.Result.OkAsync(stubClass);

                result.ShouldBeSuccessWithValue(stubClass);
            }
        }
    }
}