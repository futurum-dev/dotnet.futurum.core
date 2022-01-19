using System;
using System.Threading.Tasks;

using FluentAssertions;

using Futurum.Core.Result;
using Futurum.Core.Tests.Helper.Result;

using Xunit;

namespace Futurum.Core.Tests.Result;

public class ResultSwitchTests
{
    private const string ErrorMessage = "ERROR_MESSAGE_1";

    public class Sync
    {
        public class NonGeneric
        {
            public class WithoutErrorPayloadOnFailure
            {
                [Fact]
                public void FailureInput_Matches()
                {
                    var trueValue = Guid.NewGuid();
                    var falseValue = Guid.NewGuid();

                    var resultInput = Core.Result.Result.Fail(ErrorMessage);

                    var returnValue = resultInput.Switch(() => trueValue,
                                                         () => falseValue);

                    returnValue.Should().Be(falseValue);
                }

                [Fact]
                public void SuccessInput()
                {
                    var trueValue = Guid.NewGuid();
                    var falseValue = Guid.NewGuid();

                    var resultInput = Core.Result.Result.Ok();

                    var returnValue = resultInput.Switch(() => trueValue,
                                                         () => falseValue);

                    returnValue.Should().Be(trueValue);
                }
            }

            public class WithErrorPayloadOnFailure
            {
                [Fact]
                public void FailureInput_Matches()
                {
                    var trueValue = Guid.NewGuid();
                    var falseValue = Guid.NewGuid();

                    IResultError passedError = null;

                    var resultInput = Core.Result.Result.Fail(ErrorMessage);

                    var returnValue = resultInput.Switch(() => trueValue,
                                                         error =>
                                                         {
                                                             passedError = error;

                                                             return falseValue;
                                                         });

                    returnValue.Should().Be(falseValue);
                    passedError.ShouldBeError(ErrorMessage);
                }

                [Fact]
                public void SuccessInput()
                {
                    var trueValue = Guid.NewGuid();
                    var falseValue = Guid.NewGuid();

                    var resultInput = Core.Result.Result.Ok();

                    var returnValue = resultInput.Switch(() => trueValue,
                                                         _ => falseValue);

                    returnValue.Should().Be(trueValue);
                }
            }
        }

        public class Generic
        {
            public class WithoutErrorPayloadOnFailure
            {
                [Fact]
                public void FailureInput_Matches()
                {
                    var trueValue = Guid.NewGuid();
                    var falseValue = Guid.NewGuid();

                    var resultInput = Core.Result.Result.Fail<Guid>(ErrorMessage);

                    var returnValue = resultInput.Switch(value => trueValue,
                                                         () => falseValue);

                    returnValue.Should().Be(falseValue);
                }

                [Fact]
                public void SuccessInput()
                {
                    var inputValue = Guid.NewGuid();

                    var trueValue = Guid.NewGuid();
                    var falseValue = Guid.NewGuid();

                    var passedValue = Guid.Empty;

                    var resultInput = Core.Result.Result.Ok(inputValue);

                    var returnValue = resultInput.Switch(value =>
                                                         {
                                                             passedValue = value;

                                                             return trueValue;
                                                         },
                                                         () => falseValue);

                    passedValue.Should().Be(inputValue);
                    returnValue.Should().Be(trueValue);
                }
            }

            public class WithErrorPayloadOnFailure
            {
                [Fact]
                public void FailureInput_Matches()
                {
                    var trueValue = Guid.NewGuid();
                    var falseValue = Guid.NewGuid();

                    IResultError passedError = null;

                    var resultInput = Core.Result.Result.Fail<Guid>(ErrorMessage);

                    var returnValue = resultInput.Switch(value => trueValue,
                                                         error =>
                                                         {
                                                             passedError = error;

                                                             return falseValue;
                                                         });

                    returnValue.Should().Be(falseValue);
                    passedError.ShouldBeError(ErrorMessage);
                }

                [Fact]
                public void SuccessInput()
                {
                    var inputValue = Guid.NewGuid();

                    var trueValue = Guid.NewGuid();
                    var falseValue = Guid.NewGuid();

                    var passedValue = Guid.Empty;

                    var resultInput = Core.Result.Result.Ok(inputValue);

                    var returnValue = resultInput.Switch(value =>
                                                         {
                                                             passedValue = value;

                                                             return trueValue;
                                                         },
                                                         _ => falseValue);

                    passedValue.Should().Be(inputValue);
                    returnValue.Should().Be(trueValue);
                }
            }
        }
    }

    public class Async
    {
        public class NonGeneric
        {
            public class WithoutErrorPayloadOnFailure
            {
                [Fact]
                public async Task FailureInput_Matches()
                {
                    var trueValue = Guid.NewGuid();
                    var falseValue = Guid.NewGuid();

                    var resultInput = Core.Result.Result.FailAsync(ErrorMessage);

                    var returnValue = await resultInput.SwitchAsync(() => trueValue,
                                                                    () => falseValue);

                    returnValue.Should().Be(falseValue);
                }

                [Fact]
                public async Task SuccessInput()
                {
                    var trueValue = Guid.NewGuid();
                    var falseValue = Guid.NewGuid();

                    var resultInput = Core.Result.Result.OkAsync();

                    var returnValue = await resultInput.SwitchAsync(() => trueValue,
                                                                    () => falseValue);

                    returnValue.Should().Be(trueValue);
                }
            }

            public class WithErrorPayloadOnFailure
            {
                [Fact]
                public async Task FailureInput_Matches()
                {
                    var trueValue = Guid.NewGuid();
                    var falseValue = Guid.NewGuid();

                    IResultError passedError = null;

                    var resultInput = Core.Result.Result.FailAsync(ErrorMessage);

                    var returnValue = await resultInput.SwitchAsync(() => trueValue,
                                                                    error =>
                                                                    {
                                                                        passedError = error;

                                                                        return falseValue;
                                                                    });

                    returnValue.Should().Be(falseValue);
                    passedError.ShouldBeError(ErrorMessage);
                }

                [Fact]
                public async Task SuccessInput()
                {
                    var trueValue = Guid.NewGuid();
                    var falseValue = Guid.NewGuid();

                    var resultInput = Core.Result.Result.OkAsync();

                    var returnValue = await resultInput.SwitchAsync(() => trueValue,
                                                                    _ => falseValue);

                    returnValue.Should().Be(trueValue);
                }
            }
        }

        public class Generic
        {
            public class WithoutErrorPayloadOnFailure
            {
                [Fact]
                public async Task FailureInput_Matches()
                {
                    var trueValue = Guid.NewGuid();
                    var falseValue = Guid.NewGuid();

                    var resultInput = Core.Result.Result.FailAsync<Guid>(ErrorMessage);

                    var returnValue = await resultInput.SwitchAsync(value => trueValue,
                                                                    () => falseValue);

                    returnValue.Should().Be(falseValue);
                }

                [Fact]
                public async Task SuccessInput()
                {
                    var inputValue = Guid.NewGuid();

                    var trueValue = Guid.NewGuid();
                    var falseValue = Guid.NewGuid();

                    var passedValue = Guid.Empty;

                    var resultInput = Core.Result.Result.OkAsync(inputValue);

                    var returnValue = await resultInput.SwitchAsync(value =>
                                                                    {
                                                                        passedValue = value;

                                                                        return trueValue;
                                                                    },
                                                                    () => falseValue);

                    passedValue.Should().Be(inputValue);
                    returnValue.Should().Be(trueValue);
                }
            }

            public class WithErrorPayloadOnFailure
            {
                [Fact]
                public async Task FailureInput_Matches()
                {
                    var trueValue = Guid.NewGuid();
                    var falseValue = Guid.NewGuid();

                    IResultError passedError = null;

                    var resultInput = Core.Result.Result.FailAsync<Guid>(ErrorMessage);

                    var returnValue = await resultInput.SwitchAsync(value => trueValue,
                                                                    error =>
                                                                    {
                                                                        passedError = error;

                                                                        return falseValue;
                                                                    });

                    returnValue.Should().Be(falseValue);
                    passedError.ShouldBeError(ErrorMessage);
                }

                [Fact]
                public async Task SuccessInput()
                {
                    var inputValue = Guid.NewGuid();

                    var trueValue = Guid.NewGuid();
                    var falseValue = Guid.NewGuid();

                    var passedValue = Guid.Empty;

                    var resultInput = Core.Result.Result.OkAsync(inputValue);

                    var returnValue = await resultInput.SwitchAsync(value =>
                                                                    {
                                                                        passedValue = value;

                                                                        return trueValue;
                                                                    },
                                                                    _ => falseValue);

                    passedValue.Should().Be(inputValue);
                    returnValue.Should().Be(trueValue);
                }
            }

            public class WithAsyncFuncWithErrorPayloadOnFailure
            {
                [Fact]
                public async Task FailureInput_Matches()
                {
                    var trueValue = Guid.NewGuid();
                    var falseValue = Guid.NewGuid();

                    IResultError passedError = null;

                    var resultInput = Core.Result.Result.FailAsync<Guid>(ErrorMessage);

                    var returnValue = await resultInput.SwitchAsync(value => Task.FromResult(trueValue),
                                                                    error =>
                                                                    {
                                                                        passedError = error;

                                                                        return Task.FromResult(falseValue);
                                                                    });

                    returnValue.Should().Be(falseValue);
                    passedError.ShouldBeError(ErrorMessage);
                }

                [Fact]
                public async Task SuccessInput()
                {
                    var inputValue = Guid.NewGuid();

                    var trueValue = Guid.NewGuid();
                    var falseValue = Guid.NewGuid();

                    var passedValue = Guid.Empty;

                    var resultInput = Core.Result.Result.OkAsync(inputValue);

                    var returnValue = await resultInput.SwitchAsync(value =>
                                                                    {
                                                                        passedValue = value;

                                                                        return Task.FromResult(trueValue);
                                                                    },
                                                                    _ => Task.FromResult(falseValue));

                    passedValue.Should().Be(inputValue);
                    returnValue.Should().Be(trueValue);
                }
            }
        }
    }
}