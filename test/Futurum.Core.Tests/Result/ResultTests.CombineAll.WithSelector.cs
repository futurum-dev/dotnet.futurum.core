using System;
using System.Threading.Tasks;

using FluentAssertions;

using Futurum.Core.Tests.Helper.Result;

using Xunit;

namespace Futurum.Core.Tests.Result;

public class ResultCombineAllWithSelectorTests
{
    private const string ErrorMessage1 = "ERROR_MESSAGE1";
    private const string ErrorMessage2 = "ERROR_MESSAGE2";
    private const string ErrorMessage3 = "ERROR_MESSAGE3";
    private const string ErrorMessage4 = "ERROR_MESSAGE4";

    public class Sync
    {
        public class Two
        {
            [Fact]
            public void all_failure()
            {
                var result1 = Core.Result.Result.Fail<int>(ErrorMessage1);
                var result2 = Core.Result.Result.Fail<int>(ErrorMessage2);

                var result = Core.Result.Result.CombineAll(result1, result2, (a, b) => (a, b));

                result.ShouldBeFailureWithError($"{ErrorMessage1};{ErrorMessage2}");
            }

            [Fact]
            public void all_success()
            {
                var result1 = Core.Result.Result.Ok(Guid.NewGuid().ToString());
                var result2 = Core.Result.Result.Ok(Guid.NewGuid().ToString());

                var result = Core.Result.Result.CombineAll(result1, result2, (a, b) => (a, b));

                result.ShouldBeSuccess();
                result.Value.Value.a.Should().Be(result1.Value.Value);
                result.Value.Value.b.Should().Be(result2.Value.Value);
            }

            [Fact]
            public void mixed()
            {
                var result1 = Core.Result.Result.Ok(Guid.NewGuid().ToString());
                var result2 = Core.Result.Result.Fail<string>(ErrorMessage1);

                var result = Core.Result.Result.CombineAll(result1, result2, (a, b) => (a, b));

                result.ShouldBeFailureWithError($"{ErrorMessage1}");
            }
        }
        
        public class Three
        {
            [Fact]
            public void all_failure()
            {
                var result1 = Core.Result.Result.Fail<int>(ErrorMessage1);
                var result2 = Core.Result.Result.Fail<int>(ErrorMessage2);
                var result3 = Core.Result.Result.Fail<int>(ErrorMessage3);

                var result = Core.Result.Result.CombineAll(result1, result2, result3, (a, b, c) => (a, b, c));
                result.ShouldBeFailureWithError($"{ErrorMessage1};{ErrorMessage2};{ErrorMessage3}");
            }

            [Fact]
            public void all_success()
            {
                var result1 = Core.Result.Result.Ok(Guid.NewGuid().ToString());
                var result2 = Core.Result.Result.Ok(Guid.NewGuid().ToString());
                var result3 = Core.Result.Result.Ok(Guid.NewGuid().ToString());

                var result = Core.Result.Result.CombineAll(result1, result2, result3, (a, b, c) => (a, b, c));

                result.ShouldBeSuccess();
                result.Value.Value.a.Should().Be(result1.Value.Value);
                result.Value.Value.b.Should().Be(result2.Value.Value);
                result.Value.Value.c.Should().Be(result3.Value.Value);
            }

            [Fact]
            public void mixed()
            {
                var result1 = Core.Result.Result.Ok(Guid.NewGuid().ToString());
                var result2 = Core.Result.Result.Fail<string>(ErrorMessage1);
                var result3 = Core.Result.Result.Fail<string>(ErrorMessage2);

                var result = Core.Result.Result.CombineAll(result1, result2, result3, (a, b, c) => (a, b, c));

                result.ShouldBeFailureWithError($"{ErrorMessage1};{ErrorMessage2}");
            }
        }
        
        public class Four
        {
            [Fact]
            public void all_failure()
            {
                var result1 = Core.Result.Result.Fail<int>(ErrorMessage1);
                var result2 = Core.Result.Result.Fail<int>(ErrorMessage2);
                var result3 = Core.Result.Result.Fail<int>(ErrorMessage3);
                var result4 = Core.Result.Result.Fail<int>(ErrorMessage4);

                var result = Core.Result.Result.CombineAll(result1, result2, result3, result4, (a, b, c, d) => (a, b, c, d));

                result.ShouldBeFailureWithError($"{ErrorMessage1};{ErrorMessage2};{ErrorMessage3};{ErrorMessage4}");
            }

            [Fact]
            public void all_success()
            {
                var result1 = Core.Result.Result.Ok(Guid.NewGuid().ToString());
                var result2 = Core.Result.Result.Ok(Guid.NewGuid().ToString());
                var result3 = Core.Result.Result.Ok(Guid.NewGuid().ToString());
                var result4 = Core.Result.Result.Ok(Guid.NewGuid().ToString());

                var result = Core.Result.Result.CombineAll(result1, result2, result3, result4, (a, b, c, d) => (a, b, c, d));

                result.ShouldBeSuccess();
                result.Value.Value.a.Should().Be(result1.Value.Value);
                result.Value.Value.b.Should().Be(result2.Value.Value);
                result.Value.Value.c.Should().Be(result3.Value.Value);
                result.Value.Value.d.Should().Be(result4.Value.Value);
            }

            [Fact]
            public void mixed()
            {
                var result1 = Core.Result.Result.Ok(Guid.NewGuid().ToString());
                var result2 = Core.Result.Result.Fail<string>(ErrorMessage1);
                var result3 = Core.Result.Result.Fail<string>(ErrorMessage2);
                var result4 = Core.Result.Result.Fail<string>(ErrorMessage3);

                var result = Core.Result.Result.CombineAll(result1, result2, result3, result4, (a, b, c, d) => (a, b, c, d));

                result.ShouldBeFailureWithError($"{ErrorMessage1};{ErrorMessage2};{ErrorMessage3}");
            }
        }
    }
    
    public class Async
    {
        public class Two
        {
            [Fact]
            public async Task all_failure()
            {
                var result1 = Core.Result.Result.FailAsync<int>(ErrorMessage1);
                var result2 = Core.Result.Result.FailAsync<int>(ErrorMessage2);

                var result = await Core.Result.Result.CombineAllAsync(result1, result2, (a, b) => (a, b));

                result.ShouldBeFailureWithError($"{ErrorMessage1};{ErrorMessage2}");
            }

            [Fact]
            public async Task all_success()
            {
                var result1 = Core.Result.Result.OkAsync(Guid.NewGuid().ToString());
                var result2 = Core.Result.Result.OkAsync(Guid.NewGuid().ToString());

                var result = await Core.Result.Result.CombineAllAsync(result1, result2, (a, b) => (a, b));

                result.ShouldBeSuccess();
                result.Value.Value.Item1.Should().Be(result1.Result.Value.Value);
                result.Value.Value.Item2.Should().Be(result2.Result.Value.Value);
            }

            [Fact]
            public async Task mixed()
            {
                var result1 = Core.Result.Result.OkAsync(Guid.NewGuid().ToString());
                var result2 = Core.Result.Result.FailAsync<string>(ErrorMessage1);

                var result = await Core.Result.Result.CombineAllAsync(result1, result2, (a, b) => (a, b));

                result.ShouldBeFailureWithError($"{ErrorMessage1}");
            }
        }
        
        public class Three
        {
            [Fact]
            public async Task all_failure()
            {
                var result1 = Core.Result.Result.FailAsync<int>(ErrorMessage1);
                var result2 = Core.Result.Result.FailAsync<int>(ErrorMessage2);
                var result3 = Core.Result.Result.FailAsync<int>(ErrorMessage3);

                var result = await Core.Result.Result.CombineAllAsync(result1, result2, result3, (a, b, c) => (a, b, c));

                result.ShouldBeFailureWithError($"{ErrorMessage1};{ErrorMessage2};{ErrorMessage3}");
            }

            [Fact]
            public async Task all_success()
            {
                var result1 = Core.Result.Result.OkAsync(Guid.NewGuid().ToString());
                var result2 = Core.Result.Result.OkAsync(Guid.NewGuid().ToString());
                var result3 = Core.Result.Result.OkAsync(Guid.NewGuid().ToString());

                var result = await Core.Result.Result.CombineAllAsync(result1, result2, result3, (a, b, c) => (a, b, c));

                result.ShouldBeSuccess();
                result.Value.Value.a.Should().Be(result1.Result.Value.Value);
                result.Value.Value.b.Should().Be(result2.Result.Value.Value);
                result.Value.Value.c.Should().Be(result3.Result.Value.Value);
            }

            [Fact]
            public async Task mixed()
            {
                var result1 = Core.Result.Result.OkAsync(Guid.NewGuid().ToString());
                var result2 = Core.Result.Result.FailAsync<string>(ErrorMessage1);
                var result3 = Core.Result.Result.OkAsync(Guid.NewGuid().ToString());

                var result = await Core.Result.Result.CombineAllAsync(result1, result2, result3, (a, b, c) => (a, b, c));

                result.ShouldBeFailureWithError($"{ErrorMessage1}");
            }
        }
        
        public class Four
        {
            [Fact]
            public async Task all_failure()
            {
                var result1 = Core.Result.Result.FailAsync<int>(ErrorMessage1);
                var result2 = Core.Result.Result.FailAsync<int>(ErrorMessage2);
                var result3 = Core.Result.Result.FailAsync<int>(ErrorMessage3);
                var result4 = Core.Result.Result.FailAsync<int>(ErrorMessage4);

                var result = await Core.Result.Result.CombineAllAsync(result1, result2, result3, result4, (a, b, c, d) => (a, b, c, d));

                result.ShouldBeFailureWithError($"{ErrorMessage1};{ErrorMessage2};{ErrorMessage3};{ErrorMessage4}");
            }

            [Fact]
            public async Task all_success()
            {
                var result1 = Core.Result.Result.OkAsync(Guid.NewGuid().ToString());
                var result2 = Core.Result.Result.OkAsync(Guid.NewGuid().ToString());
                var result3 = Core.Result.Result.OkAsync(Guid.NewGuid().ToString());
                var result4 = Core.Result.Result.OkAsync(Guid.NewGuid().ToString());

                var result = await Core.Result.Result.CombineAllAsync(result1, result2, result3, result4, (a, b, c, d) => (a, b, c, d));

                result.ShouldBeSuccess();
                result.Value.Value.a.Should().Be(result1.Result.Value.Value);
                result.Value.Value.b.Should().Be(result2.Result.Value.Value);
                result.Value.Value.c.Should().Be(result3.Result.Value.Value);
                result.Value.Value.d.Should().Be(result4.Result.Value.Value);
            }

            [Fact]
            public async Task mixed()
            {
                var result1 = Core.Result.Result.OkAsync(Guid.NewGuid().ToString());
                var result2 = Core.Result.Result.FailAsync<string>(ErrorMessage1);
                var result3 = Core.Result.Result.OkAsync(Guid.NewGuid().ToString());
                var result4 = Core.Result.Result.OkAsync(Guid.NewGuid().ToString());

                var result = await Core.Result.Result.CombineAllAsync(result1, result2, result3, result4, (a, b, c, d) => (a, b, c, d));

                result.ShouldBeFailureWithError($"{ErrorMessage1}");
            }
        }
    }
}