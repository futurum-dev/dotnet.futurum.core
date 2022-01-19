using System;

using Futurum.Core.Option;
using Futurum.Core.Tests.Helper.Option;
using Futurum.Core.Tests.Helper.Result;

using Xunit;

namespace Futurum.Core.Tests.Option;

public class OptionExtensionsTryCastTests
{
    [Fact]
    public void success()
    {
        var name = Guid.NewGuid().ToString();

        object nameAsObject = name;
        
        var option = nameAsObject.TryCast<string>();
        
        option.ShouldBeHasValueWithValue(name);
    }
    
    [Fact]
    public void failure()
    {
        var name = 10;

        object nameAsObject = name;
        
        var option = nameAsObject.TryCast<string>();
        
        option.ShouldBeHasNoValue();
    }
    
    public class AsResult
    {
        private const string ErrorMessage = "ERROR_MESSAGE";

        [Fact]
        public void success()
        {
            var name = Guid.NewGuid().ToString();

            object nameAsObject = name;
        
            var option = nameAsObject.TryCast<string>(() => ErrorMessage);
        
            option.ShouldBeSuccessWithValue(name);
        }
    
        [Fact]
        public void failure()
        {
            var name = 10;

            object nameAsObject = name;
        
            var option = nameAsObject.TryCast<string>(() => ErrorMessage);
        
            option.ShouldBeFailureWithError(ErrorMessage);
        }
    }
}