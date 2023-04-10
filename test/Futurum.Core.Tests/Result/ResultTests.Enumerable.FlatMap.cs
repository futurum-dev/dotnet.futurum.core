using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using FluentAssertions;

using Futurum.Core.Result;
using Futurum.Core.Tests.Helper.Result;

using Xunit;

namespace Futurum.Core.Tests.Result;

public class ResultEnumerableFlatMapTests
{
    private const string ErrorMessage1 = "ERROR_MESSAGE_1";
    private const string ErrorMessage2 = "ERROR_MESSAGE_2";

    public class Sync
    {
        public class IEnumerableToResultToResultIEnumberable
        {
            [Fact]
            public void AllFailure()
            {
                var value1 = 1;
                var value2 = 2;

                var input = new[] {value1, value2};

                var outputResult = input.FlatMap(x =>
                {
                    if (x == 1) return Core.Result.Result.Fail(ErrorMessage1);

                    return Core.Result.Result.Fail(ErrorMessage2);
                });

                outputResult.ShouldBeFailureWithError($"{ErrorMessage1};{ErrorMessage2}");
            }

            [Fact]
            public void AllSuccess()
            {
                var value1 = 1;
                var value2 = 2;

                var input = new[] {value1, value2};

                var outputResult = input.FlatMap(x => Core.Result.Result.Ok());

                outputResult.ShouldBeSuccess();
            }

            [Fact]
            public void Mixed()
            {
                var value1 = 1;
                var value2 = 2;

                var input = new[] {value1, value2};

                var outputResult = input.FlatMap(x => x == value1
                                                     ? Core.Result.Result.Ok()
                                                     : Core.Result.Result.Fail(ErrorMessage2));

                outputResult.ShouldBeFailureWithError(ErrorMessage2);
            }
        }

        public class IEnumerableToResultGenericToResultIEnumberable
        {
            private static int Transform(int x) =>
                x * 2;

            [Fact]
            public void AllFailure()
            {
                var value1 = 1;
                var value2 = 2;

                var input = new[] {value1, value2};

                var outputResult = input.FlatMap(x =>
                {
                    if (x == 1) return Core.Result.Result.Fail<int>(ErrorMessage1);

                    return Core.Result.Result.Fail<int>(ErrorMessage2);
                });

                outputResult.ShouldBeFailureWithError($"{ErrorMessage1};{ErrorMessage2}");
            }

            [Fact]
            public void AllSuccess()
            {
                var value1 = 1;
                var value2 = 2;

                var input = new[] {value1, value2};

                var outputResult = input.FlatMap(x => Transform(x).ToResultOk());

                outputResult.ShouldBeSuccess();
                var outputValue = outputResult.Value.Value.ToList();
                outputValue[0].Should().Be(Transform(value1));
                outputValue[1].Should().Be(Transform(value2));
            }

            [Fact]
            public void Mixed()
            {
                var value1 = 1;
                var value2 = 2;

                var input = new[] {value1, value2};

                var outputResult = input.FlatMap(x => x == value1
                                                     ? Transform(x).ToResultOk()
                                                     : Core.Result.Result.Fail<int>(ErrorMessage2));

                outputResult.ShouldBeFailureWithError(ErrorMessage2);
            }
        }

        public class IEnumerableToResultIEnumerableToResultIEnumberable
        {
            private static IEnumerable<int> TransformToArray(int x) =>
                new[] {Transform(x)};

            private static int Transform(int x) =>
                x * 2;

            [Fact]
            public void AllFailure()
            {
                var value1 = 1;
                var value2 = 2;

                var input = new[] {value1, value2};

                var outputResult = input.FlatMap(x =>
                {
                    if (x == 1) return Core.Result.Result.Fail<IEnumerable<int>>(ErrorMessage1);

                    return Core.Result.Result.Fail<IEnumerable<int>>(ErrorMessage2);
                });

                outputResult.ShouldBeFailureWithError($"{ErrorMessage1};{ErrorMessage2}");
            }

            [Fact]
            public void AllSuccess()
            {
                var value1 = 1;
                var value2 = 2;

                var input = new[] {value1, value2};

                var outputResult = input.FlatMap(x => TransformToArray(x).ToResultOk());

                outputResult.ShouldBeSuccess();
                var outputValue = outputResult.Value.Value.ToList();
                outputValue[0].Should().Be(Transform(value1));
                outputValue[1].Should().Be(Transform(value2));
            }

            [Fact]
            public void Mixed()
            {
                var value1 = 1;
                var value2 = 2;

                var input = new[] {value1, value2};

                var outputResult = input.FlatMap(x =>
                {
                    if (x == value1) return TransformToArray(x).ToResultOk();

                    return Core.Result.Result.Fail<IEnumerable<int>>(ErrorMessage2);
                });

                outputResult.ShouldBeFailureWithError(ErrorMessage2);
            }
        }

        public class IEnumerableToResultListToResultIEnumberable
        {
            private static List<int> TransformToArray(int x) =>
                new[] {Transform(x)}.ToList();

            private static int Transform(int x) =>
                x * 2;

            [Fact]
            public void AllFailure()
            {
                var value1 = 1;
                var value2 = 2;

                var input = new[] {value1, value2};

                var outputResult = input.FlatMap(x =>
                {
                    if (x == 1) return Core.Result.Result.Fail<int>(ErrorMessage1);

                    return Core.Result.Result.Fail<int>(ErrorMessage2);
                });

                outputResult.ShouldBeFailureWithError($"{ErrorMessage1};{ErrorMessage2}");
            }

            [Fact]
            public void AllSuccess()
            {
                var value1 = 1;
                var value2 = 2;

                var input = new[] {value1, value2};

                var outputResult = input.FlatMap(x => TransformToArray(x).ToResultOk());

                outputResult.ShouldBeSuccess();
                var outputValue = outputResult.Value.Value.ToList();
                outputValue[0].Should().Be(Transform(value1));
                outputValue[1].Should().Be(Transform(value2));
            }

            [Fact]
            public void Mixed()
            {
                var value1 = 1;
                var value2 = 2;

                var input = new[] {value1, value2};

                var outputResult = input.FlatMap(x =>
                {
                    if (x == value1) return TransformToArray(x).ToResultOk();

                    return Core.Result.Result.Fail<List<int>>(ErrorMessage2);
                });

                outputResult.ShouldBeFailureWithError(ErrorMessage2);
            }
        }

        public class ResultIEnumerableToIEnumerableToResultIEnumberable
        {
            [Fact]
            public void check()
            {
                var value1 = 1;
                var value2 = 2;

                var input = new[] {value1, value2}.AsEnumerable().ToResultOk();

                var xs = input.FlatMap(x => Enumerable.Repeat(x, 5));
                
                xs.ShouldBeSuccess();
                
                xs.ShouldBeSuccessWithValueEquivalentTo(Enumerable.Repeat(value1, 5).Concat(Enumerable.Repeat(value2, 5)));
            }
        }
        
        public class ResultIEnumerableToResultIEnumerableToResultIEnumberable
        {
            private static IEnumerable<int> TransformToArray(int x) =>
                new[] {Transform(x)};

            private static int Transform(int x) =>
                x * 2;

            [Fact]
            public void AllFailure()
            {
                var value1 = 1;
                var value2 = 2;

                var input = new[] {value1, value2}.AsEnumerable().ToResultOk();

                var outputResult = input.FlatMap(x =>
                {
                    if (x == 1) return Core.Result.Result.Fail<IEnumerable<int>>(ErrorMessage1);

                    return Core.Result.Result.Fail<IEnumerable<int>>(ErrorMessage2);
                });

                outputResult.ShouldBeFailureWithError($"{ErrorMessage1};{ErrorMessage2}");
            }

            [Fact]
            public void AllSuccess()
            {
                var value1 = 1;
                var value2 = 2;

                var input = new[] {value1, value2}.AsEnumerable().ToResultOk();

                var outputResult = input.FlatMap(x => TransformToArray(x).ToResultOk());

                outputResult.ShouldBeSuccess();
                var outputValue = outputResult.Value.Value.ToList();
                outputValue[0].Should().Be(Transform(value1));
                outputValue[1].Should().Be(Transform(value2));
            }

            [Fact]
            public void Mixed()
            {
                var value1 = 1;
                var value2 = 2;

                var input = new[] {value1, value2}.AsEnumerable().ToResultOk();

                var outputResult = input.FlatMap(x =>
                {
                    if (x == value1) return TransformToArray(x).ToResultOk();

                    return Core.Result.Result.Fail<IEnumerable<int>>(ErrorMessage2);
                });

                outputResult.ShouldBeFailureWithError(ErrorMessage2);
            }
        }
        
        public class ResultIEnumerableToResultGenericToResultIEnumberable
        {
            [Fact]
            public void AllFailure()
            {
                var value1 = 1;
                var value2 = 2;

                var input = new[] {value1, value2}.AsEnumerable().ToResultOk();

                var outputResult = input.FlatMap(x =>
                {
                    if (x == 1) return Core.Result.Result.Fail<int>(ErrorMessage1);

                    return Core.Result.Result.Fail<int>(ErrorMessage2);
                });

                outputResult.ShouldBeFailureWithError($"{ErrorMessage1};{ErrorMessage2}");
            }

            [Fact]
            public void AllSuccess()
            {
                var value1 = 1;
                var value2 = 2;

                var input = new[] {value1, value2}.AsEnumerable().ToResultOk();

                var outputResult = input.FlatMap(x => x.ToResultOk());

                outputResult.ShouldBeSuccess();
                var outputValue = outputResult.Value.Value.ToList();
                outputValue[0].Should().Be(value1);
                outputValue[1].Should().Be(value2);
            }

            [Fact]
            public void Mixed()
            {
                var value1 = 1;
                var value2 = 2;

                var input = new[] {value1, value2}.AsEnumerable().ToResultOk();

                var outputResult = input.FlatMap(x =>
                {
                    if (x == value1) return x.ToResultOk();

                    return Core.Result.Result.Fail<int>(ErrorMessage2);
                });

                outputResult.ShouldBeFailureWithError(ErrorMessage2);
            }
        }
    }

    public class Async
    {
        public class NonGenericReturn
        {
            [Fact]
            public async Task AllFailure()
            {
                var value1 = 1;
                var value2 = 2;

                var input = new[] {value1, value2};

                var outputResult = await input.FlatMapAsync(x =>
                {
                    if (x == 1) return Core.Result.Result.FailAsync(ErrorMessage1);

                    return Core.Result.Result.FailAsync(ErrorMessage2);
                });

                outputResult.ShouldBeFailureWithErrorContaining(ErrorMessage1);
                outputResult.ShouldBeFailureWithErrorContaining(ErrorMessage2);
            }

            [Fact]
            public async Task AllSuccess()
            {
                var value1 = 1;
                var value2 = 2;

                var input = new[] {value1, value2};

                var outputResult = await input.FlatMapAsync(x => Core.Result.Result.OkAsync());

                outputResult.ShouldBeSuccess();
            }

            [Fact]
            public async Task Mixed()
            {
                var value1 = 1;
                var value2 = 2;

                var input = new[] {value1, value2};

                var outputResult = await input.FlatMapAsync(x => x == value1
                                                                ? Core.Result.Result.OkAsync()
                                                                : Core.Result.Result.FailAsync(ErrorMessage2));

                outputResult.ShouldBeFailureWithError(ErrorMessage2);
            }
        }
        
        public class IEnumerableToResultToResultIEnumberable
        {
            private static int Transform(int x) =>
                x * 2;

            [Fact]
            public async Task AllFailure()
            {
                var value1 = 1;
                var value2 = 2;

                var input = new[] {value1, value2};

                var outputResult = await input.FlatMapAsync(x =>
                {
                    if (x == 1) return Core.Result.Result.FailAsync<int>(ErrorMessage1);

                    return Core.Result.Result.FailAsync<int>(ErrorMessage2);
                });

                outputResult.ShouldBeFailureWithErrorContaining(ErrorMessage1);
                outputResult.ShouldBeFailureWithErrorContaining(ErrorMessage2);
            }

            [Fact]
            public async Task AllSuccess()
            {
                var value1 = 1;
                var value2 = 2;

                var input = new[] {value1, value2};

                var outputResult = await input.FlatMapAsync(x => Transform(x).ToResultOkAsync());

                outputResult.ShouldBeSuccess();
                var outputValue = outputResult.Value.Value.ToList();
                outputValue.Single(x => x == Transform(value1));
                outputValue.Single(x => x == Transform(value2));
            }

            [Fact]
            public async Task Mixed()
            {
                var value1 = 1;
                var value2 = 2;

                var input = new[] {value1, value2};

                var outputResult = await input.FlatMapAsync(x => x == value1
                                                                ? Transform(x).ToResultOkAsync()
                                                                : Core.Result.Result.FailAsync<int>(ErrorMessage2));

                outputResult.ShouldBeFailureWithError(ErrorMessage2);
            }
        }

        public class IEnumerableToResultIEnumerableToResultIEnumberable
        {
            private static IEnumerable<int> TransformToArray(int x) =>
                new[] {Transform(x)};

            private static int Transform(int x) =>
                x * 2;

            [Fact]
            public async Task AllFailure()
            {
                var value1 = 1;
                var value2 = 2;

                var input = new[] {value1, value2};

                var outputResult = await input.FlatMapAsync(x =>
                {
                    if (x == 1) return Core.Result.Result.FailAsync<IEnumerable<int>>(ErrorMessage1);

                    return Core.Result.Result.FailAsync<IEnumerable<int>>(ErrorMessage2);
                });

                outputResult.ShouldBeFailureWithErrorContaining(ErrorMessage1);
                outputResult.ShouldBeFailureWithErrorContaining(ErrorMessage2);
            }

            [Fact]
            public async Task AllSuccess()
            {
                var value1 = 1;
                var value2 = 2;

                var input = new[] {value1, value2};

                var outputResult = await input.FlatMapAsync(x => TransformToArray(x).ToResultOkAsync());

                outputResult.ShouldBeSuccess();
                var outputValue = outputResult.Value.Value.ToList();
                outputValue.Single(x => x == Transform(value1));
                outputValue.Single(x => x == Transform(value2));
            }

            [Fact]
            public async Task Mixed()
            {
                var value1 = 1;
                var value2 = 2;

                var input = new[] {value1, value2};

                var outputResult = await input.FlatMapAsync(x =>
                {
                    if (x == value1) return TransformToArray(x).ToResultOkAsync();

                    return Core.Result.Result.FailAsync<IEnumerable<int>>(ErrorMessage2);
                });

                outputResult.ShouldBeFailureWithError(ErrorMessage2);
            }
        }

        public class IEnumerableToResultListToResultIEnumberable
        {
            private static List<int> TransformToArray(int x) =>
                new[] {Transform(x)}.ToList();

            private static int Transform(int x) =>
                x * 2;

            [Fact]
            public async Task AllFailure()
            {
                var value1 = 1;
                var value2 = 2;

                var input = new[] {value1, value2};

                var outputResult = await input.FlatMapAsync(x =>
                {
                    if (x == 1) return Core.Result.Result.FailAsync<int>(ErrorMessage1);

                    return Core.Result.Result.FailAsync<int>(ErrorMessage2);
                });

                outputResult.ShouldBeFailureWithErrorContaining(ErrorMessage1);
                outputResult.ShouldBeFailureWithErrorContaining(ErrorMessage2);
            }

            [Fact]
            public async Task AllSuccess()
            {
                var value1 = 1;
                var value2 = 2;

                var input = new[] {value1, value2};

                var outputResult = await input.FlatMapAsync(x => TransformToArray(x).ToResultOkAsync());

                outputResult.ShouldBeSuccess();
                var outputValue = outputResult.Value.Value.ToList();
                outputValue.Single(x => x == Transform(value1));
                outputValue.Single(x => x == Transform(value2));
            }

            [Fact]
            public async Task Mixed()
            {
                var value1 = 1;
                var value2 = 2;

                var input = new[] {value1, value2};

                var outputResult = await input.FlatMapAsync(x =>
                {
                    if (x == value1) return TransformToArray(x).ToResultOkAsync();

                    return Core.Result.Result.FailAsync<List<int>>(ErrorMessage2);
                });

                outputResult.ShouldBeFailureWithError(ErrorMessage2);
            }
        }

        public class ResultIEnumerableToResultIEnumerableToResultIEnumberable
        {
            private static IEnumerable<int> TransformToArray(int x) =>
                new[] {Transform(x)};

            private static int Transform(int x) =>
                x * 2;

            [Fact]
            public async Task AllFailure()
            {
                var value1 = 1;
                var value2 = 2;

                var input = new[] {value1, value2}.AsEnumerable().ToResultOkAsync();

                var outputResult = await input.FlatMapAsync(x =>
                {
                    if (x == 1) return Core.Result.Result.FailAsync<IEnumerable<int>>(ErrorMessage1);

                    return Core.Result.Result.FailAsync<IEnumerable<int>>(ErrorMessage2);
                });

                outputResult.ShouldBeFailureWithErrorContaining(ErrorMessage1);
                outputResult.ShouldBeFailureWithErrorContaining(ErrorMessage2);
            }

            [Fact]
            public async Task AllSuccess()
            {
                var value1 = 1;
                var value2 = 2;

                var input = new[] {value1, value2}.AsEnumerable().ToResultOkAsync();

                var outputResult = await input.FlatMapAsync(x => TransformToArray(x).ToResultOkAsync());

                outputResult.ShouldBeSuccess();
                var outputValue = outputResult.Value.Value.ToList();
                outputValue.Count.Should().Be(2);
                outputValue.Single(x => x == Transform(value1));
                outputValue.Single(x => x == Transform(value2));
            }

            [Fact]
            public async Task Mixed()
            {
                var value1 = 1;
                var value2 = 2;

                var input = new[] {value1, value2}.AsEnumerable().ToResultOkAsync();

                var outputResult = await input.FlatMapAsync(x =>
                {
                    if (x == value1) return TransformToArray(x).ToResultOkAsync();

                    return Core.Result.Result.FailAsync<IEnumerable<int>>(ErrorMessage2);
                });

                outputResult.ShouldBeFailureWithError(ErrorMessage2);
            }
        }
        
        public class ResultIEnumerableToResultGenericToResultIEnumberable
        {
            [Fact]
            public async Task AllFailure()
            {
                var value1 = 1;
                var value2 = 2;

                var input = new[] {value1, value2}.AsEnumerable().ToResultOkAsync();

                var outputResult = await input.FlatMapAsync(x =>
                {
                    if (x == 1) return Core.Result.Result.Fail<int>(ErrorMessage1);

                    return Core.Result.Result.Fail<int>(ErrorMessage2);
                });

                outputResult.ShouldBeFailureWithError($"{ErrorMessage1};{ErrorMessage2}");
            }

            [Fact]
            public async Task AllSuccess()
            {
                var value1 = 1;
                var value2 = 2;

                var input = new[] {value1, value2}.AsEnumerable().ToResultOkAsync();

                var outputResult = await input.FlatMapAsync(x => x.ToResultOk());

                outputResult.ShouldBeSuccess();
                var outputValue = outputResult.Value.Value.ToList();
                outputValue[0].Should().Be(value1);
                outputValue[1].Should().Be(value2);
            }

            [Fact]
            public async Task Mixed()
            {
                var value1 = 1;
                var value2 = 2;

                var input = new[] {value1, value2}.AsEnumerable().ToResultOkAsync();

                var outputResult = await input.FlatMapAsync(x =>
                {
                    if (x == value1) return x.ToResultOk();

                    return Core.Result.Result.Fail<int>(ErrorMessage2);
                });

                outputResult.ShouldBeFailureWithError(ErrorMessage2);
            }
        }
        
        public class AsyncResultNonGenericReturn
        {
            [Fact]
            public async Task AllFailure()
            {
                var value1 = 1;
                var value2 = 2;

                var input = new[] {value1, value2}.AsEnumerable().ToResultOkAsync();

                var outputResult = await input.FlatMapAsync(x =>
                {
                    if (x == 1) return Core.Result.Result.FailAsync(ErrorMessage1);

                    return Core.Result.Result.FailAsync(ErrorMessage2);
                });

                outputResult.ShouldBeFailureWithErrorContaining(ErrorMessage1);
                outputResult.ShouldBeFailureWithErrorContaining(ErrorMessage2);
            }

            [Fact]
            public async Task AllSuccess()
            {
                var value1 = 1;
                var value2 = 2;

                var input = new[] {value1, value2}.AsEnumerable().ToResultOkAsync();

                var outputResult = await input.FlatMapAsync(x => Core.Result.Result.OkAsync());

                outputResult.ShouldBeSuccess();
            }

            [Fact]
            public async Task Mixed()
            {
                var value1 = 1;
                var value2 = 2;

                var input = new[] {value1, value2}.AsEnumerable().ToResultOkAsync();

                var outputResult = await input.FlatMapAsync(x => x == value1
                                                                ? Core.Result.Result.OkAsync()
                                                                : Core.Result.Result.FailAsync(ErrorMessage2));

                outputResult.ShouldBeFailureWithError(ErrorMessage2);
            }
        }

    }
}