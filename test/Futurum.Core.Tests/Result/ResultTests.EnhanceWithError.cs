using System.Threading.Tasks;

using Futurum.Core.Result;
using Futurum.Core.Tests.Helper.Result;

using Xunit;

namespace Futurum.Core.Tests.Result;

public class ResultEnhanceWithErrorTests
{
    private const string ErrorMessage1 = "ERROR_MESSAGE_1";
    private const string ErrorMessage2 = "ERROR_MESSAGE_2";

    public class Sync
    {
        public class NonGenericResultWith
        {
            public class ErrorMessage
            {
                [Fact]
                public void Failure()
                {
                    var resultInput = Core.Result.Result.Fail(ErrorMessage1);

                    var result = resultInput.EnhanceWithError(() => ErrorMessage2);

                    result.ShouldBeFailureWithError(ErrorMessage2, ErrorMessage1);
                }

                [Fact]
                public void Success()
                {
                    var resultInput = Core.Result.Result.Ok();

                    var result = resultInput.EnhanceWithError(() => ErrorMessage1);

                    result.ShouldBeSuccess();
                }
            }

            public class ResultError
            {
                [Fact]
                public void Failure()
                {
                    var resultInput = Core.Result.Result.Fail(ErrorMessage1);

                    var result = resultInput.EnhanceWithError(() => ErrorMessage2.ToResultError());

                    result.ShouldBeFailureWithError(ErrorMessage2, ErrorMessage1);
                }

                [Fact]
                public void Success()
                {
                    var resultInput = Core.Result.Result.Ok();

                    var result = resultInput.EnhanceWithError(() => ErrorMessage1.ToResultError());

                    result.ShouldBeSuccess();
                }
            }
        }

        public class GenericResultWith
        {
            public class ErrorMessage
            {
                [Fact]
                public void Failure()
                {
                    var resultInput = Core.Result.Result.Fail<int>(ErrorMessage1);

                    var result = resultInput.EnhanceWithError(() => ErrorMessage2);

                    result.ShouldBeFailureWithError(ErrorMessage2, ErrorMessage1);
                }

                [Fact]
                public void Success()
                {
                    var input = 1;

                    var resultInput = Core.Result.Result.Ok(input);

                    var result = resultInput.EnhanceWithError(() => ErrorMessage1);

                    result.ShouldBeSuccessWithValue(input);
                }
            }

            public class ResultError
            {
                [Fact]
                public void Failure()
                {
                    var resultInput = Core.Result.Result.Fail<int>(ErrorMessage1);

                    var result = resultInput.EnhanceWithError(() => ErrorMessage2.ToResultError());

                    result.ShouldBeFailureWithError(ErrorMessage2, ErrorMessage1);
                }

                [Fact]
                public void Success()
                {
                    var input = 1;

                    var resultInput = Core.Result.Result.Ok(input);

                    var result = resultInput.EnhanceWithError(() => ErrorMessage1.ToResultError());

                    result.ShouldBeSuccessWithValue(input);
                }
            }
        }
    }

    public class Async
    {
        public class NonGenericResultWith
        {
            public class ErrorMessage
            {
                [Fact]
                public async Task Failure()
                {
                    var resultInput = Core.Result.Result.FailAsync(ErrorMessage1);

                    var result = await resultInput.EnhanceWithErrorAsync(() => ErrorMessage2);

                    result.ShouldBeFailureWithError(ErrorMessage2, ErrorMessage1);
                }

                [Fact]
                public async Task Success()
                {
                    var resultInput = Core.Result.Result.OkAsync();

                    var result = await resultInput.EnhanceWithErrorAsync(() => ErrorMessage1);

                    result.ShouldBeSuccess();
                }
            }

            public class ResultError
            {
                [Fact]
                public async Task Failure()
                {
                    var resultInput = Core.Result.Result.FailAsync(ErrorMessage1);

                    var result = await resultInput.EnhanceWithErrorAsync(() => ErrorMessage2.ToResultError());

                    result.ShouldBeFailureWithError(ErrorMessage2, ErrorMessage1);
                }

                [Fact]
                public async Task Success()
                {
                    var resultInput = Core.Result.Result.OkAsync();

                    var result = await resultInput.EnhanceWithErrorAsync(() => ErrorMessage1.ToResultError());

                    result.ShouldBeSuccess();
                }
            }
        }

        public class GenericResultWith
        {
            public class ErrorMessage
            {
                [Fact]
                public async Task Failure()
                {
                    var resultInput = Core.Result.Result.FailAsync<int>(ErrorMessage1);

                    var result = await resultInput.EnhanceWithErrorAsync(() => ErrorMessage2);

                    result.ShouldBeFailureWithError(ErrorMessage2, ErrorMessage1);
                }

                [Fact]
                public async Task Success()
                {
                    var input = 1;

                    var resultInput = Core.Result.Result.OkAsync(input);

                    var result = await resultInput.EnhanceWithErrorAsync(() => ErrorMessage1);

                    result.ShouldBeSuccessWithValue(input);
                }
            }

            public class ResultError
            {
                [Fact]
                public async Task Failure()
                {
                    var resultInput = Core.Result.Result.FailAsync<int>(ErrorMessage1);

                    var result = await resultInput.EnhanceWithErrorAsync(() => ErrorMessage2.ToResultError());

                    result.ShouldBeFailureWithError(ErrorMessage2, ErrorMessage1);
                }

                [Fact]
                public async Task Success()
                {
                    var input = 1;

                    var resultInput = Core.Result.Result.OkAsync(input);

                    var result = await resultInput.EnhanceWithErrorAsync(() => ErrorMessage1.ToResultError());

                    result.ShouldBeSuccessWithValue(input);
                }
            }
        }
    }
}