using System.Threading.Tasks;

using Futurum.Core.Result;
using Futurum.Core.Tests.Helper.Result;

using Xunit;

namespace Futurum.Core.Tests.Result;

public class ResultEnumerableExtensionsTryFirstTests
{
    private const string ErrorMessage = "ERROR_MESSAGE";

    public class Sync
    {
        public class StringErrorMessage
        {
            [Fact]
            public void FirstIsSuccess()
            {
                var input = new[] { 1, 2, 3 };

                var result = input.TryFirst(x => x.ToString().ToResultOk(),
                                            ErrorMessage);

                result.ShouldBeSuccessWithValue(1.ToString());
            }

            [Fact]
            public void FirstOfThreeIsFailure()
            {
                var input = new[] { 1, 2, 3 };

                var result = input.TryFirst(x =>
                                            {
                                                if (x == 1)
                                                {
                                                    return Core.Result.Result.Fail<string>(ErrorMessage);
                                                }

                                                return x.ToString().ToResultOk();
                                            },
                                            ErrorMessage);

                result.ShouldBeSuccessWithValue(2.ToString());
            }

            [Fact]
            public void FirstAndSecondOfThreeIsFailure()
            {
                var input = new[] { 1, 2, 3 };

                var result = input.TryFirst(x =>
                                            {
                                                if (x is 1 or 2)
                                                {
                                                    return Core.Result.Result.Fail<string>(ErrorMessage);
                                                }

                                                return x.ToString().ToResultOk();
                                            },
                                            ErrorMessage);

                result.ShouldBeSuccessWithValue(3.ToString());
            }

            [Fact]
            public void AllFailure()
            {
                var input = new[] { 1, 2, 3 };

                var result = input.TryFirst(x => Core.Result.Result.Fail<string>(ErrorMessage),
                                            ErrorMessage);

                result.ShouldBeFailureWithError(ErrorMessage);
            }
        }

        public class IResultError
        {
            [Fact]
            public void FirstIsSuccess()
            {
                var input = new[] { 1, 2, 3 };

                var result = input.TryFirst(x => x.ToString().ToResultOk(),
                                            ErrorMessage.ToResultError());

                result.ShouldBeSuccessWithValue(1.ToString());
            }

            [Fact]
            public void FirstOfThreeIsFailure()
            {
                var input = new[] { 1, 2, 3 };

                var result = input.TryFirst(x =>
                                            {
                                                if (x == 1)
                                                {
                                                    return Core.Result.Result.Fail<string>(ErrorMessage);
                                                }

                                                return x.ToString().ToResultOk();
                                            },
                                            ErrorMessage.ToResultError());

                result.ShouldBeSuccessWithValue(2.ToString());
            }

            [Fact]
            public void FirstAndSecondOfThreeIsFailure()
            {
                var input = new[] { 1, 2, 3 };

                var result = input.TryFirst(x =>
                                            {
                                                if (x is 1 or 2)
                                                {
                                                    return Core.Result.Result.Fail<string>(ErrorMessage);
                                                }

                                                return x.ToString().ToResultOk();
                                            },
                                            ErrorMessage.ToResultError());

                result.ShouldBeSuccessWithValue(3.ToString());
            }

            [Fact]
            public void AllFailure()
            {
                var input = new[] { 1, 2, 3 };

                var result = input.TryFirst(x => Core.Result.Result.Fail<string>(ErrorMessage),
                                            ErrorMessage.ToResultError());

                result.ShouldBeFailureWithError(ErrorMessage);
            }
        }
    }

    public class Async
    {
        public class StringErrorMessage
        {
            [Fact]
            public async Task FirstIsSuccess()
            {
                var input = new[] { 1, 2, 3 };

                var result = await input.TryFirstAsync(x => x.ToString().ToResultOkAsync(),
                                                       ErrorMessage);

                result.ShouldBeSuccessWithValue(1.ToString());
            }

            [Fact]
            public async Task FirstOfThreeIsFailure()
            {
                var input = new[] { 1, 2, 3 };

                var result = await input.TryFirstAsync(x =>
                                                       {
                                                           if (x == 1)
                                                           {
                                                               return Core.Result.Result.FailAsync<string>(ErrorMessage);
                                                           }

                                                           return x.ToString().ToResultOkAsync();
                                                       },
                                                       ErrorMessage);

                result.ShouldBeSuccessWithValue(2.ToString());
            }

            [Fact]
            public async Task FirstAndSecondOfThreeIsFailure()
            {
                var input = new[] { 1, 2, 3 };

                var result = await input.TryFirstAsync(x =>
                                                       {
                                                           if (x is 1 or 2)
                                                           {
                                                               return Core.Result.Result.FailAsync<string>(ErrorMessage);
                                                           }

                                                           return x.ToString().ToResultOkAsync();
                                                       },
                                                       ErrorMessage);

                result.ShouldBeSuccessWithValue(3.ToString());
            }

            [Fact]
            public async Task AllFailure()
            {
                var input = new[] { 1, 2, 3 };

                var result = await input.TryFirstAsync(x => Core.Result.Result.FailAsync<string>(ErrorMessage),
                                                       ErrorMessage);

                result.ShouldBeFailureWithError(ErrorMessage);
            }
        }

        public class IResultError
        {
            [Fact]
            public async Task FirstIsSuccess()
            {
                var input = new[] { 1, 2, 3 };

                var result = await input.TryFirstAsync(x => x.ToString().ToResultOkAsync(),
                                                       ErrorMessage.ToResultError());

                result.ShouldBeSuccessWithValue(1.ToString());
            }

            [Fact]
            public async Task FirstOfThreeIsFailure()
            {
                var input = new[] { 1, 2, 3 };

                var result = await input.TryFirstAsync(x =>
                                                       {
                                                           if (x == 1)
                                                           {
                                                               return Core.Result.Result.FailAsync<string>(ErrorMessage);
                                                           }

                                                           return x.ToString().ToResultOkAsync();
                                                       },
                                                       ErrorMessage.ToResultError());

                result.ShouldBeSuccessWithValue(2.ToString());
            }

            [Fact]
            public async Task FirstAndSecondOfThreeIsFailure()
            {
                var input = new[] { 1, 2, 3 };

                var result = await input.TryFirstAsync(x =>
                                                       {
                                                           if (x is 1 or 2)
                                                           {
                                                               return Core.Result.Result.FailAsync<string>(ErrorMessage);
                                                           }

                                                           return x.ToString().ToResultOkAsync();
                                                       },
                                                       ErrorMessage.ToResultError());

                result.ShouldBeSuccessWithValue(3.ToString());
            }

            [Fact]
            public async Task AllFailure()
            {
                var input = new[] { 1, 2, 3 };

                var result = await input.TryFirstAsync(x => Core.Result.Result.FailAsync<string>(ErrorMessage),
                                                       ErrorMessage.ToResultError());

                result.ShouldBeFailureWithError(ErrorMessage);
            }
        }
    }
}