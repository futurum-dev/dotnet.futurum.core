using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using FluentAssertions;

using Futurum.Core.Result;
using Futurum.Core.Tests.Helper.Result;

using Xunit;

namespace Futurum.Core.Tests.Result;

public class ResultCombineTests
{
    private const string ErrorMessage1 = "ERROR_MESSAGE1";
    private const string ErrorMessage2 = "ERROR_MESSAGE2";
    private const string ErrorMessage3 = "ERROR_MESSAGE3";

    public class Sync
    {
        public class NonGeneric
        {
            [Fact]
            public void all_failure()
            {
                var result1 = Core.Result.Result.Fail(ErrorMessage1);
                var result2 = Core.Result.Result.Fail(ErrorMessage2);
                var result3 = Core.Result.Result.Fail(ErrorMessage3);

                var result = Core.Result.Result.Combine(result1, result2, result3);

                result.ShouldBeFailureWithError($"{ErrorMessage1};{ErrorMessage2};{ErrorMessage3}");
            }

            [Fact]
            public void all_success()
            {
                var result1 = Core.Result.Result.Ok();
                var result2 = Core.Result.Result.Ok();
                var result3 = Core.Result.Result.Ok(Guid.NewGuid().ToString());

                var result = Core.Result.Result.Combine(result1, result2, result3);

                result.ShouldBeSuccess();
            }

            [Fact]
            public void mixed()
            {
                var result1 = Core.Result.Result.Ok();
                var result2 = Core.Result.Result.Fail(ErrorMessage1);
                var result3 = Core.Result.Result.Fail(ErrorMessage2);

                var result = Core.Result.Result.Combine(result1, result2, result3);

                result.ShouldBeFailureWithError($"{ErrorMessage1};{ErrorMessage2}");
            }
        }

        public class Generic
        {
            [Fact]
            public void all_failure()
            {
                var result3 = Core.Result.Result.Fail<string>(ErrorMessage3);
                var result1 = Core.Result.Result.Fail<string>(ErrorMessage1);
                var result2 = Core.Result.Result.Fail<string>(ErrorMessage2);

                var result = Core.Result.Result.Combine(result1, result2, result3);

                result.ShouldBeFailureWithError($"{ErrorMessage1};{ErrorMessage2};{ErrorMessage3}");
            }

            [Fact]
            public void all_success()
            {
                var value1 = Guid.NewGuid().ToString();
                var value2 = Guid.NewGuid().ToString();
                var value3 = Guid.NewGuid().ToString();

                var result1 = Core.Result.Result.Ok(value1);
                var result2 = Core.Result.Result.Ok(value2);
                var result3 = Core.Result.Result.Ok(value3);

                var result = Core.Result.Result.Combine(result1, result2, result3);

                result.ShouldBeSuccess();

                var resultValues = result.Value.Value.ToList();
                resultValues[0].Should().Be(value1);
                resultValues[1].Should().Be(value2);
                resultValues[2].Should().Be(value3);
            }

            [Fact]
            public void mixed()
            {
                var result1 = Core.Result.Result.Ok(Guid.NewGuid().ToString());
                var result2 = Core.Result.Result.Fail<string>(ErrorMessage1);
                var result3 = Core.Result.Result.Fail<string>(ErrorMessage2);

                var result = Core.Result.Result.Combine(result1, result2, result3);

                result.ShouldBeFailureWithError($"{ErrorMessage1};{ErrorMessage2}");
            }
        }
    }

    public class Async
    {
        public class NonGeneric
        {
            [Fact]
            public async Task all_failure()
            {
                var result1 = Core.Result.Result.FailAsync(ErrorMessage1);
                var result2 = Core.Result.Result.FailAsync(ErrorMessage2);
                var result3 = Core.Result.Result.FailAsync(ErrorMessage3);

                var result = await Core.Result.Result.CombineAsync(result1, result2, result3);

                result.ShouldBeFailureWithError($"{ErrorMessage1};{ErrorMessage2};{ErrorMessage3}");
            }

            [Fact]
            public async Task all_success()
            {
                var result1 = Core.Result.Result.OkAsync();
                var result2 = Core.Result.Result.OkAsync();
                var result3 = Core.Result.Result.OkAsync(Guid.NewGuid().ToString()).ToNonGenericAsync();

                var result = await Core.Result.Result.CombineAsync(result1, result2, result3);

                result.ShouldBeSuccess();
            }

            [Fact]
            public async Task mixed()
            {
                var result1 = Core.Result.Result.OkAsync();
                var result2 = Core.Result.Result.FailAsync(ErrorMessage1);
                var result3 = Core.Result.Result.FailAsync(ErrorMessage2);

                var result = await Core.Result.Result.CombineAsync(result1, result2, result3);

                result.ShouldBeFailureWithError($"{ErrorMessage1};{ErrorMessage2}");
            }
        }

        public class Generic
        {
            [Fact]
            public async Task all_failure()
            {
                var result3 = Core.Result.Result.FailAsync<string>(ErrorMessage3);
                var result1 = Core.Result.Result.FailAsync<string>(ErrorMessage1);
                var result2 = Core.Result.Result.FailAsync<string>(ErrorMessage2);

                var result = await Core.Result.Result.CombineAsync(result1, result2, result3);

                result.ShouldBeFailureWithError($"{ErrorMessage1};{ErrorMessage2};{ErrorMessage3}");
            }

            [Fact]
            public async Task all_success()
            {
                var value1 = Guid.NewGuid().ToString();
                var value2 = Guid.NewGuid().ToString();
                var value3 = Guid.NewGuid().ToString();

                var result1 = Core.Result.Result.OkAsync(value1);
                var result2 = Core.Result.Result.OkAsync(value2);
                var result3 = Core.Result.Result.OkAsync(value3);

                var result = await Core.Result.Result.CombineAsync(result1, result2, result3);

                result.ShouldBeSuccess();

                var resultValues = result.Value.Value.ToList();
                resultValues[0].Should().Be(value1);
                resultValues[1].Should().Be(value2);
                resultValues[2].Should().Be(value3);
            }

            [Fact]
            public async Task mixed()
            {
                var result1 = Core.Result.Result.OkAsync(Guid.NewGuid().ToString());
                var result2 = Core.Result.Result.FailAsync<string>(ErrorMessage1);
                var result3 = Core.Result.Result.FailAsync<string>(ErrorMessage2);

                var result = await Core.Result.Result.CombineAsync(result1, result2, result3);

                result.ShouldBeFailureWithError($"{ErrorMessage1};{ErrorMessage2}");
            }
        }

        public class IEnumerable
        {
            [Fact]
            public async Task all_failure()
            {
                var result3 = Core.Result.Result.FailAsync<IEnumerable<string>>(ErrorMessage3);
                var result1 = Core.Result.Result.FailAsync<IEnumerable<string>>(ErrorMessage1);
                var result2 = Core.Result.Result.FailAsync<IEnumerable<string>>(ErrorMessage2);

                var result = await Core.Result.Result.CombineAsync(result1, result2, result3);

                result.ShouldBeFailureWithError($"{ErrorMessage1};{ErrorMessage2};{ErrorMessage3}");
            }

            [Fact]
            public async Task all_success()
            {
                var value1 = Guid.NewGuid().ToString();
                var value2 = Guid.NewGuid().ToString();
                var value3 = Guid.NewGuid().ToString();

                var result1 = Core.Result.Result.OkAsync(new[] {value1}.AsEnumerable());
                var result2 = Core.Result.Result.OkAsync(new[] {value2}.AsEnumerable());
                var result3 = Core.Result.Result.OkAsync(new[] {value3}.AsEnumerable());

                var result = await Core.Result.Result.CombineAsync(result1, result2, result3);

                result.ShouldBeSuccess();

                var resultValues = result.Value.Value.ToList();
                resultValues[0].Should().Be(value1);
                resultValues[1].Should().Be(value2);
                resultValues[2].Should().Be(value3);
            }

            [Fact]
            public async Task mixed()
            {
                var result1 = Core.Result.Result.OkAsync(new[] {Guid.NewGuid().ToString()}.AsEnumerable());
                var result2 = Core.Result.Result.FailAsync<IEnumerable<string>>(ErrorMessage1);
                var result3 = Core.Result.Result.FailAsync<IEnumerable<string>>(ErrorMessage2);

                var result = await Core.Result.Result.CombineAsync(result1, result2, result3);

                result.ShouldBeFailureWithError($"{ErrorMessage1};{ErrorMessage2}");
            }
        }

        public class List
        {
            [Fact]
            public async Task all_failure()
            {
                var result3 = Core.Result.Result.FailAsync<List<string>>(ErrorMessage3);
                var result1 = Core.Result.Result.FailAsync<List<string>>(ErrorMessage1);
                var result2 = Core.Result.Result.FailAsync<List<string>>(ErrorMessage2);

                var result = await Core.Result.Result.CombineAsync(result1, result2, result3);

                result.ShouldBeFailureWithError($"{ErrorMessage1};{ErrorMessage2};{ErrorMessage3}");
            }

            [Fact]
            public async Task all_success()
            {
                var value1 = Guid.NewGuid().ToString();
                var value2 = Guid.NewGuid().ToString();
                var value3 = Guid.NewGuid().ToString();

                var result1 = Core.Result.Result.OkAsync(new[] {value1}.ToList());
                var result2 = Core.Result.Result.OkAsync(new[] {value2}.ToList());
                var result3 = Core.Result.Result.OkAsync(new[] {value3}.ToList());

                var result = await Core.Result.Result.CombineAsync(result1, result2, result3);

                result.ShouldBeSuccess();

                var resultValues = result.Value.Value.ToList();
                resultValues[0].Should().Be(value1);
                resultValues[1].Should().Be(value2);
                resultValues[2].Should().Be(value3);
            }

            [Fact]
            public async Task mixed()
            {
                var result1 = Core.Result.Result.OkAsync(new[] {Guid.NewGuid().ToString()}.ToList());
                var result2 = Core.Result.Result.FailAsync<List<string>>(ErrorMessage1);
                var result3 = Core.Result.Result.FailAsync<List<string>>(ErrorMessage2);

                var result = await Core.Result.Result.CombineAsync(result1, result2, result3);

                result.ShouldBeFailureWithError($"{ErrorMessage1};{ErrorMessage2}");
            }
        }
    }

    public class IEnumerable
    {
        public class Sync
        {
            public class NonGeneric
            {
                [Fact]
                public void all_failure()
                {
                    var result1 = Core.Result.Result.Fail(ErrorMessage1);
                    var result2 = Core.Result.Result.Fail(ErrorMessage2);
                    var result3 = Core.Result.Result.Fail(ErrorMessage3);

                    var results = new[] {result1, result2, result3}.AsEnumerable();
                    var result = results.Combine();

                    result.ShouldBeFailureWithError($"{ErrorMessage1};{ErrorMessage2};{ErrorMessage3}");
                }

                [Fact]
                public void all_success()
                {
                    var result1 = Core.Result.Result.Ok();
                    var result2 = Core.Result.Result.Ok();
                    var result3 = Core.Result.Result.Ok(Guid.NewGuid().ToString());

                    var results = new[] {result1, result2, result3}.AsEnumerable();
                    var result = results.Combine();

                    result.ShouldBeSuccess();
                }

                [Fact]
                public void mixed()
                {
                    var result1 = Core.Result.Result.Ok();
                    var result2 = Core.Result.Result.Fail(ErrorMessage1);
                    var result3 = Core.Result.Result.Fail(ErrorMessage2);

                    var results = new[] {result1, result2, result3}.AsEnumerable();
                    var result = results.Combine();

                    result.ShouldBeFailureWithError($"{ErrorMessage1};{ErrorMessage2}");
                }
            }

            public class Generic
            {
                [Fact]
                public void all_failure()
                {
                    var result3 = Core.Result.Result.Fail<string>(ErrorMessage3);
                    var result1 = Core.Result.Result.Fail<string>(ErrorMessage1);
                    var result2 = Core.Result.Result.Fail<string>(ErrorMessage2);

                    var results = new[] {result1, result2, result3}.AsEnumerable();
                    var result = results.Combine();

                    result.ShouldBeFailureWithError($"{ErrorMessage1};{ErrorMessage2};{ErrorMessage3}");
                }

                [Fact]
                public void all_success()
                {
                    var value1 = Guid.NewGuid().ToString();
                    var value2 = Guid.NewGuid().ToString();
                    var value3 = Guid.NewGuid().ToString();

                    var result1 = Core.Result.Result.Ok(value1);
                    var result2 = Core.Result.Result.Ok(value2);
                    var result3 = Core.Result.Result.Ok(value3);

                    var results = new[] {result1, result2, result3}.AsEnumerable();
                    var result = results.Combine();

                    result.ShouldBeSuccess();

                    var resultValues = result.Value.Value.ToList();
                    resultValues[0].Should().Be(value1);
                    resultValues[1].Should().Be(value2);
                    resultValues[2].Should().Be(value3);
                }

                [Fact]
                public void mixed()
                {
                    var result1 = Core.Result.Result.Ok(Guid.NewGuid().ToString());
                    var result2 = Core.Result.Result.Fail<string>(ErrorMessage1);
                    var result3 = Core.Result.Result.Fail<string>(ErrorMessage2);

                    var results = new[] {result1, result2, result3}.AsEnumerable();
                    var result = results.Combine();

                    result.ShouldBeFailureWithError($"{ErrorMessage1};{ErrorMessage2}");
                }
            }
        }

        public class Async
        {
            public class NonGeneric
            {
                [Fact]
                public async Task all_failure()
                {
                    var result1 = Core.Result.Result.FailAsync(ErrorMessage1);
                    var result2 = Core.Result.Result.FailAsync(ErrorMessage2);
                    var result3 = Core.Result.Result.FailAsync(ErrorMessage3);

                    var results = new[] {result1, result2, result3}.AsEnumerable();
                    var result = await results.CombineAsync();

                    result.ShouldBeFailureWithError($"{ErrorMessage1};{ErrorMessage2};{ErrorMessage3}");
                }

                [Fact]
                public async Task all_success()
                {
                    var result1 = Core.Result.Result.OkAsync();
                    var result2 = Core.Result.Result.OkAsync();
                    var result3 = Core.Result.Result.OkAsync(Guid.NewGuid().ToString()).ToNonGenericAsync();

                    var results = new[] {result1, result2, result3}.AsEnumerable();
                    var result = await results.CombineAsync();

                    result.ShouldBeSuccess();
                }

                [Fact]
                public async Task mixed()
                {
                    var result1 = Core.Result.Result.OkAsync();
                    var result2 = Core.Result.Result.FailAsync(ErrorMessage1);
                    var result3 = Core.Result.Result.FailAsync(ErrorMessage2);

                    var results = new[] {result1, result2, result3}.AsEnumerable();
                    var result = await results.CombineAsync();

                    result.ShouldBeFailureWithError($"{ErrorMessage1};{ErrorMessage2}");
                }
            }

            public class Generic
            {
                [Fact]
                public async Task all_failure()
                {
                    var result3 = Core.Result.Result.FailAsync<string>(ErrorMessage3);
                    var result1 = Core.Result.Result.FailAsync<string>(ErrorMessage1);
                    var result2 = Core.Result.Result.FailAsync<string>(ErrorMessage2);

                    var results = new[] {result1, result2, result3}.AsEnumerable();
                    var result = await results.CombineAsync();

                    result.ShouldBeFailureWithError($"{ErrorMessage1};{ErrorMessage2};{ErrorMessage3}");
                }

                [Fact]
                public async Task all_success()
                {
                    var value1 = Guid.NewGuid().ToString();
                    var value2 = Guid.NewGuid().ToString();
                    var value3 = Guid.NewGuid().ToString();

                    var result1 = Core.Result.Result.OkAsync(value1);
                    var result2 = Core.Result.Result.OkAsync(value2);
                    var result3 = Core.Result.Result.OkAsync(value3);

                    var results = new[] {result1, result2, result3}.AsEnumerable();
                    var result = await results.CombineAsync();

                    result.ShouldBeSuccess();

                    var resultValues = result.Value.Value.ToList();
                    resultValues[0].Should().Be(value1);
                    resultValues[1].Should().Be(value2);
                    resultValues[2].Should().Be(value3);
                }

                [Fact]
                public async Task mixed()
                {
                    var result1 = Core.Result.Result.OkAsync(Guid.NewGuid().ToString());
                    var result2 = Core.Result.Result.FailAsync<string>(ErrorMessage1);
                    var result3 = Core.Result.Result.FailAsync<string>(ErrorMessage2);

                    var results = new[] {result1, result2, result3}.AsEnumerable();
                    var result = await results.CombineAsync();

                    result.ShouldBeFailureWithError($"{ErrorMessage1};{ErrorMessage2}");
                }
            }
        }
    }

    public class IEnumerableResultT_To_ResultIEnumerableT
    {
        public class Sync
        {
            [Fact]
            public void AllSuccess()
            {
                var value1 = 1;
                var value2 = 2;

                var result1 = Core.Result.Result.Ok(value1);
                var result2 = Core.Result.Result.Ok(value2);

                var results = new[] {result1, result2};

                var outputResult = results.Combine();

                outputResult.ShouldBeSuccess();
                var outputValue = outputResult.Value.Value.ToList();
                outputValue[0].Should().Be(value1);
                outputValue[1].Should().Be(value2);
            }

            [Fact]
            public void Failure()
            {
                var result1 = Core.Result.Result.Fail<int>(ErrorMessage1);
                var result2 = Core.Result.Result.Fail<int>(ErrorMessage2);

                var results = new[] {result1, result2};

                var outputResult = results.Combine();

                outputResult.ShouldBeFailureWithError($"{ErrorMessage1};{ErrorMessage2}");
            }

            [Fact]
            public void Mixed()
            {
                var result1 = Core.Result.Result.Ok(1);
                var result2 = Core.Result.Result.Fail<int>(ErrorMessage2);

                var results = new[] {result1, result2};

                var outputResult = results.Combine();

                outputResult.ShouldBeFailureWithError(ErrorMessage2);
            }
        }

        public class Async
        {
            [Fact]
            public async Task AllSuccess()
            {
                var value1 = 1;
                var value2 = 2;

                var result1 = Core.Result.Result.OkAsync(value1);
                var result2 = Core.Result.Result.OkAsync(value2);

                var results = new[] {result1, result2};

                var outputResult = await results.CombineAsync();

                outputResult.ShouldBeSuccess();
                var outputValue = outputResult.Value.Value.ToList();
                outputValue[0].Should().Be(value1);
                outputValue[1].Should().Be(value2);
            }

            [Fact]
            public async Task Failure()
            {
                var result1 = Core.Result.Result.FailAsync<int>(ErrorMessage1);
                var result2 = Core.Result.Result.FailAsync<int>(ErrorMessage2);

                var results = new[] {result1, result2};

                var outputResult = await results.CombineAsync();

                outputResult.ShouldBeFailureWithError($"{ErrorMessage1};{ErrorMessage2}");
            }

            [Fact]
            public async Task Mixed()
            {
                var result1 = Core.Result.Result.OkAsync(1);
                var result2 = Core.Result.Result.FailAsync<int>(ErrorMessage2);

                var results = new[] {result1, result2};

                var outputResult = await results.CombineAsync();

                outputResult.ShouldBeFailureWithError(ErrorMessage2);
            }
        }
    }

    public class IEnumerableResultIEnumerableT_To_ResultIEnumerableT
    {
        public class Sync
        {
            [Fact]
            public void AllSuccess()
            {
                var value1 = 1;
                var value2 = 2;

                var result1 = Core.Result.Result.Ok(new[] {value1}.AsEnumerable());
                var result2 = Core.Result.Result.Ok(new[] {value2}.AsEnumerable());

                var results = new[] {result1, result2};

                var outputResult = results.Combine();

                outputResult.ShouldBeSuccess();
                var outputValue = outputResult.Value.Value.ToList();
                outputValue[0].Should().Be(value1);
                outputValue[1].Should().Be(value2);
            }

            [Fact]
            public void Failure()
            {
                var result1 = Core.Result.Result.Fail<IEnumerable<int>>(ErrorMessage1);
                var result2 = Core.Result.Result.Fail<IEnumerable<int>>(ErrorMessage2);

                var results = new[] {result1, result2};

                var outputResult = results.Combine();

                outputResult.ShouldBeFailureWithError($"{ErrorMessage1};{ErrorMessage2}");
            }

            [Fact]
            public void LocalFunctionYield()
            {
                const int value1 = 1;
                const int value2 = 2;

                IEnumerable<Result<IEnumerable<int>>> Get()
                {
                    yield return new[] {value1}.AsEnumerable().ToResultOk();
                    yield return new[] {value2}.AsEnumerable().ToResultOk();
                }

                var results = Get();

                var outputResult = results.Combine();

                outputResult.ShouldBeSuccess();
                var outputValue = outputResult.Value.Value.ToList();
                outputValue[0].Should().Be(value1);
                outputValue[1].Should().Be(value2);
            }

            [Fact]
            public void Mixed()
            {
                var result1 = Core.Result.Result.Ok(new[] {1}.AsEnumerable());
                var result2 = Core.Result.Result.Fail<IEnumerable<int>>(ErrorMessage2);

                var results = new[] {result1, result2};

                var outputResult = results.Combine();

                outputResult.ShouldBeFailureWithError(ErrorMessage2);
            }
        }

        public class Async
        {
            [Fact]
            public async Task AllSuccess()
            {
                var value1 = 1;
                var value2 = 2;

                var result1 = Core.Result.Result.OkAsync(new[] {value1}.AsEnumerable());
                var result2 = Core.Result.Result.OkAsync(new[] {value2}.AsEnumerable());

                var results = new[] {result1, result2};

                var outputResult = await results.CombineAsync();

                outputResult.ShouldBeSuccess();
                var outputValue = outputResult.Value.Value.ToList();
                outputValue[0].Should().Be(value1);
                outputValue[1].Should().Be(value2);
            }

            [Fact]
            public async Task Failure()
            {
                var result1 = Core.Result.Result.FailAsync<IEnumerable<int>>(ErrorMessage1);
                var result2 = Core.Result.Result.FailAsync<IEnumerable<int>>(ErrorMessage2);

                var results = new[] {result1, result2};

                var outputResult = await results.CombineAsync();

                outputResult.ShouldBeFailureWithError($"{ErrorMessage1};{ErrorMessage2}");
            }

            [Fact]
            public async Task Mixed()
            {
                var result1 = Core.Result.Result.OkAsync(new[] {1}.AsEnumerable());
                var result2 = Core.Result.Result.FailAsync<IEnumerable<int>>(ErrorMessage2);

                var results = new[] {result1, result2};

                var outputResult = await results.CombineAsync();

                outputResult.ShouldBeFailureWithError(ErrorMessage2);
            }
        }
    }

    public class IEnumerableResultIEnumerableT_To_ResultListT
    {
        public class Sync
        {
            [Fact]
            public void AllSuccess()
            {
                var value1 = 1;
                var value2 = 2;

                var result1 = Core.Result.Result.Ok(new[] {value1}.ToList());
                var result2 = Core.Result.Result.Ok(new[] {value2}.ToList());

                var results = new[] {result1, result2};

                var outputResult = results.Combine();

                outputResult.ShouldBeSuccess();
                var outputValue = outputResult.Value.Value.ToList();
                outputValue[0].Should().Be(value1);
                outputValue[1].Should().Be(value2);
            }

            [Fact]
            public void Failure()
            {
                var result1 = Core.Result.Result.Fail<List<int>>(ErrorMessage1);
                var result2 = Core.Result.Result.Fail<List<int>>(ErrorMessage2);

                var results = new[] {result1, result2};

                var outputResult = results.Combine();

                outputResult.ShouldBeFailureWithError($"{ErrorMessage1};{ErrorMessage2}");
            }

            [Fact]
            public void LocalFunctionYield()
            {
                const int value1 = 1;
                const int value2 = 2;

                IEnumerable<Result<List<int>>> Get()
                {
                    yield return new[] {value1}.ToList().ToResultOk();
                    yield return new[] {value2}.ToList().ToResultOk();
                }

                var results = Get();

                var outputResult = results.Combine();

                outputResult.ShouldBeSuccess();
                var outputValue = outputResult.Value.Value.ToList();
                outputValue[0].Should().Be(value1);
                outputValue[1].Should().Be(value2);
            }

            [Fact]
            public void Mixed()
            {
                var result1 = Core.Result.Result.Ok(new[] {1}.ToList());
                var result2 = Core.Result.Result.Fail<List<int>>(ErrorMessage2);

                var results = new[] {result1, result2};

                var outputResult = results.Combine();

                outputResult.ShouldBeFailureWithError(ErrorMessage2);
            }
        }

        public class Async
        {
            [Fact]
            public async Task AllSuccess()
            {
                var value1 = 1;
                var value2 = 2;

                var result1 = Core.Result.Result.OkAsync(new[] {value1}.ToList());
                var result2 = Core.Result.Result.OkAsync(new[] {value2}.ToList());

                var results = new[] {result1, result2};

                var outputResult = await results.CombineAsync();

                outputResult.ShouldBeSuccess();
                var outputValue = outputResult.Value.Value.ToList();
                outputValue[0].Should().Be(value1);
                outputValue[1].Should().Be(value2);
            }

            [Fact]
            public async Task Failure()
            {
                var result1 = Core.Result.Result.FailAsync<List<int>>(ErrorMessage1);
                var result2 = Core.Result.Result.FailAsync<List<int>>(ErrorMessage2);

                var results = new[] {result1, result2};

                var outputResult = await results.CombineAsync();

                outputResult.ShouldBeFailureWithError($"{ErrorMessage1};{ErrorMessage2}");
            }

            [Fact]
            public async Task Mixed()
            {
                var result1 = Core.Result.Result.OkAsync(new[] {1}.ToList());
                var result2 = Core.Result.Result.FailAsync<List<int>>(ErrorMessage2);

                var results = new[] {result1, result2};

                var outputResult = await results.CombineAsync();

                outputResult.ShouldBeFailureWithError(ErrorMessage2);
            }
        }
    }
}