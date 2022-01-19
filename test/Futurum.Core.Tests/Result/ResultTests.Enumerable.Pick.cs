using Futurum.Core.Result;
using Futurum.Core.Tests.Helper.Result;

using Xunit;

namespace Futurum.Core.Tests.Result;

public class ResultEnumerableExtensionsPickTests
{
    private const string ErrorMessage = "ERROR_MESSAGE";

    public class WithoutSelectorFunc
    {
        public class StringErrorMessage
        {
            [Fact]
            public void AllFailure()
            {
                var result1 = Core.Result.Result.Fail<int>("Failure 1");
                var result2 = Core.Result.Result.Fail<int>("Failure 2");
                var result3 = Core.Result.Result.Fail<int>("Failure 3");

                var result = new[] {result1, result2, result3}.Pick(ErrorMessage);

                result.ShouldBeFailureWithError(ErrorMessage);
            }

            [Fact]
            public void AllSuccess()
            {
                var result1 = Core.Result.Result.Ok(1);
                var result2 = Core.Result.Result.Ok(2);
                var result3 = Core.Result.Result.Ok(3);

                var result = new[] {result1, result2, result3}.Pick(ErrorMessage);

                result.ShouldBeSuccessWithValue(result1.Value);
            }

            [Fact]
            public void Mixed()
            {
                var result1 = Core.Result.Result.Ok(1);
                var result2 = Core.Result.Result.Fail<int>("Failure 1");
                var result3 = Core.Result.Result.Fail<int>("Failure 2");

                var result = new[] {result1, result2, result3}.Pick(ErrorMessage);

                result.ShouldBeSuccessWithValue(result1.Value);
            }
        }

        public class IResultError
        {
            [Fact]
            public void AllFailure()
            {
                var result1 = Core.Result.Result.Fail<int>("Failure 1");
                var result2 = Core.Result.Result.Fail<int>("Failure 2");
                var result3 = Core.Result.Result.Fail<int>("Failure 3");

                var result = new[] {result1, result2, result3}.Pick(ErrorMessage.ToResultError());

                result.ShouldBeFailureWithError(ErrorMessage);
            }

            [Fact]
            public void AllSuccess()
            {
                var result1 = Core.Result.Result.Ok(1);
                var result2 = Core.Result.Result.Ok(2);
                var result3 = Core.Result.Result.Ok(3);

                var result = new[] {result1, result2, result3}.Pick(ErrorMessage.ToResultError());

                result.ShouldBeSuccessWithValue(result1.Value);
            }

            [Fact]
            public void Mixed()
            {
                var result1 = Core.Result.Result.Ok(1);
                var result2 = Core.Result.Result.Fail<int>("Failure 1");
                var result3 = Core.Result.Result.Fail<int>("Failure 2");

                var result = new[] {result1, result2, result3}.Pick(ErrorMessage.ToResultError());

                result.ShouldBeSuccessWithValue(result1.Value);
            }
        }
    }

    public class WithSelector
    {
        public class StringErrorMessage
        {
            [Fact]
            public void AllFailure()
            {
                var result = new[] {1, 2, 3}.Pick(x => Core.Result.Result.Fail<int>(ErrorMessage),
                                                  ErrorMessage);

                result.ShouldBeFailureWithError(ErrorMessage);
            }

            [Fact]
            public void AllSuccess()
            {
                var result = new[] {1, 2, 3}.Pick(x => Core.Result.Result.Ok(x * 2),
                                                  ErrorMessage);

                result.ShouldBeSuccessWithValue(2);
            }

            [Fact]
            public void Mixed()
            {
                var result = new[] {1, 2, 3}.Pick(x => x == 1
                                                      ? Core.Result.Result.Ok(x * 2)
                                                      : Core.Result.Result.Fail<int>(ErrorMessage),
                                                  ErrorMessage);

                result.ShouldBeSuccessWithValue(2);
            }
        }

        public class IResultError
        {
            [Fact]
            public void AllFailure()
            {
                var result = new[] {1, 2, 3}.Pick(x => Core.Result.Result.Fail<int>(ErrorMessage),
                                                  ErrorMessage.ToResultError());

                result.ShouldBeFailureWithError(ErrorMessage);
            }

            [Fact]
            public void AllSuccess()
            {
                var result = new[] {1, 2, 3}.Pick(x => Core.Result.Result.Ok(x * 2),
                                                  ErrorMessage.ToResultError());

                result.ShouldBeSuccessWithValue(2);
            }

            [Fact]
            public void Mixed()
            {
                var result = new[] {1, 2, 3}.Pick(x => x == 1
                                                      ? Core.Result.Result.Ok(x * 2)
                                                      : Core.Result.Result.Fail<int>(ErrorMessage),
                                                  ErrorMessage.ToResultError());

                result.ShouldBeSuccessWithValue(2);
            }
        }
    }
}