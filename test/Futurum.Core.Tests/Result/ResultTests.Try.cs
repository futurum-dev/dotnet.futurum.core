using System;
using System.Threading.Tasks;

using Futurum.Core.Result;
using Futurum.Core.Tests.Helper.Result;

using Xunit;

namespace Futurum.Core.Tests.Result;

public class ResultTryTests
{
    private const string ErrorMessage1 = "ERROR_MESSAGE1";
    private const string ErrorMessage2 = "ERROR_MESSAGE2";

    public class Sync
    {
        public class NoReturn
        {
            public class ErrorMessage
            {
                [Fact]
                public void Exception()
                {
                    Action action = () => throw new Exception(ErrorMessage1);
                    var result = Core.Result.Result.Try(action, ErrorMessage2);

                    result.ShouldBeFailureWithErrorSafe($"{ErrorMessage2};{ErrorMessage1}");
                }

                [Fact]
                public void Success()
                {
                    var result = Core.Result.Result.Try(() => {},
                                                        ErrorMessage2);

                    result.ShouldBeSuccess();
                }
            }

            public class FuncErrorMessage
            {
                [Fact]
                public void Exception()
                {
                    Action action = () => throw new Exception(ErrorMessage1);
                    var result = Core.Result.Result.Try(action, () => ErrorMessage2);

                    result.ShouldBeFailureWithErrorSafe($"{ErrorMessage2};{ErrorMessage1}");
                }

                [Fact]
                public void Success()
                {
                    var result = Core.Result.Result.Try(() => {},
                                                        () => ErrorMessage2);

                    result.ShouldBeSuccess();
                }
            }

            public class IResultError
            {
                [Fact]
                public void Exception()
                {
                    Action action = () => throw new Exception(ErrorMessage1);
                    var result = Core.Result.Result.Try(action, ErrorMessage2.ToResultError());

                    result.ShouldBeFailureWithErrorSafe($"{ErrorMessage2};{ErrorMessage1}");
                }

                [Fact]
                public void Success()
                {
                    var result = Core.Result.Result.Try(() => {},
                                                        ErrorMessage2.ToResultError());

                    result.ShouldBeSuccess();
                }
            }

            public class FuncIResultError
            {
                [Fact]
                public void Exception()
                {
                    Action action = () => throw new Exception(ErrorMessage1);
                    var result = Core.Result.Result.Try(action, () => ErrorMessage2.ToResultError());

                    result.ShouldBeFailureWithErrorSafe($"{ErrorMessage2};{ErrorMessage1}");
                }

                [Fact]
                public void Success()
                {
                    var result = Core.Result.Result.Try(() => {},
                                                        () => ErrorMessage2.ToResultError());

                    result.ShouldBeSuccess();
                }
            }
        }

        public class PayloadReturn
        {
            public class ErrorMessage
            {
                [Fact]
                public void Exception()
                {
                    var value = 1;

                    var result = Core.Result.Result.Try(() =>
                                                        {
                                                            throw new Exception(ErrorMessage1);

                                                            return value;
                                                        },
                                                        ErrorMessage2);

                    result.ShouldBeFailureWithErrorSafe($"{ErrorMessage2};{ErrorMessage1}");
                }

                [Fact]
                public void Success()
                {
                    var value = 1;

                    var result = Core.Result.Result.Try(() => value,
                                                        ErrorMessage2);

                    result.ShouldBeSuccessWithValue(value);
                }
            }

            public class FuncErrorMessage
            {
                [Fact]
                public void Exception()
                {
                    var value = 1;

                    var result = Core.Result.Result.Try(() =>
                                                        {
                                                            throw new Exception(ErrorMessage1);

                                                            return value;
                                                        },
                                                        () => ErrorMessage2);

                    result.ShouldBeFailureWithErrorSafe($"{ErrorMessage2};{ErrorMessage1}");
                }

                [Fact]
                public void Success()
                {
                    var value = 1;

                    var result = Core.Result.Result.Try(() => value,
                                                        () => ErrorMessage2);

                    result.ShouldBeSuccessWithValue(value);
                }
            }

            public class IResultError
            {
                [Fact]
                public void Exception()
                {
                    var value = 1;

                    var result = Core.Result.Result.Try(() =>
                                                        {
                                                            throw new Exception(ErrorMessage1);

                                                            return value;
                                                        },
                                                        ErrorMessage2.ToResultError());

                    result.ShouldBeFailureWithErrorSafe($"{ErrorMessage2};{ErrorMessage1}");
                }

                [Fact]
                public void Success()
                {
                    var value = 1;

                    var result = Core.Result.Result.Try(() => value,
                                                        ErrorMessage2.ToResultError());

                    result.ShouldBeSuccessWithValue(value);
                }
            }

            public class FuncIResultError
            {
                [Fact]
                public void Exception()
                {
                    var value = 1;

                    var result = Core.Result.Result.Try(() =>
                                                        {
                                                            throw new Exception(ErrorMessage1);

                                                            return value;
                                                        },
                                                        () => ErrorMessage2.ToResultError());

                    result.ShouldBeFailureWithErrorSafe($"{ErrorMessage2};{ErrorMessage1}");
                }

                [Fact]
                public void Success()
                {
                    var value = 1;

                    var result = Core.Result.Result.Try(() => value,
                                                        () => ErrorMessage2.ToResultError());

                    result.ShouldBeSuccessWithValue(value);
                }
            }
        }

        public class ResultReturn
        {
            public class ErrorMessage
            {
                [Fact]
                public void Exception()
                {
                    var result = Core.Result.Result.Try(() =>
                                                        {
                                                            throw new Exception(ErrorMessage1);

                                                            return Core.Result.Result.Ok();
                                                        },
                                                        ErrorMessage2);

                    result.ShouldBeFailureWithErrorSafe($"{ErrorMessage2};{ErrorMessage1}");
                }

                [Fact]
                public void Failure()
                {
                    var result = Core.Result.Result.Try(() => Core.Result.Result.Fail(ErrorMessage1),
                                                        ErrorMessage2);

                    result.ShouldBeFailureWithError($"{ErrorMessage2};{ErrorMessage1}");
                }

                [Fact]
                public void Success()
                {
                    var result = Core.Result.Result.Try(() => Core.Result.Result.Ok(),
                                                        ErrorMessage2);

                    result.ShouldBeSuccess();
                }
            }

            public class FuncErrorMessage
            {
                [Fact]
                public void Exception()
                {
                    var result = Core.Result.Result.Try(() =>
                                                        {
                                                            throw new Exception(ErrorMessage1);

                                                            return Core.Result.Result.Ok();
                                                        },
                                                        () => ErrorMessage2);

                    result.ShouldBeFailureWithErrorSafe($"{ErrorMessage2};{ErrorMessage1}");
                }

                [Fact]
                public void Failure()
                {
                    var result = Core.Result.Result.Try(() => Core.Result.Result.Fail(ErrorMessage1),
                                                        () => ErrorMessage2);

                    result.ShouldBeFailureWithError($"{ErrorMessage2};{ErrorMessage1}");
                }

                [Fact]
                public void Success()
                {
                    var result = Core.Result.Result.Try(() => Core.Result.Result.Ok(),
                                                        () => ErrorMessage2);

                    result.ShouldBeSuccess();
                }
            }

            public class IResultError
            {
                [Fact]
                public void Exception()
                {
                    var result = Core.Result.Result.Try(() =>
                                                        {
                                                            throw new Exception(ErrorMessage1);

                                                            return Core.Result.Result.Ok();
                                                        },
                                                        ErrorMessage2.ToResultError());

                    result.ShouldBeFailureWithErrorSafe($"{ErrorMessage2};{ErrorMessage1}");
                }

                [Fact]
                public void Failure()
                {
                    var result = Core.Result.Result.Try(() => Core.Result.Result.Fail(ErrorMessage1),
                                                        ErrorMessage2.ToResultError());

                    result.ShouldBeFailureWithError($"{ErrorMessage2};{ErrorMessage1}");
                }

                [Fact]
                public void Success()
                {
                    var result = Core.Result.Result.Try(() => Core.Result.Result.Ok(),
                                                        ErrorMessage2.ToResultError());

                    result.ShouldBeSuccess();
                }
            }

            public class FuncIResultError
            {
                [Fact]
                public void Exception()
                {
                    var result = Core.Result.Result.Try(() =>
                                                        {
                                                            throw new Exception(ErrorMessage1);

                                                            return Core.Result.Result.Ok();
                                                        },
                                                        () => ErrorMessage2.ToResultError());

                    result.ShouldBeFailureWithErrorSafe($"{ErrorMessage2};{ErrorMessage1}");
                }

                [Fact]
                public void Failure()
                {
                    var result = Core.Result.Result.Try(() => Core.Result.Result.Fail(ErrorMessage1),
                                                        () => ErrorMessage2.ToResultError());

                    result.ShouldBeFailureWithError($"{ErrorMessage2};{ErrorMessage1}");
                }

                [Fact]
                public void Success()
                {
                    var result = Core.Result.Result.Try(() => Core.Result.Result.Ok(),
                                                        () => ErrorMessage2.ToResultError());

                    result.ShouldBeSuccess();
                }
            }
        }

        public class PayloadResultReturn
        {
            public class ErrorMessage
            {
                [Fact]
                public void Exception()
                {
                    var value = 1;

                    var result = Core.Result.Result.Try(() =>
                                                        {
                                                            throw new Exception(ErrorMessage1);

                                                            return Core.Result.Result.Ok(value);
                                                        },
                                                        ErrorMessage2);

                    result.ShouldBeFailureWithErrorSafe($"{ErrorMessage2};{ErrorMessage1}");
                }

                [Fact]
                public void Failure()
                {
                    var result = Core.Result.Result.Try(() => Core.Result.Result.Fail<int>(ErrorMessage1),
                                                        ErrorMessage2);

                    result.ShouldBeFailureWithError(ErrorMessage1);
                }

                [Fact]
                public void Success()
                {
                    var value = 1;

                    var result = Core.Result.Result.Try(() => Core.Result.Result.Ok(value),
                                                        () => ErrorMessage2);

                    result.ShouldBeSuccessWithValue(value);
                }
            }

            public class FuncErrorMessage
            {
                [Fact]
                public void Exception()
                {
                    var value = 1;

                    var result = Core.Result.Result.Try(() =>
                                                        {
                                                            throw new Exception(ErrorMessage1);

                                                            return Core.Result.Result.Ok(value);
                                                        },
                                                        () => ErrorMessage2);

                    result.ShouldBeFailureWithErrorSafe($"{ErrorMessage2};{ErrorMessage1}");
                }

                [Fact]
                public void Failure()
                {
                    var result = Core.Result.Result.Try(() => Core.Result.Result.Fail<int>(ErrorMessage1),
                                                        () => ErrorMessage2);

                    result.ShouldBeFailureWithError(ErrorMessage1);
                }

                [Fact]
                public void Success()
                {
                    var value = 1;

                    var result = Core.Result.Result.Try(() => Core.Result.Result.Ok(value),
                                                        () => ErrorMessage2);

                    result.ShouldBeSuccessWithValue(value);
                }
            }

            public class IResultError
            {
                [Fact]
                public void Exception()
                {
                    var value = 1;

                    var result = Core.Result.Result.Try(() =>
                                                        {
                                                            throw new Exception(ErrorMessage1);

                                                            return Core.Result.Result.Ok(value);
                                                        },
                                                        ErrorMessage2.ToResultError());

                    result.ShouldBeFailureWithErrorSafe($"{ErrorMessage2};{ErrorMessage1}");
                }

                [Fact]
                public void Failure()
                {
                    var result = Core.Result.Result.Try(() => Core.Result.Result.Fail<int>(ErrorMessage1),
                                                        ErrorMessage2.ToResultError());

                    result.ShouldBeFailureWithError(ErrorMessage1);
                }

                [Fact]
                public void Success()
                {
                    var value = 1;

                    var result = Core.Result.Result.Try(() => Core.Result.Result.Ok(value),
                                                        ErrorMessage2.ToResultError());

                    result.ShouldBeSuccessWithValue(value);
                }
            }

            public class FuncIResultError
            {
                [Fact]
                public void Exception()
                {
                    var value = 1;

                    var result = Core.Result.Result.Try(() =>
                                                        {
                                                            throw new Exception(ErrorMessage1);

                                                            return Core.Result.Result.Ok(value);
                                                        },
                                                        () => ErrorMessage2.ToResultError());

                    result.ShouldBeFailureWithErrorSafe($"{ErrorMessage2};{ErrorMessage1}");
                }

                [Fact]
                public void Failure()
                {
                    var result = Core.Result.Result.Try(() => Core.Result.Result.Fail<int>(ErrorMessage1),
                                                        () => ErrorMessage2.ToResultError());

                    result.ShouldBeFailureWithError(ErrorMessage1);
                }

                [Fact]
                public void Success()
                {
                    var value = 1;

                    var result = Core.Result.Result.Try(() => Core.Result.Result.Ok(value),
                                                        () => ErrorMessage2.ToResultError());

                    result.ShouldBeSuccessWithValue(value);
                }
            }
        }
    }

    public class Async
    {
        public class NoReturn
        {
            public class ErrorMessage
            {
                [Fact]
                public async Task Exception()
                {
                    Func<Task> func = () => throw new Exception(ErrorMessage1);
                    var result = await Core.Result.Result.TryAsync(func, ErrorMessage2);

                    result.ShouldBeFailureWithErrorSafe($"{ErrorMessage2};{ErrorMessage1}");
                }

                [Fact]
                public async Task Success()
                {
                    var result = await Core.Result.Result.TryAsync(() => Task.CompletedTask,
                                                                   ErrorMessage2);

                    result.ShouldBeSuccess();
                }
            }

            public class FuncErrorMessage
            {
                [Fact]
                public async Task Exception()
                {
                    Func<Task> func = () => throw new Exception(ErrorMessage1);
                    var result = await Core.Result.Result.TryAsync(func, () => ErrorMessage2);

                    result.ShouldBeFailureWithErrorSafe($"{ErrorMessage2};{ErrorMessage1}");
                }

                [Fact]
                public async Task Success()
                {
                    var result = await Core.Result.Result.TryAsync(() => Task.CompletedTask,
                                                                   () => ErrorMessage2);

                    result.ShouldBeSuccess();
                }
            }

            public class IResultError
            {
                [Fact]
                public async Task Exception()
                {
                    Func<Task> func = () => throw new Exception(ErrorMessage1);
                    var result = await Core.Result.Result.TryAsync(func, ErrorMessage2.ToResultError());

                    result.ShouldBeFailureWithErrorSafe($"{ErrorMessage2};{ErrorMessage1}");
                }

                [Fact]
                public async Task Success()
                {
                    var result = await Core.Result.Result.TryAsync(() => Task.CompletedTask,
                                                                   ErrorMessage2.ToResultError());

                    result.ShouldBeSuccess();
                }
            }

            public class FuncIResultError
            {
                [Fact]
                public async Task Exception()
                {
                    Func<Task> func = () => throw new Exception(ErrorMessage1);
                    var result = await Core.Result.Result.TryAsync(func, () => ErrorMessage2.ToResultError());

                    result.ShouldBeFailureWithErrorSafe($"{ErrorMessage2};{ErrorMessage1}");
                }

                [Fact]
                public async Task Success()
                {
                    var result = await Core.Result.Result.TryAsync(() => Task.CompletedTask,
                                                                   () => ErrorMessage2.ToResultError());

                    result.ShouldBeSuccess();
                }
            }
        }

        public class PayloadReturn
        {
            public class ErrorMessage
            {
                [Fact]
                public async Task Exception()
                {
                    var value = 1;

                    var result = await Core.Result.Result.TryAsync(() =>
                                                                   {
                                                                       throw new Exception(ErrorMessage1);

                                                                       return Task.FromResult(value);
                                                                   },
                                                                   ErrorMessage2);

                    result.ShouldBeFailureWithErrorSafe($"{ErrorMessage2};{ErrorMessage1}");
                }

                [Fact]
                public async Task Success()
                {
                    var value = 1;

                    var result = await Core.Result.Result.TryAsync(() => Task.FromResult(value),
                                                                   ErrorMessage2);

                    result.ShouldBeSuccessWithValue(value);
                }
            }

            public class FuncErrorMessage
            {
                [Fact]
                public async Task Exception()
                {
                    var value = 1;

                    var result = await Core.Result.Result.TryAsync(() =>
                                                                   {
                                                                       throw new Exception(ErrorMessage1);

                                                                       return Task.FromResult(value);
                                                                   },
                                                                   () => ErrorMessage2);

                    result.ShouldBeFailureWithErrorSafe($"{ErrorMessage2};{ErrorMessage1}");
                }

                [Fact]
                public async Task Success()
                {
                    var value = 1;

                    var result = await Core.Result.Result.TryAsync(() => Task.FromResult(value),
                                                                   () => ErrorMessage2);

                    result.ShouldBeSuccessWithValue(value);
                }
            }

            public class IResultError
            {
                [Fact]
                public async Task Exception()
                {
                    var value = 1;

                    var result = await Core.Result.Result.TryAsync(() =>
                                                                   {
                                                                       throw new Exception(ErrorMessage1);

                                                                       return Task.FromResult(value);
                                                                   },
                                                                   ErrorMessage2.ToResultError());

                    result.ShouldBeFailureWithErrorSafe($"{ErrorMessage2};{ErrorMessage1}");
                }

                [Fact]
                public async Task Success()
                {
                    var value = 1;

                    var result = await Core.Result.Result.TryAsync(() => Task.FromResult(value),
                                                                   ErrorMessage2.ToResultError());

                    result.ShouldBeSuccessWithValue(value);
                }
            }

            public class FuncIResultError
            {
                [Fact]
                public async Task Exception()
                {
                    var value = 1;

                    var result = await Core.Result.Result.TryAsync(() =>
                                                                   {
                                                                       throw new Exception(ErrorMessage1);

                                                                       return Task.FromResult(value);
                                                                   },
                                                                   () => ErrorMessage2.ToResultError());

                    result.ShouldBeFailureWithErrorSafe($"{ErrorMessage2};{ErrorMessage1}");
                }

                [Fact]
                public async Task Success()
                {
                    var value = 1;

                    var result = await Core.Result.Result.TryAsync(() => Task.FromResult(value),
                                                                   () => ErrorMessage2.ToResultError());

                    result.ShouldBeSuccessWithValue(value);
                }
            }
        }

        public class ValueTaskPayloadReturn
        {
            public class ErrorMessage
            {
                [Fact]
                public async Task Exception()
                {
                    var value = 1;

                    var result = await Core.Result.Result.TryAsync(() =>
                                                                   {
                                                                       throw new Exception(ErrorMessage1);

                                                                       return ValueTask.FromResult(value);
                                                                   },
                                                                   ErrorMessage2);

                    result.ShouldBeFailureWithErrorSafe($"{ErrorMessage2};{ErrorMessage1}");
                }

                [Fact]
                public async Task Success()
                {
                    var value = 1;

                    var result = await Core.Result.Result.TryAsync(() => ValueTask.FromResult(value),
                                                                   ErrorMessage2);

                    result.ShouldBeSuccessWithValue(value);
                }
            }

            public class FuncErrorMessage
            {
                [Fact]
                public async Task Exception()
                {
                    var value = 1;

                    var result = await Core.Result.Result.TryAsync(() =>
                                                                   {
                                                                       throw new Exception(ErrorMessage1);

                                                                       return ValueTask.FromResult(value);
                                                                   },
                                                                   () => ErrorMessage2);

                    result.ShouldBeFailureWithErrorSafe($"{ErrorMessage2};{ErrorMessage1}");
                }

                [Fact]
                public async Task Success()
                {
                    var value = 1;

                    var result = await Core.Result.Result.TryAsync(() => ValueTask.FromResult(value),
                                                                   () => ErrorMessage2);

                    result.ShouldBeSuccessWithValue(value);
                }
            }

            public class IResultError
            {
                [Fact]
                public async Task Exception()
                {
                    var value = 1;

                    var result = await Core.Result.Result.TryAsync(() =>
                                                                   {
                                                                       throw new Exception(ErrorMessage1);

                                                                       return ValueTask.FromResult(value);
                                                                   },
                                                                   ErrorMessage2.ToResultError());

                    result.ShouldBeFailureWithErrorSafe($"{ErrorMessage2};{ErrorMessage1}");
                }

                [Fact]
                public async Task Success()
                {
                    var value = 1;

                    var result = await Core.Result.Result.TryAsync(() => ValueTask.FromResult(value),
                                                                   ErrorMessage2.ToResultError());

                    result.ShouldBeSuccessWithValue(value);
                }
            }

            public class FuncIResultError
            {
                [Fact]
                public async Task Exception()
                {
                    var value = 1;

                    var result = await Core.Result.Result.TryAsync(() =>
                                                                   {
                                                                       throw new Exception(ErrorMessage1);

                                                                       return ValueTask.FromResult(value);
                                                                   },
                                                                   () => ErrorMessage2.ToResultError());

                    result.ShouldBeFailureWithErrorSafe($"{ErrorMessage2};{ErrorMessage1}");
                }

                [Fact]
                public async Task Success()
                {
                    var value = 1;

                    var result = await Core.Result.Result.TryAsync(() => ValueTask.FromResult(value),
                                                                   () => ErrorMessage2.ToResultError());

                    result.ShouldBeSuccessWithValue(value);
                }
            }
        }

        public class ResultReturn
        {
            public class ErrorMessage
            {
                [Fact]
                public async Task Exception()
                {
                    var result = await Core.Result.Result.TryAsync(() =>
                                                                   {
                                                                       throw new Exception(ErrorMessage1);

                                                                       return Core.Result.Result.OkAsync();
                                                                   },
                                                                   ErrorMessage2);

                    result.ShouldBeFailureWithErrorSafe($"{ErrorMessage2};{ErrorMessage1}");
                }

                [Fact]
                public async Task Failure()
                {
                    var result = await Core.Result.Result.TryAsync(() => Core.Result.Result.FailAsync(ErrorMessage1),
                                                                   ErrorMessage2);

                    result.ShouldBeFailureWithError($"{ErrorMessage2};{ErrorMessage1}");
                }

                [Fact]
                public async Task Success()
                {
                    var result = await Core.Result.Result.TryAsync(() => Core.Result.Result.OkAsync(),
                                                                   ErrorMessage2);

                    result.ShouldBeSuccess();
                }
            }

            public class FuncErrorMessage
            {
                [Fact]
                public async Task Exception()
                {
                    var result = await Core.Result.Result.TryAsync(() =>
                                                                   {
                                                                       throw new Exception(ErrorMessage1);

                                                                       return Core.Result.Result.OkAsync();
                                                                   },
                                                                   () => ErrorMessage2);

                    result.ShouldBeFailureWithErrorSafe($"{ErrorMessage2};{ErrorMessage1}");
                }

                [Fact]
                public async Task Failure()
                {
                    var result = await Core.Result.Result.TryAsync(() => Core.Result.Result.FailAsync(ErrorMessage1),
                                                                   () => ErrorMessage2);

                    result.ShouldBeFailureWithError($"{ErrorMessage2};{ErrorMessage1}");
                }

                [Fact]
                public async Task Success()
                {
                    var result = await Core.Result.Result.TryAsync(() => Core.Result.Result.OkAsync(),
                                                                   () => ErrorMessage2);

                    result.ShouldBeSuccess();
                }
            }

            public class IResultError
            {
                [Fact]
                public async Task Exception()
                {
                    var result = await Core.Result.Result.TryAsync(() =>
                                                                   {
                                                                       throw new Exception(ErrorMessage1);

                                                                       return Core.Result.Result.OkAsync();
                                                                   },
                                                                   ErrorMessage2.ToResultError());

                    result.ShouldBeFailureWithErrorSafe($"{ErrorMessage2};{ErrorMessage1}");
                }

                [Fact]
                public async Task Failure()
                {
                    var result = await Core.Result.Result.TryAsync(() => Core.Result.Result.FailAsync(ErrorMessage1),
                                                                   ErrorMessage2.ToResultError());

                    result.ShouldBeFailureWithError($"{ErrorMessage2};{ErrorMessage1}");
                }

                [Fact]
                public async Task Success()
                {
                    var result = await Core.Result.Result.TryAsync(() => Core.Result.Result.OkAsync(),
                                                                   ErrorMessage2.ToResultError());

                    result.ShouldBeSuccess();
                }
            }

            public class FuncIResultError
            {
                [Fact]
                public async Task Exception()
                {
                    var result = await Core.Result.Result.TryAsync(() =>
                                                                   {
                                                                       throw new Exception(ErrorMessage1);

                                                                       return Core.Result.Result.OkAsync();
                                                                   },
                                                                   () => ErrorMessage2.ToResultError());

                    result.ShouldBeFailureWithErrorSafe($"{ErrorMessage2};{ErrorMessage1}");
                }

                [Fact]
                public async Task Failure()
                {
                    var result = await Core.Result.Result.TryAsync(() => Core.Result.Result.FailAsync(ErrorMessage1),
                                                                   () => ErrorMessage2.ToResultError());

                    result.ShouldBeFailureWithError($"{ErrorMessage2};{ErrorMessage1}");
                }

                [Fact]
                public async Task Success()
                {
                    var result = await Core.Result.Result.TryAsync(() => Core.Result.Result.OkAsync(),
                                                                   () => ErrorMessage2.ToResultError());

                    result.ShouldBeSuccess();
                }
            }
        }

        public class PayloadResultReturn
        {
            public class ErrorMessage
            {
                [Fact]
                public async Task Exception()
                {
                    var value = 1;

                    var result = await Core.Result.Result.TryAsync(() =>
                                                                   {
                                                                       throw new Exception(ErrorMessage1);

                                                                       return Core.Result.Result.OkAsync(value);
                                                                   },
                                                                   ErrorMessage2);

                    result.ShouldBeFailureWithErrorSafe($"{ErrorMessage2};{ErrorMessage1}");
                }

                [Fact]
                public async Task Failure()
                {
                    var result = await Core.Result.Result.TryAsync(() => Core.Result.Result.FailAsync<int>(ErrorMessage1),
                                                                   ErrorMessage2);

                    result.ShouldBeFailureWithError($"{ErrorMessage2};{ErrorMessage1}");
                }

                [Fact]
                public async Task Success()
                {
                    var value = 1;

                    var result = await Core.Result.Result.TryAsync(() => Core.Result.Result.OkAsync(value),
                                                                   ErrorMessage2);

                    result.ShouldBeSuccessWithValue(value);
                }
            }

            public class FuncErrorMessage
            {
                [Fact]
                public async Task Exception()
                {
                    var value = 1;

                    var result = await Core.Result.Result.TryAsync(() =>
                                                                   {
                                                                       throw new Exception(ErrorMessage1);

                                                                       return Core.Result.Result.OkAsync(value);
                                                                   },
                                                                   () => ErrorMessage2);

                    result.ShouldBeFailureWithErrorSafe($"{ErrorMessage2};{ErrorMessage1}");
                }

                [Fact]
                public async Task Failure()
                {
                    var result = await Core.Result.Result.TryAsync(() => Core.Result.Result.FailAsync<int>(ErrorMessage1),
                                                                   () => ErrorMessage2);

                    result.ShouldBeFailureWithError($"{ErrorMessage2};{ErrorMessage1}");
                }

                [Fact]
                public async Task Success()
                {
                    var value = 1;

                    var result = await Core.Result.Result.TryAsync(() => Core.Result.Result.OkAsync(value),
                                                                   () => ErrorMessage2);

                    result.ShouldBeSuccessWithValue(value);
                }
            }

            public class IResultError
            {
                [Fact]
                public async Task Exception()
                {
                    var value = 1;

                    var result = await Core.Result.Result.TryAsync(() =>
                                                                   {
                                                                       throw new Exception(ErrorMessage1);

                                                                       return Core.Result.Result.OkAsync(value);
                                                                   },
                                                                   ErrorMessage2.ToResultError());

                    result.ShouldBeFailureWithErrorSafe($"{ErrorMessage2};{ErrorMessage1}");
                }

                [Fact]
                public async Task Failure()
                {
                    var result = await Core.Result.Result.TryAsync(() => Core.Result.Result.FailAsync<int>(ErrorMessage1),
                                                                   ErrorMessage2.ToResultError());

                    result.ShouldBeFailureWithError($"{ErrorMessage2};{ErrorMessage1}");
                }

                [Fact]
                public async Task Success()
                {
                    var value = 1;

                    var result = await Core.Result.Result.TryAsync(() => Core.Result.Result.OkAsync(value),
                                                                   ErrorMessage2.ToResultError());

                    result.ShouldBeSuccessWithValue(value);
                }
            }

            public class FuncIResultError
            {
                [Fact]
                public async Task Exception()
                {
                    var value = 1;

                    var result = await Core.Result.Result.TryAsync(() =>
                                                                   {
                                                                       throw new Exception(ErrorMessage1);

                                                                       return Core.Result.Result.OkAsync(value);
                                                                   },
                                                                   () => ErrorMessage2.ToResultError());

                    result.ShouldBeFailureWithErrorSafe($"{ErrorMessage2};{ErrorMessage1}");
                }

                [Fact]
                public async Task Failure()
                {
                    var result = await Core.Result.Result.TryAsync(() => Core.Result.Result.FailAsync<int>(ErrorMessage1),
                                                                   () => ErrorMessage2.ToResultError());

                    result.ShouldBeFailureWithError($"{ErrorMessage2};{ErrorMessage1}");
                }

                [Fact]
                public async Task Success()
                {
                    var value = 1;

                    var result = await Core.Result.Result.TryAsync(() => Core.Result.Result.OkAsync(value),
                                                                   () => ErrorMessage2.ToResultError());

                    result.ShouldBeSuccessWithValue(value);
                }
            }
        }
    }
}