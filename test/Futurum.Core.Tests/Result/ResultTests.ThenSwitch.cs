using System;
using System.Threading.Tasks;

using FluentAssertions;

using Futurum.Core.Result;
using Futurum.Core.Tests.Helper.Result;

using Xunit;

namespace Futurum.Core.Tests.Result;

public class ResultThenSwitchTests
{
    private const string ErrorMessage1 = "ERROR_MESSAGE_1";
    private const string ErrorMessage2 = "ERROR_MESSAGE_2";

    public class Sync
    {
        public class FalsePayload
        {
            [Fact]
            public void FailureInputThatDoesMatchesPredicate()
            {
                var trueValue = Guid.NewGuid();
                var falseValue = Guid.NewGuid();

                var resultInput = Core.Result.Result.Fail<int>(ErrorMessage1);

                var returnedResult = resultInput.ThenSwitch(_ => false,
                                                            _ => Core.Result.Result.Ok(trueValue),
                                                            _ => Core.Result.Result.Ok(falseValue));

                returnedResult.ShouldBeFailureWithError(ErrorMessage1);
            }

            [Fact]
            public void FailureInputThatMatchesPredicate()
            {
                var trueValue = Guid.NewGuid();
                var falseValue = Guid.NewGuid();

                var resultInput = Core.Result.Result.Fail<int>(ErrorMessage1);

                var returnedResult = resultInput.ThenSwitch(_ => true,
                                                            _ => Core.Result.Result.Ok(trueValue),
                                                            _ => Core.Result.Result.Ok(falseValue));

                returnedResult.ShouldBeFailureWithError(ErrorMessage1);
            }

            [Fact]
            public void SuccessInputThatDoesMatchesPredicate()
            {
                var inputValue = Guid.NewGuid();

                var trueValue = Guid.NewGuid();
                var falseValue = Guid.NewGuid();

                var passedValue = Guid.Empty;

                var resultInput = Core.Result.Result.Ok(inputValue);

                var returnedResult = resultInput.ThenSwitch(value =>
                                                            {
                                                                passedValue = value;

                                                                return false;
                                                            },
                                                            _ => Core.Result.Result.Ok(trueValue),
                                                            _ => Core.Result.Result.Ok(falseValue));

                passedValue.Should().Be(inputValue);
                returnedResult.ShouldBeSuccessWithValue(falseValue);
            }

            [Fact]
            public void SuccessInputThatMatchesPredicate()
            {
                var inputValue = Guid.NewGuid();

                var trueValue = Guid.NewGuid();
                var falseValue = Guid.NewGuid();

                var passedValue = Guid.Empty;

                var resultInput = Core.Result.Result.Ok(inputValue);

                var returnedResult = resultInput.ThenSwitch(value =>
                                                            {
                                                                passedValue = value;

                                                                return true;
                                                            },
                                                            _ => Core.Result.Result.Ok(trueValue),
                                                            _ => Core.Result.Result.Ok(falseValue));

                passedValue.Should().Be(inputValue);
                returnedResult.ShouldBeSuccessWithValue(trueValue);
            }
        }

        public class FalseNoPayload
        {
            [Fact]
            public void FailureInputThatDoesMatchesPredicate()
            {
                var trueValue = Guid.NewGuid();
                var falseValue = Guid.NewGuid();

                var resultInput = Core.Result.Result.Fail<int>(ErrorMessage1);

                var returnedResult = resultInput.ThenSwitch(_ => false,
                                                            _ => Core.Result.Result.Ok(trueValue),
                                                            () => Core.Result.Result.Ok(falseValue));

                returnedResult.ShouldBeFailureWithError(ErrorMessage1);
            }

            [Fact]
            public void FailureInputThatMatchesPredicate()
            {
                var trueValue = Guid.NewGuid();
                var falseValue = Guid.NewGuid();

                var resultInput = Core.Result.Result.Fail<int>(ErrorMessage1);

                var returnedResult = resultInput.ThenSwitch(_ => true,
                                                            _ => Core.Result.Result.Ok(trueValue),
                                                            () => Core.Result.Result.Ok(falseValue));

                returnedResult.ShouldBeFailureWithError(ErrorMessage1);
            }

            [Fact]
            public void SuccessInputThatDoesMatchesPredicate()
            {
                var inputValue = Guid.NewGuid();

                var trueValue = Guid.NewGuid();
                var falseValue = Guid.NewGuid();

                var passedValue = Guid.Empty;

                var resultInput = Core.Result.Result.Ok(inputValue);

                var returnedResult = resultInput.ThenSwitch(value =>
                                                            {
                                                                passedValue = value;

                                                                return false;
                                                            },
                                                            _ => Core.Result.Result.Ok(trueValue),
                                                            () => Core.Result.Result.Ok(falseValue));

                passedValue.Should().Be(inputValue);
                returnedResult.ShouldBeSuccessWithValue(falseValue);
            }

            [Fact]
            public void SuccessInputThatMatchesPredicate()
            {
                var inputValue = Guid.NewGuid();

                var trueValue = Guid.NewGuid();
                var falseValue = Guid.NewGuid();

                var passedValue = Guid.Empty;

                var resultInput = Core.Result.Result.Ok(inputValue);

                var returnedResult = resultInput.ThenSwitch(value =>
                                                            {
                                                                passedValue = value;

                                                                return true;
                                                            },
                                                            _ => Core.Result.Result.Ok(trueValue),
                                                            () => Core.Result.Result.Ok(falseValue));

                passedValue.Should().Be(inputValue);
                returnedResult.ShouldBeSuccessWithValue(trueValue);
            }
        }

        public class FalseErrorPayload
        {
            [Fact]
            public void FailureInputThatDoesMatchesPredicate()
            {
                var trueValue = Guid.NewGuid();

                var resultInput = Core.Result.Result.Fail<int>(ErrorMessage1);

                var returnedResult = resultInput.ThenSwitch(_ => Core.Result.Result.Fail(ErrorMessage2),
                                                            _ => Core.Result.Result.Ok(trueValue),
                                                            error => Core.Result.Result.Fail<Guid>(error));

                returnedResult.ShouldBeFailureWithError(ErrorMessage1);
            }

            [Fact]
            public void FailureInputThatMatchesPredicate()
            {
                var trueValue = Guid.NewGuid();

                var resultInput = Core.Result.Result.Fail<int>(ErrorMessage1);

                var returnedResult = resultInput.ThenSwitch(_ => Core.Result.Result.Fail(ErrorMessage2),
                                                            _ => Core.Result.Result.Ok(trueValue),
                                                            error => Core.Result.Result.Fail<Guid>(error));

                returnedResult.ShouldBeFailureWithError(ErrorMessage1);
            }

            [Fact]
            public void SuccessInputThatDoesMatchesPredicate()
            {
                var inputValue = Guid.NewGuid();

                var trueValue = Guid.NewGuid();
                var falseValue = Guid.NewGuid();

                var passedValue = Guid.Empty;

                var resultInput = Core.Result.Result.Ok(inputValue);

                var returnedResult = resultInput.ThenSwitch(value =>
                                                            {
                                                                passedValue = value;

                                                                return Core.Result.Result.Fail(ErrorMessage2);
                                                            },
                                                            _ => Core.Result.Result.Ok(trueValue),
                                                            error => Core.Result.Result.Fail<Guid>(error));

                passedValue.Should().Be(inputValue);
                returnedResult.ShouldBeFailureWithError(ErrorMessage2);
            }

            [Fact]
            public void SuccessInputThatMatchesPredicate()
            {
                var inputValue = Guid.NewGuid();

                var trueValue = Guid.NewGuid();
                var falseValue = Guid.NewGuid();

                var passedValue = Guid.Empty;

                var resultInput = Core.Result.Result.Ok(inputValue);

                var returnedResult = resultInput.ThenSwitch(value =>
                                                            {
                                                                passedValue = value;

                                                                return Core.Result.Result.Ok();
                                                            },
                                                            _ => Core.Result.Result.Ok(trueValue),
                                                            error => Core.Result.Result.Fail<Guid>(error));

                passedValue.Should().Be(inputValue);
                returnedResult.ShouldBeSuccessWithValue(trueValue);
            }
        }
    }

    public class Async
    {
        public class FalsePayload
        {
            [Fact]
            public async Task FailureInputThatDoesMatchesPredicate()
            {
                var trueValue = Guid.NewGuid();
                var falseValue = Guid.NewGuid();

                var resultInput = Core.Result.Result.FailAsync<int>(ErrorMessage1);

                var returnedResult = await resultInput.ThenSwitchAsync(_ => false,
                                                                       _ => Core.Result.Result.Ok(trueValue),
                                                                       _ => Core.Result.Result.Ok(falseValue));

                returnedResult.ShouldBeFailureWithError(ErrorMessage1);
            }

            [Fact]
            public async Task FailureInputThatMatchesPredicate()
            {
                var trueValue = Guid.NewGuid();
                var falseValue = Guid.NewGuid();

                var resultInput = Core.Result.Result.FailAsync<int>(ErrorMessage1);

                var returnedResult = await resultInput.ThenSwitchAsync(_ => true,
                                                                       _ => Core.Result.Result.Ok(trueValue),
                                                                       _ => Core.Result.Result.Ok(falseValue));

                returnedResult.ShouldBeFailureWithError(ErrorMessage1);
            }

            [Fact]
            public async Task SuccessInputThatDoesMatchesPredicate()
            {
                var inputValue = Guid.NewGuid();

                var trueValue = Guid.NewGuid();
                var falseValue = Guid.NewGuid();

                var passedValue = Guid.Empty;

                var resultInput = Core.Result.Result.OkAsync(inputValue);

                var returnedResult = await resultInput.ThenSwitchAsync(value =>
                                                                       {
                                                                           passedValue = value;

                                                                           return false;
                                                                       },
                                                                       _ => Core.Result.Result.Ok(trueValue),
                                                                       _ => Core.Result.Result.Ok(falseValue));

                passedValue.Should().Be(inputValue);
                returnedResult.ShouldBeSuccessWithValue(falseValue);
            }

            [Fact]
            public async Task SuccessInputThatMatchesPredicate()
            {
                var inputValue = Guid.NewGuid();

                var trueValue = Guid.NewGuid();
                var falseValue = Guid.NewGuid();

                var passedValue = Guid.Empty;

                var resultInput = Core.Result.Result.OkAsync(inputValue);

                var returnedResult = await resultInput.ThenSwitchAsync(value =>
                                                                       {
                                                                           passedValue = value;

                                                                           return true;
                                                                       },
                                                                       _ => Core.Result.Result.Ok(trueValue),
                                                                       _ => Core.Result.Result.Ok(falseValue));

                passedValue.Should().Be(inputValue);
                returnedResult.ShouldBeSuccessWithValue(trueValue);
            }
        }

        public class FalseNoPayload
        {
            [Fact]
            public async Task FailureInputThatDoesMatchesPredicate()
            {
                var trueValue = Guid.NewGuid();
                var falseValue = Guid.NewGuid();

                var resultInput = Core.Result.Result.FailAsync<int>(ErrorMessage1);

                var returnedResult = await resultInput.ThenSwitchAsync(_ => false,
                                                                       _ => Core.Result.Result.Ok(trueValue),
                                                                       () => Core.Result.Result.Ok(falseValue));

                returnedResult.ShouldBeFailureWithError(ErrorMessage1);
            }

            [Fact]
            public async Task FailureInputThatMatchesPredicate()
            {
                var trueValue = Guid.NewGuid();
                var falseValue = Guid.NewGuid();

                var resultInput = Core.Result.Result.FailAsync<int>(ErrorMessage1);

                var returnedResult = await resultInput.ThenSwitchAsync(_ => true,
                                                                       _ => Core.Result.Result.Ok(trueValue),
                                                                       () => Core.Result.Result.Ok(falseValue));

                returnedResult.ShouldBeFailureWithError(ErrorMessage1);
            }

            [Fact]
            public async Task SuccessInputThatDoesMatchesPredicate()
            {
                var inputValue = Guid.NewGuid();

                var trueValue = Guid.NewGuid();
                var falseValue = Guid.NewGuid();

                var passedValue = Guid.Empty;

                var resultInput = Core.Result.Result.OkAsync(inputValue);

                var returnedResult = await resultInput.ThenSwitchAsync(value =>
                                                                       {
                                                                           passedValue = value;

                                                                           return false;
                                                                       },
                                                                       _ => Core.Result.Result.Ok(trueValue),
                                                                       () => Core.Result.Result.Ok(falseValue));

                passedValue.Should().Be(inputValue);
                returnedResult.ShouldBeSuccessWithValue(falseValue);
            }

            [Fact]
            public async Task SuccessInputThatMatchesPredicate()
            {
                var inputValue = Guid.NewGuid();

                var trueValue = Guid.NewGuid();
                var falseValue = Guid.NewGuid();

                var passedValue = Guid.Empty;

                var resultInput = Core.Result.Result.OkAsync(inputValue);

                var returnedResult = await resultInput.ThenSwitchAsync(value =>
                                                                       {
                                                                           passedValue = value;

                                                                           return true;
                                                                       },
                                                                       _ => Core.Result.Result.Ok(trueValue),
                                                                       () => Core.Result.Result.Ok(falseValue));

                passedValue.Should().Be(inputValue);
                returnedResult.ShouldBeSuccessWithValue(trueValue);
            }
        }
    }
}