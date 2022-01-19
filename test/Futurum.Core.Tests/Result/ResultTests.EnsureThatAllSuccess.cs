using System.Collections.Generic;
using System.Linq;

using Futurum.Core.Result;
using Futurum.Core.Tests.Helper.Result;

using Xunit;

namespace Futurum.Core.Tests.Result;

public class ResultEnsureThatAllSuccessTests
{
    private const string ErrorMessage1 = "ERROR_MESSAGE_1";
    private const string ErrorMessage2 = "ERROR_MESSAGE_2";

    private static Result<int> CreateSuccess(int number) =>
        Core.Result.Result.Ok(number);

    private static Result<int> CreateFailure1() =>
        Core.Result.Result.Fail<int>(ErrorMessage1);

    private static Result<int> CreateFailure2() =>
        Core.Result.Result.Fail<int>(ErrorMessage2);

    public class IEnumerable
    {
        [Fact]
        public void AllFailure()
        {
            IEnumerable<Result<int>> Get()
            {
                yield return CreateFailure1();
                yield return CreateFailure2();
            }

            var input = Get();

            var resultInput = input.ToResultOk();

            var result = resultInput.EnsureThatAllSuccess(() => ErrorMessage1);

            result.ShouldBeFailureWithError(ErrorMessage1, ErrorMessage1, ErrorMessage2);
        }

        [Fact]
        public void AllSuccess()
        {
            var input = Enumerable.Range(0, 10)
                                  .ToList();

            var resultInput = input.Select(CreateSuccess)
                                   .ToResultOk();

            var result = resultInput.EnsureThatAllSuccess(() => ErrorMessage1);

            result.ShouldBeSuccessWithValueEquivalentTo(input);
        }

        [Fact]
        public void Mixed()
        {
            IEnumerable<Result<int>> Get()
            {
                foreach (var i in Enumerable.Range(0, 10)) yield return i.ToResultOk();

                yield return CreateFailure1();
                yield return CreateFailure2();
            }

            var input = Get();

            var resultInput = input.ToResultOk();

            var result = resultInput.EnsureThatAllSuccess(() => ErrorMessage1);

            result.ShouldBeFailureWithError(ErrorMessage1, ErrorMessage1, ErrorMessage2);
        }
    }
        
    public class List
    {
        [Fact]
        public void AllFailure()
        {
            IEnumerable<Result<int>> Get()
            {
                yield return CreateFailure1();
                yield return CreateFailure2();
            }

            var input = Get();

            var resultInput = input.ToList().ToResultOk();

            var result = resultInput.EnsureThatAllSuccess(() => ErrorMessage1);

            result.ShouldBeFailureWithError(ErrorMessage1, ErrorMessage1, ErrorMessage2);
        }

        [Fact]
        public void AllSuccess()
        {
            var input = Enumerable.Range(0, 10)
                                  .ToList();

            var resultInput = input.Select(CreateSuccess)
                                   .ToList()
                                   .ToResultOk();

            var result = resultInput.EnsureThatAllSuccess(() => ErrorMessage1);

            result.ShouldBeSuccessWithValueEquivalentTo(input);
        }

        [Fact]
        public void Mixed()
        {
            IEnumerable<Result<int>> Get()
            {
                foreach (var i in Enumerable.Range(0, 10)) yield return i.ToResultOk();

                yield return CreateFailure1();
                yield return CreateFailure2();
            }

            var input = Get();

            var resultInput = input.ToList().ToResultOk();

            var result = resultInput.EnsureThatAllSuccess(() => ErrorMessage1);

            result.ShouldBeFailureWithError(ErrorMessage1, ErrorMessage1, ErrorMessage2);
        }
    }
}