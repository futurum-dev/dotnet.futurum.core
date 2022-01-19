using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using FluentAssertions;

using Futurum.Core.Result;
using Futurum.Core.Tests.Helper.Result;

using Xunit;

namespace Futurum.Core.Tests.Result;

public class ResultFlatMapSequentialTests
{
    private const string ErrorMessage1 = "ERROR_MESSAGE_1";
    private const string ErrorMessage2 = "ERROR_MESSAGE_2";

    public class NonGenericReturn
    {
        [Fact]
        public async Task AllFailure()
        {
            var value1 = 1;
            var value2 = 2;

            var input = new[] { value1, value2 };

            var outputResult = await input.FlatMapSequentialAsync(x =>
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

            var input = new[] { value1, value2 };

            var outputResult = await input.FlatMapSequentialAsync(x => Core.Result.Result.OkAsync());

            outputResult.ShouldBeSuccess();
        }

        [Fact]
        public async Task Mixed()
        {
            var value1 = 1;
            var value2 = 2;

            var input = new[] { value1, value2 };

            var outputResult = await input.FlatMapSequentialAsync(x => x == value1
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

            var input = new[] { value1, value2 };

            var outputResult = await input.FlatMapSequentialAsync(x =>
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

            var input = new[] { value1, value2 };

            var outputResult = await input.FlatMapSequentialAsync(x => Transform(x).ToResultOkAsync());

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

            var input = new[] { value1, value2 };

            var outputResult = await input.FlatMapSequentialAsync(x => x == value1
                                                                      ? Transform(x).ToResultOkAsync()
                                                                      : Core.Result.Result.FailAsync<int>(ErrorMessage2));

            outputResult.ShouldBeFailureWithError(ErrorMessage2);
        }
    }

    public class IEnumerableToResultIEnumerableToResultIEnumberable
    {
        private static IEnumerable<int> TransformToArray(int x) =>
            new[] { Transform(x) };

        private static int Transform(int x) =>
            x * 2;

        [Fact]
        public async Task AllFailure()
        {
            var value1 = 1;
            var value2 = 2;

            var input = new[] { value1, value2 };

            var outputResult = await input.FlatMapSequentialAsync(x =>
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

            var input = new[] { value1, value2 };

            var outputResult = await input.FlatMapSequentialAsync(x => TransformToArray(x).ToResultOkAsync());

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

            var input = new[] { value1, value2 };

            var outputResult = await input.FlatMapSequentialAsync(x =>
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
            new[] { Transform(x) }.ToList();

        private static int Transform(int x) =>
            x * 2;

        [Fact]
        public async Task AllFailure()
        {
            var value1 = 1;
            var value2 = 2;

            var input = new[] { value1, value2 };

            var outputResult = await input.FlatMapSequentialAsync(x =>
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

            var input = new[] { value1, value2 };

            var outputResult = await input.FlatMapSequentialAsync(x => TransformToArray(x).ToResultOkAsync());

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

            var input = new[] { value1, value2 };

            var outputResult = await input.FlatMapSequentialAsync(x =>
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
            new[] { Transform(x) };

        private static int Transform(int x) =>
            x * 2;

        [Fact]
        public async Task AllFailure()
        {
            var value1 = 1;
            var value2 = 2;

            var input = new[] { value1, value2 }.AsEnumerable().ToResultOkAsync();

            var outputResult = await input.FlatMapSequentialAsync(x =>
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

            var input = new[] { value1, value2 }.AsEnumerable().ToResultOkAsync();

            var outputResult = await input.FlatMapSequentialAsync(x => TransformToArray(x).ToResultOkAsync());

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

            var input = new[] { value1, value2 }.AsEnumerable().ToResultOkAsync();

            var outputResult = await input.FlatMapSequentialAsync(x =>
            {
                if (x == value1) return TransformToArray(x).ToResultOkAsync();

                return Core.Result.Result.FailAsync<IEnumerable<int>>(ErrorMessage2);
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

            var input = new[] { value1, value2 }.AsEnumerable().ToResultOkAsync();

            var outputResult = await input.FlatMapSequentialAsync(x =>
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

            var input = new[] { value1, value2 }.AsEnumerable().ToResultOkAsync();

            var outputResult = await input.FlatMapSequentialAsync(x => Core.Result.Result.OkAsync());

            outputResult.ShouldBeSuccess();
        }

        [Fact]
        public async Task Mixed()
        {
            var value1 = 1;
            var value2 = 2;

            var input = new[] { value1, value2 }.AsEnumerable().ToResultOkAsync();

            var outputResult = await input.FlatMapSequentialAsync(x => x == value1
                                                                      ? Core.Result.Result.OkAsync()
                                                                      : Core.Result.Result.FailAsync(ErrorMessage2));

            outputResult.ShouldBeFailureWithError(ErrorMessage2);
        }
    }
}