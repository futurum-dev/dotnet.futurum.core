using Futurum.Core.Tests.Helper.Result;

using Xunit;

namespace Futurum.Core.Tests.Result;

public class ResultMapSwitchTests
{
    private const string ErrorMessage = "ERROR_MESSAGE_1";
    
    [Fact]
    public void FailureInput()
    {
        var resultInput = Core.Result.Result.Fail<int>(ErrorMessage);

        var resultOutput = resultInput.MapSwitch(_ => true, () => false);

        resultOutput.ShouldBeSuccess();
        
        resultOutput.ShouldBeSuccessWithValue(false);
    }
    
    [Fact]
    public void SuccessInput()
    {
        var resultInput = Core.Result.Result.Ok(10);

        var resultOutput = resultInput.MapSwitch(_ => true, () => false);

        resultOutput.ShouldBeSuccess();
        
        resultOutput.ShouldBeSuccessWithValue(true);
    }
}