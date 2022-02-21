using System;
using System.Threading.Tasks;

using FluentAssertions;

using Futurum.Core.Result;
using Futurum.Core.Tests.Helper.Result;

using Xunit;

namespace Futurum.Core.Tests.Result;

public class ResultDoSwitchTests
{
    private const string ErrorMessage = "ERROR_MESSAGE_1";

    public class Sync
    {
        public class NonGeneric
        {
            public class Sync
            {
                public class WithoutErrorPayloadOnFailure
                {
                    [Fact]
                    public void FailureInput_Matches()
                    {
                        var trueWasCalled = false;
                        var falseWasCalled = false;

                        var resultInput = Core.Result.Result.Fail(ErrorMessage);

                        var returnResult = resultInput.DoSwitch(() => { trueWasCalled = true; },
                                                                () => { falseWasCalled = true; });

                        returnResult.ShouldBeFailureWithError(ErrorMessage);
                        trueWasCalled.Should().BeFalse();
                        falseWasCalled.Should().BeTrue();
                    }

                    [Fact]
                    public void SuccessInput()
                    {
                        var trueWasCalled = false;
                        var falseWasCalled = false;

                        var resultInput = Core.Result.Result.Ok();

                        var returnResult = resultInput.DoSwitch(() => { trueWasCalled = true; },
                                                                () => { falseWasCalled = true; });

                        returnResult.ShouldBeSuccess();
                        trueWasCalled.Should().BeTrue();
                        falseWasCalled.Should().BeFalse();
                    }
                }

                public class WithErrorPayloadOnFailure
                {
                    [Fact]
                    public void FailureInput_Matches()
                    {
                        var trueWasCalled = false;
                        var falseWasCalled = false;

                        IResultError passedError = null;

                        var resultInput = Core.Result.Result.Fail(ErrorMessage);

                        var returnResult = resultInput.DoSwitch(() => { trueWasCalled = true; },
                                                                error =>
                                                                {
                                                                    passedError = error;

                                                                    falseWasCalled = true;
                                                                });

                        returnResult.ShouldBeFailureWithError(ErrorMessage);
                        trueWasCalled.Should().BeFalse();
                        falseWasCalled.Should().BeTrue();
                        passedError.ShouldBeError(ErrorMessage);
                    }

                    [Fact]
                    public void SuccessInput()
                    {
                        var trueWasCalled = false;
                        var falseWasCalled = false;

                        var resultInput = Core.Result.Result.Ok();

                        var returnResult = resultInput.DoSwitch(() => { trueWasCalled = true; },
                                                                _ => { falseWasCalled = true; });

                        returnResult.ShouldBeSuccess();
                        trueWasCalled.Should().BeTrue();
                        falseWasCalled.Should().BeFalse();
                    }
                }
            }

            public class Async
            {
                public class WithoutErrorPayloadOnFailure
                {
                    [Fact]
                    public async Task FailureInput_Matches()
                    {
                        var trueWasCalled = false;
                        var falseWasCalled = false;

                        var resultInput = Core.Result.Result.Fail(ErrorMessage);

                        var returnResult = await resultInput.DoSwitchAsync(() =>
                                                                           {
                                                                               trueWasCalled = true;
                                                                               return Task.CompletedTask;
                                                                           },
                                                                           () =>
                                                                           {
                                                                               falseWasCalled = true;
                                                                               return Task.CompletedTask;
                                                                           });

                        returnResult.ShouldBeFailureWithError(ErrorMessage);
                        trueWasCalled.Should().BeFalse();
                        falseWasCalled.Should().BeTrue();
                    }

                    [Fact]
                    public async Task SuccessInput()
                    {
                        var trueWasCalled = false;
                        var falseWasCalled = false;

                        var resultInput = Core.Result.Result.Ok();

                        var returnResult = await resultInput.DoSwitchAsync(() =>
                                                                           {
                                                                               trueWasCalled = true;
                                                                               return Task.CompletedTask;
                                                                           },
                                                                           () =>
                                                                           {
                                                                               falseWasCalled = true;
                                                                               return Task.CompletedTask;
                                                                           });

                        returnResult.ShouldBeSuccess();
                        trueWasCalled.Should().BeTrue();
                        falseWasCalled.Should().BeFalse();
                    }
                }

                public class WithErrorPayloadOnFailure
                {
                    [Fact]
                    public async Task FailureInput_Matches()
                    {
                        var trueWasCalled = false;
                        var falseWasCalled = false;

                        IResultError passedError = null;

                        var resultInput = Core.Result.Result.Fail(ErrorMessage);

                        var returnResult = await resultInput.DoSwitchAsync(() =>
                                                                           {
                                                                               trueWasCalled = true;
                                                                               return Task.CompletedTask;
                                                                           },
                                                                           error =>
                                                                           {
                                                                               passedError = error;

                                                                               falseWasCalled = true;

                                                                               return Task.CompletedTask;
                                                                           });

                        returnResult.ShouldBeFailureWithError(ErrorMessage);
                        trueWasCalled.Should().BeFalse();
                        falseWasCalled.Should().BeTrue();
                        passedError.ShouldBeError(ErrorMessage);
                    }

                    [Fact]
                    public async Task SuccessInput()
                    {
                        var trueWasCalled = false;
                        var falseWasCalled = false;

                        var resultInput = Core.Result.Result.Ok();

                        var returnResult = await resultInput.DoSwitchAsync(() =>
                                                                           {
                                                                               trueWasCalled = true;
                                                                               return Task.CompletedTask;
                                                                           },
                                                                           _ =>
                                                                           {
                                                                               falseWasCalled = true;
                                                                               return Task.CompletedTask;
                                                                           });

                        returnResult.ShouldBeSuccess();
                        trueWasCalled.Should().BeTrue();
                        falseWasCalled.Should().BeFalse();
                    }
                }
            }
        }

        public class Generic
        {
            public class Sync
            {
                public class WithoutErrorPayloadOnFailure
                {
                    [Fact]
                    public void FailureInput_Matches()
                    {
                        var trueWasCalled = false;
                        var falseWasCalled = false;

                        var resultInput = Core.Result.Result.Fail<Guid>(ErrorMessage);

                        var returnResult = resultInput.DoSwitch(_ => { trueWasCalled = true; },
                                                                () => { falseWasCalled = true; });

                        returnResult.ShouldBeFailureWithError(ErrorMessage);
                        trueWasCalled.Should().BeFalse();
                        falseWasCalled.Should().BeTrue();
                    }

                    [Fact]
                    public void SuccessInput()
                    {
                        var inputValue = Guid.NewGuid();

                        var trueWasCalled = false;
                        var falseWasCalled = false;

                        var passedValue = Guid.Empty;

                        var resultInput = Core.Result.Result.Ok(inputValue);

                        var returnResult = resultInput.DoSwitch(value =>
                                                                {
                                                                    passedValue = value;

                                                                    trueWasCalled = true;
                                                                },
                                                                () => { falseWasCalled = true; });

                        returnResult.ShouldBeSuccessWithValue(inputValue);
                        trueWasCalled.Should().BeTrue();
                        falseWasCalled.Should().BeFalse();
                        passedValue.Should().Be(inputValue);
                    }
                }

                public class WithErrorPayloadOnFailure
                {
                    [Fact]
                    public void FailureInput_Matches()
                    {
                        var trueWasCalled = false;
                        var falseWasCalled = false;

                        IResultError passedError = null;

                        var resultInput = Core.Result.Result.Fail<Guid>(ErrorMessage);

                        var returnResult = resultInput.DoSwitch(_ => { trueWasCalled = true; },
                                                                error =>
                                                                {
                                                                    passedError = error;

                                                                    falseWasCalled = true;
                                                                });

                        returnResult.ShouldBeFailureWithError(ErrorMessage);
                        trueWasCalled.Should().BeFalse();
                        falseWasCalled.Should().BeTrue();
                        passedError.ShouldBeError(ErrorMessage);
                    }

                    [Fact]
                    public void SuccessInput()
                    {
                        var inputValue = Guid.NewGuid();

                        var trueWasCalled = false;
                        var falseWasCalled = false;

                        var passedValue = Guid.Empty;

                        var resultInput = Core.Result.Result.Ok(inputValue);

                        var returnResult = resultInput.DoSwitch(value =>
                                                                {
                                                                    passedValue = value;

                                                                    trueWasCalled = true;
                                                                },
                                                                _ => { falseWasCalled = true; });

                        returnResult.ShouldBeSuccessWithValue(inputValue);
                        trueWasCalled.Should().BeTrue();
                        falseWasCalled.Should().BeFalse();
                        passedValue.Should().Be(inputValue);
                    }
                }
            }

            public class Async
            {
                public class WithoutErrorPayloadOnFailure
                {
                    [Fact]
                    public async Task FailureInput_Matches()
                    {
                        var trueWasCalled = false;
                        var falseWasCalled = false;

                        var resultInput = Core.Result.Result.Fail<Guid>(ErrorMessage);

                        var returnResult = await resultInput.DoSwitchAsync(_ =>
                                                                           {
                                                                               trueWasCalled = true;
                                                                               return Task.CompletedTask;
                                                                           },
                                                                           () =>
                                                                           {
                                                                               falseWasCalled = true;
                                                                               return Task.CompletedTask;
                                                                           });

                        returnResult.ShouldBeFailureWithError(ErrorMessage);
                        trueWasCalled.Should().BeFalse();
                        falseWasCalled.Should().BeTrue();
                    }

                    [Fact]
                    public async Task SuccessInput()
                    {
                        var inputValue = Guid.NewGuid();

                        var trueWasCalled = false;
                        var falseWasCalled = false;

                        var passedValue = Guid.Empty;

                        var resultInput = Core.Result.Result.Ok(inputValue);

                        var returnResult = await resultInput.DoSwitchAsync(value =>
                                                                           {
                                                                               passedValue = value;

                                                                               trueWasCalled = true;

                                                                               return Task.CompletedTask;
                                                                           },
                                                                           () =>
                                                                           {
                                                                               falseWasCalled = true;
                                                                               return Task.CompletedTask;
                                                                           });

                        returnResult.ShouldBeSuccessWithValue(inputValue);
                        trueWasCalled.Should().BeTrue();
                        falseWasCalled.Should().BeFalse();
                        passedValue.Should().Be(inputValue);
                    }
                }

                public class WithErrorPayloadOnFailure
                {
                    [Fact]
                    public async Task FailureInput_Matches()
                    {
                        var trueWasCalled = false;
                        var falseWasCalled = false;

                        IResultError passedError = null;

                        var resultInput = Core.Result.Result.Fail<Guid>(ErrorMessage);

                        var returnResult = await resultInput.DoSwitchAsync(_ =>
                                                                           {
                                                                               trueWasCalled = true;
                                                                               return Task.CompletedTask;
                                                                           },
                                                                           error =>
                                                                           {
                                                                               passedError = error;

                                                                               falseWasCalled = true;

                                                                               return Task.CompletedTask;
                                                                           });

                        returnResult.ShouldBeFailureWithError(ErrorMessage);
                        trueWasCalled.Should().BeFalse();
                        falseWasCalled.Should().BeTrue();
                        passedError.ShouldBeError(ErrorMessage);
                    }

                    [Fact]
                    public async Task SuccessInput()
                    {
                        var inputValue = Guid.NewGuid();

                        var trueWasCalled = false;
                        var falseWasCalled = false;

                        var passedValue = Guid.Empty;

                        var resultInput = Core.Result.Result.Ok(inputValue);

                        var returnResult = await resultInput.DoSwitchAsync(value =>
                                                                           {
                                                                               passedValue = value;

                                                                               trueWasCalled = true;

                                                                               return Task.CompletedTask;
                                                                           },
                                                                           _ =>
                                                                           {
                                                                               falseWasCalled = true;
                                                                               return Task.CompletedTask;
                                                                           });

                        returnResult.ShouldBeSuccessWithValue(inputValue);
                        trueWasCalled.Should().BeTrue();
                        falseWasCalled.Should().BeFalse();
                        passedValue.Should().Be(inputValue);
                    }
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
                    var trueWasCalled = false;
                    var falseWasCalled = false;

                    var resultInput = Core.Result.Result.FailAsync(ErrorMessage);

                    var returnResult = await resultInput.DoSwitchAsync(() => { trueWasCalled = true; },
                                                                       () => { falseWasCalled = true; });

                    returnResult.ShouldBeFailureWithError(ErrorMessage);
                    trueWasCalled.Should().BeFalse();
                    falseWasCalled.Should().BeTrue();
                }

                [Fact]
                public async Task SuccessInput()
                {
                    var trueWasCalled = false;
                    var falseWasCalled = false;

                    var resultInput = Core.Result.Result.OkAsync();

                    var returnResult = await resultInput.DoSwitchAsync(() => { trueWasCalled = true; },
                                                                       () => { falseWasCalled = true; });

                    returnResult.ShouldBeSuccess();
                    trueWasCalled.Should().BeTrue();
                    falseWasCalled.Should().BeFalse();
                }
            }

            public class WithErrorPayloadOnFailure
            {
                [Fact]
                public async Task FailureInput_Matches()
                {
                    var trueWasCalled = false;
                    var falseWasCalled = false;

                    IResultError passedError = null;

                    var resultInput = Core.Result.Result.FailAsync(ErrorMessage);

                    var returnResult = await resultInput.DoSwitchAsync(() => { trueWasCalled = true; },
                                                                       error =>
                                                                       {
                                                                           passedError = error;

                                                                           falseWasCalled = true;
                                                                       });

                    returnResult.ShouldBeFailureWithError(ErrorMessage);
                    trueWasCalled.Should().BeFalse();
                    falseWasCalled.Should().BeTrue();
                    passedError.ShouldBeError(ErrorMessage);
                }

                [Fact]
                public async Task SuccessInput()
                {
                    var trueWasCalled = false;
                    var falseWasCalled = false;

                    var resultInput = Core.Result.Result.OkAsync();

                    var returnResult = await resultInput.DoSwitchAsync(() => { trueWasCalled = true; },
                                                                       _ => { falseWasCalled = true; });

                    returnResult.ShouldBeSuccess();
                    trueWasCalled.Should().BeTrue();
                    falseWasCalled.Should().BeFalse();
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
                    var trueWasCalled = false;
                    var falseWasCalled = false;

                    var resultInput = Core.Result.Result.FailAsync<Guid>(ErrorMessage);

                    var returnResult = await resultInput.DoSwitchAsync(_ => { trueWasCalled = true; },
                                                                       () => { falseWasCalled = true; });

                    returnResult.ShouldBeFailureWithError(ErrorMessage);
                    trueWasCalled.Should().BeFalse();
                    falseWasCalled.Should().BeTrue();
                }

                [Fact]
                public async Task SuccessInput()
                {
                    var inputValue = Guid.NewGuid();

                    var trueWasCalled = false;
                    var falseWasCalled = false;

                    var passedValue = Guid.Empty;

                    var resultInput = Core.Result.Result.OkAsync(inputValue);

                    var returnResult = await resultInput.DoSwitchAsync(value =>
                                                                       {
                                                                           passedValue = value;

                                                                           trueWasCalled = true;
                                                                       },
                                                                       () => { falseWasCalled = true; });

                    returnResult.ShouldBeSuccessWithValue(inputValue);
                    trueWasCalled.Should().BeTrue();
                    falseWasCalled.Should().BeFalse();
                    passedValue.Should().Be(inputValue);
                }
            }

            public class WithErrorPayloadOnFailure
            {
                [Fact]
                public async Task FailureInput_Matches()
                {
                    var trueWasCalled = false;
                    var falseWasCalled = false;

                    IResultError passedError = null;

                    var resultInput = Core.Result.Result.FailAsync<Guid>(ErrorMessage);

                    var returnResult = await resultInput.DoSwitchAsync(_ => { trueWasCalled = true; },
                                                                       error =>
                                                                       {
                                                                           passedError = error;

                                                                           falseWasCalled = true;
                                                                       });

                    returnResult.ShouldBeFailureWithError(ErrorMessage);
                    trueWasCalled.Should().BeFalse();
                    falseWasCalled.Should().BeTrue();
                    passedError.ShouldBeError(ErrorMessage);
                }

                [Fact]
                public async Task SuccessInput()
                {
                    var inputValue = Guid.NewGuid();

                    var trueWasCalled = false;
                    var falseWasCalled = false;

                    var passedValue = Guid.Empty;

                    var resultInput = Core.Result.Result.OkAsync(inputValue);

                    var returnResult = await resultInput.DoSwitchAsync(value =>
                                                                       {
                                                                           passedValue = value;

                                                                           trueWasCalled = true;
                                                                       },
                                                                       _ => { falseWasCalled = true; });

                    returnResult.ShouldBeSuccessWithValue(inputValue);
                    trueWasCalled.Should().BeTrue();
                    falseWasCalled.Should().BeFalse();
                    passedValue.Should().Be(inputValue);
                }
            }
        }
    }
}