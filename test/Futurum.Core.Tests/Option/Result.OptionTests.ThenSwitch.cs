using System;
using System.Threading.Tasks;

using FluentAssertions;

using Futurum.Core.Option;
using Futurum.Core.Result;
using Futurum.Core.Tests.Helper.Result;

using Xunit;

namespace Futurum.Core.Tests.Option;

public class ResultOptionExtensionsThenSwitchTests
{
    private const string ErrorMessage1 = "ERROR_MESSAGE_1";
    private const string ErrorMessage2 = "ERROR_MESSAGE_2";
    private const string ErrorMessage3 = "ERROR_MESSAGE_3";

    public class Sync
    {
        public class InputSuccess
        {
            public class OutputSuccess
            {
                [Fact]
                public void OptionHasNoValue()
                {
                    var trueValue = Guid.NewGuid();
                    var falseValue = Guid.NewGuid();

                    var resultOption = Option<Guid>.None.ToResultOk();

                    var returnValue = resultOption.ThenSwitch(value => trueValue.ToResultOk(),
                                                              () => falseValue.ToResultOk());

                    returnValue.ShouldBeSuccessWithValue(falseValue);
                }

                [Fact]
                public void OptionHasValue()
                {
                    var inputValue = Guid.NewGuid();

                    var trueValue = Guid.NewGuid();
                    var falseValue = Guid.NewGuid();

                    var passedValue = Guid.Empty;

                    var resultOption = inputValue.ToOption().ToResultOk();

                    var returnValue = resultOption.ThenSwitch(value =>
                                                              {
                                                                  passedValue = value;

                                                                  return trueValue.ToResultOk();
                                                              },
                                                              () => falseValue.ToResultOk());

                    returnValue.ShouldBeSuccessWithValue(trueValue);
                    passedValue.Should().Be(inputValue);
                }
            }

            public class OutputFailure
            {
                [Fact]
                public void OptionHasNoValue()
                {
                    var resultOption = Option<Guid>.None.ToResultOk();

                    var returnValue = resultOption.ThenSwitch(
                        value => Core.Result.Result.Fail<Guid>(ErrorMessage1),
                        () => Core.Result.Result.Fail<Guid>(ErrorMessage2));

                    returnValue.ShouldBeFailureWithError(ErrorMessage2);
                }

                [Fact]
                public void OptionHasValue()
                {
                    var inputValue = Guid.NewGuid();

                    var passedValue = Guid.Empty;

                    var resultOption = inputValue.ToOption().ToResultOk();

                    var returnValue = resultOption.ThenSwitch(value =>
                                                              {
                                                                  passedValue = value;

                                                                  return Core.Result.Result.Fail<Guid>(
                                                                      ErrorMessage1);
                                                              },
                                                              () => Core.Result.Result
                                                                        .Fail<Guid>(ErrorMessage2));

                    returnValue.ShouldBeFailureWithError(ErrorMessage1);
                    passedValue.Should().Be(inputValue);
                }
            }
        }

        public class InputFailure
        {
            [Fact]
            public void OutputFailure()
            {
                var trueWasCalled = false;
                var falseWasCalled = false;

                var resultOption = Core.Result.Result.Fail<Option<Guid>>(ErrorMessage1);

                var returnValue = resultOption.ThenSwitch(value =>
                                                          {
                                                              trueWasCalled = true;

                                                              return Core.Result.Result
                                                                         .Fail<Guid>(ErrorMessage2);
                                                          },
                                                          () =>
                                                          {
                                                              falseWasCalled = true;

                                                              return Core.Result.Result
                                                                         .Fail<Guid>(ErrorMessage3);
                                                          });

                returnValue.ShouldBeFailureWithError(ErrorMessage1);
                trueWasCalled.Should().BeFalse();
                falseWasCalled.Should().BeFalse();
            }

            [Fact]
            public void OutputSuccess()
            {
                var trueWasCalled = false;
                var falseWasCalled = false;

                var resultOption = Core.Result.Result.Fail<Option<Guid>>(ErrorMessage1);

                var returnValue = resultOption.ThenSwitch(value =>
                                                          {
                                                              trueWasCalled = true;

                                                              return 0.ToResultOk();
                                                          },
                                                          () =>
                                                          {
                                                              falseWasCalled = true;

                                                              return 0.ToResultOk();
                                                          });

                returnValue.ShouldBeFailureWithError(ErrorMessage1);
                trueWasCalled.Should().BeFalse();
                falseWasCalled.Should().BeFalse();
            }
        }
    }

    public class Async
    {
        public class SyncFunc
        {
            public class InputSuccess
            {
                public class OutputSuccess
                {
                    [Fact]
                    public async Task OptionHasNoValue()
                    {
                        var trueValue = Guid.NewGuid();
                        var falseValue = Guid.NewGuid();

                        var resultOption = Option<Guid>.None.ToResultOkAsync();

                        var returnValue = await resultOption.ThenSwitchAsync(value => trueValue.ToResultOk(),
                                                                             () => falseValue.ToResultOk());

                        returnValue.ShouldBeSuccessWithValue(falseValue);
                    }

                    [Fact]
                    public async Task OptionHasValue()
                    {
                        var inputValue = Guid.NewGuid();

                        var trueValue = Guid.NewGuid();
                        var falseValue = Guid.NewGuid();

                        var passedValue = Guid.Empty;

                        var resultOption = inputValue.ToOption().ToResultOkAsync();

                        var returnValue = await resultOption.ThenSwitchAsync(value =>
                                                                             {
                                                                                 passedValue = value;

                                                                                 return trueValue.ToResultOk();
                                                                             },
                                                                             () => falseValue.ToResultOk());

                        returnValue.ShouldBeSuccessWithValue(trueValue);
                        passedValue.Should().Be(inputValue);
                    }
                }

                public class OutputFailure
                {
                    [Fact]
                    public async Task OptionHasNoValue()
                    {
                        var resultOption = Option<Guid>.None.ToResultOkAsync();

                        var returnValue = await resultOption.ThenSwitchAsync(
                            value => Core.Result.Result.Fail<Guid>(ErrorMessage1),
                            () => Core.Result.Result.Fail<Guid>(ErrorMessage2));

                        returnValue.ShouldBeFailureWithError(ErrorMessage2);
                    }

                    [Fact]
                    public async Task OptionHasValue()
                    {
                        var inputValue = Guid.NewGuid();

                        var passedValue = Guid.Empty;

                        var resultOption = inputValue.ToOption().ToResultOkAsync();

                        var returnValue = await resultOption.ThenSwitchAsync(value =>
                                                                             {
                                                                                 passedValue = value;

                                                                                 return Core.Result.Result
                                                                                            .Fail<Guid>(
                                                                                                ErrorMessage1);
                                                                             },
                                                                             () => Core.Result.Result
                                                                                       .Fail<Guid>(ErrorMessage2));

                        returnValue.ShouldBeFailureWithError(ErrorMessage1);
                        passedValue.Should().Be(inputValue);
                    }
                }
            }

            public class InputFailure
            {
                [Fact]
                public async Task OutputFailure()
                {
                    var trueWasCalled = false;
                    var falseWasCalled = false;

                    var resultOption = Core.Result.Result.FailAsync<Option<Guid>>(ErrorMessage1);

                    var returnValue = await resultOption.ThenSwitchAsync(value =>
                                                                         {
                                                                             trueWasCalled = true;

                                                                             return Core.Result.Result
                                                                                        .Fail<Guid>(ErrorMessage2);
                                                                         },
                                                                         () =>
                                                                         {
                                                                             falseWasCalled = true;

                                                                             return Core.Result.Result
                                                                                        .Fail<Guid>(ErrorMessage3);
                                                                         });

                    returnValue.ShouldBeFailureWithError(ErrorMessage1);
                    trueWasCalled.Should().BeFalse();
                    falseWasCalled.Should().BeFalse();
                }

                [Fact]
                public async Task OutputSuccess()
                {
                    var trueWasCalled = false;
                    var falseWasCalled = false;

                    var resultOption = Core.Result.Result.FailAsync<Option<Guid>>(ErrorMessage1);

                    var returnValue = await resultOption.ThenSwitchAsync(value =>
                                                                         {
                                                                             trueWasCalled = true;

                                                                             return 0.ToResultOk();
                                                                         },
                                                                         () =>
                                                                         {
                                                                             falseWasCalled = true;

                                                                             return 0.ToResultOk();
                                                                         });

                    returnValue.ShouldBeFailureWithError(ErrorMessage1);
                    trueWasCalled.Should().BeFalse();
                    falseWasCalled.Should().BeFalse();
                }
            }
        }

        public class AsyncFunc
        {
            public class InputSuccess
            {
                public class OutputSuccess
                {
                    [Fact]
                    public async Task OptionHasNoValue()
                    {
                        var trueValue = Guid.NewGuid();
                        var falseValue = Guid.NewGuid();

                        var resultOption = Option<Guid>.None.ToResultOk();

                        var returnValue = await resultOption.ThenSwitchAsync(value => trueValue.ToResultOkAsync(),
                                                                             () => falseValue.ToResultOkAsync());

                        returnValue.ShouldBeSuccessWithValue(falseValue);
                    }

                    [Fact]
                    public async Task OptionHasValue()
                    {
                        var inputValue = Guid.NewGuid();

                        var trueValue = Guid.NewGuid();
                        var falseValue = Guid.NewGuid();

                        var passedValue = Guid.Empty;

                        var resultOption = inputValue.ToOption().ToResultOk();

                        var returnValue = await resultOption.ThenSwitchAsync(value =>
                                                                             {
                                                                                 passedValue = value;

                                                                                 return trueValue.ToResultOkAsync();
                                                                             },
                                                                             () => falseValue.ToResultOkAsync());

                        returnValue.ShouldBeSuccessWithValue(trueValue);
                        passedValue.Should().Be(inputValue);
                    }
                }

                public class OutputFailure
                {
                    [Fact]
                    public async Task OptionHasNoValue()
                    {
                        var resultOption = Option<Guid>.None.ToResultOk();

                        var returnValue = await resultOption.ThenSwitchAsync(
                            value => Core.Result.Result.FailAsync<Guid>(ErrorMessage1),
                            () => Core.Result.Result.FailAsync<Guid>(ErrorMessage2));

                        returnValue.ShouldBeFailureWithError(ErrorMessage2);
                    }

                    [Fact]
                    public async Task OptionHasValue()
                    {
                        var inputValue = Guid.NewGuid();

                        var passedValue = Guid.Empty;

                        var resultOption = inputValue.ToOption().ToResultOk();

                        var returnValue = await resultOption.ThenSwitchAsync(value =>
                                                                             {
                                                                                 passedValue = value;

                                                                                 return Core.Result.Result.FailAsync<Guid>(
                                                                                     ErrorMessage1);
                                                                             },
                                                                             () => Core.Result.Result
                                                                                       .FailAsync<Guid>(ErrorMessage2));

                        returnValue.ShouldBeFailureWithError(ErrorMessage1);
                        passedValue.Should().Be(inputValue);
                    }
                }
            }

            public class InputFailure
            {
                [Fact]
                public async Task OutputFailure()
                {
                    var trueWasCalled = false;
                    var falseWasCalled = false;

                    var resultOption = Core.Result.Result.Fail<Option<Guid>>(ErrorMessage1);

                    var returnValue = await resultOption.ThenSwitchAsync(value =>
                                                                         {
                                                                             trueWasCalled = true;

                                                                             return Core.Result.Result
                                                                                        .FailAsync<Guid>(ErrorMessage2);
                                                                         },
                                                                         () =>
                                                                         {
                                                                             falseWasCalled = true;

                                                                             return Core.Result.Result
                                                                                        .FailAsync<Guid>(ErrorMessage3);
                                                                         });

                    returnValue.ShouldBeFailureWithError(ErrorMessage1);
                    trueWasCalled.Should().BeFalse();
                    falseWasCalled.Should().BeFalse();
                }

                [Fact]
                public async Task OutputSuccess()
                {
                    var trueWasCalled = false;
                    var falseWasCalled = false;

                    var resultOption = Core.Result.Result.Fail<Option<Guid>>(ErrorMessage1);

                    var returnValue = await resultOption.ThenSwitchAsync(value =>
                                                                         {
                                                                             trueWasCalled = true;

                                                                             return 0.ToResultOkAsync();
                                                                         },
                                                                         () =>
                                                                         {
                                                                             falseWasCalled = true;

                                                                             return 0.ToResultOkAsync();
                                                                         });

                    returnValue.ShouldBeFailureWithError(ErrorMessage1);
                    trueWasCalled.Should().BeFalse();
                    falseWasCalled.Should().BeFalse();
                }
            }
        }
    }
}