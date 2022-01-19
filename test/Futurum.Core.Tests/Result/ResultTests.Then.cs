using System;
using System.Threading.Tasks;

using FluentAssertions;

using Futurum.Core.Result;
using Futurum.Core.Tests.Helper.Result;

using Xunit;

namespace Futurum.Core.Tests.Result;

public class ResultThenTests
{
    private const string ErrorMessage = "ERROR_MESSAGE_1";

    public class Sync
    {
        public class SyncReturn
        {
            public class NonGenericInput
            {
                public class NonGenericReturn
                {
                    [Fact]
                    public void FailureInput_FailureOutput()
                    {
                        var resultInput = Core.Result.Result.Fail(ErrorMessage);

                        var resultOutput = resultInput.Then(() => Core.Result.Result.Fail(ErrorMessage));

                        resultOutput.ShouldBeFailureWithError(ErrorMessage);
                    }

                    [Fact]
                    public void FailureInput_SuccessOutput()
                    {
                        var resultInput = Core.Result.Result.Fail(ErrorMessage);

                        var resultOutput = resultInput.Then(Core.Result.Result.Ok);

                        resultOutput.ShouldBeFailureWithError(ErrorMessage);
                    }

                    [Fact]
                    public void SuccessInput_FailureOutput()
                    {
                        var resultInput = Core.Result.Result.Ok();

                        var resultOutput = resultInput.Then(() => Core.Result.Result.Fail(ErrorMessage));

                        resultOutput.ShouldBeFailureWithError(ErrorMessage);
                    }

                    [Fact]
                    public void SuccessInput_SuccessOutput()
                    {
                        var resultInput = Core.Result.Result.Ok();

                        var resultOutput = resultInput.Then(Core.Result.Result.Ok);

                        resultOutput.ShouldBeSuccess();
                    }
                }

                public class GenericReturn
                {
                    [Fact]
                    public void FailureInput_FailureOutput()
                    {
                        var resultInput = Core.Result.Result.Fail(ErrorMessage);

                        var resultOutput = resultInput.Then(() => Core.Result.Result.Fail<Guid>(ErrorMessage));

                        resultOutput.ShouldBeFailureWithError(ErrorMessage);
                    }

                    [Fact]
                    public void FailureInput_SuccessOutput()
                    {
                        var resultInput = Core.Result.Result.Fail(ErrorMessage);

                        var outputValue = Guid.NewGuid();

                        var resultOutput = resultInput.Then(() => Core.Result.Result.Ok(outputValue));

                        resultOutput.ShouldBeFailureWithError(ErrorMessage);
                    }

                    [Fact]
                    public void SuccessInput_FailureOutput()
                    {
                        var resultInput = Core.Result.Result.Ok();

                        var resultOutput = resultInput.Then(() => Core.Result.Result.Fail<Guid>(ErrorMessage));

                        resultOutput.ShouldBeFailureWithError(ErrorMessage);
                    }

                    [Fact]
                    public void SuccessInput_SuccessOutput()
                    {
                        var resultInput = Core.Result.Result.Ok();

                        var outputValue = Guid.NewGuid();

                        var resultOutput = resultInput.Then(() => Core.Result.Result.Ok(outputValue));

                        resultOutput.ShouldBeSuccessWithValue(outputValue);
                    }
                }
            }

            public class GenericInput
            {
                public class NonGenericReturn
                {
                    [Fact]
                    public void FailureInput_FailureOutput()
                    {
                        var resultInput = Core.Result.Result.Fail<Guid>(ErrorMessage);

                        var resultOutput = resultInput.Then(_ => Core.Result.Result.Fail(ErrorMessage));

                        resultOutput.ShouldBeFailureWithError(ErrorMessage);
                    }

                    [Fact]
                    public void FailureInput_SuccessOutput()
                    {
                        var resultInput = Core.Result.Result.Fail<Guid>(ErrorMessage);

                        var resultOutput = resultInput.Then(_ => Core.Result.Result.Ok());

                        resultOutput.ShouldBeFailureWithError(ErrorMessage);
                    }

                    [Fact]
                    public void SuccessInput_FailureOutput()
                    {
                        var inputValue = Guid.NewGuid();

                        var resultInput = Core.Result.Result.Ok(inputValue);

                        var passedValue = Guid.Empty;

                        var resultOutput = resultInput.Then(value =>
                        {
                            passedValue = value;

                            return Core.Result.Result.Fail(ErrorMessage);
                        });

                        passedValue.Should().Be(inputValue);
                        resultOutput.ShouldBeFailureWithError(ErrorMessage);
                    }

                    [Fact]
                    public void SuccessInput_SuccessOutput()
                    {
                        var inputValue = Guid.NewGuid();

                        var resultInput = Core.Result.Result.Ok(inputValue);

                        var passedValue = Guid.Empty;

                        var resultOutput = resultInput.Then(value =>
                        {
                            passedValue = value;

                            return Core.Result.Result.Ok();
                        });

                        resultOutput.ShouldBeSuccessWithValue(inputValue);
                        passedValue.Should().Be(inputValue);
                    }
                }

                public class GenericReturn
                {
                    [Fact]
                    public void FailureInput_FailureOutput()
                    {
                        var resultInput = Core.Result.Result.Fail<Guid>(ErrorMessage);

                        var resultOutput = resultInput.Then(_ => Core.Result.Result.Fail<Guid>(ErrorMessage));

                        resultOutput.ShouldBeFailureWithError(ErrorMessage);
                    }

                    [Fact]
                    public void FailureInput_SuccessOutput()
                    {
                        var resultInput = Core.Result.Result.Fail<Guid>(ErrorMessage);

                        var outputValue = Guid.NewGuid();

                        var resultOutput = resultInput.Then(_ => Core.Result.Result.Ok(outputValue));

                        resultOutput.ShouldBeFailureWithError(ErrorMessage);
                    }

                    [Fact]
                    public void SuccessInput_FailureOutput()
                    {
                        var inputValue = Guid.NewGuid();

                        var resultInput = Core.Result.Result.Ok(inputValue);

                        var passedValue = Guid.Empty;

                        var resultOutput = resultInput.Then(value =>
                        {
                            passedValue = value;

                            return Core.Result.Result.Fail<Guid>(ErrorMessage);
                        });

                        passedValue.Should().Be(inputValue);
                        resultOutput.ShouldBeFailureWithError(ErrorMessage);
                    }

                    [Fact]
                    public void SuccessInput_SuccessOutput()
                    {
                        var inputValue = Guid.NewGuid();

                        var resultInput = Core.Result.Result.Ok(inputValue);

                        var outputValue = Guid.NewGuid();

                        var passedValue = Guid.Empty;

                        var resultOutput = resultInput.Then(value =>
                        {
                            passedValue = value;

                            return Core.Result.Result.Ok(outputValue);
                        });

                        passedValue.Should().Be(inputValue);
                        resultOutput.ShouldBeSuccessWithValue(outputValue);
                    }
                }
            }
        }

        public class AsyncReturn
        {
            public class NonGenericInput
            {
                public class NonGenericReturn
                {
                    [Fact]
                    public async Task FailureInput_FailureOutput()
                    {
                        var resultInput = Core.Result.Result.Fail(ErrorMessage);

                        var resultOutput = await resultInput.ThenAsync(() => Core.Result.Result.FailAsync(ErrorMessage));

                        resultOutput.ShouldBeFailureWithError(ErrorMessage);
                    }

                    [Fact]
                    public async Task FailureInput_SuccessOutput()
                    {
                        var resultInput = Core.Result.Result.Fail(ErrorMessage);

                        var resultOutput = await resultInput.ThenAsync(Core.Result.Result.OkAsync);

                        resultOutput.ShouldBeFailureWithError(ErrorMessage);
                    }

                    [Fact]
                    public async Task SuccessInput_FailureOutput()
                    {
                        var resultInput = Core.Result.Result.Ok();

                        var resultOutput = await resultInput.ThenAsync(() => Core.Result.Result.FailAsync(ErrorMessage));

                        resultOutput.ShouldBeFailureWithError(ErrorMessage);
                    }

                    [Fact]
                    public async Task SuccessInput_SuccessOutput()
                    {
                        var resultInput = Core.Result.Result.Ok();

                        var resultOutput = await resultInput.ThenAsync(Core.Result.Result.OkAsync);

                        resultOutput.ShouldBeSuccess();
                    }
                }

                public class GenericReturn
                {
                    [Fact]
                    public async Task FailureInput_FailureOutput()
                    {
                        var resultInput = Core.Result.Result.Fail(ErrorMessage);

                        var resultOutput =
                            await resultInput.ThenAsync(() => Core.Result.Result.FailAsync<Guid>(ErrorMessage));

                        resultOutput.ShouldBeFailureWithError(ErrorMessage);
                    }

                    [Fact]
                    public async Task FailureInput_SuccessOutput()
                    {
                        var resultInput = Core.Result.Result.Fail(ErrorMessage);

                        var outputValue = Guid.NewGuid();

                        var resultOutput = await resultInput.ThenAsync(() => Core.Result.Result.OkAsync(outputValue));

                        resultOutput.ShouldBeFailureWithError(ErrorMessage);
                    }

                    [Fact]
                    public async Task SuccessInput_FailureOutput()
                    {
                        var resultInput = Core.Result.Result.Ok();

                        var resultOutput =
                            await resultInput.ThenAsync(() => Core.Result.Result.FailAsync<Guid>(ErrorMessage));

                        resultOutput.ShouldBeFailureWithError(ErrorMessage);
                    }

                    [Fact]
                    public async Task SuccessInput_SuccessOutput()
                    {
                        var resultInput = Core.Result.Result.Ok();

                        var outputValue = Guid.NewGuid();

                        var resultOutput = await resultInput.ThenAsync(() => Core.Result.Result.OkAsync(outputValue));

                        resultOutput.ShouldBeSuccessWithValue(outputValue);
                    }
                }
            }

            public class GenericInput
            {
                public class NonGenericReturn
                {
                    [Fact]
                    public async Task FailureInput_FailureOutput()
                    {
                        var resultInput = Core.Result.Result.Fail<Guid>(ErrorMessage);

                        var resultOutput =
                            await resultInput.ThenAsync(_ => Core.Result.Result.FailAsync(ErrorMessage));

                        resultOutput.ShouldBeFailureWithError(ErrorMessage);
                    }

                    [Fact]
                    public async Task FailureInput_SuccessOutput()
                    {
                        var resultInput = Core.Result.Result.Fail<Guid>(ErrorMessage);

                        var resultOutput = await resultInput.ThenAsync(_ => Core.Result.Result.OkAsync());

                        resultOutput.ShouldBeFailureWithError(ErrorMessage);
                    }

                    [Fact]
                    public async Task SuccessInput_FailureOutput()
                    {
                        var inputValue = Guid.NewGuid();

                        var resultInput = Core.Result.Result.Ok(inputValue);

                        var passedValue = Guid.Empty;

                        var resultOutput = await resultInput.ThenAsync(value =>
                        {
                            passedValue = value;

                            return Core.Result.Result.FailAsync(ErrorMessage);
                        });

                        passedValue.Should().Be(inputValue);
                        resultOutput.ShouldBeFailureWithError(ErrorMessage);
                    }

                    [Fact]
                    public async Task SuccessInput_SuccessOutput()
                    {
                        var inputValue = Guid.NewGuid();

                        var resultInput = Core.Result.Result.Ok(inputValue);

                        var passedValue = Guid.Empty;

                        var resultOutput = await resultInput.ThenAsync(value =>
                        {
                            passedValue = value;

                            return Core.Result.Result.OkAsync();
                        });

                        resultOutput.ShouldBeSuccessWithValue(inputValue);
                        passedValue.Should().Be(inputValue);
                    }
                }

                public class GenericReturn
                {
                    [Fact]
                    public async Task FailureInput_FailureOutput()
                    {
                        var resultInput = Core.Result.Result.Fail<Guid>(ErrorMessage);

                        var resultOutput =
                            await resultInput.ThenAsync(_ => Core.Result.Result.FailAsync<Guid>(ErrorMessage));

                        resultOutput.ShouldBeFailureWithError(ErrorMessage);
                    }

                    [Fact]
                    public async Task FailureInput_SuccessOutput()
                    {
                        var resultInput = Core.Result.Result.Fail<Guid>(ErrorMessage);

                        var outputValue = Guid.NewGuid();

                        var resultOutput = await resultInput.ThenAsync(_ => Core.Result.Result.OkAsync(outputValue));

                        resultOutput.ShouldBeFailureWithError(ErrorMessage);
                    }

                    [Fact]
                    public async Task SuccessInput_FailureOutput()
                    {
                        var inputValue = Guid.NewGuid();

                        var resultInput = Core.Result.Result.Ok(inputValue);

                        var passedValue = Guid.Empty;

                        var resultOutput = await resultInput.ThenAsync(value =>
                        {
                            passedValue = value;

                            return Core.Result.Result.FailAsync<Guid>(ErrorMessage);
                        });

                        passedValue.Should().Be(inputValue);
                        resultOutput.ShouldBeFailureWithError(ErrorMessage);
                    }

                    [Fact]
                    public async Task SuccessInput_SuccessOutput()
                    {
                        var inputValue = Guid.NewGuid();

                        var resultInput = Core.Result.Result.Ok(inputValue);

                        var outputValue = Guid.NewGuid();

                        var passedValue = Guid.Empty;

                        var resultOutput = await resultInput.ThenAsync(value =>
                        {
                            passedValue = value;

                            return Core.Result.Result.OkAsync(outputValue);
                        });

                        passedValue.Should().Be(inputValue);
                        resultOutput.ShouldBeSuccessWithValue(outputValue);
                    }
                }
            }
        }
    }

    public class Async
    {
        public class SyncReturn
        {
            public class NonGenericInput
            {
                public class NonGenericReturn
                {
                    [Fact]
                    public async Task FailureInput_FailureOutput()
                    {
                        var resultInput = Core.Result.Result.FailAsync(ErrorMessage);

                        var resultOutput = await resultInput.ThenAsync(() => Core.Result.Result.Fail(ErrorMessage));

                        resultOutput.ShouldBeFailureWithError(ErrorMessage);
                    }

                    [Fact]
                    public async Task FailureInput_SuccessOutput()
                    {
                        var resultInput = Core.Result.Result.FailAsync(ErrorMessage);

                        var resultOutput = await resultInput.ThenAsync(Core.Result.Result.Ok);

                        resultOutput.ShouldBeFailureWithError(ErrorMessage);
                    }

                    [Fact]
                    public async Task SuccessInput_FailureOutput()
                    {
                        var resultInput = Core.Result.Result.OkAsync();

                        var resultOutput = await resultInput.ThenAsync(() => Core.Result.Result.Fail(ErrorMessage));

                        resultOutput.ShouldBeFailureWithError(ErrorMessage);
                    }

                    [Fact]
                    public async Task SuccessInput_SuccessOutput()
                    {
                        var resultInput = Core.Result.Result.OkAsync();

                        var resultOutput = await resultInput.ThenAsync(Core.Result.Result.Ok);

                        resultOutput.ShouldBeSuccess();
                    }
                }

                public class GenericReturn
                {
                    [Fact]
                    public async Task FailureInput_FailureOutput()
                    {
                        var resultInput = Core.Result.Result.FailAsync(ErrorMessage);

                        var resultOutput =
                            await resultInput.ThenAsync(() => Core.Result.Result.Fail<Guid>(ErrorMessage));

                        resultOutput.ShouldBeFailureWithError(ErrorMessage);
                    }

                    [Fact]
                    public async Task FailureInput_SuccessOutput()
                    {
                        var resultInput = Core.Result.Result.FailAsync(ErrorMessage);

                        var outputValue = Guid.NewGuid();

                        var resultOutput = await resultInput.ThenAsync(() => Core.Result.Result.Ok(outputValue));

                        resultOutput.ShouldBeFailureWithError(ErrorMessage);
                    }

                    [Fact]
                    public async Task SuccessInput_FailureOutput()
                    {
                        var resultInput = Core.Result.Result.OkAsync();

                        var resultOutput =
                            await resultInput.ThenAsync(() => Core.Result.Result.Fail<Guid>(ErrorMessage));

                        resultOutput.ShouldBeFailureWithError(ErrorMessage);
                    }

                    [Fact]
                    public async Task SuccessInput_SuccessOutput()
                    {
                        var resultInput = Core.Result.Result.OkAsync();

                        var outputValue = Guid.NewGuid();

                        var resultOutput = await resultInput.ThenAsync(() => Core.Result.Result.Ok(outputValue));

                        resultOutput.ShouldBeSuccessWithValue(outputValue);
                    }
                }
            }

            public class GenericInput
            {
                public class NonGenericReturn
                {
                    [Fact]
                    public async Task FailureInput_FailureOutput()
                    {
                        var resultInput = Core.Result.Result.FailAsync<Guid>(ErrorMessage);

                        var resultOutput = await resultInput.ThenAsync(_ => Core.Result.Result.Fail(ErrorMessage));

                        resultOutput.ShouldBeFailureWithError(ErrorMessage);
                    }

                    [Fact]
                    public async Task FailureInput_SuccessOutput()
                    {
                        var resultInput = Core.Result.Result.FailAsync<Guid>(ErrorMessage);

                        var resultOutput = await resultInput.ThenAsync(_ => Core.Result.Result.Ok());

                        resultOutput.ShouldBeFailureWithError(ErrorMessage);
                    }

                    [Fact]
                    public async Task SuccessInput_FailureOutput()
                    {
                        var inputValue = Guid.NewGuid();

                        var resultInput = Core.Result.Result.OkAsync(inputValue);

                        var passedValue = Guid.Empty;

                        var resultOutput = await resultInput.ThenAsync(value =>
                        {
                            passedValue = value;

                            return Core.Result.Result.Fail(ErrorMessage);
                        });

                        passedValue.Should().Be(inputValue);
                        resultOutput.ShouldBeFailureWithError(ErrorMessage);
                    }

                    [Fact]
                    public async Task SuccessInput_SuccessOutput()
                    {
                        var inputValue = Guid.NewGuid();

                        var resultInput = Core.Result.Result.OkAsync(inputValue);

                        var passedValue = Guid.Empty;

                        var resultOutput = await resultInput.ThenAsync(value =>
                        {
                            passedValue = value;

                            return Core.Result.Result.Ok();
                        });

                        resultOutput.ShouldBeSuccessWithValue(inputValue);
                        passedValue.Should().Be(inputValue);
                    }
                }

                public class GenericReturn
                {
                    [Fact]
                    public async Task FailureInput_FailureOutput()
                    {
                        var resultInput = Core.Result.Result.FailAsync<Guid>(ErrorMessage);

                        var resultOutput =
                            await resultInput.ThenAsync(_ => Core.Result.Result.Fail<Guid>(ErrorMessage));

                        resultOutput.ShouldBeFailureWithError(ErrorMessage);
                    }

                    [Fact]
                    public async Task FailureInput_SuccessOutput()
                    {
                        var resultInput = Core.Result.Result.FailAsync<Guid>(ErrorMessage);

                        var outputValue = Guid.NewGuid();

                        var resultOutput = await resultInput.ThenAsync(_ => Core.Result.Result.Ok(outputValue));

                        resultOutput.ShouldBeFailureWithError(ErrorMessage);
                    }

                    [Fact]
                    public async Task SuccessInput_FailureOutput()
                    {
                        var inputValue = Guid.NewGuid();

                        var resultInput = Core.Result.Result.OkAsync(inputValue);

                        var passedValue = Guid.Empty;

                        var resultOutput = await resultInput.ThenAsync(value =>
                        {
                            passedValue = value;

                            return Core.Result.Result.Fail<Guid>(ErrorMessage);
                        });

                        passedValue.Should().Be(inputValue);
                        resultOutput.ShouldBeFailureWithError(ErrorMessage);
                    }

                    [Fact]
                    public async Task SuccessInput_SuccessOutput()
                    {
                        var inputValue = Guid.NewGuid();

                        var resultInput = Core.Result.Result.OkAsync(inputValue);

                        var outputValue = Guid.NewGuid();

                        var passedValue = Guid.Empty;

                        var resultOutput = await resultInput.ThenAsync(value =>
                        {
                            passedValue = value;

                            return Core.Result.Result.Ok(outputValue);
                        });

                        passedValue.Should().Be(inputValue);
                        resultOutput.ShouldBeSuccessWithValue(outputValue);
                    }
                }
            }
        }

        public class AsyncReturn
        {
            public class NonGenericInput
            {
                public class NonGenericReturn
                {
                    [Fact]
                    public async Task FailureInput_FailureOutput()
                    {
                        var resultInput = Core.Result.Result.FailAsync(ErrorMessage);

                        var resultOutput = await resultInput.ThenAsync(() => Core.Result.Result.FailAsync(ErrorMessage));

                        resultOutput.ShouldBeFailureWithError(ErrorMessage);
                    }

                    [Fact]
                    public async Task FailureInput_SuccessOutput()
                    {
                        var resultInput = Core.Result.Result.FailAsync(ErrorMessage);

                        var resultOutput = await resultInput.ThenAsync(Core.Result.Result.OkAsync);

                        resultOutput.ShouldBeFailureWithError(ErrorMessage);
                    }

                    [Fact]
                    public async Task SuccessInput_FailureOutput()
                    {
                        var resultInput = Core.Result.Result.OkAsync();

                        var resultOutput = await resultInput.ThenAsync(() => Core.Result.Result.FailAsync(ErrorMessage));

                        resultOutput.ShouldBeFailureWithError(ErrorMessage);
                    }

                    [Fact]
                    public async Task SuccessInput_SuccessOutput()
                    {
                        var resultInput = Core.Result.Result.OkAsync();

                        var resultOutput = await resultInput.ThenAsync(Core.Result.Result.OkAsync);

                        resultOutput.ShouldBeSuccess();
                    }
                }

                public class GenericReturn
                {
                    [Fact]
                    public async Task FailureInput_FailureOutput()
                    {
                        var resultInput = Core.Result.Result.FailAsync(ErrorMessage);

                        var resultOutput =
                            await resultInput.ThenAsync(() => Core.Result.Result.FailAsync<Guid>(ErrorMessage));

                        resultOutput.ShouldBeFailureWithError(ErrorMessage);
                    }

                    [Fact]
                    public async Task FailureInput_SuccessOutput()
                    {
                        var resultInput = Core.Result.Result.FailAsync(ErrorMessage);

                        var outputValue = Guid.NewGuid();

                        var resultOutput = await resultInput.ThenAsync(() => Core.Result.Result.OkAsync(outputValue));

                        resultOutput.ShouldBeFailureWithError(ErrorMessage);
                    }

                    [Fact]
                    public async Task SuccessInput_FailureOutput()
                    {
                        var resultInput = Core.Result.Result.OkAsync();

                        var resultOutput = await resultInput.ThenAsync(() => Core.Result.Result.FailAsync<Guid>(ErrorMessage));

                        resultOutput.ShouldBeFailureWithError(ErrorMessage);
                    }

                    [Fact]
                    public async Task SuccessInput_SuccessOutput()
                    {
                        var resultInput = Core.Result.Result.OkAsync();

                        var outputValue = Guid.NewGuid();

                        var resultOutput = await resultInput.ThenAsync(() => Core.Result.Result.OkAsync(outputValue));

                        resultOutput.ShouldBeSuccessWithValue(outputValue);
                    }
                }
            }

            public class GenericInput
            {
                public class NonGenericReturn
                {
                    [Fact]
                    public async Task FailureInput_FailureOutput()
                    {
                        var resultInput = Core.Result.Result.FailAsync<Guid>(ErrorMessage);

                        var resultOutput =
                            await resultInput.ThenAsync(_ => Core.Result.Result.FailAsync(ErrorMessage));

                        resultOutput.ShouldBeFailureWithError(ErrorMessage);
                    }

                    [Fact]
                    public async Task FailureInput_SuccessOutput()
                    {
                        var resultInput = Core.Result.Result.FailAsync<Guid>(ErrorMessage);

                        var resultOutput = await resultInput.ThenAsync(_ => Core.Result.Result.OkAsync());

                        resultOutput.ShouldBeFailureWithError(ErrorMessage);
                    }

                    [Fact]
                    public async Task SuccessInput_FailureOutput()
                    {
                        var inputValue = Guid.NewGuid();

                        var resultInput = Core.Result.Result.OkAsync(inputValue);

                        var passedValue = Guid.Empty;

                        var resultOutput = await resultInput.ThenAsync(value =>
                        {
                            passedValue = value;

                            return Core.Result.Result.FailAsync(ErrorMessage);
                        });

                        passedValue.Should().Be(inputValue);
                        resultOutput.ShouldBeFailureWithError(ErrorMessage);
                    }

                    [Fact]
                    public async Task SuccessInput_SuccessOutput()
                    {
                        var inputValue = Guid.NewGuid();

                        var resultInput = Core.Result.Result.OkAsync(inputValue);

                        var passedValue = Guid.Empty;

                        var resultOutput = await resultInput.ThenAsync(value =>
                        {
                            passedValue = value;

                            return Core.Result.Result.OkAsync();
                        });

                        resultOutput.ShouldBeSuccessWithValue(inputValue);
                        passedValue.Should().Be(inputValue);
                    }
                }

                public class GenericReturn
                {
                    [Fact]
                    public async Task FailureInput_FailureOutput()
                    {
                        var resultInput = Core.Result.Result.FailAsync<Guid>(ErrorMessage);

                        var resultOutput =
                            await resultInput.ThenAsync(_ => Core.Result.Result.FailAsync<Guid>(ErrorMessage));

                        resultOutput.ShouldBeFailureWithError(ErrorMessage);
                    }

                    [Fact]
                    public async Task FailureInput_SuccessOutput()
                    {
                        var resultInput = Core.Result.Result.FailAsync<Guid>(ErrorMessage);

                        var outputValue = Guid.NewGuid();

                        var resultOutput = await resultInput.ThenAsync(_ => Core.Result.Result.OkAsync(outputValue));

                        resultOutput.ShouldBeFailureWithError(ErrorMessage);
                    }

                    [Fact]
                    public async Task SuccessInput_FailureOutput()
                    {
                        var inputValue = Guid.NewGuid();

                        var resultInput = Core.Result.Result.OkAsync(inputValue);

                        var passedValue = Guid.Empty;

                        var resultOutput = await resultInput.ThenAsync(value =>
                        {
                            passedValue = value;

                            return Core.Result.Result.FailAsync<Guid>(ErrorMessage);
                        });

                        passedValue.Should().Be(inputValue);
                        resultOutput.ShouldBeFailureWithError(ErrorMessage);
                    }

                    [Fact]
                    public async Task SuccessInput_SuccessOutput()
                    {
                        var inputValue = Guid.NewGuid();

                        var resultInput = Core.Result.Result.OkAsync(inputValue);

                        var outputValue = Guid.NewGuid();

                        var passedValue = Guid.Empty;

                        var resultOutput = await resultInput.ThenAsync(value =>
                        {
                            passedValue = value;

                            return Core.Result.Result.OkAsync(outputValue);
                        });

                        passedValue.Should().Be(inputValue);
                        resultOutput.ShouldBeSuccessWithValue(outputValue);
                    }
                }
            }
        }
    }
}