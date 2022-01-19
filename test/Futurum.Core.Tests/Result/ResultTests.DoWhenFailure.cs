using System;
using System.Threading.Tasks;

using FluentAssertions;

using Futurum.Core.Result;
using Futurum.Core.Tests.Helper.Result;

using Xunit;

namespace Futurum.Core.Tests.Result;

public class ResultDoWhenFailureTests
{
    private const string ErrorMessage = "ERROR_MESSAGE_1";

    public class Sync
    {
        public class ReturnSync
        {
            public class WithErrorPayload
            {
                public class NonGenericInput
                {
                    [Fact]
                    public void Failure()
                    {
                        var resultInput = Core.Result.Result.Fail(ErrorMessage);

                        var wasCalled = false;

                        IResultError passedResultError = null;

                        var returnedResult = resultInput.DoWhenFailure(error =>
                        {
                            passedResultError = error;

                            wasCalled = true;
                        });

                        wasCalled.Should().BeTrue();
                        passedResultError.ToErrorString().Should().Be(ErrorMessage);
                        returnedResult.ShouldBeFailureWithError(ErrorMessage);
                    }

                    [Fact]
                    public void Success()
                    {
                        var resultInput = Core.Result.Result.Ok();

                        var wasCalled = false;

                        var returnedResult = resultInput.DoWhenFailure(error => wasCalled = true);

                        wasCalled.Should().BeFalse();
                        returnedResult.ShouldBeSuccess();
                    }
                }

                public class GenericInput
                {
                    [Fact]
                    public void Failure()
                    {
                        var resultInput = Core.Result.Result.Fail<Guid>(ErrorMessage);

                        var wasCalled = false;

                        IResultError passedResultError = null;

                        var resultOutput = resultInput.DoWhenFailure(error =>
                        {
                            passedResultError = error;

                            wasCalled = true;
                        });

                        wasCalled.Should().BeTrue();
                        passedResultError.ToErrorString().Should().Be(ErrorMessage);
                        resultOutput.ShouldBeFailureWithError(ErrorMessage);
                    }

                    [Fact]
                    public void Success()
                    {
                        var inputValue = Guid.NewGuid();

                        var resultInput = Core.Result.Result.Ok(inputValue);

                        var wasCalled = false;

                        var resultOutput = resultInput.DoWhenFailure(error => wasCalled = true);

                        wasCalled.Should().BeFalse();
                        resultOutput.ShouldBeSuccessWithValue(inputValue);
                    }
                }
            }

            public class WithoutErrorPayload
            {
                public class NonGenericInput
                {
                    [Fact]
                    public void Failure()
                    {
                        var resultInput = Core.Result.Result.Fail(ErrorMessage);

                        var wasCalled = false;

                        var returnedResult = resultInput.DoWhenFailure(() => { wasCalled = true; });

                        wasCalled.Should().BeTrue();
                        returnedResult.ShouldBeFailureWithError(ErrorMessage);
                    }

                    [Fact]
                    public void Success()
                    {
                        var resultInput = Core.Result.Result.Ok();

                        var wasCalled = false;

                        var returnedResult = resultInput.DoWhenFailure(() => wasCalled = true);

                        wasCalled.Should().BeFalse();
                        returnedResult.ShouldBeSuccess();
                    }
                }

                public class GenericInput
                {
                    [Fact]
                    public void Failure()
                    {
                        var resultInput = Core.Result.Result.Fail<Guid>(ErrorMessage);

                        var wasCalled = false;

                        var resultOutput = resultInput.DoWhenFailure(() => { wasCalled = true; });

                        wasCalled.Should().BeTrue();
                        resultOutput.ShouldBeFailureWithError(ErrorMessage);
                    }

                    [Fact]
                    public void Success()
                    {
                        var inputValue = Guid.NewGuid();

                        var resultInput = Core.Result.Result.Ok(inputValue);

                        var wasCalled = false;

                        var resultOutput = resultInput.DoWhenFailure(() => wasCalled = true);

                        wasCalled.Should().BeFalse();
                        resultOutput.ShouldBeSuccessWithValue(inputValue);
                    }
                }
            }
        }

        public class ReturnAsync
        {
            public class WithErrorPayload
            {
                public class NonGenericInput
                {
                    [Fact]
                    public async Task Failure()
                    {
                        var resultInput = Core.Result.Result.Fail(ErrorMessage);

                        var wasCalled = false;

                        IResultError passedResultError = null;

                        var returnedResult = await resultInput.DoWhenFailureAsync(error =>
                        {
                            passedResultError = error;

                            wasCalled = true;

                            return Task.CompletedTask;
                        });

                        wasCalled.Should().BeTrue();
                        passedResultError.ToErrorString().Should().Be(ErrorMessage);
                        returnedResult.ShouldBeFailureWithError(ErrorMessage);
                    }

                    [Fact]
                    public async Task Success()
                    {
                        var resultInput = Core.Result.Result.Ok();

                        var wasCalled = false;

                        var returnedResult = await resultInput.DoWhenFailureAsync(error =>
                        {
                            wasCalled = true;

                            return Task.CompletedTask;
                        });

                        wasCalled.Should().BeFalse();
                        returnedResult.ShouldBeSuccess();
                    }
                }

                public class GenericInput
                {
                    [Fact]
                    public async Task Failure()
                    {
                        var resultInput = Core.Result.Result.Fail<Guid>(ErrorMessage);

                        var wasCalled = false;

                        IResultError passedResultError = null;

                        var resultOutput = await resultInput.DoWhenFailureAsync(error =>
                        {
                            passedResultError = error;

                            wasCalled = true;

                            return Task.CompletedTask;
                        });

                        wasCalled.Should().BeTrue();
                        passedResultError.ToErrorString().Should().Be(ErrorMessage);
                        resultOutput.ShouldBeFailureWithError(ErrorMessage);
                    }

                    [Fact]
                    public async Task Success()
                    {
                        var inputValue = Guid.NewGuid();

                        var resultInput = Core.Result.Result.Ok(inputValue);

                        var wasCalled = false;

                        var resultOutput = await resultInput.DoWhenFailureAsync(error =>
                        {
                            wasCalled = true;

                            return Task.CompletedTask;
                        });

                        wasCalled.Should().BeFalse();
                        resultOutput.ShouldBeSuccessWithValue(inputValue);
                    }
                }
            }

            public class WithoutErrorPayload
            {
                public class NonGenericInput
                {
                    [Fact]
                    public async Task Failure()
                    {
                        var resultInput = Core.Result.Result.Fail(ErrorMessage);

                        var wasCalled = false;

                        var returnedResult = await resultInput.DoWhenFailureAsync(() =>
                        {
                            wasCalled = true;

                            return Task.CompletedTask;
                        });

                        wasCalled.Should().BeTrue();
                        returnedResult.ShouldBeFailureWithError(ErrorMessage);
                    }

                    [Fact]
                    public async Task Success()
                    {
                        var resultInput = Core.Result.Result.Ok();

                        var wasCalled = false;

                        var returnedResult = await resultInput.DoWhenFailureAsync(() =>
                        {
                            wasCalled = true;

                            return Task.CompletedTask;
                        });

                        wasCalled.Should().BeFalse();
                        returnedResult.ShouldBeSuccess();
                    }
                }

                public class GenericInput
                {
                    [Fact]
                    public async Task Failure()
                    {
                        var resultInput = Core.Result.Result.Fail<Guid>(ErrorMessage);

                        var wasCalled = false;

                        var resultOutput = await resultInput.DoWhenFailureAsync(() =>
                        {
                            wasCalled = true;

                            return Task.CompletedTask;
                        });

                        wasCalled.Should().BeTrue();
                        resultOutput.ShouldBeFailureWithError(ErrorMessage);
                    }

                    [Fact]
                    public async Task Success()
                    {
                        var inputValue = Guid.NewGuid();

                        var resultInput = Core.Result.Result.Ok(inputValue);

                        var wasCalled = false;

                        var resultOutput = await resultInput.DoWhenFailureAsync(() =>
                        {
                            wasCalled = true;

                            return Task.CompletedTask;
                        });

                        wasCalled.Should().BeFalse();
                        resultOutput.ShouldBeSuccessWithValue(inputValue);
                    }
                }
            }
        }
    }

    public class Async
    {
        public class ReturnSync
        {
            public class WithErrorPayload
            {
                public class NonGenericInput
                {
                    [Fact]
                    public async Task Failure()
                    {
                        var resultInput = Core.Result.Result.FailAsync(ErrorMessage);

                        var wasCalled = false;

                        IResultError passedResultError = null;

                        var returnedResult = await resultInput.DoWhenFailureAsync(error =>
                        {
                            passedResultError = error;

                            wasCalled = true;
                        });

                        wasCalled.Should().BeTrue();
                        passedResultError.ToErrorString().Should().Be(ErrorMessage);
                        returnedResult.ShouldBeFailureWithError(ErrorMessage);
                    }

                    [Fact]
                    public async Task Success()
                    {
                        var resultInput = Core.Result.Result.OkAsync();

                        var wasCalled = false;

                        var returnedResult = await resultInput.DoWhenFailureAsync(error => wasCalled = true);

                        wasCalled.Should().BeFalse();
                        returnedResult.ShouldBeSuccess();
                    }
                }

                public class GenericInput
                {
                    [Fact]
                    public async Task Failure()
                    {
                        var resultInput = Core.Result.Result.FailAsync<Guid>(ErrorMessage);

                        var wasCalled = false;

                        IResultError passedResultError = null;

                        var resultOutput = await resultInput.DoWhenFailureAsync(error =>
                        {
                            passedResultError = error;

                            wasCalled = true;
                        });

                        wasCalled.Should().BeTrue();
                        passedResultError.ToErrorString().Should().Be(ErrorMessage);
                        resultOutput.ShouldBeFailureWithError(ErrorMessage);
                    }

                    [Fact]
                    public async Task Success()
                    {
                        var inputValue = Guid.NewGuid();

                        var resultInput = Core.Result.Result.OkAsync(inputValue);

                        var wasCalled = false;

                        var resultOutput = await resultInput.DoWhenFailureAsync(error => wasCalled = true);

                        wasCalled.Should().BeFalse();
                        resultOutput.ShouldBeSuccessWithValue(inputValue);
                    }
                }
            }

            public class WithoutErrorPayload
            {
                public class NonGenericInput
                {
                    [Fact]
                    public async Task Failure()
                    {
                        var resultInput = Core.Result.Result.FailAsync(ErrorMessage);

                        var wasCalled = false;

                        var returnedResult = await resultInput.DoWhenFailureAsync(() => { wasCalled = true; });

                        wasCalled.Should().BeTrue();
                        returnedResult.ShouldBeFailureWithError(ErrorMessage);
                    }

                    [Fact]
                    public async Task Success()
                    {
                        var resultInput = Core.Result.Result.OkAsync();

                        var wasCalled = false;

                        var returnedResult = await resultInput.DoWhenFailureAsync(() => wasCalled = true);

                        wasCalled.Should().BeFalse();
                        returnedResult.ShouldBeSuccess();
                    }
                }

                public class GenericInput
                {
                    [Fact]
                    public async Task Failure()
                    {
                        var resultInput = Core.Result.Result.FailAsync<Guid>(ErrorMessage);

                        var wasCalled = false;

                        var resultOutput = await resultInput.DoWhenFailureAsync(() => { wasCalled = true; });

                        wasCalled.Should().BeTrue();
                        resultOutput.ShouldBeFailureWithError(ErrorMessage);
                    }

                    [Fact]
                    public async Task Success()
                    {
                        var inputValue = Guid.NewGuid();

                        var resultInput = Core.Result.Result.OkAsync(inputValue);

                        var wasCalled = false;

                        var resultOutput = await resultInput.DoWhenFailureAsync(() => wasCalled = true);

                        wasCalled.Should().BeFalse();
                        resultOutput.ShouldBeSuccessWithValue(inputValue);
                    }
                }
            }
        }

        public class ReturnAsync
        {
            public class WithErrorPayload
            {
                public class NonGenericInput
                {
                    [Fact]
                    public async Task Failure()
                    {
                        var resultInput = Core.Result.Result.FailAsync(ErrorMessage);

                        var wasCalled = false;

                        IResultError passedResultError = null;

                        var returnedResult = await resultInput.DoWhenFailureAsync(error =>
                        {
                            passedResultError = error;

                            wasCalled = true;

                            return Task.CompletedTask;
                        });

                        wasCalled.Should().BeTrue();
                        passedResultError.ToErrorString().Should().Be(ErrorMessage);
                        returnedResult.ShouldBeFailureWithError(ErrorMessage);
                    }

                    [Fact]
                    public async Task Success()
                    {
                        var resultInput = Core.Result.Result.OkAsync();

                        var wasCalled = false;

                        var returnedResult = await resultInput.DoWhenFailureAsync(error =>
                        {
                            wasCalled = true;

                            return Task.CompletedTask;
                        });

                        wasCalled.Should().BeFalse();
                        returnedResult.ShouldBeSuccess();
                    }
                }

                public class GenericInput
                {
                    [Fact]
                    public async Task Failure()
                    {
                        var resultInput = Core.Result.Result.FailAsync<Guid>(ErrorMessage);

                        var wasCalled = false;

                        IResultError passedResultError = null;

                        var resultOutput = await resultInput.DoWhenFailureAsync(error =>
                        {
                            passedResultError = error;

                            wasCalled = true;

                            return Task.CompletedTask;
                        });

                        wasCalled.Should().BeTrue();
                        passedResultError.ToErrorString().Should().Be(ErrorMessage);
                        resultOutput.ShouldBeFailureWithError(ErrorMessage);
                    }

                    [Fact]
                    public async Task Success()
                    {
                        var inputValue = Guid.NewGuid();

                        var resultInput = Core.Result.Result.OkAsync(inputValue);

                        var wasCalled = false;

                        var resultOutput = await resultInput.DoWhenFailureAsync(error =>
                        {
                            wasCalled = true;

                            return Task.CompletedTask;
                        });

                        wasCalled.Should().BeFalse();
                        resultOutput.ShouldBeSuccessWithValue(inputValue);
                    }
                }
            }

            public class WithoutErrorPayload
            {
                public class NonGenericInput
                {
                    [Fact]
                    public async Task Failure()
                    {
                        var resultInput = Core.Result.Result.FailAsync(ErrorMessage);

                        var wasCalled = false;

                        var returnedResult = await resultInput.DoWhenFailureAsync(() =>
                        {
                            wasCalled = true;

                            return Task.CompletedTask;
                        });

                        wasCalled.Should().BeTrue();
                        returnedResult.ShouldBeFailureWithError(ErrorMessage);
                    }

                    [Fact]
                    public async Task Success()
                    {
                        var resultInput = Core.Result.Result.OkAsync();

                        var wasCalled = false;

                        var returnedResult = await resultInput.DoWhenFailureAsync(() =>
                        {
                            wasCalled = true;

                            return Task.CompletedTask;
                        });

                        wasCalled.Should().BeFalse();
                        returnedResult.ShouldBeSuccess();
                    }
                }

                public class GenericInput
                {
                    [Fact]
                    public async Task Failure()
                    {
                        var resultInput = Core.Result.Result.FailAsync<Guid>(ErrorMessage);

                        var wasCalled = false;

                        var resultOutput = await resultInput.DoWhenFailureAsync(() =>
                        {
                            wasCalled = true;

                            return Task.CompletedTask;
                        });

                        wasCalled.Should().BeTrue();
                        resultOutput.ShouldBeFailureWithError(ErrorMessage);
                    }

                    [Fact]
                    public async Task Success()
                    {
                        var inputValue = Guid.NewGuid();

                        var resultInput = Core.Result.Result.OkAsync(inputValue);

                        var wasCalled = false;

                        var resultOutput = await resultInput.DoWhenFailureAsync(() =>
                        {
                            wasCalled = true;

                            return Task.CompletedTask;
                        });

                        wasCalled.Should().BeFalse();
                        resultOutput.ShouldBeSuccessWithValue(inputValue);
                    }
                }
            }
        }
    }
}