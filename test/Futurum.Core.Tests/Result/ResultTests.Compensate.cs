using System;
using System.Threading.Tasks;

using Futurum.Core.Result;
using Futurum.Core.Tests.Helper.Result;

using Xunit;

namespace Futurum.Core.Tests.Result;

public class ResultCompensateTests
{
    private const string ErrorMessage1 = "ERROR_MESSAGE_1";
    private const string ErrorMessage2 = "ERROR_MESSAGE_2";

    public class Sync
    {
        public class SyncReturn
        {
            public class NonGenericInput
            {
                public class WithoutErrorBeingAvailable
                {
                    [Fact]
                    public void FailureInput_FailureOutput()
                    {
                        var resultInput = Core.Result.Result.Fail(ErrorMessage1);

                        var resultOutput = resultInput.Compensate(() => Core.Result.Result.Fail(ErrorMessage2));

                        resultOutput.ShouldBeFailureWithError(ErrorMessage2);
                    }

                    [Fact]
                    public void FailureInput_SuccessOutput()
                    {
                        var resultInput = Core.Result.Result.Fail(ErrorMessage1);

                        var resultOutput = resultInput.Compensate(Core.Result.Result.Ok);

                        resultOutput.ShouldBeSuccess();
                    }

                    [Fact]
                    public void SuccessInput_FailureOutput()
                    {
                        var resultInput = Core.Result.Result.Ok();

                        var resultOutput = resultInput.Compensate(() => Core.Result.Result.Fail(ErrorMessage1));

                        resultOutput.ShouldBeSuccess();
                    }

                    [Fact]
                    public void SuccessInput_SuccessOutput()
                    {
                        var resultInput = Core.Result.Result.Ok();

                        var resultOutput = resultInput.Compensate(Core.Result.Result.Ok);

                        resultOutput.ShouldBeSuccess();
                    }
                }

                public class WithErrorBeingAvailable
                {
                    [Fact]
                    public void FailureInput_FailureOutput()
                    {
                        var resultInput = Core.Result.Result.Fail(ErrorMessage1);

                        IResultError passedError = null;

                        var resultOutput = resultInput.Compensate(error =>
                        {
                            passedError = error;

                            return Core.Result.Result.Fail(ErrorMessage2);
                        });

                        resultOutput.ShouldBeFailureWithError(ErrorMessage2);
                        passedError.ShouldBeError(ErrorMessage1);
                    }

                    [Fact]
                    public void FailureInput_SuccessOutput()
                    {
                        var resultInput = Core.Result.Result.Fail(ErrorMessage1);

                        IResultError passedError = null;

                        var resultOutput = resultInput.Compensate(error =>
                        {
                            passedError = error;

                            return Core.Result.Result.Ok();
                        });

                        resultOutput.ShouldBeSuccess();
                        passedError.ShouldBeError(ErrorMessage1);
                    }

                    [Fact]
                    public void SuccessInput_FailureOutput()
                    {
                        var resultInput = Core.Result.Result.Ok();

                        var resultOutput = resultInput.Compensate(_ => Core.Result.Result.Fail(ErrorMessage1));

                        resultOutput.ShouldBeSuccess();
                    }

                    [Fact]
                    public void SuccessInput_SuccessOutput()
                    {
                        var resultInput = Core.Result.Result.Ok();

                        var resultOutput = resultInput.Compensate(_ => Core.Result.Result.Ok());

                        resultOutput.ShouldBeSuccess();
                    }
                }
            }

            public class GenericInput
            {
                public class WithoutErrorBeingAvailable
                {
                    [Fact]
                    public void FailureInput_FailureOutput()
                    {
                        var resultInput = Core.Result.Result.Fail<Guid>(ErrorMessage1);

                        var resultOutput = resultInput.Compensate(() => Core.Result.Result.Fail<Guid>(ErrorMessage2));

                        resultOutput.ShouldBeFailureWithError(ErrorMessage2);
                    }

                    [Fact]
                    public void FailureInput_SuccessOutput()
                    {
                        var resultInput = Core.Result.Result.Fail<Guid>(ErrorMessage1);

                        var outputValue = Guid.NewGuid();

                        var resultOutput = resultInput.Compensate(() => Core.Result.Result.Ok(outputValue));

                        resultOutput.ShouldBeSuccessWithValue(outputValue);
                    }

                    [Fact]
                    public void SuccessInput_FailureOutput()
                    {
                        var inputValue = Guid.NewGuid();

                        var resultInput = Core.Result.Result.Ok(inputValue);

                        var resultOutput = resultInput.Compensate(() => Core.Result.Result.Fail<Guid>(ErrorMessage1));

                        resultOutput.ShouldBeSuccessWithValue(inputValue);
                    }

                    [Fact]
                    public void SuccessInput_SuccessOutput()
                    {
                        var inputValue = Guid.NewGuid();
                        var outputValue = Guid.NewGuid();

                        var resultInput = Core.Result.Result.Ok(inputValue);

                        var resultOutput = resultInput.Compensate(() => Core.Result.Result.Ok(outputValue));

                        resultOutput.ShouldBeSuccessWithValue(inputValue);
                    }
                }

                public class WithErrorBeingAvailable
                {
                    [Fact]
                    public void FailureInput_FailureOutput()
                    {
                        var resultInput = Core.Result.Result.Fail<Guid>(ErrorMessage1);

                        IResultError passedError = null;

                        var resultOutput = resultInput.Compensate(error =>
                        {
                            passedError = error;

                            return Core.Result.Result.Fail<Guid>(ErrorMessage2);
                        });

                        resultOutput.ShouldBeFailureWithError(ErrorMessage2);
                        passedError.ShouldBeError(ErrorMessage1);
                    }

                    [Fact]
                    public void FailureInput_SuccessOutput()
                    {
                        var resultInput = Core.Result.Result.Fail<Guid>(ErrorMessage1);

                        IResultError passedError = null;

                        var outputValue = Guid.NewGuid();

                        var resultOutput = resultInput.Compensate(error =>
                        {
                            passedError = error;

                            return Core.Result.Result.Ok(outputValue);
                        });

                        resultOutput.ShouldBeSuccessWithValue(outputValue);
                        passedError.ShouldBeError(ErrorMessage1);
                    }

                    [Fact]
                    public void SuccessInput_FailureOutput()
                    {
                        var inputValue = Guid.NewGuid();

                        var resultInput = Core.Result.Result.Ok(inputValue);

                        var resultOutput = resultInput.Compensate(_ => Core.Result.Result.Fail<Guid>(ErrorMessage1));

                        resultOutput.ShouldBeSuccessWithValue(inputValue);
                    }

                    [Fact]
                    public void SuccessInput_SuccessOutput()
                    {
                        var inputValue = Guid.NewGuid();
                        var outputValue = Guid.NewGuid();

                        var resultInput = Core.Result.Result.Ok(inputValue);

                        var resultOutput = resultInput.Compensate(_ => Core.Result.Result.Ok(outputValue));

                        resultOutput.ShouldBeSuccessWithValue(inputValue);
                    }
                }
            }
        }

        public class AsyncReturn
        {
            public class NonGenericInput
            {
                public class WithoutErrorBeingAvailable
                {
                    [Fact]
                    public async Task FailureInput_FailureOutput()
                    {
                        var resultInput = Core.Result.Result.Fail(ErrorMessage1);

                        var resultOutput = await resultInput.CompensateAsync(() => Core.Result.Result.FailAsync(ErrorMessage2));

                        resultOutput.ShouldBeFailureWithError(ErrorMessage2);
                    }

                    [Fact]
                    public async Task FailureInput_SuccessOutput()
                    {
                        var resultInput = Core.Result.Result.Fail(ErrorMessage1);

                        var resultOutput = await resultInput.CompensateAsync(Core.Result.Result.OkAsync);

                        resultOutput.ShouldBeSuccess();
                    }

                    [Fact]
                    public async Task SuccessInput_FailureOutput()
                    {
                        var resultInput = Core.Result.Result.Ok();

                        var resultOutput = await resultInput.CompensateAsync(() => Core.Result.Result.FailAsync(ErrorMessage1));

                        resultOutput.ShouldBeSuccess();
                    }

                    [Fact]
                    public async Task SuccessInput_SuccessOutput()
                    {
                        var resultInput = Core.Result.Result.Ok();

                        var resultOutput = await resultInput.CompensateAsync(Core.Result.Result.OkAsync);

                        resultOutput.ShouldBeSuccess();
                    }
                }

                public class WithErrorBeingAvailable
                {
                    [Fact]
                    public async Task FailureInput_FailureOutput()
                    {
                        var resultInput = Core.Result.Result.Fail(ErrorMessage1);

                        IResultError passedError = null;

                        var resultOutput = await resultInput.CompensateAsync(error =>
                        {
                            passedError = error;

                            return Core.Result.Result.FailAsync(ErrorMessage2);
                        });

                        resultOutput.ShouldBeFailureWithError(ErrorMessage2);
                        passedError.ShouldBeError(ErrorMessage1);
                    }

                    [Fact]
                    public async Task FailureInput_SuccessOutput()
                    {
                        var resultInput = Core.Result.Result.Fail(ErrorMessage1);

                        IResultError passedError = null;

                        var resultOutput = await resultInput.CompensateAsync(error =>
                        {
                            passedError = error;

                            return Core.Result.Result.OkAsync();
                        });

                        resultOutput.ShouldBeSuccess();
                        passedError.ShouldBeError(ErrorMessage1);
                    }

                    [Fact]
                    public async Task SuccessInput_FailureOutput()
                    {
                        var resultInput = Core.Result.Result.Ok();

                        var resultOutput = await resultInput.CompensateAsync(_ => Core.Result.Result.FailAsync(ErrorMessage1));

                        resultOutput.ShouldBeSuccess();
                    }

                    [Fact]
                    public async Task SuccessInput_SuccessOutput()
                    {
                        var resultInput = Core.Result.Result.Ok();

                        var resultOutput = await resultInput.CompensateAsync(_ => Core.Result.Result.OkAsync());

                        resultOutput.ShouldBeSuccess();
                    }
                }
            }

            public class GenericInput
            {
                public class WithoutErrorBeingAvailable
                {
                    [Fact]
                    public async Task FailureInput_FailureOutput()
                    {
                        var resultInput = Core.Result.Result.Fail<Guid>(ErrorMessage1);

                        var resultOutput = await resultInput.CompensateAsync(() => Core.Result.Result.FailAsync<Guid>(ErrorMessage2));

                        resultOutput.ShouldBeFailureWithError(ErrorMessage2);
                    }

                    [Fact]
                    public async Task FailureInput_SuccessOutput()
                    {
                        var resultInput = Core.Result.Result.Fail<Guid>(ErrorMessage1);

                        var outputValue = Guid.NewGuid();

                        var resultOutput = await resultInput.CompensateAsync(() => Core.Result.Result.OkAsync(outputValue));

                        resultOutput.ShouldBeSuccessWithValue(outputValue);
                    }

                    [Fact]
                    public async Task SuccessInput_FailureOutput()
                    {
                        var inputValue = Guid.NewGuid();

                        var resultInput = Core.Result.Result.Ok(inputValue);

                        var resultOutput = await resultInput.CompensateAsync(() => Core.Result.Result.FailAsync<Guid>(ErrorMessage1));

                        resultOutput.ShouldBeSuccessWithValue(inputValue);
                    }

                    [Fact]
                    public async Task SuccessInput_SuccessOutput()
                    {
                        var inputValue = Guid.NewGuid();
                        var outputValue = Guid.NewGuid();

                        var resultInput = Core.Result.Result.Ok(inputValue);

                        var resultOutput = await resultInput.CompensateAsync(() => Core.Result.Result.OkAsync(outputValue));

                        resultOutput.ShouldBeSuccessWithValue(inputValue);
                    }
                }

                public class WithErrorBeingAvailable
                {
                    [Fact]
                    public async Task FailureInput_FailureOutput()
                    {
                        var resultInput = Core.Result.Result.Fail<Guid>(ErrorMessage1);

                        IResultError passedError = null;

                        var resultOutput = await resultInput.CompensateAsync(error =>
                        {
                            passedError = error;

                            return Core.Result.Result.FailAsync<Guid>(ErrorMessage2);
                        });

                        resultOutput.ShouldBeFailureWithError(ErrorMessage2);
                        passedError.ShouldBeError(ErrorMessage1);
                    }

                    [Fact]
                    public async Task FailureInput_SuccessOutput()
                    {
                        var resultInput = Core.Result.Result.Fail<Guid>(ErrorMessage1);

                        IResultError passedError = null;

                        var outputValue = Guid.NewGuid();

                        var resultOutput = await resultInput.CompensateAsync(error =>
                        {
                            passedError = error;

                            return Core.Result.Result.OkAsync(outputValue);
                        });

                        resultOutput.ShouldBeSuccessWithValue(outputValue);
                        passedError.ShouldBeError(ErrorMessage1);
                    }

                    [Fact]
                    public async Task SuccessInput_FailureOutput()
                    {
                        var inputValue = Guid.NewGuid();

                        var resultInput = Core.Result.Result.Ok(inputValue);

                        var resultOutput = await resultInput.CompensateAsync(_ => Core.Result.Result.FailAsync<Guid>(ErrorMessage1));

                        resultOutput.ShouldBeSuccessWithValue(inputValue);
                    }

                    [Fact]
                    public async Task SuccessInput_SuccessOutput()
                    {
                        var inputValue = Guid.NewGuid();
                        var outputValue = Guid.NewGuid();

                        var resultInput = Core.Result.Result.Ok(inputValue);

                        var resultOutput = await resultInput.CompensateAsync(_ => Core.Result.Result.OkAsync(outputValue));

                        resultOutput.ShouldBeSuccessWithValue(inputValue);
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
                public class WithoutErrorBeingAvailable
                {
                    [Fact]
                    public async Task FailureInput_FailureOutput()
                    {
                        var resultInput = Core.Result.Result.FailAsync(ErrorMessage1);

                        var resultOutput = await resultInput.CompensateAsync(() => Core.Result.Result.Fail(ErrorMessage2));

                        resultOutput.ShouldBeFailureWithError(ErrorMessage2);
                    }

                    [Fact]
                    public async Task FailureInput_SuccessOutput()
                    {
                        var resultInput = Core.Result.Result.FailAsync(ErrorMessage1);

                        var resultOutput = await resultInput.CompensateAsync(Core.Result.Result.Ok);

                        resultOutput.ShouldBeSuccess();
                    }

                    [Fact]
                    public async Task SuccessInput_FailureOutput()
                    {
                        var resultInput = Core.Result.Result.OkAsync();

                        var resultOutput = await resultInput.CompensateAsync(() => Core.Result.Result.Fail(ErrorMessage1));

                        resultOutput.ShouldBeSuccess();
                    }

                    [Fact]
                    public async Task SuccessInput_SuccessOutput()
                    {
                        var resultInput = Core.Result.Result.OkAsync();

                        var resultOutput = await resultInput.CompensateAsync(Core.Result.Result.Ok);

                        resultOutput.ShouldBeSuccess();
                    }
                }

                public class WithErrorBeingAvailable
                {
                    [Fact]
                    public async Task FailureInput_FailureOutput()
                    {
                        var resultInput = Core.Result.Result.FailAsync(ErrorMessage1);

                        IResultError passedError = null;

                        var resultOutput = await resultInput.CompensateAsync(error =>
                        {
                            passedError = error;

                            return Core.Result.Result.Fail(ErrorMessage2);
                        });

                        resultOutput.ShouldBeFailureWithError(ErrorMessage2);
                        passedError.ShouldBeError(ErrorMessage1);
                    }

                    [Fact]
                    public async Task FailureInput_SuccessOutput()
                    {
                        var resultInput = Core.Result.Result.FailAsync(ErrorMessage1);

                        IResultError passedError = null;

                        var resultOutput = await resultInput.CompensateAsync(error =>
                        {
                            passedError = error;

                            return Core.Result.Result.Ok();
                        });

                        resultOutput.ShouldBeSuccess();
                        passedError.ShouldBeError(ErrorMessage1);
                    }

                    [Fact]
                    public async Task SuccessInput_FailureOutput()
                    {
                        var resultInput = Core.Result.Result.OkAsync();

                        var resultOutput = await resultInput.CompensateAsync(_ => Core.Result.Result.Fail(ErrorMessage1));

                        resultOutput.ShouldBeSuccess();
                    }

                    [Fact]
                    public async Task SuccessInput_SuccessOutput()
                    {
                        var resultInput = Core.Result.Result.OkAsync();

                        var resultOutput = await resultInput.CompensateAsync(_ => Core.Result.Result.Ok());

                        resultOutput.ShouldBeSuccess();
                    }
                }
            }

            public class GenericInput
            {
                public class WithoutErrorBeingAvailable
                {
                    [Fact]
                    public async Task FailureInput_FailureOutput()
                    {
                        var resultInput = Core.Result.Result.FailAsync<Guid>(ErrorMessage1);

                        var resultOutput = await resultInput.CompensateAsync(() => Core.Result.Result.Fail<Guid>(ErrorMessage2));

                        resultOutput.ShouldBeFailureWithError(ErrorMessage2);
                    }

                    [Fact]
                    public async Task FailureInput_SuccessOutput()
                    {
                        var resultInput = Core.Result.Result.FailAsync<Guid>(ErrorMessage1);

                        var outputValue = Guid.NewGuid();

                        var resultOutput = await resultInput.CompensateAsync(() => Core.Result.Result.Ok(outputValue));

                        resultOutput.ShouldBeSuccessWithValue(outputValue);
                    }

                    [Fact]
                    public async Task SuccessInput_FailureOutput()
                    {
                        var inputValue = Guid.NewGuid();

                        var resultInput = Core.Result.Result.OkAsync(inputValue);

                        var resultOutput = await resultInput.CompensateAsync(() => Core.Result.Result.Fail<Guid>(ErrorMessage1));

                        resultOutput.ShouldBeSuccessWithValue(inputValue);
                    }

                    [Fact]
                    public async Task SuccessInput_SuccessOutput()
                    {
                        var inputValue = Guid.NewGuid();
                        var outputValue = Guid.NewGuid();

                        var resultInput = Core.Result.Result.OkAsync(inputValue);

                        var resultOutput = await resultInput.CompensateAsync(() => Core.Result.Result.Ok(outputValue));

                        resultOutput.ShouldBeSuccessWithValue(inputValue);
                    }
                }

                public class WithErrorBeingAvailable
                {
                    [Fact]
                    public async Task FailureInput_FailureOutput()
                    {
                        var resultInput = Core.Result.Result.FailAsync<Guid>(ErrorMessage1);

                        IResultError passedError = null;

                        var resultOutput = await resultInput.CompensateAsync(error =>
                        {
                            passedError = error;

                            return Core.Result.Result.Fail<Guid>(ErrorMessage2);
                        });

                        resultOutput.ShouldBeFailureWithError(ErrorMessage2);
                        passedError.ShouldBeError(ErrorMessage1);
                    }

                    [Fact]
                    public async Task FailureInput_SuccessOutput()
                    {
                        var resultInput = Core.Result.Result.FailAsync<Guid>(ErrorMessage1);

                        IResultError passedError = null;

                        var outputValue = Guid.NewGuid();

                        var resultOutput = await resultInput.CompensateAsync(error =>
                        {
                            passedError = error;

                            return Core.Result.Result.Ok(outputValue);
                        });

                        resultOutput.ShouldBeSuccessWithValue(outputValue);
                        passedError.ShouldBeError(ErrorMessage1);
                    }

                    [Fact]
                    public async Task SuccessInput_FailureOutput()
                    {
                        var inputValue = Guid.NewGuid();

                        var resultInput = Core.Result.Result.OkAsync(inputValue);

                        var resultOutput = await resultInput.CompensateAsync(_ => Core.Result.Result.Fail<Guid>(ErrorMessage1));

                        resultOutput.ShouldBeSuccessWithValue(inputValue);
                    }

                    [Fact]
                    public async Task SuccessInput_SuccessOutput()
                    {
                        var inputValue = Guid.NewGuid();
                        var outputValue = Guid.NewGuid();

                        var resultInput = Core.Result.Result.OkAsync(inputValue);

                        var resultOutput = await resultInput.CompensateAsync(_ => Core.Result.Result.Ok(outputValue));

                        resultOutput.ShouldBeSuccessWithValue(inputValue);
                    }
                }
            }
        }

        public class AsyncReturn
        {
            public class NonGenericInput
            {
                public class WithoutErrorBeingAvailable
                {
                    [Fact]
                    public async Task FailureInput_FailureOutput()
                    {
                        var resultInput = Core.Result.Result.FailAsync(ErrorMessage1);

                        var resultOutput = await resultInput.CompensateAsync(() => Core.Result.Result.FailAsync(ErrorMessage2));

                        resultOutput.ShouldBeFailureWithError(ErrorMessage2);
                    }

                    [Fact]
                    public async Task FailureInput_SuccessOutput()
                    {
                        var resultInput = Core.Result.Result.FailAsync(ErrorMessage1);

                        var resultOutput = await resultInput.CompensateAsync(Core.Result.Result.OkAsync);

                        resultOutput.ShouldBeSuccess();
                    }

                    [Fact]
                    public async Task SuccessInput_FailureOutput()
                    {
                        var resultInput = Core.Result.Result.OkAsync();

                        var resultOutput = await resultInput.CompensateAsync(() => Core.Result.Result.FailAsync(ErrorMessage1));

                        resultOutput.ShouldBeSuccess();
                    }

                    [Fact]
                    public async Task SuccessInput_SuccessOutput()
                    {
                        var resultInput = Core.Result.Result.OkAsync();

                        var resultOutput = await resultInput.CompensateAsync(Core.Result.Result.OkAsync);

                        resultOutput.ShouldBeSuccess();
                    }
                }

                public class WithErrorBeingAvailable
                {
                    [Fact]
                    public async Task FailureInput_FailureOutput()
                    {
                        var resultInput = Core.Result.Result.FailAsync(ErrorMessage1);

                        IResultError passedError = null;

                        var resultOutput = await resultInput.CompensateAsync(error =>
                        {
                            passedError = error;

                            return Core.Result.Result.FailAsync(ErrorMessage2);
                        });

                        resultOutput.ShouldBeFailureWithError(ErrorMessage2);
                        passedError.ShouldBeError(ErrorMessage1);
                    }

                    [Fact]
                    public async Task FailureInput_SuccessOutput()
                    {
                        var resultInput = Core.Result.Result.FailAsync(ErrorMessage1);

                        IResultError passedError = null;

                        var resultOutput = await resultInput.CompensateAsync(error =>
                        {
                            passedError = error;

                            return Core.Result.Result.OkAsync();
                        });

                        resultOutput.ShouldBeSuccess();
                        passedError.ShouldBeError(ErrorMessage1);
                    }

                    [Fact]
                    public async Task SuccessInput_FailureOutput()
                    {
                        var resultInput = Core.Result.Result.OkAsync();

                        var resultOutput = await resultInput.CompensateAsync(_ => Core.Result.Result.FailAsync(ErrorMessage1));

                        resultOutput.ShouldBeSuccess();
                    }

                    [Fact]
                    public async Task SuccessInput_SuccessOutput()
                    {
                        var resultInput = Core.Result.Result.OkAsync();

                        var resultOutput = await resultInput.CompensateAsync(_ => Core.Result.Result.OkAsync());

                        resultOutput.ShouldBeSuccess();
                    }
                }
            }

            public class GenericInput
            {
                public class WithoutErrorBeingAvailable
                {
                    [Fact]
                    public async Task FailureInput_FailureOutput()
                    {
                        var resultInput = Core.Result.Result.FailAsync<Guid>(ErrorMessage1);

                        var resultOutput = await resultInput.CompensateAsync(() => Core.Result.Result.FailAsync<Guid>(ErrorMessage2));

                        resultOutput.ShouldBeFailureWithError(ErrorMessage2);
                    }

                    [Fact]
                    public async Task FailureInput_SuccessOutput()
                    {
                        var resultInput = Core.Result.Result.FailAsync<Guid>(ErrorMessage1);

                        var outputValue = Guid.NewGuid();

                        var resultOutput = await resultInput.CompensateAsync(() => Core.Result.Result.OkAsync(outputValue));

                        resultOutput.ShouldBeSuccessWithValue(outputValue);
                    }

                    [Fact]
                    public async Task SuccessInput_FailureOutput()
                    {
                        var inputValue = Guid.NewGuid();

                        var resultInput = Core.Result.Result.OkAsync(inputValue);

                        var resultOutput = await resultInput.CompensateAsync(() => Core.Result.Result.FailAsync<Guid>(ErrorMessage1));

                        resultOutput.ShouldBeSuccessWithValue(inputValue);
                    }

                    [Fact]
                    public async Task SuccessInput_SuccessOutput()
                    {
                        var inputValue = Guid.NewGuid();
                        var outputValue = Guid.NewGuid();

                        var resultInput = Core.Result.Result.OkAsync(inputValue);

                        var resultOutput = await resultInput.CompensateAsync(() => Core.Result.Result.OkAsync(outputValue));

                        resultOutput.ShouldBeSuccessWithValue(inputValue);
                    }
                }

                public class WithErrorBeingAvailable
                {
                    [Fact]
                    public async Task FailureInput_FailureOutput()
                    {
                        var resultInput = Core.Result.Result.FailAsync<Guid>(ErrorMessage1);

                        IResultError passedError = null;

                        var resultOutput = await resultInput.CompensateAsync(error =>
                        {
                            passedError = error;

                            return Core.Result.Result.FailAsync<Guid>(ErrorMessage2);
                        });

                        resultOutput.ShouldBeFailureWithError(ErrorMessage2);
                        passedError.ShouldBeError(ErrorMessage1);
                    }

                    [Fact]
                    public async Task FailureInput_SuccessOutput()
                    {
                        var resultInput = Core.Result.Result.FailAsync<Guid>(ErrorMessage1);

                        IResultError passedError = null;

                        var outputValue = Guid.NewGuid();

                        var resultOutput = await resultInput.CompensateAsync(error =>
                        {
                            passedError = error;

                            return Core.Result.Result.OkAsync(outputValue);
                        });

                        resultOutput.ShouldBeSuccessWithValue(outputValue);
                        passedError.ShouldBeError(ErrorMessage1);
                    }

                    [Fact]
                    public async Task SuccessInput_FailureOutput()
                    {
                        var inputValue = Guid.NewGuid();

                        var resultInput = Core.Result.Result.OkAsync(inputValue);

                        var resultOutput = await resultInput.CompensateAsync(_ => Core.Result.Result.FailAsync<Guid>(ErrorMessage1));

                        resultOutput.ShouldBeSuccessWithValue(inputValue);
                    }

                    [Fact]
                    public async Task SuccessInput_SuccessOutput()
                    {
                        var inputValue = Guid.NewGuid();
                        var outputValue = Guid.NewGuid();

                        var resultInput = Core.Result.Result.OkAsync(inputValue);

                        var resultOutput = await resultInput.CompensateAsync(_ => Core.Result.Result.OkAsync(outputValue));

                        resultOutput.ShouldBeSuccessWithValue(inputValue);
                    }
                }
            }
        }
    }
}