using System;
using System.Linq;

using FluentAssertions;

using Futurum.Core.Result;

using Xunit;

namespace Futurum.Core.Tests.Result;

public class ResultErrorCompositeTests
{
    [Fact]
    public void ToErrorString()
    {
        var exceptionErrorMessage = Guid.NewGuid().ToString();

        var resultErrorException = new Exception(exceptionErrorMessage).ToResultError();

        var errorMessage = Guid.NewGuid().ToString();

        var resultErrorMessage = errorMessage.ToResultError();

        var resultError = ResultErrorCompositeExtensions.ToResultError(resultErrorMessage, resultErrorException);

        var errorString = resultError.ToErrorString(",");

        errorString.Should().Be($"{errorMessage},{exceptionErrorMessage}");
    }

    [Fact]
    public void ToErrorStructure()
    {
        var exceptionErrorMessage = Guid.NewGuid().ToString();

        var resultErrorException = new Exception(exceptionErrorMessage).ToResultError();

        var errorMessage = Guid.NewGuid().ToString();

        var resultErrorMessage = errorMessage.ToResultError();

        var resultError = ResultErrorCompositeExtensions.ToResultError(resultErrorMessage, resultErrorException);

        var errorStructure = resultError.ToErrorStructure();

        var parentErrorMessage = errorStructure.Message;
        parentErrorMessage.Should().Be(errorMessage);
        
        var childErrors = errorStructure.Children.ToList();
        childErrors.Count.Should().Be(1);

        childErrors[0].Message.Should().Be(exceptionErrorMessage);
        childErrors[0].Children.Should().BeEmpty();
    }
}