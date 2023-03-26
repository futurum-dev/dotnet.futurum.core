using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using FluentAssertions;

using Futurum.Core.Option;
using Futurum.Core.Result;
using Futurum.Core.Tests.Helper.Result;

using Xunit;

namespace Futurum.Core.Tests.Result;

public class ResultCompensateWhenAnyCompositeResultErrorTests
{
    private const string ErrorMessage1 = "ERROR_MESSAGE_1";
    private const string ErrorMessage2 = "ERROR_MESSAGE_2";

    public class WithoutFilterFunc
    {
        public class Sync
        {
            public class SyncReturn
            {
                public class NonGenericInput
                {
                    public class WhenIResultErrorTypeMatches
                    {
                        [Fact]
                        public void FailureInput_FailureOutput()
                        {
                            var resultInput = Core.Result.Result.Fail(new ResultErrorTest(ErrorMessage1));

                            IResultError passedError = null;

                            var resultOutput = resultInput.CompensateWhenAny<ResultErrorTest2>(error =>
                            {
                                passedError = error;

                                return Core.Result.Result.Fail(error.ErrorMessage + ErrorMessage2);
                            });

                            resultOutput.ShouldBeFailureWithError($"{ErrorMessage1}{ErrorMessage2}");
                            passedError.ShouldBeError(ErrorMessage1);
                        }

                        [Fact]
                        public void FailureInput_SuccessOutput()
                        {
                            var resultInput = Core.Result.Result.Fail(new ResultErrorTest(ErrorMessage1));

                            IResultError passedError = null;

                            var resultOutput = resultInput.CompensateWhenAny<ResultErrorTest>(error =>
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

                            var resultOutput = resultInput.CompensateWhenAny<ResultErrorTest>(_ => Core.Result.Result.Fail(ErrorMessage1));

                            resultOutput.ShouldBeSuccess();
                        }

                        [Fact]
                        public void SuccessInput_SuccessOutput()
                        {
                            var resultInput = Core.Result.Result.Ok();

                            var resultOutput = resultInput.CompensateWhenAny<ResultErrorTest>(_ => Core.Result.Result.Ok());

                            resultOutput.ShouldBeSuccess();
                        }
                    }

                    public class WhenIResultErrorTypeDoeNotMatches
                    {
                        [Fact]
                        public void FailureInput_FailureOutput()
                        {
                            var resultInput = Core.Result.Result.Fail(new ResultErrorTest(ErrorMessage1));

                            IResultError passedError = null;

                            var resultOutput = resultInput.CompensateWhenAny<ResultErrorMessage>(error =>
                            {
                                passedError = error;

                                return Core.Result.Result.Fail(error);
                            });

                            resultOutput.ShouldBeFailureWithError(ErrorMessage1);
                            passedError.Should().BeNull();
                        }

                        [Fact]
                        public void FailureInput_SuccessOutput()
                        {
                            var resultInput = Core.Result.Result.Fail(new ResultErrorTest(ErrorMessage1));

                            IResultError passedError = null;

                            var resultOutput = resultInput.CompensateWhenAny<ResultErrorMessage>(error =>
                            {
                                passedError = error;

                                return Core.Result.Result.Ok();
                            });

                            resultOutput.ShouldBeFailureWithError(ErrorMessage1);
                            passedError.Should().BeNull();
                        }

                        [Fact]
                        public void SuccessInput_FailureOutput()
                        {
                            var resultInput = Core.Result.Result.Ok();

                            var resultOutput = resultInput.CompensateWhenAny<ResultErrorTest>(_ => Core.Result.Result.Fail(ErrorMessage1));

                            resultOutput.ShouldBeSuccess();
                        }

                        [Fact]
                        public void SuccessInput_SuccessOutput()
                        {
                            var resultInput = Core.Result.Result.Ok();

                            var resultOutput = resultInput.CompensateWhenAny<ResultErrorTest>(_ => Core.Result.Result.Ok());

                            resultOutput.ShouldBeSuccess();
                        }
                    }
                }

                public class GenericInput
                {
                    public class WhenIResultErrorTypeMatches
                    {
                        [Fact]
                        public void FailureInput_FailureOutput()
                        {
                            var resultInput = Core.Result.Result.Fail<string>(new ResultErrorTest(ErrorMessage1));

                            IResultError passedError = null;

                            var resultOutput = resultInput.CompensateWhenAny<ResultErrorTest2>(error =>
                            {
                                passedError = error;

                                return Core.Result.Result.Fail<string>(error.ErrorMessage + ErrorMessage2);
                            });

                            resultOutput.ShouldBeFailureWithError($"{ErrorMessage1}{ErrorMessage2}");
                            passedError.ShouldBeError(ErrorMessage1);
                        }

                        [Fact]
                        public void FailureInput_SuccessOutput()
                        {
                            var value = Guid.NewGuid().ToString();

                            var resultInput = Core.Result.Result.Fail<string>(new ResultErrorTest(ErrorMessage1));

                            IResultError passedError = null;

                            var resultOutput = resultInput.CompensateWhenAny<ResultErrorTest>(error =>
                            {
                                passedError = error;

                                return Core.Result.Result.Ok(value);
                            });

                            resultOutput.ShouldBeSuccessWithValue(value);
                            passedError.ShouldBeError(ErrorMessage1);
                        }

                        [Fact]
                        public void SuccessInput_FailureOutput()
                        {
                            var value = Guid.NewGuid().ToString();
                            var resultInput = Core.Result.Result.Ok(value);

                            var resultOutput = resultInput.CompensateWhenAny<ResultErrorTest>(_ => Core.Result.Result.Fail<string>(ErrorMessage1));

                            resultOutput.ShouldBeSuccessWithValue(value);
                        }

                        [Fact]
                        public void SuccessInput_SuccessOutput()
                        {
                            var value = Guid.NewGuid().ToString();
                            var resultInput = Core.Result.Result.Ok(value);

                            var resultOutput = resultInput.CompensateWhenAny<ResultErrorTest>(_ => Core.Result.Result.Ok(Guid.NewGuid().ToString()));

                            resultOutput.ShouldBeSuccess();
                        }
                    }

                    public class WhenIResultErrorTypeDoeNotMatches
                    {
                        [Fact]
                        public void FailureInput_FailureOutput()
                        {
                            var resultInput = Core.Result.Result.Fail<string>(new ResultErrorTest(ErrorMessage1));

                            IResultError passedError = null;

                            var resultOutput = resultInput.CompensateWhenAny<ResultErrorMessage>(error =>
                            {
                                passedError = error;

                                return Core.Result.Result.Fail<string>(error);
                            });

                            resultOutput.ShouldBeFailureWithError(ErrorMessage1);
                            passedError.Should().BeNull();
                        }

                        [Fact]
                        public void FailureInput_SuccessOutput()
                        {
                            var resultInput = Core.Result.Result.Fail<string>(new ResultErrorTest(ErrorMessage1));

                            IResultError passedError = null;

                            var resultOutput = resultInput.CompensateWhenAny<ResultErrorMessage>(error =>
                            {
                                passedError = error;

                                return Core.Result.Result.Ok(Guid.NewGuid().ToString());
                            });

                            resultOutput.ShouldBeFailureWithError(ErrorMessage1);
                            passedError.Should().BeNull();
                        }

                        [Fact]
                        public void SuccessInput_FailureOutput()
                        {
                            var value = Guid.NewGuid().ToString();
                            var resultInput = Core.Result.Result.Ok(value);

                            var resultOutput = resultInput.CompensateWhenAny<ResultErrorTest>(_ => Core.Result.Result.Fail<string>(ErrorMessage1));

                            resultOutput.ShouldBeSuccessWithValue(value);
                        }

                        [Fact]
                        public void SuccessInput_SuccessOutput()
                        {
                            var value = Guid.NewGuid().ToString();
                            var resultInput = Core.Result.Result.Ok(value);

                            var resultOutput = resultInput.CompensateWhenAny<ResultErrorTest>(_ => Core.Result.Result.Ok(Guid.NewGuid().ToString()));

                            resultOutput.ShouldBeSuccess();
                        }
                    }
                }
            }

            public class AsyncReturn
            {
                public class NonGenericInput
                {
                    public class WhenIResultErrorTypeMatches
                    {
                        [Fact]
                        public async Task FailureInput_FailureOutput()
                        {
                            var resultInput = Core.Result.Result.Fail(new ResultErrorTest(ErrorMessage1));

                            IResultError passedError = null;

                            var resultOutput = await resultInput.CompensateWhenAnyAsync<ResultErrorTest2>(error =>
                            {
                                passedError = error;

                                return Core.Result.Result.FailAsync(error.ErrorMessage + ErrorMessage2);
                            });

                            resultOutput.ShouldBeFailureWithError($"{ErrorMessage1}{ErrorMessage2}");
                            passedError.ShouldBeError(ErrorMessage1);
                        }

                        [Fact]
                        public async Task FailureInput_SuccessOutput()
                        {
                            var resultInput = Core.Result.Result.Fail(new ResultErrorTest(ErrorMessage1));

                            IResultError passedError = null;

                            var resultOutput = await resultInput.CompensateWhenAnyAsync<ResultErrorTest>(error =>
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

                            var resultOutput = await resultInput.CompensateWhenAnyAsync<ResultErrorTest>(_ => Core.Result.Result.FailAsync(ErrorMessage1));

                            resultOutput.ShouldBeSuccess();
                        }

                        [Fact]
                        public async Task SuccessInput_SuccessOutput()
                        {
                            var resultInput = Core.Result.Result.Ok();

                            var resultOutput = await resultInput.CompensateWhenAnyAsync<ResultErrorTest>(_ => Core.Result.Result.OkAsync());

                            resultOutput.ShouldBeSuccess();
                        }
                    }

                    public class WhenIResultErrorTypeDoeNotMatches
                    {
                        [Fact]
                        public async Task FailureInput_FailureOutput()
                        {
                            var resultInput = Core.Result.Result.Fail(new ResultErrorTest(ErrorMessage1));

                            IResultError passedError = null;

                            var resultOutput = await resultInput.CompensateWhenAnyAsync<ResultErrorMessage>(error =>
                            {
                                passedError = error;

                                return Core.Result.Result.FailAsync(error);
                            });

                            resultOutput.ShouldBeFailureWithError(ErrorMessage1);
                            passedError.Should().BeNull();
                        }

                        [Fact]
                        public async Task FailureInput_SuccessOutput()
                        {
                            var resultInput = Core.Result.Result.Fail(new ResultErrorTest(ErrorMessage1));

                            IResultError passedError = null;

                            var resultOutput = await resultInput.CompensateWhenAnyAsync<ResultErrorMessage>(error =>
                            {
                                passedError = error;

                                return Core.Result.Result.OkAsync();
                            });

                            resultOutput.ShouldBeFailureWithError(ErrorMessage1);
                            passedError.Should().BeNull();
                        }

                        [Fact]
                        public async Task SuccessInput_FailureOutput()
                        {
                            var resultInput = Core.Result.Result.Ok();

                            var resultOutput = await resultInput.CompensateWhenAnyAsync<ResultErrorTest>(_ => Core.Result.Result.FailAsync(ErrorMessage1));

                            resultOutput.ShouldBeSuccess();
                        }

                        [Fact]
                        public async Task SuccessInput_SuccessOutput()
                        {
                            var resultInput = Core.Result.Result.Ok();

                            var resultOutput = await resultInput.CompensateWhenAnyAsync<ResultErrorTest>(_ => Core.Result.Result.OkAsync());

                            resultOutput.ShouldBeSuccess();
                        }
                    }
                }

                public class GenericInput
                {
                    public class WhenIResultErrorTypeMatches
                    {
                        [Fact]
                        public async Task FailureInput_FailureOutput()
                        {
                            var resultInput = Core.Result.Result.Fail<string>(new ResultErrorTest(ErrorMessage1));

                            IResultError passedError = null;

                            var resultOutput = await resultInput.CompensateWhenAnyAsync<ResultErrorTest2>(error =>
                            {
                                passedError = error;

                                return Core.Result.Result.FailAsync<string>(error.ErrorMessage + ErrorMessage2);
                            });

                            resultOutput.ShouldBeFailureWithError($"{ErrorMessage1}{ErrorMessage2}");
                            passedError.ShouldBeError(ErrorMessage1);
                        }

                        [Fact]
                        public async Task FailureInput_SuccessOutput()
                        {
                            var value = Guid.NewGuid().ToString();

                            var resultInput = Core.Result.Result.Fail<string>(new ResultErrorTest(ErrorMessage1));

                            IResultError passedError = null;

                            var resultOutput = await resultInput.CompensateWhenAnyAsync<ResultErrorTest>(error =>
                            {
                                passedError = error;

                                return Core.Result.Result.OkAsync(value);
                            });

                            resultOutput.ShouldBeSuccessWithValue(value);
                            passedError.ShouldBeError(ErrorMessage1);
                        }

                        [Fact]
                        public async Task SuccessInput_FailureOutput()
                        {
                            var value = Guid.NewGuid().ToString();
                            var resultInput = Core.Result.Result.Ok(value);

                            var resultOutput = await resultInput.CompensateWhenAnyAsync<ResultErrorTest>(_ => Core.Result.Result.FailAsync<string>(ErrorMessage1));

                            resultOutput.ShouldBeSuccessWithValue(value);
                        }

                        [Fact]
                        public async Task SuccessInput_SuccessOutput()
                        {
                            var value = Guid.NewGuid().ToString();
                            var resultInput = Core.Result.Result.Ok(value);

                            var resultOutput = await resultInput.CompensateWhenAnyAsync<ResultErrorTest>(_ => Core.Result.Result.OkAsync(Guid.NewGuid().ToString()));

                            resultOutput.ShouldBeSuccess();
                        }
                    }

                    public class WhenIResultErrorTypeDoeNotMatches
                    {
                        [Fact]
                        public async Task FailureInput_FailureOutput()
                        {
                            var resultInput = Core.Result.Result.Fail<string>(new ResultErrorTest(ErrorMessage1));

                            IResultError passedError = null;

                            var resultOutput = await resultInput.CompensateWhenAnyAsync<ResultErrorMessage>(error =>
                            {
                                passedError = error;

                                return Core.Result.Result.FailAsync<string>(error);
                            });

                            resultOutput.ShouldBeFailureWithError(ErrorMessage1);
                            passedError.Should().BeNull();
                        }

                        [Fact]
                        public async Task FailureInput_SuccessOutput()
                        {
                            var resultInput = Core.Result.Result.Fail<string>(new ResultErrorTest(ErrorMessage1));

                            IResultError passedError = null;

                            var resultOutput = await resultInput.CompensateWhenAnyAsync<ResultErrorMessage>(error =>
                            {
                                passedError = error;

                                return Core.Result.Result.OkAsync(Guid.NewGuid().ToString());
                            });

                            resultOutput.ShouldBeFailureWithError(ErrorMessage1);
                            passedError.Should().BeNull();
                        }

                        [Fact]
                        public async Task SuccessInput_FailureOutput()
                        {
                            var value = Guid.NewGuid().ToString();
                            var resultInput = Core.Result.Result.Ok(value);

                            var resultOutput = await resultInput.CompensateWhenAnyAsync<ResultErrorTest>(_ => Core.Result.Result.FailAsync<string>(ErrorMessage1));

                            resultOutput.ShouldBeSuccessWithValue(value);
                        }

                        [Fact]
                        public async Task SuccessInput_SuccessOutput()
                        {
                            var value = Guid.NewGuid().ToString();
                            var resultInput = Core.Result.Result.Ok(value);

                            var resultOutput = await resultInput.CompensateWhenAnyAsync<ResultErrorTest>(_ => Core.Result.Result.OkAsync(Guid.NewGuid().ToString()));

                            resultOutput.ShouldBeSuccess();
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
                    public class WhenIResultErrorTypeMatches
                    {
                        [Fact]
                        public async Task FailureInput_FailureOutput()
                        {
                            var resultInput = Core.Result.Result.FailAsync(new ResultErrorTest(ErrorMessage1));

                            IResultError passedError = null;

                            var resultOutput = await resultInput.CompensateWhenAnyAsync<ResultErrorTest2>(error =>
                            {
                                passedError = error;

                                return Core.Result.Result.Fail(error.ErrorMessage + ErrorMessage2);
                            });

                            resultOutput.ShouldBeFailureWithError($"{ErrorMessage1}{ErrorMessage2}");
                            passedError.ShouldBeError(ErrorMessage1);
                        }

                        [Fact]
                        public async Task FailureInput_SuccessOutput()
                        {
                            var resultInput = Core.Result.Result.FailAsync(new ResultErrorTest(ErrorMessage1));

                            IResultError passedError = null;

                            var resultOutput = await resultInput.CompensateWhenAnyAsync<ResultErrorTest>(error =>
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

                            var resultOutput = await resultInput.CompensateWhenAnyAsync<ResultErrorTest>(_ => Core.Result.Result.Fail(ErrorMessage1));

                            resultOutput.ShouldBeSuccess();
                        }

                        [Fact]
                        public async Task SuccessInput_SuccessOutput()
                        {
                            var resultInput = Core.Result.Result.OkAsync();

                            var resultOutput = await resultInput.CompensateWhenAnyAsync<ResultErrorTest>(_ => Core.Result.Result.Ok());

                            resultOutput.ShouldBeSuccess();
                        }
                    }

                    public class WhenIResultErrorTypeDoeNotMatches
                    {
                        [Fact]
                        public async Task FailureInput_FailureOutput()
                        {
                            var resultInput = Core.Result.Result.FailAsync(new ResultErrorTest(ErrorMessage1));

                            IResultError passedError = null;

                            var resultOutput = await resultInput.CompensateWhenAnyAsync<ResultErrorMessage>(error =>
                            {
                                passedError = error;

                                return Core.Result.Result.Fail(error);
                            });

                            resultOutput.ShouldBeFailureWithError(ErrorMessage1);
                            passedError.Should().BeNull();
                        }

                        [Fact]
                        public async Task FailureInput_SuccessOutput()
                        {
                            var resultInput = Core.Result.Result.FailAsync(new ResultErrorTest(ErrorMessage1));

                            IResultError passedError = null;

                            var resultOutput = await resultInput.CompensateWhenAnyAsync<ResultErrorMessage>(error =>
                            {
                                passedError = error;

                                return Core.Result.Result.Ok();
                            });

                            resultOutput.ShouldBeFailureWithError(ErrorMessage1);
                            passedError.Should().BeNull();
                        }

                        [Fact]
                        public async Task SuccessInput_FailureOutput()
                        {
                            var resultInput = Core.Result.Result.OkAsync();

                            var resultOutput = await resultInput.CompensateWhenAnyAsync<ResultErrorTest>(_ => Core.Result.Result.Fail(ErrorMessage1));

                            resultOutput.ShouldBeSuccess();
                        }

                        [Fact]
                        public async Task SuccessInput_SuccessOutput()
                        {
                            var resultInput = Core.Result.Result.OkAsync();

                            var resultOutput = await resultInput.CompensateWhenAnyAsync<ResultErrorTest>(_ => Core.Result.Result.Ok());

                            resultOutput.ShouldBeSuccess();
                        }
                    }
                }

                public class GenericInput
                {
                    public class WhenIResultErrorTypeMatches
                    {
                        [Fact]
                        public async Task FailureInput_FailureOutput()
                        {
                            var resultInput = Core.Result.Result.FailAsync<string>(new ResultErrorTest(ErrorMessage1));

                            IResultError passedError = null;

                            var resultOutput = await resultInput.CompensateWhenAnyAsync<string, ResultErrorTest2>(error =>
                            {
                                passedError = error;

                                return Core.Result.Result.Fail<string>(error.ErrorMessage + ErrorMessage2);
                            });

                            resultOutput.ShouldBeFailureWithError($"{ErrorMessage1}{ErrorMessage2}");
                            passedError.ShouldBeError(ErrorMessage1);
                        }

                        [Fact]
                        public async Task FailureInput_SuccessOutput()
                        {
                            var value = Guid.NewGuid().ToString();

                            var resultInput = Core.Result.Result.FailAsync<string>(new ResultErrorTest(ErrorMessage1));

                            IResultError passedError = null;

                            var resultOutput = await resultInput.CompensateWhenAnyAsync<string, ResultErrorTest>(error =>
                            {
                                passedError = error;

                                return Core.Result.Result.Ok(value);
                            });

                            resultOutput.ShouldBeSuccessWithValue(value);
                            passedError.ShouldBeError(ErrorMessage1);
                        }

                        [Fact]
                        public async Task SuccessInput_FailureOutput()
                        {
                            var value = Guid.NewGuid().ToString();
                            var resultInput = Core.Result.Result.OkAsync(value);

                            var resultOutput = await resultInput.CompensateWhenAnyAsync<string, ResultErrorTest>(_ => Core.Result.Result.Fail<string>(ErrorMessage1));

                            resultOutput.ShouldBeSuccessWithValue(value);
                        }

                        [Fact]
                        public async Task SuccessInput_SuccessOutput()
                        {
                            var value = Guid.NewGuid().ToString();
                            var resultInput = Core.Result.Result.OkAsync(value);

                            var resultOutput = await resultInput.CompensateWhenAnyAsync<string, ResultErrorTest>(_ => Core.Result.Result.Ok(Guid.NewGuid().ToString()));

                            resultOutput.ShouldBeSuccess();
                        }
                    }

                    public class WhenIResultErrorTypeDoeNotMatches
                    {
                        [Fact]
                        public async Task FailureInput_FailureOutput()
                        {
                            var resultInput = Core.Result.Result.FailAsync<string>(new ResultErrorTest(ErrorMessage1));

                            IResultError passedError = null;

                            var resultOutput = await resultInput.CompensateWhenAnyAsync<string, ResultErrorMessage>(error =>
                            {
                                passedError = error;

                                return Core.Result.Result.Fail<string>(error);
                            });

                            resultOutput.ShouldBeFailureWithError(ErrorMessage1);
                            passedError.Should().BeNull();
                        }

                        [Fact]
                        public async Task FailureInput_SuccessOutput()
                        {
                            var resultInput = Core.Result.Result.FailAsync<string>(new ResultErrorTest(ErrorMessage1));

                            IResultError passedError = null;

                            var resultOutput = await resultInput.CompensateWhenAnyAsync<string, ResultErrorMessage>(error =>
                            {
                                passedError = error;

                                return Core.Result.Result.Ok(Guid.NewGuid().ToString());
                            });

                            resultOutput.ShouldBeFailureWithError(ErrorMessage1);
                            passedError.Should().BeNull();
                        }

                        [Fact]
                        public async Task SuccessInput_FailureOutput()
                        {
                            var value = Guid.NewGuid().ToString();
                            var resultInput = Core.Result.Result.OkAsync(value);

                            var resultOutput = await resultInput.CompensateWhenAnyAsync<string, ResultErrorTest>(_ => Core.Result.Result.Fail<string>(ErrorMessage1));

                            resultOutput.ShouldBeSuccessWithValue(value);
                        }

                        [Fact]
                        public async Task SuccessInput_SuccessOutput()
                        {
                            var value = Guid.NewGuid().ToString();
                            var resultInput = Core.Result.Result.OkAsync(value);

                            var resultOutput = await resultInput.CompensateWhenAnyAsync<string, ResultErrorTest>(_ => Core.Result.Result.Ok(Guid.NewGuid().ToString()));

                            resultOutput.ShouldBeSuccess();
                        }
                    }
                }
            }

            public class AsyncReturn
            {
                public class NonGenericInput
                {
                    public class WhenIResultErrorTypeMatches
                    {
                        [Fact]
                        public async Task FailureInput_FailureOutput()
                        {
                            var resultInput = Core.Result.Result.FailAsync(new ResultErrorTest(ErrorMessage1));

                            IResultError passedError = null;

                            var resultOutput = await resultInput.CompensateWhenAnyAsync<ResultErrorTest2>(error =>
                            {
                                passedError = error;

                                return Core.Result.Result.FailAsync(error.ErrorMessage + ErrorMessage2);
                            });

                            resultOutput.ShouldBeFailureWithError($"{ErrorMessage1}{ErrorMessage2}");
                            passedError.ShouldBeError(ErrorMessage1);
                        }

                        [Fact]
                        public async Task FailureInput_SuccessOutput()
                        {
                            var resultInput = Core.Result.Result.FailAsync(new ResultErrorTest(ErrorMessage1));

                            IResultError passedError = null;

                            var resultOutput = await resultInput.CompensateWhenAnyAsync<ResultErrorTest>(error =>
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

                            var resultOutput = await resultInput.CompensateWhenAnyAsync<ResultErrorTest>(_ => Core.Result.Result.FailAsync(ErrorMessage1));

                            resultOutput.ShouldBeSuccess();
                        }

                        [Fact]
                        public async Task SuccessInput_SuccessOutput()
                        {
                            var resultInput = Core.Result.Result.OkAsync();

                            var resultOutput = await resultInput.CompensateWhenAnyAsync<ResultErrorTest>(_ => Core.Result.Result.OkAsync());

                            resultOutput.ShouldBeSuccess();
                        }
                    }

                    public class WhenIResultErrorTypeDoeNotMatches
                    {
                        [Fact]
                        public async Task FailureInput_FailureOutput()
                        {
                            var resultInput = Core.Result.Result.FailAsync(new ResultErrorTest(ErrorMessage1));

                            IResultError passedError = null;

                            var resultOutput = await resultInput.CompensateWhenAnyAsync<ResultErrorMessage>(error =>
                            {
                                passedError = error;

                                return Core.Result.Result.FailAsync(error);
                            });

                            resultOutput.ShouldBeFailureWithError(ErrorMessage1);
                            passedError.Should().BeNull();
                        }

                        [Fact]
                        public async Task FailureInput_SuccessOutput()
                        {
                            var resultInput = Core.Result.Result.FailAsync(new ResultErrorTest(ErrorMessage1));

                            IResultError passedError = null;

                            var resultOutput = await resultInput.CompensateWhenAnyAsync<ResultErrorMessage>(error =>
                            {
                                passedError = error;

                                return Core.Result.Result.OkAsync();
                            });

                            resultOutput.ShouldBeFailureWithError(ErrorMessage1);
                            passedError.Should().BeNull();
                        }

                        [Fact]
                        public async Task SuccessInput_FailureOutput()
                        {
                            var resultInput = Core.Result.Result.OkAsync();

                            var resultOutput = await resultInput.CompensateWhenAnyAsync<ResultErrorTest>(_ => Core.Result.Result.FailAsync(ErrorMessage1));

                            resultOutput.ShouldBeSuccess();
                        }

                        [Fact]
                        public async Task SuccessInput_SuccessOutput()
                        {
                            var resultInput = Core.Result.Result.OkAsync();

                            var resultOutput = await resultInput.CompensateWhenAnyAsync<ResultErrorTest>(_ => Core.Result.Result.OkAsync());

                            resultOutput.ShouldBeSuccess();
                        }
                    }
                }

                public class GenericInput
                {
                    public class WhenIResultErrorTypeMatches
                    {
                        [Fact]
                        public async Task FailureInput_FailureOutput()
                        {
                            var resultInput = Core.Result.Result.FailAsync<string>(new ResultErrorTest(ErrorMessage1));

                            IResultError passedError = null;

                            var resultOutput = await resultInput.CompensateWhenAnyAsync<string, ResultErrorTest2>(error =>
                            {
                                passedError = error;

                                return Core.Result.Result.FailAsync<string>(error.ErrorMessage + ErrorMessage2);
                            });

                            resultOutput.ShouldBeFailureWithError($"{ErrorMessage1}{ErrorMessage2}");
                            passedError.ShouldBeError(ErrorMessage1);
                        }

                        [Fact]
                        public async Task FailureInput_SuccessOutput()
                        {
                            var value = Guid.NewGuid().ToString();

                            var resultInput = Core.Result.Result.FailAsync<string>(new ResultErrorTest(ErrorMessage1));

                            IResultError passedError = null;

                            var resultOutput = await resultInput.CompensateWhenAnyAsync<string, ResultErrorTest>(error =>
                            {
                                passedError = error;

                                return Core.Result.Result.OkAsync(value);
                            });

                            resultOutput.ShouldBeSuccessWithValue(value);
                            passedError.ShouldBeError(ErrorMessage1);
                        }

                        [Fact]
                        public async Task SuccessInput_FailureOutput()
                        {
                            var value = Guid.NewGuid().ToString();
                            var resultInput = Core.Result.Result.OkAsync(value);

                            var resultOutput = await resultInput.CompensateWhenAnyAsync<string, ResultErrorTest>(_ => Core.Result.Result.FailAsync<string>(ErrorMessage1));

                            resultOutput.ShouldBeSuccessWithValue(value);
                        }

                        [Fact]
                        public async Task SuccessInput_SuccessOutput()
                        {
                            var value = Guid.NewGuid().ToString();
                            var resultInput = Core.Result.Result.OkAsync(value);

                            var resultOutput = await resultInput.CompensateWhenAnyAsync<string, ResultErrorTest>(_ => Core.Result.Result.OkAsync(Guid.NewGuid().ToString()));

                            resultOutput.ShouldBeSuccess();
                        }
                    }

                    public class WhenIResultErrorTypeDoeNotMatches
                    {
                        [Fact]
                        public async Task FailureInput_FailureOutput()
                        {
                            var resultInput = Core.Result.Result.FailAsync<string>(new ResultErrorTest(ErrorMessage1));

                            IResultError passedError = null;

                            var resultOutput = await resultInput.CompensateWhenAnyAsync<string, ResultErrorMessage>(error =>
                            {
                                passedError = error;

                                return Core.Result.Result.FailAsync<string>(error);
                            });

                            resultOutput.ShouldBeFailureWithError(ErrorMessage1);
                            passedError.Should().BeNull();
                        }

                        [Fact]
                        public async Task FailureInput_SuccessOutput()
                        {
                            var resultInput = Core.Result.Result.FailAsync<string>(new ResultErrorTest(ErrorMessage1));

                            IResultError passedError = null;

                            var resultOutput = await resultInput.CompensateWhenAnyAsync<string, ResultErrorMessage>(error =>
                            {
                                passedError = error;

                                return Core.Result.Result.OkAsync(Guid.NewGuid().ToString());
                            });

                            resultOutput.ShouldBeFailureWithError(ErrorMessage1);
                            passedError.Should().BeNull();
                        }

                        [Fact]
                        public async Task SuccessInput_FailureOutput()
                        {
                            var value = Guid.NewGuid().ToString();
                            var resultInput = Core.Result.Result.OkAsync(value);

                            var resultOutput = await resultInput.CompensateWhenAnyAsync<string, ResultErrorTest>(_ => Core.Result.Result.FailAsync<string>(ErrorMessage1));

                            resultOutput.ShouldBeSuccessWithValue(value);
                        }

                        [Fact]
                        public async Task SuccessInput_SuccessOutput()
                        {
                            var value = Guid.NewGuid().ToString();
                            var resultInput = Core.Result.Result.OkAsync(value);

                            var resultOutput = await resultInput.CompensateWhenAnyAsync<string, ResultErrorTest>(_ => Core.Result.Result.OkAsync(Guid.NewGuid().ToString()));

                            resultOutput.ShouldBeSuccess();
                        }
                    }
                }
            }
        }
    }

    public class WithFilterFunc
    {
        public class Sync
        {
            public class SyncReturn
            {
                public class NonGenericInput
                {
                    public class WhenIResultErrorTypeMatches
                    {
                        public class WhenFilterFuncTrue
                        {
                            [Fact]
                            public void FailureInput_FailureOutput()
                            {
                                var resultInput = Core.Result.Result.Fail(new ResultErrorTest(ErrorMessage1));

                                IResultError passedError = null;

                                var resultOutput = resultInput.CompensateWhenAny<ResultErrorTest2>(error => error.ErrorMessage == ErrorMessage1,
                                                                                                   error =>
                                                                                                   {
                                                                                                       passedError = error;

                                                                                                       return Core.Result.Result.Fail(error.ErrorMessage + ErrorMessage2);
                                                                                                   });

                                resultOutput.ShouldBeFailureWithError($"{ErrorMessage1}{ErrorMessage2}");
                                passedError.ShouldBeError(ErrorMessage1);
                            }

                            [Fact]
                            public void FailureInput_SuccessOutput()
                            {
                                var resultInput = Core.Result.Result.Fail(new ResultErrorTest(ErrorMessage1));

                                IResultError passedError = null;

                                var resultOutput = resultInput.CompensateWhenAny<ResultErrorTest2>(error => error.ErrorMessage == ErrorMessage1,
                                                                                                   error =>
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

                                var resultOutput = resultInput.CompensateWhenAny<ResultErrorTest2>(error => error.ErrorMessage == ErrorMessage1,
                                                                                                   _ => Core.Result.Result.Fail(ErrorMessage1));

                                resultOutput.ShouldBeSuccess();
                            }

                            [Fact]
                            public void SuccessInput_SuccessOutput()
                            {
                                var resultInput = Core.Result.Result.Ok();

                                var resultOutput = resultInput.CompensateWhenAny<ResultErrorTest2>(error => error.ErrorMessage == ErrorMessage1,
                                                                                                   _ => Core.Result.Result.Ok());

                                resultOutput.ShouldBeSuccess();
                            }
                        }

                        public class WhenFilterFuncFalse
                        {
                            [Fact]
                            public void FailureInput_FailureOutput()
                            {
                                var resultInput = Core.Result.Result.Fail(new ResultErrorTest(ErrorMessage1));

                                IResultError passedError = null;

                                var resultOutput = resultInput.CompensateWhenAny<ResultErrorTest2>(error => error.ErrorMessage == ErrorMessage2,
                                                                                                   error =>
                                                                                                   {
                                                                                                       passedError = error;

                                                                                                       return Core.Result.Result.Fail(error.ErrorMessage + ErrorMessage2);
                                                                                                   });

                                resultOutput.ShouldBeFailureWithError(ErrorMessage1);
                                passedError.Should().BeNull();
                            }

                            [Fact]
                            public void FailureInput_SuccessOutput()
                            {
                                var resultInput = Core.Result.Result.Fail(new ResultErrorTest(ErrorMessage1));

                                IResultError passedError = null;

                                var resultOutput = resultInput.CompensateWhenAny<ResultErrorTest2>(error => error.ErrorMessage == ErrorMessage2,
                                                                                                   error =>
                                                                                                   {
                                                                                                       passedError = error;

                                                                                                       return Core.Result.Result.Ok();
                                                                                                   });

                                resultOutput.ShouldBeFailureWithError(ErrorMessage1);
                                passedError.Should().BeNull();
                            }

                            [Fact]
                            public void SuccessInput_FailureOutput()
                            {
                                var resultInput = Core.Result.Result.Ok();

                                var resultOutput = resultInput.CompensateWhenAny<ResultErrorTest2>(error => error.ErrorMessage == ErrorMessage2,
                                                                                                   _ => Core.Result.Result.Fail(ErrorMessage1));

                                resultOutput.ShouldBeSuccess();
                            }

                            [Fact]
                            public void SuccessInput_SuccessOutput()
                            {
                                var resultInput = Core.Result.Result.Ok();

                                var resultOutput = resultInput.CompensateWhenAny<ResultErrorTest2>(error => error.ErrorMessage == ErrorMessage2,
                                                                                                   _ => Core.Result.Result.Ok());

                                resultOutput.ShouldBeSuccess();
                            }
                        }
                    }

                    public class WhenIResultErrorTypeDoeNotMatches
                    {
                        public class WhenFilterFuncTrue
                        {
                            [Fact]
                            public void FailureInput_FailureOutput()
                            {
                                var resultInput = Core.Result.Result.Fail(new ResultErrorTest(ErrorMessage1));

                                IResultError passedError = null;

                                var resultOutput = resultInput.CompensateWhenAny<ResultErrorMessage>(error => error.Message == ErrorMessage1,
                                                                                                     error =>
                                                                                                     {
                                                                                                         passedError = error;

                                                                                                         return Core.Result.Result.Fail(error);
                                                                                                     });

                                resultOutput.ShouldBeFailureWithError(ErrorMessage1);
                                passedError.Should().BeNull();
                            }

                            [Fact]
                            public void FailureInput_SuccessOutput()
                            {
                                var resultInput = Core.Result.Result.Fail(new ResultErrorTest(ErrorMessage1));

                                IResultError passedError = null;

                                var resultOutput = resultInput.CompensateWhenAny<ResultErrorMessage>(error => error.Message == ErrorMessage1,
                                                                                                     error =>
                                                                                                     {
                                                                                                         passedError = error;

                                                                                                         return Core.Result.Result.Ok();
                                                                                                     });

                                resultOutput.ShouldBeFailureWithError(ErrorMessage1);
                                passedError.Should().BeNull();
                            }

                            [Fact]
                            public void SuccessInput_FailureOutput()
                            {
                                var resultInput = Core.Result.Result.Ok();

                                var resultOutput = resultInput.CompensateWhenAny<ResultErrorTest2>(error => error.ErrorMessage == ErrorMessage1,
                                                                                                   _ => Core.Result.Result.Fail(ErrorMessage1));

                                resultOutput.ShouldBeSuccess();
                            }

                            [Fact]
                            public void SuccessInput_SuccessOutput()
                            {
                                var resultInput = Core.Result.Result.Ok();

                                var resultOutput = resultInput.CompensateWhenAny<ResultErrorTest2>(error => error.ErrorMessage == ErrorMessage1,
                                                                                                   _ => Core.Result.Result.Ok());

                                resultOutput.ShouldBeSuccess();
                            }
                        }

                        public class WhenFilterFuncFalse
                        {
                            [Fact]
                            public void FailureInput_FailureOutput()
                            {
                                var resultInput = Core.Result.Result.Fail(new ResultErrorTest(ErrorMessage1));

                                IResultError passedError = null;

                                var resultOutput = resultInput.CompensateWhenAny<ResultErrorMessage>(error => error.Message == ErrorMessage2,
                                                                                                     error =>
                                                                                                     {
                                                                                                         passedError = error;

                                                                                                         return Core.Result.Result.Fail(error);
                                                                                                     });

                                resultOutput.ShouldBeFailureWithError(ErrorMessage1);
                                passedError.Should().BeNull();
                            }

                            [Fact]
                            public void FailureInput_SuccessOutput()
                            {
                                var resultInput = Core.Result.Result.Fail(new ResultErrorTest(ErrorMessage1));

                                IResultError passedError = null;

                                var resultOutput = resultInput.CompensateWhenAny<ResultErrorMessage>(error => error.Message == ErrorMessage2,
                                                                                                     error =>
                                                                                                     {
                                                                                                         passedError = error;

                                                                                                         return Core.Result.Result.Ok();
                                                                                                     });

                                resultOutput.ShouldBeFailureWithError(ErrorMessage1);
                                passedError.Should().BeNull();
                            }

                            [Fact]
                            public void SuccessInput_FailureOutput()
                            {
                                var resultInput = Core.Result.Result.Ok();

                                var resultOutput = resultInput.CompensateWhenAny<ResultErrorTest2>(error => error.ErrorMessage == ErrorMessage2,
                                                                                                   _ => Core.Result.Result.Fail(ErrorMessage1));

                                resultOutput.ShouldBeSuccess();
                            }

                            [Fact]
                            public void SuccessInput_SuccessOutput()
                            {
                                var resultInput = Core.Result.Result.Ok();

                                var resultOutput = resultInput.CompensateWhenAny<ResultErrorTest2>(error => error.ErrorMessage == ErrorMessage2,
                                                                                                   _ => Core.Result.Result.Ok());

                                resultOutput.ShouldBeSuccess();
                            }
                        }
                    }
                }

                public class GenericInput
                {
                    public class WhenIResultErrorTypeMatches
                    {
                        public class WhenFilterFuncTrue
                        {
                            [Fact]
                            public void FailureInput_FailureOutput()
                            {
                                var resultInput = Core.Result.Result.Fail<string>(new ResultErrorTest(ErrorMessage1));

                                IResultError passedError = null;

                                var resultOutput = resultInput.CompensateWhenAny<ResultErrorTest2>(error => error.ErrorMessage == ErrorMessage1,
                                                                                                   error =>
                                                                                                   {
                                                                                                       passedError = error;

                                                                                                       return Core.Result.Result.Fail<string>(error.ErrorMessage + ErrorMessage2);
                                                                                                   });

                                resultOutput.ShouldBeFailureWithError($"{ErrorMessage1}{ErrorMessage2}");
                                passedError.ShouldBeError(ErrorMessage1);
                            }

                            [Fact]
                            public void FailureInput_SuccessOutput()
                            {
                                var value = Guid.NewGuid().ToString();

                                var resultInput = Core.Result.Result.Fail<string>(new ResultErrorTest(ErrorMessage1));

                                IResultError passedError = null;

                                var resultOutput = resultInput.CompensateWhenAny<ResultErrorTest2>(error => error.ErrorMessage == ErrorMessage1,
                                                                                                   error =>
                                                                                                   {
                                                                                                       passedError = error;

                                                                                                       return Core.Result.Result.Ok(value);
                                                                                                   });

                                resultOutput.ShouldBeSuccessWithValue(value);
                                passedError.ShouldBeError(ErrorMessage1);
                            }

                            [Fact]
                            public void SuccessInput_FailureOutput()
                            {
                                var value = Guid.NewGuid().ToString();
                                var resultInput = Core.Result.Result.Ok(value);

                                var resultOutput = resultInput.CompensateWhenAny<ResultErrorTest2>(error => error.ErrorMessage == ErrorMessage1,
                                                                                                   _ => Core.Result.Result.Fail<string>(ErrorMessage1));

                                resultOutput.ShouldBeSuccessWithValue(value);
                            }

                            [Fact]
                            public void SuccessInput_SuccessOutput()
                            {
                                var value = Guid.NewGuid().ToString();
                                var resultInput = Core.Result.Result.Ok(value);

                                var resultOutput = resultInput.CompensateWhenAny<ResultErrorTest2>(error => error.ErrorMessage == ErrorMessage1,
                                                                                                   _ => Core.Result.Result.Ok(Guid.NewGuid().ToString()));

                                resultOutput.ShouldBeSuccess();
                            }
                        }

                        public class WhenFilterFuncFalse
                        {
                            [Fact]
                            public void FailureInput_FailureOutput()
                            {
                                var resultInput = Core.Result.Result.Fail<string>(new ResultErrorTest(ErrorMessage1));

                                IResultError passedError = null;

                                var resultOutput = resultInput.CompensateWhenAny<ResultErrorTest2>(error => error.ErrorMessage == ErrorMessage2,
                                                                                                   error =>
                                                                                                   {
                                                                                                       passedError = error;

                                                                                                       return Core.Result.Result.Fail<string>(error.ErrorMessage + ErrorMessage2);
                                                                                                   });

                                resultOutput.ShouldBeFailureWithError(ErrorMessage1);
                                passedError.Should().BeNull();
                            }

                            [Fact]
                            public void FailureInput_SuccessOutput()
                            {
                                var value = Guid.NewGuid().ToString();

                                var resultInput = Core.Result.Result.Fail<string>(new ResultErrorTest(ErrorMessage1));

                                IResultError passedError = null;

                                var resultOutput = resultInput.CompensateWhenAny<ResultErrorTest2>(error => error.ErrorMessage == ErrorMessage2,
                                                                                                   error =>
                                                                                                   {
                                                                                                       passedError = error;

                                                                                                       return Core.Result.Result.Ok(value);
                                                                                                   });

                                resultOutput.ShouldBeFailureWithError(ErrorMessage1);
                                passedError.Should().BeNull();
                            }

                            [Fact]
                            public void SuccessInput_FailureOutput()
                            {
                                var value = Guid.NewGuid().ToString();
                                var resultInput = Core.Result.Result.Ok(value);

                                var resultOutput = resultInput.CompensateWhenAny<ResultErrorTest2>(error => error.ErrorMessage == ErrorMessage2,
                                                                                                   _ => Core.Result.Result.Fail<string>(ErrorMessage1));

                                resultOutput.ShouldBeSuccessWithValue(value);
                            }

                            [Fact]
                            public void SuccessInput_SuccessOutput()
                            {
                                var value = Guid.NewGuid().ToString();
                                var resultInput = Core.Result.Result.Ok(value);

                                var resultOutput = resultInput.CompensateWhenAny<ResultErrorTest2>(error => error.ErrorMessage == ErrorMessage2,
                                                                                                   _ => Core.Result.Result.Ok(Guid.NewGuid().ToString()));

                                resultOutput.ShouldBeSuccess();
                            }
                        }
                    }

                    public class WhenIResultErrorTypeDoeNotMatches
                    {
                        public class WhenFilterFuncTrue
                        {
                            [Fact]
                            public void FailureInput_FailureOutput()
                            {
                                var resultInput = Core.Result.Result.Fail<string>(new ResultErrorTest(ErrorMessage1));

                                IResultError passedError = null;

                                var resultOutput = resultInput.CompensateWhenAny<ResultErrorMessage>(error => error.Message == ErrorMessage1,
                                                                                                     error =>
                                                                                                     {
                                                                                                         passedError = error;

                                                                                                         return Core.Result.Result.Fail<string>(error);
                                                                                                     });

                                resultOutput.ShouldBeFailureWithError(ErrorMessage1);
                                passedError.Should().BeNull();
                            }

                            [Fact]
                            public void FailureInput_SuccessOutput()
                            {
                                var resultInput = Core.Result.Result.Fail<string>(new ResultErrorTest(ErrorMessage1));

                                IResultError passedError = null;

                                var resultOutput = resultInput.CompensateWhenAny<ResultErrorMessage>(error => error.Message == ErrorMessage1,
                                                                                                     error =>
                                                                                                     {
                                                                                                         passedError = error;

                                                                                                         return Core.Result.Result.Ok(Guid.NewGuid().ToString());
                                                                                                     });

                                resultOutput.ShouldBeFailureWithError(ErrorMessage1);
                                passedError.Should().BeNull();
                            }

                            [Fact]
                            public void SuccessInput_FailureOutput()
                            {
                                var value = Guid.NewGuid().ToString();
                                var resultInput = Core.Result.Result.Ok(value);

                                var resultOutput = resultInput.CompensateWhenAny<ResultErrorTest2>(error => error.ErrorMessage == ErrorMessage1,
                                                                                                   _ => Core.Result.Result.Fail<string>(ErrorMessage1));

                                resultOutput.ShouldBeSuccessWithValue(value);
                            }

                            [Fact]
                            public void SuccessInput_SuccessOutput()
                            {
                                var value = Guid.NewGuid().ToString();
                                var resultInput = Core.Result.Result.Ok(value);

                                var resultOutput = resultInput.CompensateWhenAny<ResultErrorTest2>(error => error.ErrorMessage == ErrorMessage1,
                                                                                                   _ => Core.Result.Result.Ok(Guid.NewGuid().ToString()));

                                resultOutput.ShouldBeSuccess();
                            }
                        }

                        public class WhenFilterFuncFalse
                        {
                            [Fact]
                            public void FailureInput_FailureOutput()
                            {
                                var resultInput = Core.Result.Result.Fail<string>(new ResultErrorTest(ErrorMessage1));

                                IResultError passedError = null;

                                var resultOutput = resultInput.CompensateWhenAny<ResultErrorMessage>(error => error.Message == ErrorMessage2,
                                                                                                     error =>
                                                                                                     {
                                                                                                         passedError = error;

                                                                                                         return Core.Result.Result.Fail<string>(error);
                                                                                                     });

                                resultOutput.ShouldBeFailureWithError(ErrorMessage1);
                                passedError.Should().BeNull();
                            }

                            [Fact]
                            public void FailureInput_SuccessOutput()
                            {
                                var resultInput = Core.Result.Result.Fail<string>(new ResultErrorTest(ErrorMessage1));

                                IResultError passedError = null;

                                var resultOutput = resultInput.CompensateWhenAny<ResultErrorMessage>(error => error.Message == ErrorMessage2,
                                                                                                     error =>
                                                                                                     {
                                                                                                         passedError = error;

                                                                                                         return Core.Result.Result.Ok(Guid.NewGuid().ToString());
                                                                                                     });

                                resultOutput.ShouldBeFailureWithError(ErrorMessage1);
                                passedError.Should().BeNull();
                            }

                            [Fact]
                            public void SuccessInput_FailureOutput()
                            {
                                var value = Guid.NewGuid().ToString();
                                var resultInput = Core.Result.Result.Ok(value);

                                var resultOutput = resultInput.CompensateWhenAny<ResultErrorTest2>(error => error.ErrorMessage == ErrorMessage2,
                                                                                                   _ => Core.Result.Result.Fail<string>(ErrorMessage1));

                                resultOutput.ShouldBeSuccessWithValue(value);
                            }

                            [Fact]
                            public void SuccessInput_SuccessOutput()
                            {
                                var value = Guid.NewGuid().ToString();
                                var resultInput = Core.Result.Result.Ok(value);

                                var resultOutput = resultInput.CompensateWhenAny<ResultErrorTest2>(error => error.ErrorMessage == ErrorMessage2,
                                                                                                   _ => Core.Result.Result.Ok(Guid.NewGuid().ToString()));

                                resultOutput.ShouldBeSuccess();
                            }
                        }
                    }
                }
            }

            public class AsyncReturn
            {
                public class NonGenericInput
                {
                    public class WhenIResultErrorTypeMatches
                    {
                        public class WhenFilterFuncTrue
                        {
                            [Fact]
                            public async Task FailureInput_FailureOutput()
                            {
                                var resultInput = Core.Result.Result.Fail(new ResultErrorTest(ErrorMessage1));

                                IResultError passedError = null;

                                var resultOutput = await resultInput.CompensateWhenAnyAsync<ResultErrorTest2>(error => error.ErrorMessage == ErrorMessage1,
                                                                                                              error =>
                                                                                                              {
                                                                                                                  passedError = error;

                                                                                                                  return Core.Result.Result.FailAsync(error.ErrorMessage + ErrorMessage2);
                                                                                                              });

                                resultOutput.ShouldBeFailureWithError($"{ErrorMessage1}{ErrorMessage2}");
                                passedError.ShouldBeError(ErrorMessage1);
                            }

                            [Fact]
                            public async Task FailureInput_SuccessOutput()
                            {
                                var resultInput = Core.Result.Result.Fail(new ResultErrorTest(ErrorMessage1));

                                IResultError passedError = null;

                                var resultOutput = await resultInput.CompensateWhenAnyAsync<ResultErrorTest2>(error => error.ErrorMessage == ErrorMessage1,
                                                                                                              error =>
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

                                var resultOutput = await resultInput.CompensateWhenAnyAsync<ResultErrorTest2>(error => error.ErrorMessage == ErrorMessage1,
                                                                                                              _ => Core.Result.Result.FailAsync(ErrorMessage1));

                                resultOutput.ShouldBeSuccess();
                            }

                            [Fact]
                            public async Task SuccessInput_SuccessOutput()
                            {
                                var resultInput = Core.Result.Result.Ok();

                                var resultOutput = await resultInput.CompensateWhenAnyAsync<ResultErrorTest2>(error => error.ErrorMessage == ErrorMessage1,
                                                                                                              _ => Core.Result.Result.OkAsync());

                                resultOutput.ShouldBeSuccess();
                            }
                        }

                        public class WhenFilterFuncFalse
                        {
                            [Fact]
                            public async Task FailureInput_FailureOutput()
                            {
                                var resultInput = Core.Result.Result.Fail(new ResultErrorTest(ErrorMessage1));

                                IResultError passedError = null;

                                var resultOutput = await resultInput.CompensateWhenAnyAsync<ResultErrorTest2>(error => error.ErrorMessage == ErrorMessage2,
                                                                                                              error =>
                                                                                                              {
                                                                                                                  passedError = error;

                                                                                                                  return Core.Result.Result.FailAsync(error.ErrorMessage + ErrorMessage2);
                                                                                                              });

                                resultOutput.ShouldBeFailureWithError(ErrorMessage1);
                                passedError.Should().BeNull();
                            }

                            [Fact]
                            public async Task FailureInput_SuccessOutput()
                            {
                                var resultInput = Core.Result.Result.Fail(new ResultErrorTest(ErrorMessage1));

                                IResultError passedError = null;

                                var resultOutput = await resultInput.CompensateWhenAnyAsync<ResultErrorTest2>(error => error.ErrorMessage == ErrorMessage2,
                                                                                                              error =>
                                                                                                              {
                                                                                                                  passedError = error;

                                                                                                                  return Core.Result.Result.OkAsync();
                                                                                                              });

                                resultOutput.ShouldBeFailureWithError(ErrorMessage1);
                                passedError.Should().BeNull();
                            }

                            [Fact]
                            public async Task SuccessInput_FailureOutput()
                            {
                                var resultInput = Core.Result.Result.Ok();

                                var resultOutput = await resultInput.CompensateWhenAnyAsync<ResultErrorTest2>(error => error.ErrorMessage == ErrorMessage2,
                                                                                                              _ => Core.Result.Result.FailAsync(ErrorMessage1));

                                resultOutput.ShouldBeSuccess();
                            }

                            [Fact]
                            public async Task SuccessInput_SuccessOutput()
                            {
                                var resultInput = Core.Result.Result.Ok();

                                var resultOutput = await resultInput.CompensateWhenAnyAsync<ResultErrorTest2>(error => error.ErrorMessage == ErrorMessage2,
                                                                                                              _ => Core.Result.Result.OkAsync());

                                resultOutput.ShouldBeSuccess();
                            }
                        }
                    }

                    public class WhenIResultErrorTypeDoeNotMatches
                    {
                        public class WhenFilterFuncTrue
                        {
                            [Fact]
                            public async Task FailureInput_FailureOutput()
                            {
                                var resultInput = Core.Result.Result.Fail(new ResultErrorTest(ErrorMessage1));

                                IResultError passedError = null;

                                var resultOutput = await resultInput.CompensateWhenAnyAsync<ResultErrorMessage>(error => error.Message == ErrorMessage1,
                                                                                                                error =>
                                                                                                                {
                                                                                                                    passedError = error;

                                                                                                                    return Core.Result.Result.FailAsync(error);
                                                                                                                });

                                resultOutput.ShouldBeFailureWithError(ErrorMessage1);
                                passedError.Should().BeNull();
                            }

                            [Fact]
                            public async Task FailureInput_SuccessOutput()
                            {
                                var resultInput = Core.Result.Result.Fail(new ResultErrorTest(ErrorMessage1));

                                IResultError passedError = null;

                                var resultOutput = await resultInput.CompensateWhenAnyAsync<ResultErrorMessage>(error => error.Message == ErrorMessage1,
                                                                                                                error =>
                                                                                                                {
                                                                                                                    passedError = error;

                                                                                                                    return Core.Result.Result.OkAsync();
                                                                                                                });

                                resultOutput.ShouldBeFailureWithError(ErrorMessage1);
                                passedError.Should().BeNull();
                            }

                            [Fact]
                            public async Task SuccessInput_FailureOutput()
                            {
                                var resultInput = Core.Result.Result.Ok();

                                var resultOutput = await resultInput.CompensateWhenAnyAsync<ResultErrorTest2>(error => error.ErrorMessage == ErrorMessage1,
                                                                                                              _ => Core.Result.Result.FailAsync(ErrorMessage1));

                                resultOutput.ShouldBeSuccess();
                            }

                            [Fact]
                            public async Task SuccessInput_SuccessOutput()
                            {
                                var resultInput = Core.Result.Result.Ok();

                                var resultOutput = await resultInput.CompensateWhenAnyAsync<ResultErrorTest2>(error => error.ErrorMessage == ErrorMessage1,
                                                                                                              _ => Core.Result.Result.OkAsync());

                                resultOutput.ShouldBeSuccess();
                            }
                        }

                        public class WhenFilterFuncFalse
                        {
                            [Fact]
                            public async Task FailureInput_FailureOutput()
                            {
                                var resultInput = Core.Result.Result.Fail(new ResultErrorTest(ErrorMessage1));

                                IResultError passedError = null;

                                var resultOutput = await resultInput.CompensateWhenAnyAsync<ResultErrorMessage>(error => error.Message == ErrorMessage2,
                                                                                                                error =>
                                                                                                                {
                                                                                                                    passedError = error;

                                                                                                                    return Core.Result.Result.FailAsync(error);
                                                                                                                });

                                resultOutput.ShouldBeFailureWithError(ErrorMessage1);
                                passedError.Should().BeNull();
                            }

                            [Fact]
                            public async Task FailureInput_SuccessOutput()
                            {
                                var resultInput = Core.Result.Result.Fail(new ResultErrorTest(ErrorMessage1));

                                IResultError passedError = null;

                                var resultOutput = await resultInput.CompensateWhenAnyAsync<ResultErrorMessage>(error => error.Message == ErrorMessage2,
                                                                                                                error =>
                                                                                                                {
                                                                                                                    passedError = error;

                                                                                                                    return Core.Result.Result.OkAsync();
                                                                                                                });

                                resultOutput.ShouldBeFailureWithError(ErrorMessage1);
                                passedError.Should().BeNull();
                            }

                            [Fact]
                            public async Task SuccessInput_FailureOutput()
                            {
                                var resultInput = Core.Result.Result.Ok();

                                var resultOutput = await resultInput.CompensateWhenAnyAsync<ResultErrorTest2>(error => error.ErrorMessage == ErrorMessage2,
                                                                                                              _ => Core.Result.Result.FailAsync(ErrorMessage1));

                                resultOutput.ShouldBeSuccess();
                            }

                            [Fact]
                            public async Task SuccessInput_SuccessOutput()
                            {
                                var resultInput = Core.Result.Result.Ok();

                                var resultOutput = await resultInput.CompensateWhenAnyAsync<ResultErrorTest2>(error => error.ErrorMessage == ErrorMessage2,
                                                                                                              _ => Core.Result.Result.OkAsync());

                                resultOutput.ShouldBeSuccess();
                            }
                        }
                    }
                }

                public class GenericInput
                {
                    public class WhenIResultErrorTypeMatches
                    {
                        public class WhenFilterFuncTrue
                        {
                            [Fact]
                            public async Task FailureInput_FailureOutput()
                            {
                                var resultInput = Core.Result.Result.Fail<string>(new ResultErrorTest(ErrorMessage1));

                                IResultError passedError = null;

                                var resultOutput = await resultInput.CompensateWhenAnyAsync<ResultErrorTest2>(error => error.ErrorMessage == ErrorMessage1,
                                                                                                              error =>
                                                                                                              {
                                                                                                                  passedError = error;

                                                                                                                  return Core.Result.Result.FailAsync<string>(error.ErrorMessage + ErrorMessage2);
                                                                                                              });

                                resultOutput.ShouldBeFailureWithError($"{ErrorMessage1}{ErrorMessage2}");
                                passedError.ShouldBeError(ErrorMessage1);
                            }

                            [Fact]
                            public async Task FailureInput_SuccessOutput()
                            {
                                var value = Guid.NewGuid().ToString();

                                var resultInput = Core.Result.Result.Fail<string>(new ResultErrorTest(ErrorMessage1));

                                IResultError passedError = null;

                                var resultOutput = await resultInput.CompensateWhenAnyAsync<ResultErrorTest2>(error => error.ErrorMessage == ErrorMessage1,
                                                                                                              error =>
                                                                                                              {
                                                                                                                  passedError = error;

                                                                                                                  return Core.Result.Result.OkAsync(value);
                                                                                                              });

                                resultOutput.ShouldBeSuccessWithValue(value);
                                passedError.ShouldBeError(ErrorMessage1);
                            }

                            [Fact]
                            public async Task SuccessInput_FailureOutput()
                            {
                                var value = Guid.NewGuid().ToString();
                                var resultInput = Core.Result.Result.Ok(value);

                                var resultOutput = await resultInput.CompensateWhenAnyAsync<ResultErrorTest2>(error => error.ErrorMessage == ErrorMessage1,
                                                                                                              _ => Core.Result.Result.FailAsync<string>(ErrorMessage1));

                                resultOutput.ShouldBeSuccessWithValue(value);
                            }

                            [Fact]
                            public async Task SuccessInput_SuccessOutput()
                            {
                                var value = Guid.NewGuid().ToString();
                                var resultInput = Core.Result.Result.Ok(value);

                                var resultOutput = await resultInput.CompensateWhenAnyAsync<ResultErrorTest2>(error => error.ErrorMessage == ErrorMessage1,
                                                                                                              _ => Core.Result.Result.OkAsync(Guid.NewGuid().ToString()));

                                resultOutput.ShouldBeSuccess();
                            }
                        }

                        public class WhenFilterFuncFalse
                        {
                            [Fact]
                            public async Task FailureInput_FailureOutput()
                            {
                                var resultInput = Core.Result.Result.Fail<string>(new ResultErrorTest(ErrorMessage1));

                                IResultError passedError = null;

                                var resultOutput = await resultInput.CompensateWhenAnyAsync<ResultErrorTest2>(error => error.ErrorMessage == ErrorMessage2,
                                                                                                              error =>
                                                                                                              {
                                                                                                                  passedError = error;

                                                                                                                  return Core.Result.Result.FailAsync<string>(error.ErrorMessage + ErrorMessage2);
                                                                                                              });

                                resultOutput.ShouldBeFailureWithError(ErrorMessage1);
                                passedError.Should().BeNull();
                            }

                            [Fact]
                            public async Task FailureInput_SuccessOutput()
                            {
                                var value = Guid.NewGuid().ToString();

                                var resultInput = Core.Result.Result.Fail<string>(new ResultErrorTest(ErrorMessage1));

                                IResultError passedError = null;

                                var resultOutput = await resultInput.CompensateWhenAnyAsync<ResultErrorTest2>(error => error.ErrorMessage == ErrorMessage2,
                                                                                                              error =>
                                                                                                              {
                                                                                                                  passedError = error;

                                                                                                                  return Core.Result.Result.OkAsync(value);
                                                                                                              });

                                resultOutput.ShouldBeFailureWithError(ErrorMessage1);
                                passedError.Should().BeNull();
                            }

                            [Fact]
                            public async Task SuccessInput_FailureOutput()
                            {
                                var value = Guid.NewGuid().ToString();
                                var resultInput = Core.Result.Result.Ok(value);

                                var resultOutput = await resultInput.CompensateWhenAnyAsync<ResultErrorTest2>(error => error.ErrorMessage == ErrorMessage2,
                                                                                                              _ => Core.Result.Result.FailAsync<string>(ErrorMessage1));

                                resultOutput.ShouldBeSuccessWithValue(value);
                            }

                            [Fact]
                            public async Task SuccessInput_SuccessOutput()
                            {
                                var value = Guid.NewGuid().ToString();
                                var resultInput = Core.Result.Result.Ok(value);

                                var resultOutput = await resultInput.CompensateWhenAnyAsync<ResultErrorTest2>(error => error.ErrorMessage == ErrorMessage2,
                                                                                                              _ => Core.Result.Result.OkAsync(Guid.NewGuid().ToString()));

                                resultOutput.ShouldBeSuccess();
                            }
                        }
                    }

                    public class WhenIResultErrorTypeDoeNotMatches
                    {
                        public class WhenFilterFuncTrue
                        {
                            [Fact]
                            public async Task FailureInput_FailureOutput()
                            {
                                var resultInput = Core.Result.Result.Fail<string>(new ResultErrorTest(ErrorMessage1));

                                IResultError passedError = null;

                                var resultOutput = await resultInput.CompensateWhenAnyAsync<ResultErrorMessage>(error => error.Message == ErrorMessage1,
                                                                                                                error =>
                                                                                                                {
                                                                                                                    passedError = error;

                                                                                                                    return Core.Result.Result.FailAsync<string>(error);
                                                                                                                });

                                resultOutput.ShouldBeFailureWithError(ErrorMessage1);
                                passedError.Should().BeNull();
                            }

                            [Fact]
                            public async Task FailureInput_SuccessOutput()
                            {
                                var resultInput = Core.Result.Result.Fail<string>(new ResultErrorTest(ErrorMessage1));

                                IResultError passedError = null;

                                var resultOutput = await resultInput.CompensateWhenAnyAsync<ResultErrorMessage>(error => error.Message == ErrorMessage1,
                                                                                                                error =>
                                                                                                                {
                                                                                                                    passedError = error;

                                                                                                                    return Core.Result.Result.OkAsync(Guid.NewGuid().ToString());
                                                                                                                });

                                resultOutput.ShouldBeFailureWithError(ErrorMessage1);
                                passedError.Should().BeNull();
                            }

                            [Fact]
                            public async Task SuccessInput_FailureOutput()
                            {
                                var value = Guid.NewGuid().ToString();
                                var resultInput = Core.Result.Result.Ok(value);

                                var resultOutput = await resultInput.CompensateWhenAnyAsync<ResultErrorTest2>(error => error.ErrorMessage == ErrorMessage1,
                                                                                                              _ => Core.Result.Result.FailAsync<string>(ErrorMessage1));

                                resultOutput.ShouldBeSuccessWithValue(value);
                            }

                            [Fact]
                            public async Task SuccessInput_SuccessOutput()
                            {
                                var value = Guid.NewGuid().ToString();
                                var resultInput = Core.Result.Result.Ok(value);

                                var resultOutput = await resultInput.CompensateWhenAnyAsync<ResultErrorTest2>(error => error.ErrorMessage == ErrorMessage1,
                                                                                                              _ => Core.Result.Result.OkAsync(Guid.NewGuid().ToString()));

                                resultOutput.ShouldBeSuccess();
                            }
                        }

                        public class WhenFilterFuncFalse
                        {
                            [Fact]
                            public async Task FailureInput_FailureOutput()
                            {
                                var resultInput = Core.Result.Result.Fail<string>(new ResultErrorTest(ErrorMessage1));

                                IResultError passedError = null;

                                var resultOutput = await resultInput.CompensateWhenAnyAsync<ResultErrorMessage>(error => error.Message == ErrorMessage2,
                                                                                                                error =>
                                                                                                                {
                                                                                                                    passedError = error;

                                                                                                                    return Core.Result.Result.FailAsync<string>(error);
                                                                                                                });

                                resultOutput.ShouldBeFailureWithError(ErrorMessage1);
                                passedError.Should().BeNull();
                            }

                            [Fact]
                            public async Task FailureInput_SuccessOutput()
                            {
                                var resultInput = Core.Result.Result.Fail<string>(new ResultErrorTest(ErrorMessage1));

                                IResultError passedError = null;

                                var resultOutput = await resultInput.CompensateWhenAnyAsync<ResultErrorMessage>(error => error.Message == ErrorMessage2,
                                                                                                                error =>
                                                                                                                {
                                                                                                                    passedError = error;

                                                                                                                    return Core.Result.Result.OkAsync(Guid.NewGuid().ToString());
                                                                                                                });

                                resultOutput.ShouldBeFailureWithError(ErrorMessage1);
                                passedError.Should().BeNull();
                            }

                            [Fact]
                            public async Task SuccessInput_FailureOutput()
                            {
                                var value = Guid.NewGuid().ToString();
                                var resultInput = Core.Result.Result.Ok(value);

                                var resultOutput = await resultInput.CompensateWhenAnyAsync<ResultErrorTest2>(error => error.ErrorMessage == ErrorMessage2,
                                                                                                              _ => Core.Result.Result.FailAsync<string>(ErrorMessage1));

                                resultOutput.ShouldBeSuccessWithValue(value);
                            }

                            [Fact]
                            public async Task SuccessInput_SuccessOutput()
                            {
                                var value = Guid.NewGuid().ToString();
                                var resultInput = Core.Result.Result.Ok(value);

                                var resultOutput = await resultInput.CompensateWhenAnyAsync<ResultErrorTest2>(error => error.ErrorMessage == ErrorMessage2,
                                                                                                              _ => Core.Result.Result.OkAsync(Guid.NewGuid().ToString()));

                                resultOutput.ShouldBeSuccess();
                            }
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
                    public class WhenIResultErrorTypeMatches
                    {
                        public class WhenFilterFuncTrue
                        {
                            [Fact]
                            public async Task FailureInput_FailureOutput()
                            {
                                var resultInput = Core.Result.Result.FailAsync(new ResultErrorTest(ErrorMessage1));

                                IResultError passedError = null;

                                var resultOutput = await resultInput.CompensateWhenAnyAsync<ResultErrorTest2>(error => error.ErrorMessage == ErrorMessage1,
                                                                                                        error =>
                                                                                                        {
                                                                                                            passedError = error;

                                                                                                            return Core.Result.Result.Fail(error.ErrorMessage + ErrorMessage2);
                                                                                                        });

                                resultOutput.ShouldBeFailureWithError($"{ErrorMessage1}{ErrorMessage2}");
                                passedError.ShouldBeError(ErrorMessage1);
                            }

                            [Fact]
                            public async Task FailureInput_SuccessOutput()
                            {
                                var resultInput = Core.Result.Result.FailAsync(new ResultErrorTest(ErrorMessage1));

                                IResultError passedError = null;

                                var resultOutput = await resultInput.CompensateWhenAnyAsync<ResultErrorTest2>(error => error.ErrorMessage == ErrorMessage1,
                                                                                                        error =>
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

                                var resultOutput = await resultInput.CompensateWhenAnyAsync<ResultErrorTest2>(error => error.ErrorMessage == ErrorMessage1,
                                                                                                        _ => Core.Result.Result.Fail(ErrorMessage1));

                                resultOutput.ShouldBeSuccess();
                            }

                            [Fact]
                            public async Task SuccessInput_SuccessOutput()
                            {
                                var resultInput = Core.Result.Result.OkAsync();

                                var resultOutput = await resultInput.CompensateWhenAnyAsync<ResultErrorTest2>(error => error.ErrorMessage == ErrorMessage1,
                                                                                                        _ => Core.Result.Result.Ok());

                                resultOutput.ShouldBeSuccess();
                            }
                        }

                        public class WhenFilterFuncFalse
                        {
                            [Fact]
                            public async Task FailureInput_FailureOutput()
                            {
                                var resultInput = Core.Result.Result.FailAsync(new ResultErrorTest(ErrorMessage1));

                                IResultError passedError = null;

                                var resultOutput = await resultInput.CompensateWhenAnyAsync<ResultErrorTest2>(error => error.ErrorMessage == ErrorMessage2,
                                                                                                        error =>
                                                                                                        {
                                                                                                            passedError = error;

                                                                                                            return Core.Result.Result.Fail(error.ErrorMessage + ErrorMessage2);
                                                                                                        });

                                resultOutput.ShouldBeFailureWithError(ErrorMessage1);
                                passedError.Should().BeNull();
                            }

                            [Fact]
                            public async Task FailureInput_SuccessOutput()
                            {
                                var resultInput = Core.Result.Result.FailAsync(new ResultErrorTest(ErrorMessage1));

                                IResultError passedError = null;

                                var resultOutput = await resultInput.CompensateWhenAnyAsync<ResultErrorTest2>(error => error.ErrorMessage == ErrorMessage2,
                                                                                                        error =>
                                                                                                        {
                                                                                                            passedError = error;

                                                                                                            return Core.Result.Result.Ok();
                                                                                                        });

                                resultOutput.ShouldBeFailureWithError(ErrorMessage1);
                                passedError.Should().BeNull();
                            }

                            [Fact]
                            public async Task SuccessInput_FailureOutput()
                            {
                                var resultInput = Core.Result.Result.OkAsync();

                                var resultOutput = await resultInput.CompensateWhenAnyAsync<ResultErrorTest2>(error => error.ErrorMessage == ErrorMessage2,
                                                                                                        _ => Core.Result.Result.Fail(ErrorMessage1));

                                resultOutput.ShouldBeSuccess();
                            }

                            [Fact]
                            public async Task SuccessInput_SuccessOutput()
                            {
                                var resultInput = Core.Result.Result.OkAsync();

                                var resultOutput = await resultInput.CompensateWhenAnyAsync<ResultErrorTest2>(error => error.ErrorMessage == ErrorMessage2,
                                                                                                        _ => Core.Result.Result.Ok());

                                resultOutput.ShouldBeSuccess();
                            }
                        }
                    }

                    public class WhenIResultErrorTypeDoeNotMatches
                    {
                        public class WhenFilterFuncTrue
                        {
                            [Fact]
                            public async Task FailureInput_FailureOutput()
                            {
                                var resultInput = Core.Result.Result.FailAsync(new ResultErrorTest(ErrorMessage1));

                                IResultError passedError = null;

                                var resultOutput = await resultInput.CompensateWhenAnyAsync<ResultErrorMessage>(error => error.Message == ErrorMessage1,
                                                                                                          error =>
                                                                                                          {
                                                                                                              passedError = error;

                                                                                                              return Core.Result.Result.Fail(error);
                                                                                                          });

                                resultOutput.ShouldBeFailureWithError(ErrorMessage1);
                                passedError.Should().BeNull();
                            }

                            [Fact]
                            public async Task FailureInput_SuccessOutput()
                            {
                                var resultInput = Core.Result.Result.FailAsync(new ResultErrorTest(ErrorMessage1));

                                IResultError passedError = null;

                                var resultOutput = await resultInput.CompensateWhenAnyAsync<ResultErrorMessage>(error => error.Message == ErrorMessage1,
                                                                                                          error =>
                                                                                                          {
                                                                                                              passedError = error;

                                                                                                              return Core.Result.Result.Ok();
                                                                                                          });

                                resultOutput.ShouldBeFailureWithError(ErrorMessage1);
                                passedError.Should().BeNull();
                            }

                            [Fact]
                            public async Task SuccessInput_FailureOutput()
                            {
                                var resultInput = Core.Result.Result.OkAsync();

                                var resultOutput = await resultInput.CompensateWhenAnyAsync<ResultErrorTest2>(error => error.ErrorMessage == ErrorMessage1,
                                                                                                   _ => Core.Result.Result.Fail(ErrorMessage1));

                                resultOutput.ShouldBeSuccess();
                            }

                            [Fact]
                            public async Task SuccessInput_SuccessOutput()
                            {
                                var resultInput = Core.Result.Result.OkAsync();

                                var resultOutput = await resultInput.CompensateWhenAnyAsync<ResultErrorTest2>(error => error.ErrorMessage == ErrorMessage1,
                                                                                                        _ => Core.Result.Result.Ok());

                                resultOutput.ShouldBeSuccess();
                            }
                        }

                        public class WhenFilterFuncFalse
                        {
                            [Fact]
                            public async Task FailureInput_FailureOutput()
                            {
                                var resultInput = Core.Result.Result.FailAsync(new ResultErrorTest(ErrorMessage1));

                                IResultError passedError = null;

                                var resultOutput = await resultInput.CompensateWhenAnyAsync<ResultErrorMessage>(error => error.Message == ErrorMessage2,
                                                                                                          error =>
                                                                                                          {
                                                                                                              passedError = error;

                                                                                                              return Core.Result.Result.Fail(error);
                                                                                                          });

                                resultOutput.ShouldBeFailureWithError(ErrorMessage1);
                                passedError.Should().BeNull();
                            }

                            [Fact]
                            public async Task FailureInput_SuccessOutput()
                            {
                                var resultInput = Core.Result.Result.FailAsync(new ResultErrorTest(ErrorMessage1));

                                IResultError passedError = null;

                                var resultOutput = await resultInput.CompensateWhenAnyAsync<ResultErrorMessage>(error => error.Message == ErrorMessage2,
                                                                                                          error =>
                                                                                                          {
                                                                                                              passedError = error;

                                                                                                              return Core.Result.Result.Ok();
                                                                                                          });

                                resultOutput.ShouldBeFailureWithError(ErrorMessage1);
                                passedError.Should().BeNull();
                            }

                            [Fact]
                            public async Task SuccessInput_FailureOutput()
                            {
                                var resultInput = Core.Result.Result.OkAsync();

                                var resultOutput = await resultInput.CompensateWhenAnyAsync<ResultErrorTest2>(error => error.ErrorMessage == ErrorMessage2,
                                                                                                        _ => Core.Result.Result.Fail(ErrorMessage1));

                                resultOutput.ShouldBeSuccess();
                            }

                            [Fact]
                            public async Task SuccessInput_SuccessOutput()
                            {
                                var resultInput = Core.Result.Result.OkAsync();

                                var resultOutput = await resultInput.CompensateWhenAnyAsync<ResultErrorTest2>(error => error.ErrorMessage == ErrorMessage2,
                                                                                                        _ => Core.Result.Result.Ok());

                                resultOutput.ShouldBeSuccess();
                            }
                        }
                    }
                }

                public class GenericInput
                {
                    public class WhenIResultErrorTypeMatches
                    {
                        public class WhenFilterFuncTrue
                        {
                            [Fact]
                            public async Task FailureInput_FailureOutput()
                            {
                                var resultInput = Core.Result.Result.FailAsync<string>(new ResultErrorTest(ErrorMessage1));

                                IResultError passedError = null;

                                var resultOutput = await resultInput.CompensateWhenAnyAsync<string, ResultErrorTest2>(error => error.ErrorMessage == ErrorMessage1,
                                                                                                                      error =>
                                                                                                                      {
                                                                                                                          passedError = error;

                                                                                                                          return Core.Result.Result.Fail<string>(error.ErrorMessage + ErrorMessage2);
                                                                                                                      });

                                resultOutput.ShouldBeFailureWithError($"{ErrorMessage1}{ErrorMessage2}");
                                passedError.ShouldBeError(ErrorMessage1);
                            }

                            [Fact]
                            public async Task FailureInput_SuccessOutput()
                            {
                                var value = Guid.NewGuid().ToString();

                                var resultInput = Core.Result.Result.FailAsync<string>(new ResultErrorTest(ErrorMessage1));

                                IResultError passedError = null;

                                var resultOutput = await resultInput.CompensateWhenAnyAsync<string, ResultErrorTest2>(error => error.ErrorMessage == ErrorMessage1,
                                                                                                                      error =>
                                                                                                                      {
                                                                                                                          passedError = error;

                                                                                                                          return Core.Result.Result.Ok(value);
                                                                                                                      });

                                resultOutput.ShouldBeSuccessWithValue(value);
                                passedError.ShouldBeError(ErrorMessage1);
                            }

                            [Fact]
                            public async Task SuccessInput_FailureOutput()
                            {
                                var value = Guid.NewGuid().ToString();
                                var resultInput = Core.Result.Result.OkAsync(value);

                                var resultOutput = await resultInput.CompensateWhenAnyAsync<string, ResultErrorTest2>(error => error.ErrorMessage == ErrorMessage1,
                                                                                                                      _ => Core.Result.Result.Fail<string>(ErrorMessage1));

                                resultOutput.ShouldBeSuccessWithValue(value);
                            }

                            [Fact]
                            public async Task SuccessInput_SuccessOutput()
                            {
                                var value = Guid.NewGuid().ToString();
                                var resultInput = Core.Result.Result.OkAsync(value);

                                var resultOutput = await resultInput.CompensateWhenAnyAsync<string, ResultErrorTest2>(error => error.ErrorMessage == ErrorMessage1,
                                                                                                                      _ => Core.Result.Result.Ok(Guid.NewGuid().ToString()));

                                resultOutput.ShouldBeSuccess();
                            }
                        }

                        public class WhenFilterFuncFalse
                        {
                            [Fact]
                            public async Task FailureInput_FailureOutput()
                            {
                                var resultInput = Core.Result.Result.FailAsync<string>(new ResultErrorTest(ErrorMessage1));

                                IResultError passedError = null;

                                var resultOutput = await resultInput.CompensateWhenAnyAsync<string, ResultErrorTest2>(error => error.ErrorMessage == ErrorMessage2,
                                                                                                                      error =>
                                                                                                                      {
                                                                                                                          passedError = error;

                                                                                                                          return Core.Result.Result.Fail<string>(error.ErrorMessage + ErrorMessage2);
                                                                                                                      });

                                resultOutput.ShouldBeFailureWithError(ErrorMessage1);
                                passedError.Should().BeNull();
                            }

                            [Fact]
                            public async Task FailureInput_SuccessOutput()
                            {
                                var value = Guid.NewGuid().ToString();

                                var resultInput = Core.Result.Result.FailAsync<string>(new ResultErrorTest(ErrorMessage1));

                                IResultError passedError = null;

                                var resultOutput = await resultInput.CompensateWhenAnyAsync<string, ResultErrorTest2>(error => error.ErrorMessage == ErrorMessage2,
                                                                                                                      error =>
                                                                                                                      {
                                                                                                                          passedError = error;

                                                                                                                          return Core.Result.Result.Ok(value);
                                                                                                                      });

                                resultOutput.ShouldBeFailureWithError(ErrorMessage1);
                                passedError.Should().BeNull();
                            }

                            [Fact]
                            public async Task SuccessInput_FailureOutput()
                            {
                                var value = Guid.NewGuid().ToString();
                                var resultInput = Core.Result.Result.OkAsync(value);

                                var resultOutput = await resultInput.CompensateWhenAnyAsync<string, ResultErrorTest2>(error => error.ErrorMessage == ErrorMessage2,
                                                                                                                      _ => Core.Result.Result.Fail<string>(ErrorMessage1));

                                resultOutput.ShouldBeSuccessWithValue(value);
                            }

                            [Fact]
                            public async Task SuccessInput_SuccessOutput()
                            {
                                var value = Guid.NewGuid().ToString();
                                var resultInput = Core.Result.Result.OkAsync(value);

                                var resultOutput = await resultInput.CompensateWhenAnyAsync<string, ResultErrorTest2>(error => error.ErrorMessage == ErrorMessage2,
                                                                                                                      _ => Core.Result.Result.Ok(Guid.NewGuid().ToString()));

                                resultOutput.ShouldBeSuccess();
                            }
                        }
                    }

                    public class WhenIResultErrorTypeDoeNotMatches
                    {
                        public class WhenFilterFuncTrue
                        {
                            [Fact]
                            public async Task FailureInput_FailureOutput()
                            {
                                var resultInput = Core.Result.Result.FailAsync<string>(new ResultErrorTest(ErrorMessage1));

                                IResultError passedError = null;

                                var resultOutput = await resultInput.CompensateWhenAnyAsync<string, ResultErrorMessage>(error => error.Message == ErrorMessage1,
                                                                                                                        error =>
                                                                                                                        {
                                                                                                                            passedError = error;

                                                                                                                            return Core.Result.Result.Fail<string>(error);
                                                                                                                        });

                                resultOutput.ShouldBeFailureWithError(ErrorMessage1);
                                passedError.Should().BeNull();
                            }

                            [Fact]
                            public async Task FailureInput_SuccessOutput()
                            {
                                var resultInput = Core.Result.Result.FailAsync<string>(new ResultErrorTest(ErrorMessage1));

                                IResultError passedError = null;

                                var resultOutput = await resultInput.CompensateWhenAnyAsync<string, ResultErrorMessage>(error => error.Message == ErrorMessage1,
                                                                                                                        error =>
                                                                                                                        {
                                                                                                                            passedError = error;

                                                                                                                            return Core.Result.Result.Ok(Guid.NewGuid().ToString());
                                                                                                                        });

                                resultOutput.ShouldBeFailureWithError(ErrorMessage1);
                                passedError.Should().BeNull();
                            }

                            [Fact]
                            public async Task SuccessInput_FailureOutput()
                            {
                                var value = Guid.NewGuid().ToString();
                                var resultInput = Core.Result.Result.OkAsync(value);

                                var resultOutput = await resultInput.CompensateWhenAnyAsync<string, ResultErrorTest2>(error => error.ErrorMessage == ErrorMessage1,
                                                                                                                      _ => Core.Result.Result.Fail<string>(ErrorMessage1));

                                resultOutput.ShouldBeSuccessWithValue(value);
                            }

                            [Fact]
                            public async Task SuccessInput_SuccessOutput()
                            {
                                var value = Guid.NewGuid().ToString();
                                var resultInput = Core.Result.Result.OkAsync(value);

                                var resultOutput = await resultInput.CompensateWhenAnyAsync<string, ResultErrorTest2>(error => error.ErrorMessage == ErrorMessage1,
                                                                                                                      _ => Core.Result.Result.Ok(Guid.NewGuid().ToString()));

                                resultOutput.ShouldBeSuccess();
                            }
                        }

                        public class WhenFilterFuncFalse
                        {
                            [Fact]
                            public async Task FailureInput_FailureOutput()
                            {
                                var resultInput = Core.Result.Result.FailAsync<string>(new ResultErrorTest(ErrorMessage1));

                                IResultError passedError = null;

                                var resultOutput = await resultInput.CompensateWhenAnyAsync<string, ResultErrorMessage>(error => error.Message == ErrorMessage2,
                                                                                                                        error =>
                                                                                                                        {
                                                                                                                            passedError = error;

                                                                                                                            return Core.Result.Result.Fail<string>(error);
                                                                                                                        });

                                resultOutput.ShouldBeFailureWithError(ErrorMessage1);
                                passedError.Should().BeNull();
                            }

                            [Fact]
                            public async Task FailureInput_SuccessOutput()
                            {
                                var resultInput = Core.Result.Result.FailAsync<string>(new ResultErrorTest(ErrorMessage1));

                                IResultError passedError = null;

                                var resultOutput = await resultInput.CompensateWhenAnyAsync<string, ResultErrorMessage>(error => error.Message == ErrorMessage2,
                                                                                                                        error =>
                                                                                                                        {
                                                                                                                            passedError = error;

                                                                                                                            return Core.Result.Result.Ok(Guid.NewGuid().ToString());
                                                                                                                        });

                                resultOutput.ShouldBeFailureWithError(ErrorMessage1);
                                passedError.Should().BeNull();
                            }

                            [Fact]
                            public async Task SuccessInput_FailureOutput()
                            {
                                var value = Guid.NewGuid().ToString();
                                var resultInput = Core.Result.Result.OkAsync(value);

                                var resultOutput = await resultInput.CompensateWhenAnyAsync<string, ResultErrorTest2>(error => error.ErrorMessage == ErrorMessage2,
                                                                                                                      _ => Core.Result.Result.Fail<string>(ErrorMessage1));

                                resultOutput.ShouldBeSuccessWithValue(value);
                            }

                            [Fact]
                            public async Task SuccessInput_SuccessOutput()
                            {
                                var value = Guid.NewGuid().ToString();
                                var resultInput = Core.Result.Result.OkAsync(value);

                                var resultOutput = await resultInput.CompensateWhenAnyAsync<string, ResultErrorTest2>(error => error.ErrorMessage == ErrorMessage2,
                                                                                                                      _ => Core.Result.Result.Ok(Guid.NewGuid().ToString()));

                                resultOutput.ShouldBeSuccess();
                            }
                        }
                    }
                }
            }

            public class AsyncReturn
            {
                public class NonGenericInput
                {
                    public class WhenIResultErrorTypeMatches
                    {
                        public class WhenFilterFuncTrue
                        {
                            [Fact]
                            public async Task FailureInput_FailureOutput()
                            {
                                var resultInput = Core.Result.Result.FailAsync(new ResultErrorTest(ErrorMessage1));

                                IResultError passedError = null;

                                var resultOutput = await resultInput.CompensateWhenAnyAsync<ResultErrorTest2>(error => error.ErrorMessage == ErrorMessage1,
                                                                                                              error =>
                                                                                                              {
                                                                                                                  passedError = error;

                                                                                                                  return Core.Result.Result.FailAsync(error.ErrorMessage + ErrorMessage2);
                                                                                                              });

                                resultOutput.ShouldBeFailureWithError($"{ErrorMessage1}{ErrorMessage2}");
                                passedError.ShouldBeError(ErrorMessage1);
                            }

                            [Fact]
                            public async Task FailureInput_SuccessOutput()
                            {
                                var resultInput = Core.Result.Result.FailAsync(new ResultErrorTest(ErrorMessage1));

                                IResultError passedError = null;

                                var resultOutput = await resultInput.CompensateWhenAnyAsync<ResultErrorTest2>(error => error.ErrorMessage == ErrorMessage1,
                                                                                                              error =>
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

                                var resultOutput = await resultInput.CompensateWhenAnyAsync<ResultErrorTest2>(error => error.ErrorMessage == ErrorMessage1,
                                                                                                              _ => Core.Result.Result.FailAsync(ErrorMessage1));

                                resultOutput.ShouldBeSuccess();
                            }

                            [Fact]
                            public async Task SuccessInput_SuccessOutput()
                            {
                                var resultInput = Core.Result.Result.OkAsync();

                                var resultOutput = await resultInput.CompensateWhenAnyAsync<ResultErrorTest2>(error => error.ErrorMessage == ErrorMessage1,
                                                                                                              _ => Core.Result.Result.OkAsync());

                                resultOutput.ShouldBeSuccess();
                            }
                        }

                        public class WhenFilterFuncFalse
                        {
                            [Fact]
                            public async Task FailureInput_FailureOutput()
                            {
                                var resultInput = Core.Result.Result.FailAsync(new ResultErrorTest(ErrorMessage1));

                                IResultError passedError = null;

                                var resultOutput = await resultInput.CompensateWhenAnyAsync<ResultErrorTest2>(error => error.ErrorMessage == ErrorMessage2,
                                                                                                              error =>
                                                                                                              {
                                                                                                                  passedError = error;

                                                                                                                  return Core.Result.Result.FailAsync(error.ErrorMessage + ErrorMessage2);
                                                                                                              });

                                resultOutput.ShouldBeFailureWithError(ErrorMessage1);
                                passedError.Should().BeNull();
                            }

                            [Fact]
                            public async Task FailureInput_SuccessOutput()
                            {
                                var resultInput = Core.Result.Result.FailAsync(new ResultErrorTest(ErrorMessage1));

                                IResultError passedError = null;

                                var resultOutput = await resultInput.CompensateWhenAnyAsync<ResultErrorTest2>(error => error.ErrorMessage == ErrorMessage2,
                                                                                                              error =>
                                                                                                              {
                                                                                                                  passedError = error;

                                                                                                                  return Core.Result.Result.OkAsync();
                                                                                                              });

                                resultOutput.ShouldBeFailureWithError(ErrorMessage1);
                                passedError.Should().BeNull();
                            }

                            [Fact]
                            public async Task SuccessInput_FailureOutput()
                            {
                                var resultInput = Core.Result.Result.OkAsync();

                                var resultOutput = await resultInput.CompensateWhenAnyAsync<ResultErrorTest2>(error => error.ErrorMessage == ErrorMessage2,
                                                                                                              _ => Core.Result.Result.FailAsync(ErrorMessage1));

                                resultOutput.ShouldBeSuccess();
                            }

                            [Fact]
                            public async Task SuccessInput_SuccessOutput()
                            {
                                var resultInput = Core.Result.Result.OkAsync();

                                var resultOutput = await resultInput.CompensateWhenAnyAsync<ResultErrorTest2>(error => error.ErrorMessage == ErrorMessage2,
                                                                                                              _ => Core.Result.Result.OkAsync());

                                resultOutput.ShouldBeSuccess();
                            }
                        }
                    }

                    public class WhenIResultErrorTypeDoeNotMatches
                    {
                        public class WhenFilterFuncTrue
                        {
                            [Fact]
                            public async Task FailureInput_FailureOutput()
                            {
                                var resultInput = Core.Result.Result.FailAsync(new ResultErrorTest(ErrorMessage1));

                                IResultError passedError = null;

                                var resultOutput = await resultInput.CompensateWhenAnyAsync<ResultErrorMessage>(error => error.Message == ErrorMessage1,
                                                                                                                error =>
                                                                                                                {
                                                                                                                    passedError = error;

                                                                                                                    return Core.Result.Result.FailAsync(error);
                                                                                                                });

                                resultOutput.ShouldBeFailureWithError(ErrorMessage1);
                                passedError.Should().BeNull();
                            }

                            [Fact]
                            public async Task FailureInput_SuccessOutput()
                            {
                                var resultInput = Core.Result.Result.FailAsync(new ResultErrorTest(ErrorMessage1));

                                IResultError passedError = null;

                                var resultOutput = await resultInput.CompensateWhenAnyAsync<ResultErrorMessage>(error => error.Message == ErrorMessage1,
                                                                                                                error =>
                                                                                                                {
                                                                                                                    passedError = error;

                                                                                                                    return Core.Result.Result.OkAsync();
                                                                                                                });

                                resultOutput.ShouldBeFailureWithError(ErrorMessage1);
                                passedError.Should().BeNull();
                            }

                            [Fact]
                            public async Task SuccessInput_FailureOutput()
                            {
                                var resultInput = Core.Result.Result.OkAsync();

                                var resultOutput = await resultInput.CompensateWhenAnyAsync<ResultErrorTest2>(error => error.ErrorMessage == ErrorMessage1,
                                                                                                              _ => Core.Result.Result.FailAsync(ErrorMessage1));

                                resultOutput.ShouldBeSuccess();
                            }

                            [Fact]
                            public async Task SuccessInput_SuccessOutput()
                            {
                                var resultInput = Core.Result.Result.OkAsync();

                                var resultOutput = await resultInput.CompensateWhenAnyAsync<ResultErrorTest2>(error => error.ErrorMessage == ErrorMessage1,
                                                                                                              _ => Core.Result.Result.OkAsync());

                                resultOutput.ShouldBeSuccess();
                            }
                        }

                        public class WhenFilterFuncFalse
                        {
                            [Fact]
                            public async Task FailureInput_FailureOutput()
                            {
                                var resultInput = Core.Result.Result.FailAsync(new ResultErrorTest(ErrorMessage1));

                                IResultError passedError = null;

                                var resultOutput = await resultInput.CompensateWhenAnyAsync<ResultErrorMessage>(error => error.Message == ErrorMessage2,
                                                                                                                error =>
                                                                                                                {
                                                                                                                    passedError = error;

                                                                                                                    return Core.Result.Result.FailAsync(error);
                                                                                                                });

                                resultOutput.ShouldBeFailureWithError(ErrorMessage1);
                                passedError.Should().BeNull();
                            }

                            [Fact]
                            public async Task FailureInput_SuccessOutput()
                            {
                                var resultInput = Core.Result.Result.FailAsync(new ResultErrorTest(ErrorMessage1));

                                IResultError passedError = null;

                                var resultOutput = await resultInput.CompensateWhenAnyAsync<ResultErrorMessage>(error => error.Message == ErrorMessage2,
                                                                                                                error =>
                                                                                                                {
                                                                                                                    passedError = error;

                                                                                                                    return Core.Result.Result.OkAsync();
                                                                                                                });

                                resultOutput.ShouldBeFailureWithError(ErrorMessage1);
                                passedError.Should().BeNull();
                            }

                            [Fact]
                            public async Task SuccessInput_FailureOutput()
                            {
                                var resultInput = Core.Result.Result.OkAsync();

                                var resultOutput = await resultInput.CompensateWhenAnyAsync<ResultErrorTest2>(error => error.ErrorMessage == ErrorMessage2,
                                                                                                              _ => Core.Result.Result.FailAsync(ErrorMessage1));

                                resultOutput.ShouldBeSuccess();
                            }

                            [Fact]
                            public async Task SuccessInput_SuccessOutput()
                            {
                                var resultInput = Core.Result.Result.OkAsync();

                                var resultOutput = await resultInput.CompensateWhenAnyAsync<ResultErrorTest2>(error => error.ErrorMessage == ErrorMessage2,
                                                                                                              _ => Core.Result.Result.OkAsync());

                                resultOutput.ShouldBeSuccess();
                            }
                        }
                    }
                }

                public class GenericInput
                {
                    public class WhenIResultErrorTypeMatches
                    {
                        public class WhenFilterFuncTrue
                        {
                            [Fact]
                            public async Task FailureInput_FailureOutput()
                            {
                                var resultInput = Core.Result.Result.FailAsync<string>(new ResultErrorTest(ErrorMessage1));

                                IResultError passedError = null;

                                var resultOutput = await resultInput.CompensateWhenAnyAsync<string, ResultErrorTest2>(error => error.ErrorMessage == ErrorMessage1,
                                                                                                                      error =>
                                                                                                                      {
                                                                                                                          passedError = error;

                                                                                                                          return Core.Result.Result.FailAsync<string>(error.ErrorMessage + ErrorMessage2);
                                                                                                                      });

                                resultOutput.ShouldBeFailureWithError($"{ErrorMessage1}{ErrorMessage2}");
                                passedError.ShouldBeError(ErrorMessage1);
                            }

                            [Fact]
                            public async Task FailureInput_SuccessOutput()
                            {
                                var value = Guid.NewGuid().ToString();

                                var resultInput = Core.Result.Result.FailAsync<string>(new ResultErrorTest(ErrorMessage1));

                                IResultError passedError = null;

                                var resultOutput = await resultInput.CompensateWhenAnyAsync<string, ResultErrorTest2>(error => error.ErrorMessage == ErrorMessage1,
                                                                                                                      error =>
                                                                                                                      {
                                                                                                                          passedError = error;

                                                                                                                          return Core.Result.Result.OkAsync(value);
                                                                                                                      });

                                resultOutput.ShouldBeSuccessWithValue(value);
                                passedError.ShouldBeError(ErrorMessage1);
                            }

                            [Fact]
                            public async Task SuccessInput_FailureOutput()
                            {
                                var value = Guid.NewGuid().ToString();
                                var resultInput = Core.Result.Result.OkAsync(value);

                                var resultOutput = await resultInput.CompensateWhenAnyAsync<string, ResultErrorTest2>(error => error.ErrorMessage == ErrorMessage1,
                                                                                                                      _ => Core.Result.Result.FailAsync<string>(ErrorMessage1));

                                resultOutput.ShouldBeSuccessWithValue(value);
                            }

                            [Fact]
                            public async Task SuccessInput_SuccessOutput()
                            {
                                var value = Guid.NewGuid().ToString();
                                var resultInput = Core.Result.Result.OkAsync(value);

                                var resultOutput = await resultInput.CompensateWhenAnyAsync<string, ResultErrorTest2>(error => error.ErrorMessage == ErrorMessage1,
                                                                                                                      _ => Core.Result.Result.OkAsync(Guid.NewGuid().ToString()));

                                resultOutput.ShouldBeSuccess();
                            }
                        }

                        public class WhenFilterFuncFalse
                        {
                            [Fact]
                            public async Task FailureInput_FailureOutput()
                            {
                                var resultInput = Core.Result.Result.FailAsync<string>(new ResultErrorTest(ErrorMessage1));

                                IResultError passedError = null;

                                var resultOutput = await resultInput.CompensateWhenAnyAsync<string, ResultErrorTest2>(error => error.ErrorMessage == ErrorMessage2,
                                                                                                                      error =>
                                                                                                                      {
                                                                                                                          passedError = error;

                                                                                                                          return Core.Result.Result.FailAsync<string>(error.ErrorMessage + ErrorMessage2);
                                                                                                                      });

                                resultOutput.ShouldBeFailureWithError(ErrorMessage1);
                                passedError.Should().BeNull();
                            }

                            [Fact]
                            public async Task FailureInput_SuccessOutput()
                            {
                                var value = Guid.NewGuid().ToString();

                                var resultInput = Core.Result.Result.FailAsync<string>(new ResultErrorTest(ErrorMessage1));

                                IResultError passedError = null;

                                var resultOutput = await resultInput.CompensateWhenAnyAsync<string, ResultErrorTest2>(error => error.ErrorMessage == ErrorMessage2,
                                                                                                                      error =>
                                                                                                                      {
                                                                                                                          passedError = error;

                                                                                                                          return Core.Result.Result.OkAsync(value);
                                                                                                                      });

                                resultOutput.ShouldBeFailureWithError(ErrorMessage1);
                                passedError.Should().BeNull();
                            }

                            [Fact]
                            public async Task SuccessInput_FailureOutput()
                            {
                                var value = Guid.NewGuid().ToString();
                                var resultInput = Core.Result.Result.OkAsync(value);

                                var resultOutput = await resultInput.CompensateWhenAnyAsync<string, ResultErrorTest2>(error => error.ErrorMessage == ErrorMessage2,
                                                                                                                      _ => Core.Result.Result.FailAsync<string>(ErrorMessage1));

                                resultOutput.ShouldBeSuccessWithValue(value);
                            }

                            [Fact]
                            public async Task SuccessInput_SuccessOutput()
                            {
                                var value = Guid.NewGuid().ToString();
                                var resultInput = Core.Result.Result.OkAsync(value);

                                var resultOutput = await resultInput.CompensateWhenAnyAsync<string, ResultErrorTest2>(error => error.ErrorMessage == ErrorMessage2,
                                                                                                                      _ => Core.Result.Result.OkAsync(Guid.NewGuid().ToString()));

                                resultOutput.ShouldBeSuccess();
                            }
                        }
                    }

                    public class WhenIResultErrorTypeDoeNotMatches
                    {
                        public class WhenFilterFuncTrue
                        {
                            [Fact]
                            public async Task FailureInput_FailureOutput()
                            {
                                var resultInput = Core.Result.Result.FailAsync<string>(new ResultErrorTest(ErrorMessage1));

                                IResultError passedError = null;

                                var resultOutput = await resultInput.CompensateWhenAnyAsync<string, ResultErrorMessage>(error => error.Message == ErrorMessage1,
                                                                                                                        error =>
                                                                                                                        {
                                                                                                                            passedError = error;

                                                                                                                            return Core.Result.Result.FailAsync<string>(error);
                                                                                                                        });

                                resultOutput.ShouldBeFailureWithError(ErrorMessage1);
                                passedError.Should().BeNull();
                            }

                            [Fact]
                            public async Task FailureInput_SuccessOutput()
                            {
                                var resultInput = Core.Result.Result.FailAsync<string>(new ResultErrorTest(ErrorMessage1));

                                IResultError passedError = null;

                                var resultOutput = await resultInput.CompensateWhenAnyAsync<string, ResultErrorMessage>(error => error.Message == ErrorMessage1,
                                                                                                                        error =>
                                                                                                                        {
                                                                                                                            passedError = error;

                                                                                                                            return Core.Result.Result.OkAsync(Guid.NewGuid().ToString());
                                                                                                                        });

                                resultOutput.ShouldBeFailureWithError(ErrorMessage1);
                                passedError.Should().BeNull();
                            }

                            [Fact]
                            public async Task SuccessInput_FailureOutput()
                            {
                                var value = Guid.NewGuid().ToString();
                                var resultInput = Core.Result.Result.OkAsync(value);

                                var resultOutput = await resultInput.CompensateWhenAnyAsync<string, ResultErrorTest2>(error => error.ErrorMessage == ErrorMessage1,
                                                                                                                      _ => Core.Result.Result.FailAsync<string>(ErrorMessage1));

                                resultOutput.ShouldBeSuccessWithValue(value);
                            }

                            [Fact]
                            public async Task SuccessInput_SuccessOutput()
                            {
                                var value = Guid.NewGuid().ToString();
                                var resultInput = Core.Result.Result.OkAsync(value);

                                var resultOutput = await resultInput.CompensateWhenAnyAsync<string, ResultErrorTest2>(error => error.ErrorMessage == ErrorMessage1,
                                                                                                                      _ => Core.Result.Result.OkAsync(Guid.NewGuid().ToString()));

                                resultOutput.ShouldBeSuccess();
                            }
                        }

                        public class WhenFilterFuncFalse
                        {
                            [Fact]
                            public async Task FailureInput_FailureOutput()
                            {
                                var resultInput = Core.Result.Result.FailAsync<string>(new ResultErrorTest(ErrorMessage1));

                                IResultError passedError = null;

                                var resultOutput = await resultInput.CompensateWhenAnyAsync<string, ResultErrorMessage>(error => error.Message == ErrorMessage2,
                                                                                                                        error =>
                                                                                                                        {
                                                                                                                            passedError = error;

                                                                                                                            return Core.Result.Result.FailAsync<string>(error);
                                                                                                                        });

                                resultOutput.ShouldBeFailureWithError(ErrorMessage1);
                                passedError.Should().BeNull();
                            }

                            [Fact]
                            public async Task FailureInput_SuccessOutput()
                            {
                                var resultInput = Core.Result.Result.FailAsync<string>(new ResultErrorTest(ErrorMessage1));

                                IResultError passedError = null;

                                var resultOutput = await resultInput.CompensateWhenAnyAsync<string, ResultErrorMessage>(error => error.Message == ErrorMessage2,
                                                                                                                        error =>
                                                                                                                        {
                                                                                                                            passedError = error;

                                                                                                                            return Core.Result.Result.OkAsync(Guid.NewGuid().ToString());
                                                                                                                        });

                                resultOutput.ShouldBeFailureWithError(ErrorMessage1);
                                passedError.Should().BeNull();
                            }

                            [Fact]
                            public async Task SuccessInput_FailureOutput()
                            {
                                var value = Guid.NewGuid().ToString();
                                var resultInput = Core.Result.Result.OkAsync(value);

                                var resultOutput = await resultInput.CompensateWhenAnyAsync<string, ResultErrorTest2>(error => error.ErrorMessage == ErrorMessage2,
                                                                                                                      _ => Core.Result.Result.FailAsync<string>(ErrorMessage1));

                                resultOutput.ShouldBeSuccessWithValue(value);
                            }

                            [Fact]
                            public async Task SuccessInput_SuccessOutput()
                            {
                                var value = Guid.NewGuid().ToString();
                                var resultInput = Core.Result.Result.OkAsync(value);

                                var resultOutput = await resultInput.CompensateWhenAnyAsync<string, ResultErrorTest2>(error => error.ErrorMessage == ErrorMessage2,
                                                                                                              _ => Core.Result.Result.OkAsync(Guid.NewGuid().ToString()));

                                resultOutput.ShouldBeSuccess();
                            }
                        }
                    }
                }
            }
        }
    }

    public class ResultErrorTest : IResultErrorComposite
    {
        private readonly string _errorMessage;
        private readonly List<IResultError> _children = new();

        public ResultErrorTest(string errorMessage)
        {
            _errorMessage = errorMessage;
            _children.Add(new ResultErrorTest2(errorMessage));
        }

        public ResultErrorStructure GetErrorStructureSafe() =>
            throw new NotImplementedException();

        public ResultErrorStructure GetErrorStructure() =>
            throw new NotImplementedException();

        public string GetErrorStringSafe(string seperator) =>
            _errorMessage;

        public string GetErrorString(string seperator) =>
            _errorMessage;

        public Option<IResultErrorNonComposite> Parent { get; }
        public IEnumerable<IResultError> Children => _children;
    }

    public class ResultErrorTest2 : IResultErrorNonComposite
    {
        public ResultErrorTest2(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        public string ErrorMessage { get; }

        public ResultErrorStructure GetErrorStructureSafe() =>
            ResultErrorStructureExtensions.CreateEmptyResultErrorStructure();

        public ResultErrorStructure GetErrorStructure() =>
            ResultErrorStructureExtensions.CreateEmptyResultErrorStructure();

        public string GetErrorStringSafe() =>
            ErrorMessage;

        public string GetErrorString() =>
            ErrorMessage;
    }
}