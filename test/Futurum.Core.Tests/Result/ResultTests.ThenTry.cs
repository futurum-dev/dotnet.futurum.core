using System;
using System.Threading.Tasks;

using FluentAssertions;

using Futurum.Core.Result;
using Futurum.Core.Tests.Helper.Result;

using Xunit;

namespace Futurum.Core.Tests.Result;

public class ResultThenTryTests
{
    private const string ErrorMessage1 = "ERROR_MESSAGE_1";
    private const string ErrorMessage2 = "ERROR_MESSAGE_2";
    private const string ErrorMessage3 = "ERROR_MESSAGE_3";

    public class Sync
    {
        public class ReturnSync
        {
            public class NonGenericInput
            {
                [Fact]
                public void FailureInput_Exception()
                {
                    var resultInput = Core.Result.Result.Fail(ErrorMessage1);

                    var resultOutput = resultInput.ThenTry(() =>
                                                           {
                                                               throw new Exception(ErrorMessage2);

                                                               return Guid.NewGuid();
                                                           },
                                                           () => ErrorMessage3);

                    resultOutput.ShouldBeFailureWithError(ErrorMessage1);
                }

                [Fact]
                public void FailureInput_NoException()
                {
                    var resultInput = Core.Result.Result.Fail(ErrorMessage1);

                    var outputValue = Guid.NewGuid();

                    var resultOutput = resultInput.ThenTry(() => outputValue,
                                                           () => ErrorMessage3);

                    resultOutput.ShouldBeFailureWithError(ErrorMessage1);
                }

                [Fact]
                public void SuccessInput_Exception()
                {
                    var resultInput = Core.Result.Result.Ok();

                    var resultOutput = resultInput.ThenTry(() =>
                                                           {
                                                               throw new Exception(ErrorMessage1);

                                                               return Guid.NewGuid();
                                                           },
                                                           () => ErrorMessage3);

                    resultOutput.ShouldBeFailureWithErrorSafe($"{ErrorMessage3};{ErrorMessage1}");
                }

                [Fact]
                public void SuccessInput_NoException()
                {
                    var resultInput = Core.Result.Result.Ok();

                    var outputValue = Guid.NewGuid();

                    var resultOutput = resultInput.ThenTry(() => outputValue,
                                                           () => ErrorMessage3);

                    resultOutput.ShouldBeSuccessWithValue(outputValue);
                }
            }

            public class GenericInput
            {
                [Fact]
                public void FailureInput_Exception()
                {
                    var resultInput = Core.Result.Result.Fail<Guid>(ErrorMessage1);

                    var resultOutput = resultInput.ThenTry(_ =>
                                                           {
                                                               throw new Exception(ErrorMessage2);

                                                               return Guid.NewGuid();
                                                           },
                                                           () => ErrorMessage3);

                    resultOutput.ShouldBeFailureWithError(ErrorMessage1);
                }

                [Fact]
                public void FailureInput_NoException()
                {
                    var resultInput = Core.Result.Result.Fail<Guid>(ErrorMessage1);

                    var outputValue = Guid.NewGuid();

                    var resultOutput = resultInput.ThenTry(_ => outputValue,
                                                           () => ErrorMessage3);

                    resultOutput.ShouldBeFailureWithError(ErrorMessage1);
                }

                [Fact]
                public void SuccessInput_Exception()
                {
                    var inputValue = Guid.NewGuid();

                    var resultInput = Core.Result.Result.Ok(inputValue);

                    var passedValue = Guid.Empty;

                    var resultOutput = resultInput.ThenTry(value =>
                                                           {
                                                               passedValue = value;

                                                               throw new Exception(ErrorMessage1);

                                                               return Guid.NewGuid();
                                                           },
                                                           () => ErrorMessage3);

                    passedValue.Should().Be(inputValue);
                    resultOutput.ShouldBeFailureWithErrorSafe($"{ErrorMessage3};{ErrorMessage1}");
                }

                [Fact]
                public void SuccessInput_NoException()
                {
                    var inputValue = Guid.NewGuid();

                    var resultInput = Core.Result.Result.Ok(inputValue);

                    var passedValue = Guid.Empty;

                    var outputValue = Guid.NewGuid();

                    var resultOutput = resultInput.ThenTry(value =>
                                                           {
                                                               passedValue = value;

                                                               return outputValue;
                                                           },
                                                           () => ErrorMessage3);

                    passedValue.Should().Be(inputValue);
                    resultOutput.ShouldBeSuccessWithValue(outputValue);
                }
            }
            
            public class GenericInputAction
            {
                [Fact]
                public void FailureInput_Exception()
                {
                    var resultInput = Core.Result.Result.Fail<Guid>(ErrorMessage1);

                    var resultOutput = resultInput.ThenTry(_ =>
                                                           {
                                                               throw new Exception(ErrorMessage2);
                                                           },
                                                           _ => ErrorMessage3);

                    resultOutput.ShouldBeFailureWithError(ErrorMessage1);
                }

                [Fact]
                public void FailureInput_NoException()
                {
                    var resultInput = Core.Result.Result.Fail<Guid>(ErrorMessage1);

                    var resultOutput = resultInput.ThenTry(_ => {},
                                                           _ => ErrorMessage3);

                    resultOutput.ShouldBeFailureWithError(ErrorMessage1);
                }

                [Fact]
                public void SuccessInput_Exception()
                {
                    var inputValue = Guid.NewGuid();

                    var resultInput = Core.Result.Result.Ok(inputValue);

                    var passedValue = Guid.Empty;

                    var resultOutput = resultInput.ThenTry(value =>
                                                           {
                                                               passedValue = value;

                                                               throw new Exception(ErrorMessage1);
                                                           },
                                                           _ => ErrorMessage3);

                    passedValue.Should().Be(inputValue);
                    resultOutput.ShouldBeFailureWithErrorSafe($"{ErrorMessage3};{ErrorMessage1}");
                }

                [Fact]
                public void SuccessInput_NoException()
                {
                    var inputValue = Guid.NewGuid();

                    var resultInput = Core.Result.Result.Ok(inputValue);

                    var passedValue = Guid.Empty;

                    var outputValue = Guid.NewGuid();

                    var resultOutput = resultInput.ThenTry(value =>
                                                           {
                                                               passedValue = value;
                                                           },
                                                           _ => ErrorMessage3);

                    passedValue.Should().Be(inputValue);
                    resultOutput.ShouldBeSuccessWithValue(inputValue);
                }
            }
        }

        public class ReturnAsync
        {
            public class NonGenericInput
            {
                [Fact]
                public async Task FailureInput_Exception()
                {
                    var resultInput = Core.Result.Result.Fail(ErrorMessage1);

                    var resultOutput = await resultInput.ThenTryAsync(() =>
                                                                      {
                                                                          throw new Exception(ErrorMessage2);

                                                                          return Task.FromResult(
                                                                              Guid.NewGuid());
                                                                      },
                                                                      () => ErrorMessage3);

                    resultOutput.ShouldBeFailureWithError(ErrorMessage1);
                }

                [Fact]
                public async Task FailureInput_NoException()
                {
                    var resultInput = Core.Result.Result.Fail(ErrorMessage1);

                    var outputValue = Guid.NewGuid();

                    var resultOutput = await resultInput.ThenTryAsync(() => Task.FromResult(outputValue),
                                                                      () => ErrorMessage3);

                    resultOutput.ShouldBeFailureWithError(ErrorMessage1);
                }

                [Fact]
                public async Task SuccessInput_Exception()
                {
                    var resultInput = Core.Result.Result.Ok();

                    var resultOutput = await resultInput.ThenTryAsync(() =>
                                                                      {
                                                                          throw new Exception(ErrorMessage1);

                                                                          return Task.FromResult(
                                                                              Guid.NewGuid());
                                                                      },
                                                                      () => ErrorMessage3);

                    resultOutput.ShouldBeFailureWithErrorSafe($"{ErrorMessage3};{ErrorMessage1}");
                }

                [Fact]
                public async Task SuccessInput_NoException()
                {
                    var resultInput = Core.Result.Result.Ok();

                    var outputValue = Guid.NewGuid();

                    var resultOutput = await resultInput.ThenTryAsync(() => Task.FromResult(outputValue),
                                                                      () => ErrorMessage3);

                    resultOutput.ShouldBeSuccessWithValue(outputValue);
                }
            }

            public class GenericInput
            {
                [Fact]
                public async Task FailureInput_Exception()
                {
                    var resultInput = Core.Result.Result.Fail<Guid>(ErrorMessage1);

                    var resultOutput = await resultInput.ThenTryAsync(_ =>
                                                                      {
                                                                          throw new Exception(ErrorMessage2);

                                                                          return Task.FromResult(
                                                                              Guid.NewGuid());
                                                                      },
                                                                      () => ErrorMessage3);

                    resultOutput.ShouldBeFailureWithError(ErrorMessage1);
                }

                [Fact]
                public async Task FailureInput_NoException()
                {
                    var resultInput = Core.Result.Result.Fail<Guid>(ErrorMessage1);

                    var outputValue = Guid.NewGuid();

                    var resultOutput = await resultInput.ThenTryAsync(_ => Task.FromResult(outputValue),
                                                                      () => ErrorMessage3);

                    resultOutput.ShouldBeFailureWithError(ErrorMessage1);
                }

                [Fact]
                public async Task SuccessInput_Exception()
                {
                    var inputValue = Guid.NewGuid();

                    var resultInput = Core.Result.Result.Ok(inputValue);

                    var passedValue = Guid.Empty;

                    var resultOutput = await resultInput.ThenTryAsync(value =>
                                                                      {
                                                                          passedValue = value;

                                                                          throw new Exception(ErrorMessage1);

                                                                          return Task.FromResult(
                                                                              Guid.NewGuid());
                                                                      },
                                                                      () => ErrorMessage3);

                    passedValue.Should().Be(inputValue);
                    resultOutput.ShouldBeFailureWithErrorSafe($"{ErrorMessage3};{ErrorMessage1}");
                }

                [Fact]
                public async Task SuccessInput_NoException()
                {
                    var inputValue = Guid.NewGuid();

                    var resultInput = Core.Result.Result.Ok(inputValue);

                    var passedValue = Guid.Empty;

                    var outputValue = Guid.NewGuid();

                    var resultOutput = await resultInput.ThenTryAsync(value =>
                                                                      {
                                                                          passedValue = value;

                                                                          return Task.FromResult(outputValue);
                                                                      },
                                                                      () => ErrorMessage3);

                    passedValue.Should().Be(inputValue);
                    resultOutput.ShouldBeSuccessWithValue(outputValue);
                }
            }
        }
    }

    public class Async
    {
        public class ReturnSync
        {
            public class NonGenericInput
            {
                [Fact]
                public async Task FailureInput_Exception()
                {
                    var resultInput = Core.Result.Result.FailAsync(ErrorMessage1);

                    var resultOutput = await resultInput.ThenTryAsync(() =>
                                                                      {
                                                                          throw new Exception(ErrorMessage2);

                                                                          return Guid.NewGuid();
                                                                      },
                                                                      () => ErrorMessage3);

                    resultOutput.ShouldBeFailureWithError(ErrorMessage1);
                }

                [Fact]
                public async Task FailureInput_NoException()
                {
                    var resultInput = Core.Result.Result.FailAsync(ErrorMessage1);

                    var outputValue = Guid.NewGuid();

                    var resultOutput = await resultInput.ThenTryAsync(() => outputValue,
                                                                      () => ErrorMessage3);

                    resultOutput.ShouldBeFailureWithError(ErrorMessage1);
                }

                [Fact]
                public async Task SuccessInput_Exception()
                {
                    var resultInput = Core.Result.Result.OkAsync();

                    var resultOutput = await resultInput.ThenTryAsync(() =>
                                                                      {
                                                                          throw new Exception(ErrorMessage1);

                                                                          return Guid.NewGuid();
                                                                      },
                                                                      () => ErrorMessage3);

                    resultOutput.ShouldBeFailureWithErrorSafe($"{ErrorMessage3};{ErrorMessage1}");
                }

                [Fact]
                public async Task SuccessInput_NoException()
                {
                    var resultInput = Core.Result.Result.OkAsync();

                    var outputValue = Guid.NewGuid();

                    var resultOutput = await resultInput.ThenTryAsync(() => outputValue,
                                                                      () => ErrorMessage3);

                    resultOutput.ShouldBeSuccessWithValue(outputValue);
                }
            }

            public class GenericInput
            {
                [Fact]
                public async Task FailureInput_Exception()
                {
                    var resultInput = Core.Result.Result.FailAsync<Guid>(ErrorMessage1);

                    var resultOutput = await resultInput.ThenTryAsync(_ =>
                                                                      {
                                                                          throw new Exception(ErrorMessage2);

                                                                          return Guid.NewGuid();
                                                                      },
                                                                      () => ErrorMessage3);

                    resultOutput.ShouldBeFailureWithError(ErrorMessage1);
                }

                [Fact]
                public async Task FailureInput_NoException()
                {
                    var resultInput = Core.Result.Result.FailAsync<Guid>(ErrorMessage1);

                    var outputValue = Guid.NewGuid();

                    var resultOutput = await resultInput.ThenTryAsync(_ => outputValue,
                                                                      () => ErrorMessage3);

                    resultOutput.ShouldBeFailureWithError(ErrorMessage1);
                }

                [Fact]
                public async Task SuccessInput_Exception()
                {
                    var inputValue = Guid.NewGuid();

                    var resultInput = Core.Result.Result.OkAsync(inputValue);

                    var passedValue = Guid.Empty;

                    var resultOutput = await resultInput.ThenTryAsync(value =>
                                                                      {
                                                                          passedValue = value;

                                                                          throw new Exception(ErrorMessage1);

                                                                          return Guid.NewGuid();
                                                                      },
                                                                      () => ErrorMessage3);

                    passedValue.Should().Be(inputValue);
                    resultOutput.ShouldBeFailureWithErrorSafe($"{ErrorMessage3};{ErrorMessage1}");
                }

                [Fact]
                public async Task SuccessInput_NoException()
                {
                    var inputValue = Guid.NewGuid();

                    var resultInput = Core.Result.Result.OkAsync(inputValue);

                    var passedValue = Guid.Empty;

                    var outputValue = Guid.NewGuid();

                    var resultOutput = await resultInput.ThenTryAsync(value =>
                                                                      {
                                                                          passedValue = value;

                                                                          return outputValue;
                                                                      },
                                                                      () => ErrorMessage3);

                    passedValue.Should().Be(inputValue);
                    resultOutput.ShouldBeSuccessWithValue(outputValue);
                }
            }
        }

        public class ReturnAsync
        {
            public class NonGenericInput
            {
                [Fact]
                public async Task FailureInput_Exception()
                {
                    var resultInput = Core.Result.Result.FailAsync(ErrorMessage1);

                    var resultOutput = await resultInput.ThenTryAsync(() =>
                                                                      {
                                                                          throw new Exception(ErrorMessage2);

                                                                          return Task.FromResult(
                                                                              Guid.NewGuid());
                                                                      },
                                                                      () => ErrorMessage3);

                    resultOutput.ShouldBeFailureWithError(ErrorMessage1);
                }

                [Fact]
                public async Task FailureInput_NoException()
                {
                    var resultInput = Core.Result.Result.FailAsync(ErrorMessage1);

                    var outputValue = Guid.NewGuid();

                    var resultOutput = await resultInput.ThenTryAsync(() => Task.FromResult(outputValue),
                                                                      () => ErrorMessage3);

                    resultOutput.ShouldBeFailureWithError(ErrorMessage1);
                }

                [Fact]
                public async Task SuccessInput_Exception()
                {
                    var resultInput = Core.Result.Result.OkAsync();

                    var resultOutput = await resultInput.ThenTryAsync(() =>
                                                                      {
                                                                          throw new Exception(ErrorMessage1);

                                                                          return Task.FromResult(
                                                                              Guid.NewGuid());
                                                                      },
                                                                      () => ErrorMessage3);

                    resultOutput.ShouldBeFailureWithErrorSafe($"{ErrorMessage3};{ErrorMessage1}");
                }

                [Fact]
                public async Task SuccessInput_NoException()
                {
                    var resultInput = Core.Result.Result.OkAsync();

                    var outputValue = Guid.NewGuid();

                    var resultOutput = await resultInput.ThenTryAsync(() => Task.FromResult(outputValue),
                                                                      () => ErrorMessage3);

                    resultOutput.ShouldBeSuccessWithValue(outputValue);
                }
            }

            public class GenericInput
            {
                [Fact]
                public async Task FailureInput_Exception()
                {
                    var resultInput = Core.Result.Result.FailAsync<Guid>(ErrorMessage1);

                    var resultOutput = await resultInput.ThenTryAsync(_ =>
                                                                      {
                                                                          throw new Exception(ErrorMessage2);

                                                                          return Task.FromResult(
                                                                              Guid.NewGuid());
                                                                      },
                                                                      () => ErrorMessage3);

                    resultOutput.ShouldBeFailureWithError(ErrorMessage1);
                }

                [Fact]
                public async Task FailureInput_NoException()
                {
                    var resultInput = Core.Result.Result.FailAsync<Guid>(ErrorMessage1);

                    var outputValue = Guid.NewGuid();

                    var resultOutput = await resultInput.ThenTryAsync(_ => Task.FromResult(outputValue),
                                                                      () => ErrorMessage3);

                    resultOutput.ShouldBeFailureWithError(ErrorMessage1);
                }

                [Fact]
                public async Task SuccessInput_Exception()
                {
                    var inputValue = Guid.NewGuid();

                    var resultInput = Core.Result.Result.OkAsync(inputValue);

                    var passedValue = Guid.Empty;

                    var resultOutput = await resultInput.ThenTryAsync(value =>
                                                                      {
                                                                          passedValue = value;

                                                                          throw new Exception(ErrorMessage1);

                                                                          return Task.FromResult(
                                                                              Guid.NewGuid());
                                                                      },
                                                                      () => ErrorMessage3);

                    passedValue.Should().Be(inputValue);
                    resultOutput.ShouldBeFailureWithErrorSafe($"{ErrorMessage3};{ErrorMessage1}");
                }

                [Fact]
                public async Task SuccessInput_NoException()
                {
                    var inputValue = Guid.NewGuid();

                    var resultInput = Core.Result.Result.OkAsync(inputValue);

                    var passedValue = Guid.Empty;

                    var outputValue = Guid.NewGuid();

                    var resultOutput = await resultInput.ThenTryAsync(value =>
                                                                      {
                                                                          passedValue = value;

                                                                          return Task.FromResult(outputValue);
                                                                      },
                                                                      () => ErrorMessage3);

                    passedValue.Should().Be(inputValue);
                    resultOutput.ShouldBeSuccessWithValue(outputValue);
                }
            }
        }
    }
}